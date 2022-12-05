/*
 *
 *
 *    SerialPortHelper.cs
 *
 *    Convenience wrapper for code to help the SerialPort class actually work.
 *
 *    License: GNU General Public License Version 3.0.
 *    
 *    Copyright (C) 2014-2015 by Matthew K. Roberts, KK5JY. All rights reserved.
 *
 *    This program is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU General Public License as published by
 *    the Free Software Foundation, either version 3 of the License, or
 *    (at your option) any later version.
 *    
 *    This program is distributed in the hope that it will be useful, but
 *    WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *    General Public License for more details.
 *    
 *    You should have received a copy of the GNU General Public License
 *    along with this program.  If not, see: http://www.gnu.org/licenses/
 *    
 *
 */

using System;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace KK5JY.IO {
	/// <summary>
	/// Convenience wrapper for code to help the SerialPort
	/// class actually work.
	/// </summary>
	/// <remarks>
	/// The SerialPort class is a complete and total mess, not just
	/// on Windows, but also on Mono.  In Windows, the functionality
	/// presented is broken in several ways.  On Mono, most of the
	/// interface is not even implemented.  This class tries to use
	/// the functionality that is present AND working on BOTH of
	/// these platforms (which isn't much), and use it to reliably
	/// provide working serial port services to the rest of the
	/// program.
	/// </remarks>
	public class SerialPortHelper : IStart, IDisposable, IDeviceHelper {
		/// <summary>
		/// The maximum number of bytes to read before rasing
		/// an event, even if there are other bytes pending
		/// in the RX buffer.
		/// </summary>
		public const int MaxBytesToRead = 64;

		/// <summary>
		/// Reference to the inner port object.
		/// </summary>
		protected SerialPort Port { get; private set; }

		/// <summary>
		/// The data event.
		/// </summary>
		public event EventHandler<ReceivedDataEvent> DataReceived;

		/// <summary>
		/// I/O thread.
		/// </summary>
		private Thread m_Thread;

		/// <summary>
		/// I/O termination flag.
		/// </summary>
		private bool m_Quit;

		/// <summary>
		/// Returns true if the port is open.
		/// </summary>
		public bool IsOpen {
			get {
				return (!ReferenceEquals(Port, null)) && Port.IsOpen;
			}
		}

		/// <summary>
		/// The current error count.
		/// </summary>
		public uint Errors { get; private set; }

		/// <summary>
		/// Construct a new serial port wrapper.
		/// </summary>
		/// <param name="port">The raw <c>SerialPort</c> object.</param>
		public SerialPortHelper(SerialPort port) {
			this.Port = port;
		}

		/// <summary>
		/// Start processing I/O.
		/// </summary>
		public void Start() {
			#if ! __MonoCS__
			Port.ReceivedBytesThreshold = 1;
			#endif
			Port.Encoding = System.Text.ASCIIEncoding.ASCII;
			Port.ReadTimeout = 60000;
			Errors = 0u;

			// fire up the worker thread
			m_Quit = false;
			m_Thread = new Thread(IoThread);
			m_Thread.Name = "SerialPortHelper I/O Thread (" + Port.PortName + ")";
			m_Thread.Start();
		}

		/// <summary>
		/// Stop processing I/O and close the inner <c>SerialPort</c> object.
		/// </summary>
		public void Dispose() {
			m_Quit = true;
			System.Threading.Thread.Sleep(250); // wait for pending hardware bytes to be received
			try { Port.DiscardInBuffer(); } catch { /* nop */ }
			try { Port.DiscardOutBuffer(); } catch { /* nop */ }
			try { Port.Close(); } catch { /* nop */ }
			if (m_Thread != null) {
				m_Thread.Join();
			}
			m_Thread = null;
			Errors = 0u;
		}

		/// <summary>
		/// Write the string to the port as a sequence of equivalent ASCII bytes.
		/// </summary>
		/// <param name="data">The data to write</param>
		public bool Write(string data) {
			if (!Port.IsOpen) return false;
			if (m_Quit) return false;
			try {
				var bytes = ASCIIEncoding.Default.GetBytes(data);
				Port.Write(bytes, 0, bytes.Length);
			} catch {
				return false;
			}
            return true;
		}

		/// <summary>
		/// The I/O thread method.
		/// </summary>
		private void IoThread() {
			StringBuilder rxBuffer = new StringBuilder();
			while (!m_Quit) {
				try {
					if (!Port.IsOpen) {
						try {
							Port.Open();
							Errors = 0u;
						} catch (System.IO.IOException) {
							try { Port.Close(); } catch { }
							#if ! __MonoCS__
							try { SerialPortFixer.SerialPortFixer.Execute(Port.PortName); } catch { /* nop */ }
							#endif

							// tick the error counter
							Errors++;

							// wait one second
							Thread.Sleep(1000);
							continue;
						}
					}

					var b = Port.ReadByte();
					if (b < 0) {
						return; // EOF - nothing left to do
					}
					rxBuffer.Append((char)(b));
					if (Port.BytesToRead != 0) {
						var oldTimeout = Port.ReadTimeout;
						Port.ReadTimeout = 0;
						try {
							// if there are other bytes, get them, too
							while (Port.BytesToRead != 0 && rxBuffer.Length < MaxBytesToRead) {
								b = Port.ReadByte();
								if (b < 0) {
									return; // EOF - nothing left to do
								}
								rxBuffer.Append((char)(b));
							}
						} catch (TimeoutException) {
							// nop
						} finally {
							Port.ReadTimeout = oldTimeout;
						}
					}
					if (DataReceived != null && rxBuffer.Length != 0) {
						DataReceived(this, new ReceivedDataEvent { Data = rxBuffer.ToString() });
					}
				} catch {
					// nop - just keep going
					try { Port.Close(); } catch { }
					#if ! __MonoCS__
					try { SerialPortFixer.SerialPortFixer.Execute(Port.PortName); } catch { /* nop */ }
					#endif

					// tick the error counter
					Errors++;
					Thread.Sleep(100);
				}

				// empty out whatever was read, regardless of whether it was used
				rxBuffer.Clear();
			}
		}

		/// <summary>
		/// Return a mostly-sorted list of port names.
		/// </summary>
		public static string[] GetPortNames() {
			string[] result = null;
			int idx = 0;
			var original = SerialPort.GetPortNames();
			const string basePrefix =
#if __MonoCS__
				"/dev/tty";
#else
				"COM";
#endif
			var table = new Dictionary<string, Dictionary<int, string>>();
			var leftover = new List<string>();
			foreach (var item in original) {
				if (!item.StartsWith(basePrefix, StringComparison.InvariantCultureIgnoreCase)) {
					leftover.Add(item);
					continue;
				}

				for (idx = basePrefix.Length; idx != item.Length; ++idx) {
					if (Char.IsDigit(item[idx])) {
						var numeric = item.Substring(idx);
						int number;
						if (!Int32.TryParse(numeric, out number)) {
							leftover.Add(item);
							break;
						}

						var prefix = item.Substring(0, idx);
						if (!table.ContainsKey(prefix)) {
							table.Add(prefix, new Dictionary<int, string>());
						}
						table[prefix][number] = item;
						break;
					}
				}
			}

			var processed = new LinkedList<string>();
			foreach (var kind in table) {
				var sorted = new List<int>(kind.Value.Keys);
				sorted.Sort();
				foreach (var key in sorted) {
					processed.AddLast(kind.Value[key]);
				}
			}

#if __MonoCS__
			// find nonstandard ACM-style Arduino ports
			foreach (var devNode in System.IO.Directory.GetFiles("/dev")) {
				if (devNode.StartsWith("/dev/ttyACM") && !processed.Contains(devNode) && !leftover.Contains(devNode)) {
					// add the node as a nonstandard port
					leftover.Add(devNode);
				}
			}
#endif
			leftover.Sort();

			result = new string[processed.Count + leftover.Count];
			idx = 0;
			foreach (var port in processed) {
				result[idx++] = port;
			}
			foreach (var port in leftover) {
				result[idx++] = port;
			}
#if VERBOSE_DEBUG
			Debug.WriteLine("DEBUG: Sorted Table:");
			foreach (var kvp1 in table) {
				Debug.WriteLine(" + " + kvp1.Key);
				foreach(var kvp2 in kvp1.Value) {
					Debug.WriteLine("   + " + kvp2.Key.ToString() + " = " + kvp2.Value.ToString());
				}
			}
			Debug.WriteLine("DEBUG: Leftovers:");
			foreach (var item in leftover) {
				Debug.WriteLine(" + " + item);
			}
#endif
			return result;
		}
	}
}
