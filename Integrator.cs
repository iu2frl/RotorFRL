/*
 *
 *
 *    Integrator.cs
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

namespace KK5JY.RotorCraft {
	/// <summary>
	/// Integrator for azimuth readings.
	/// </summary>
	public class BearingIntegrator {
		#region Private Types & Fields
		/// <summary>
		/// A single angular reading, expressed as X/Y pair.
		/// </summary>
		private struct Angle {
			// the data
			public float X, Y;

			public Angle(int angle) {
				var rad = angle * Math.PI / 180;
				X = Convert.ToSingle(Math.Cos(rad));
				Y = Convert.ToSingle(Math.Sin(rad));
			}

			private Angle(float x, float y) {
				X = x;
				Y = y;
			}

			public static Angle operator + (Angle a, Angle b) {
				return new Angle(a.X + b.X, a.Y + b.Y);
			}

			public static Angle operator -(Angle a, Angle b) {
				return new Angle(a.X - b.X, a.Y - b.Y);
			}

			public static Angle operator /(Angle a, float d) {
				return new Angle(a.X / d, a.Y / d);
			}

			public bool IsZero { get { return X == 0 && Y == 0; } }

			public static Angle Zero { get { return new Angle(0, 0); } }

			public float Value {
				get {
					if (X == 0) return Y < 0 ? 0 : 180;
					var result = Convert.ToSingle(Math.Atan(Y / X) * 180 / Math.PI);
					if (X < 0) {
						result += 180;
					} else if (Y < 0) {
						result = 360 + result;
					}

					return result;
				}
			}
		}

		private Angle m_Sum;
		private Angle[] m_Values;
		private int m_Size;
		private int m_Index;
		#endregion

		#region Public Properties
		/// <summary>
		/// The current value of the integrator.
		/// </summary>
		public float Value {
			get {
				return (m_Sum / m_Size).Value;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Create a new integrator.
		/// </summary>
		/// <param name="size">The number of values to include in the moving average.</param>
		public BearingIntegrator(int size) {
			m_Size = size;
			m_Values = new Angle[size];
			m_Index = 0;
			m_Sum = Angle.Zero;
		}

		/// <summary>
		/// Update the integrator with new data.
		/// </summary>
		/// <param name="value">The new value.</param>
		/// <returns>The new value of the integrator.</returns>
		public float Update(int value) {
			m_Sum -= m_Values[m_Index];
			m_Values[m_Index] = new Angle(value);
			m_Sum += m_Values[m_Index];
			m_Index = (m_Index + 1) % m_Size;
			return Value;
		}

#if DEBUG
		/// <summary>
		/// Unit tests.
		/// </summary>
		public static void Main() {
			Console.WriteLine("BearingIntegrator Tests:");
			BearingIntegrator bi = new BearingIntegrator(20);
			for (float i = 0; i != 380; i += 0.25f) {
				Console.WriteLine("Update[{0}] = {1}", i, bi.Update(Convert.ToInt32(i % 360)));
			}
		}
#endif
		#endregion
	}

	/// <summary>
	/// Integrator for single-precision numbers.
	/// </summary>
	public class SingleIntegrator {
		#region Private Fields
		private float m_Sum;
		private readonly int m_Count;
		private float[] m_Samples;
		private int m_Index;
		#endregion

		#region Public Properties
		/// <summary>
		/// Return the current mean value.
		/// </summary>
		public float Value {
			get { return m_Sum / m_Count; }
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Create a new integrator.
		/// </summary>
		/// <param name="size">The number of samples in the sample window.</param>
		public SingleIntegrator(int size) {
			if (size <= 0)
				throw new ArgumentException("Size cannot be zero");
			m_Samples = new float[size];
			m_Count = size;
			m_Sum = 0.0f;
			m_Index = 0;
		}

		/// <summary>
		/// Update the value with a new sample.
		/// </summary>
		/// <param name="val">The new data.</param>
		/// <returns>The new mean value of the integrator.</returns>
		public float Update(float val) {
			m_Sum += val;
			m_Sum -= m_Samples[m_Index];
			m_Samples[m_Index] = val;
			m_Index = (m_Index + 1) % m_Count;
			return Value;
		}
		#endregion
	}
}
