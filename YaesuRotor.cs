/*
 *
 *
 *    YaesuRotor.cs
 *
 *    License: GNU General Public License Version 3.0.
 *    
 *    Copyright (C) 2014,2015 by Matthew K. Roberts, KK5JY. All rights reserved.
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
using System.Diagnostics;

using KK5JY.IO;
using System.Collections.Generic;

namespace KK5JY.RotorCraft {
	/// <summary>
	/// Communications object for rotor using Yaesu protocol.
	/// </summary>
	public sealed class YaesuRotor : IRotor, IStart, IDisposable {
		public const string Terminator = "\r\n";

		#region Private Fields
		private IDeviceHelper m_Device;
		private System.Threading.Timer m_Timer;
		private DateTimeOffset m_LastDataUpdate;
		private DateTimeOffset m_LastRateUpdate;
		private SingleIntegrator m_RateIntegrator;
		private long m_LastCount;
		private long m_MessageCount;
		private bool m_Acknowledged;
		private string m_RxBuffer;
		private int m_TimerInterval;
		private int m_Position;
		private int m_UpdateCount;
		#endregion

		/// <summary>
		/// The minimum poll interval, in milliseconds.
		/// </summary>
		public const int MinPollInterval = 200;

		/// <summary>
		/// The timeout for poll responses, in milliseconds.
		/// </summary>
		public const int MinPollTimeout  = 2000;

		/// <summary>
		/// Returns the current azimuth.
		/// </summary>
		public int Position { get { return m_Position; } }

		/// <summary>
		/// Returns the current accumulated error count.
		/// </summary>
		public uint Errors {
			get {
				var dev = m_Device;
				if (dev == null) return 0u;
				return dev.Errors;
			}
		}

		/// <summary>
		/// Minimum time between update requests.
		/// </summary>
		public int UpdateInterval {
			get { return m_TimerInterval; }
			set {
				if (value < MinPollInterval) {
					m_TimerInterval = MinPollInterval;
				} else {
					m_TimerInterval = value;
				}
				if (m_Timer != null)
					m_Timer.Change(m_TimerInterval, m_TimerInterval);
			}
		}

		/// <summary>
		/// Maximum time between update requests.
		/// </summary>
		public int Timeout { get; set; }

		/// <summary>
		/// Reports the update rate in updates/sec.
		/// </summary>
		public float UpdateRate { get; private set; }

		/// <summary>
		/// Returns true if connected to the controller.
		/// </summary>
		public bool IsOpen {
			get {
				return (m_Device != null) && (m_Device.IsOpen);
			}
		}

		/// <summary>
		/// Construct a new rotor object.
		/// </summary>
		/// <param name="device">The I/O device to use to communicate with the rotor controller.</param>
		public YaesuRotor(IDeviceHelper device) {
			m_Device = device;
			m_Device.DataReceived += DeviceDataReceived;
			m_LastDataUpdate = DateTimeOffset.MinValue;
			m_RxBuffer = "";
			m_Acknowledged = true;
			m_LastDataUpdate = m_LastRateUpdate = DateTimeOffset.UtcNow;
			UpdateInterval = MinPollInterval;
			m_RateIntegrator = new SingleIntegrator(4);
		}

		/// <summary>
		/// Stop communication and release resources.
		/// </summary>
		public void Dispose() {
			if (!ReferenceEquals(m_Device, null)) {
				try { m_Timer.Dispose(); } catch { }
				m_Timer = null;
				lock (m_Device) {
					m_Device.DataReceived -= DeviceDataReceived;
					try { m_Device.Dispose(); } catch { }
					m_Device = null;
				}
			}
			UpdateRate = 0;
		}

		/// <summary>
		/// Start communicating with the rotor.
		/// </summary>
		public void Start() {
			m_Device.Start();
			m_Timer = new System.Threading.Timer(TimerTick, null, UpdateInterval, UpdateInterval);
		}

		/// <summary>
		/// Go to a specific heading.
		/// </summary>
		/// <param name="bearingAngle">The heading.</param>
		public void Preset(int bearingAngle) {
			var s = bearingAngle.ToString("000");
			WriteMessage("M" + s);
			Debug.WriteLine("DEBUG: YaesuRotor.Preset(" + s + ")");
		}

		/// <summary>
		/// Move right, left, or stop rotation.
		/// </summary>
		/// <param name="direction">The direction.</param>
		public void Move(Directions direction) {
			switch (direction) {
				case Directions.Stop:
					WriteMessage("S");
					break;
				case Directions.Clockwise:
					WriteMessage("R");
					break;
				case Directions.CounterClockwise:
					WriteMessage("L");
					break;
			}

			Debug.WriteLine("DEBUG: YaesuRotor.Move(" + direction.ToString() + ")");
		}

		/// <summary>
		/// Request a position update.
		/// </summary>
		public void PollPosition() {
			m_Acknowledged = false;
			WriteMessage("C");
		}

		/// <summary>
		/// Writes a terminated message to the controller.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>True if successful.</returns>
		private bool WriteMessage(string message) {
			try {
				lock (m_Device) {
					m_Device.Write(message + Terminator);
				}
				return true;
			} catch {
				return false;
			}
		}

		#region Events and Handlers
		/// <summary>
		/// Raised when the position changes.
		/// </summary>
		public event EventHandler<PositionUpdateArgs> PositionUpdate;

		/// <summary>
		/// Raised when data is received from the controller.
		/// </summary>
		private void DeviceDataReceived(object sender, ReceivedDataEvent e) {
			m_RxBuffer += e.Data;

			// find first message
			var idx = m_RxBuffer.IndexOf(Terminator);
			while (idx >= 0) {
				var msg = m_RxBuffer.Substring(0, idx + Terminator.Length);
				m_RxBuffer = m_RxBuffer.Substring(idx + Terminator.Length);
				if (!String.IsNullOrEmpty(msg)) {
					// parse the message
					if (msg.StartsWith("AZ=") || msg.StartsWith("+")) {
						msg = msg.Substring(msg.StartsWith("+") ? 1 : 3);
						if (Int32.TryParse(msg, out m_Position)) {
							var now = DateTimeOffset.UtcNow;
							m_LastDataUpdate = now;
							m_Acknowledged = true;
							System.Threading.Interlocked.Exchange(ref m_MessageCount, m_MessageCount + 1);

							if (PositionUpdate != null) {
								PositionUpdate(this, new PositionUpdateArgs { AzimuthAngle = m_Position });
							}
						}
					}
				}

				// find next message
				idx = m_RxBuffer.IndexOf(Terminator);
			}
		}

		/// <summary>
		/// Raised every 'UpdateInterval' to poll the controller.
		/// </summary>
		private void TimerTick(Object state) {
			var now = DateTimeOffset.UtcNow;
			var msgCount = System.Threading.Interlocked.Read(ref m_MessageCount);

			// update rate information
			if (m_UpdateCount++ > 2) {
				m_UpdateCount = 0;
				if (m_LastCount == msgCount) {
					UpdateRate = m_RateIntegrator.Update(0f);
				} else {
					var diff = (msgCount - m_LastCount);
					var howLong = (now - m_LastRateUpdate);
					UpdateRate = m_RateIntegrator.Update(Convert.ToSingle(diff / howLong.TotalSeconds));
					m_LastRateUpdate = now;
					m_LastCount = msgCount;
				}
			}

			var elapsed = (now - m_LastDataUpdate).TotalMilliseconds;
			if (m_Acknowledged && (elapsed > UpdateInterval)) {
				PollPosition();
			}

			if ((!m_Acknowledged) && (elapsed > Timeout)) {
				UpdateRate = 0.0f;
				PollPosition();
			}
		}
		#endregion
	}
}
