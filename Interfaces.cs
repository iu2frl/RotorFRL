/*
 *
 *
 *    Interfaces.cs
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
	/// Any object that needs to be started before it will perform
	/// its task.
	/// </summary>
	public interface IStart {
		void Start();
	}
}

namespace KK5JY.RotorCraft {
	/// <summary>
	/// A position update object.
	/// </summary>
	public sealed class PositionUpdateArgs : EventArgs {
		public int AzimuthAngle { get; set; }
	}


	/// <summary>
	/// Rotor directions
	/// </summary>
	public enum Directions {
		Stop,
		Clockwise,
		CounterClockwise,
		Up,
		Down,
	}

	/// <summary>
	/// Interface to control objects.
	/// </summary>
	public interface IRotor : IDisposable, KK5JY.IO.IStart {
		event EventHandler<PositionUpdateArgs> PositionUpdate;

		/// <summary>
		/// Returns the most recent position reading.
		/// </summary>
		int Position { get; }

		/// <summary>
		/// Go to a specific bearing angle.
		/// </summary>
		/// <param name="bearingAngle">The angle in degrees.</param>
		void Preset(int bearingAngle);

		/// <summary>
		/// Move the rotor continuously.
		/// </summary>
		/// <param name="direction">The direction.  Direcitons.Stop will stop the motion.</param>
		void Move(Directions direction);

		/// <summary>
		/// The mimimum interval between position update requests.
		/// </summary>
		int UpdateInterval { get; set; }

		/// <summary>
		/// The timeout while waiting for a position update request.
		/// </summary>
		int Timeout { get; set; }

		/// <summary>
		/// Reports the update rate in updates/sec.
		/// </summary>
		float UpdateRate { get; }

		/// <summary>
		/// Returns true if connected to the controller.
		/// </summary>
		bool IsOpen { get; }

		/// <summary>
		/// Returns the current accumulated error count.
		/// </summary>
		uint Errors { get; }
	}
}
