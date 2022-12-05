/*
 *
 *
 *    Helpers.cs
 *
 *    Convenience wrappers for I/O devices.
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KK5JY.IO {
	/// <summary>
	/// A uniform interface to sockets and serial ports.
	/// </summary>
	public interface IDeviceHelper : IDisposable {
		/// <summary>
		/// Raised when new data is received.
		/// </summary>
		event EventHandler<ReceivedDataEvent> DataReceived;

		/// <summary>
		/// Returns true if currently connected.
		/// </summary>
		bool IsOpen { get; }

		/// <summary>
		/// Start the I/O thread and process received data.
		/// </summary>
		void Start();

		/// <summary>
		/// Write the string to the device as a sequence of equivalent ASCII bytes.
		/// </summary>
		/// <param name="data">The data to write</param>
        /// <returns>True if successful.</returns>
		bool Write(string data);

		/// <summary>
		/// Returns the current accumulated number of errors.
		/// </summary>
		uint Errors { get; }
	}
	
	/// <summary>
	/// An event type for new data from a socket or serial port.
	/// </summary>
	public sealed class ReceivedDataEvent : EventArgs {
		public string Data { get; internal set; }
	}
}
