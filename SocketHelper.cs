/*
 *
 *
 *    SocketHelper.cs
 *
 *    Convenience wrapper around sockets.
 *
 *    License: GNU General Public License Version 3.0.
 *    
 *    Copyright (C) 2014 by Matthew K. Roberts, KK5JY. All rights reserved.
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
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Net;
using System.Diagnostics;

namespace KK5JY.IO {
	/// <summary>
	/// Convenience wrapper around sockets, to match the SerialPortHelper.
	/// </summary>
	public class SocketHelper : IStart, IDisposable, IDeviceHelper {
		/// <summary>
		/// Reference to the inner socket object.
		/// </summary>
		protected Socket Socket { get; private set; }

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
		/// The remote endpoint address.
		/// </summary>
		public IPEndPoint EndPoint { get; private set; }

		/// <summary>
		/// Returns true if currently connected.
		/// </summary>
		public bool IsOpen {
			get {
				var sock = Socket;
				return (sock != null) && (sock.Connected);
			}
		}

		/// <summary>
		/// Returns the current accumulated error count.
		/// </summary>
		public uint Errors { get; private set; }

		/// <summary>
		/// Construct a new wrapper.
		/// </summary>
		/// <param name="ep">The remote address.</param>
		public SocketHelper(IPEndPoint ep) {
			this.EndPoint = ep;
		}

		/// <summary>
		/// Start processing I/O.
		/// </summary>
		public void Start() {
			// fire up the worker thread
			m_Quit = false;
			m_Thread = new Thread(IoThread);
			m_Thread.Name = "SocketHelper I/O Thread (" + EndPoint.Address + ":" + EndPoint.Port + ")";
			m_Thread.Start();
			Errors = 0u;
		}

		/// <summary>
		/// Stop processing I/O and close the inner <c>SerialPort</c> object.
		/// </summary>
		public void Dispose() {
			m_Quit = true;
			try { Socket.Close(); } catch { /* nop */ }
			m_Thread.Join();
			Errors = 0u;
		}

		/// <summary>
		/// Write the string to the port as a sequence of equivalent ASCII bytes.
		/// </summary>
		/// <param name="data">The data to write</param>
		public bool Write(string data) {
            var s = Socket;
            if (!ReferenceEquals(s, null) && s.Connected) {
                var bytes = ASCIIEncoding.Default.GetBytes(data);
                return s.Send(bytes, 0, bytes.Length, SocketFlags.None) == bytes.Length;
            }
            return false;
		}

		/// <summary>
		/// The I/O thread method.
		/// </summary>
		private void IoThread() {
			byte[] ioBuffer = new byte[64];
			while (!m_Quit) {
				try {
					if (Socket == null) {
						Socket = new Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
						Socket.Connect(EndPoint);
						Errors = 0u;
					}

					var count = Socket.Receive(ioBuffer, SocketFlags.None);
					if (count == 0) {
						throw new ApplicationException("Socket closed");
					}
					if (count > 0) {
						var s = ASCIIEncoding.Default.GetString(ioBuffer, 0, count);
						if (DataReceived != null) {
							DataReceived(this, new ReceivedDataEvent { Data = s });
						}
					}
				} catch {
					Errors++;

					try { if (Socket.Connected) Socket.Close(); } catch { /* nop */ }
					Socket = null;
                    Debug.WriteLine("DEBUG: Socket failure, will wait and retry: " + EndPoint.ToString());

                    // sleep for two seconds, but check now and then to see if we should exit
                    for (int i = 0; i != 4 && !m_Quit; ++i) {
                        Thread.Sleep(500);
                    }
				}
			}
		}
	}
}
