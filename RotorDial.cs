/*
 *
 *
 *    RotorDial.cs
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

//#define USE_DROP_PANEL

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace KK5JY.RotorCraft {
	/// <summary>
	/// A preset event.
	/// </summary>
	public sealed class PresetEventArgs : EventArgs {
		/// <summary>
		/// The target azimuth.
		/// </summary>
		public int TargetAzimuth { get; set; }
	}

	/// <summary>
	/// Draws a simple rotor dial with preset control.
	/// </summary>
	public partial class RotorDial : UserControl {
		#region Private Fields
		private const int IndicatorThickness = 2;
		private const int IndicatorShadowThickness = 5;
		private const int PresetThickness = 3;
		private const int DialOutlineThickness = 3;
		private const int DialHubSize = 10;
		private const int TickThickness = 2;

		private const int FontOffsetX = 5;
		private const int FontDropShadowDepth = 2;

		private float m_Position;
		private Pen m_IndicatorPen, m_IndicatorShadowPen;
		private Pen m_OutlinePen;
		private Pen m_PresetPen, m_PresetShadowPen;
		private Pen m_TickPen;
		private Brush m_PresetBrush;
		private Brush m_OutlineBrush;
		private Brush m_TextBrush;
		private Brush m_TextShadowBrush;
		private Brush m_FaceBrush;
#if USE_DROP_PANEL
		private Brush m_DropPanelBrush;
#endif
		private Rectangle m_DialBounds;
		private Rectangle m_HubBounds;
		private Point m_Center;
		private int m_Radius;
		private Font m_PositionFont;
		private Font m_PresetFont;
		private float m_PresetAngle;
		#endregion

		/// <summary>
		/// Get/set the pointer position.
		/// </summary>
		public float Position {
			get { return m_Position; }
			set {
				if (value > 360f) value = value % 360f;
				if (value < 0f) value = 0f;
				m_Position = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Get/set the position font.
		/// </summary>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public Font PositionFont {
			get { return m_PositionFont; }
			set {
				m_PositionFont = value;
				if (this.Visible)
					Invalidate();
			}
		}

		/// <summary>
		/// Get/set the position font.
		/// </summary>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public Font PresetFont {
			get { return m_PresetFont; }
			set {
				m_PresetFont = value;
				if (this.Visible)
					Invalidate();
			}
		}

		/// <summary>
		/// Set to 'true' to enable drop shadows under dial text.
		/// </summary>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public bool UseTextDropShadow { get; set; }

		public RotorDial() {
			InitializeComponent();
			m_IndicatorPen = new Pen(Color.WhiteSmoke, IndicatorThickness);
			m_IndicatorShadowPen = new Pen(Color.FromArgb(255, 64, 64, 64), IndicatorShadowThickness);
			m_OutlinePen = new Pen(Brushes.Black, DialOutlineThickness);
			m_TickPen = new Pen(Color.FromArgb(255, 112, 112, 112), TickThickness);
			m_PresetPen = new Pen(Brushes.Red, PresetThickness);
			m_PresetShadowPen = new Pen(Color.FromArgb(255, 64, 64, 64), IndicatorShadowThickness);
			m_PresetBrush = new SolidBrush(Color.Red);
			m_TextBrush = new SolidBrush(Color.Black);
			m_TextShadowBrush = new SolidBrush(Color.FromArgb(255, 96, 96, 96));
#if USE_DROP_PANEL
			m_DropPanelBrush = new SolidBrush(Color.FromArgb(128, 128, 128, 128));
#endif
			m_FaceBrush = new SolidBrush(Color.FromArgb(255, 172, 172, 172));
			m_OutlineBrush = new SolidBrush(Color.Black);
			m_PositionFont = new System.Drawing.Font(SystemFonts.DefaultFont.SystemFontName, 21.0f, FontStyle.Bold);
			m_PresetFont = new System.Drawing.Font(SystemFonts.DefaultFont.SystemFontName, 21.0f, FontStyle.Bold);

			UseTextDropShadow = true;
			DoubleBuffered = true;

			m_PresetAngle = -1;
			m_Position = -1;

			MeasureDialSize();
		}

		#region Events and Handlers
		/// <summary>
		/// Raised when a user preset has been selected.
		/// </summary>
		public event System.EventHandler<PresetEventArgs> PresetSelected;

		/// <summary>
		/// Paint the control details.
		/// </summary>
		protected override void OnPaint(PaintEventArgs e) {
			const double PresetLength = 0.8;
			const double MajorTickInset = 0.825;
			const double HalfTickInset = 0.875;
			const double MinorTickInset = 0.912;
			const double FiveTickInset = 0.950;
			const double PositionLength = 0.975;

			// try not to look too blocky when drawing lines
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			// draw the base disc
			e.Graphics.FillEllipse(m_FaceBrush, m_DialBounds);

			// fill in tick marks
			double angle;
			var inner = new PointF();
			var end = new PointF();
			double inset = MajorTickInset;
			for (int i = 0; i < 360; i += 90) {
				angle = (i * Math.PI) / 180.0;
				inner.X = Convert.ToSingle(m_Center.X + Math.Round(m_Radius * inset * Math.Cos(angle)));
				inner.Y = Convert.ToSingle(m_Center.Y + Math.Round(m_Radius * inset * Math.Sin(angle)));
				end.X = Convert.ToSingle(m_Center.X + Math.Round(m_Radius * Math.Cos(angle)));
				end.Y = Convert.ToSingle(m_Center.Y + Math.Round(m_Radius * Math.Sin(angle)));
				e.Graphics.DrawLine(m_TickPen, inner, end);
			}
			inset = HalfTickInset;
			for (int i = 45; i < 360; i += 90) {
				angle = (i * Math.PI) / 180.0;
				inner.X = Convert.ToSingle(m_Center.X + Math.Round(m_Radius * inset * Math.Cos(angle)));
				inner.Y = Convert.ToSingle(m_Center.Y + Math.Round(m_Radius * inset * Math.Sin(angle)));
				end.X = Convert.ToSingle(m_Center.X + Math.Round(m_Radius * Math.Cos(angle)));
				end.Y = Convert.ToSingle(m_Center.Y + Math.Round(m_Radius * Math.Sin(angle)));
				e.Graphics.DrawLine(m_TickPen, inner, end);
			}
			inset = MinorTickInset;
			foreach (var start in new[] { 15, 30, 60, 75 }) {
				for (int i = start; i < 360; i += 90) {
					angle = (i * Math.PI) / 180.0;
					inner.X = Convert.ToSingle(m_Center.X + Math.Round(m_Radius * inset * Math.Cos(angle)));
					inner.Y = Convert.ToSingle(m_Center.Y + Math.Round(m_Radius * inset * Math.Sin(angle)));
					end.X = Convert.ToSingle(m_Center.X + Math.Round(m_Radius * Math.Cos(angle)));
					end.Y = Convert.ToSingle(m_Center.Y + Math.Round(m_Radius * Math.Sin(angle)));
					e.Graphics.DrawLine(m_TickPen, inner, end);
				}
			}
			inset = FiveTickInset;
			foreach (var start in new[] { 5, 10 }) {
				for (int i = start; i < 360; i += 15) {
					angle = (i * Math.PI) / 180.0;
					inner.X = Convert.ToSingle(m_Center.X + Math.Round(m_Radius * inset * Math.Cos(angle)));
					inner.Y = Convert.ToSingle(m_Center.Y + Math.Round(m_Radius * inset * Math.Sin(angle)));
					end.X = Convert.ToSingle(m_Center.X + Math.Round(m_Radius * Math.Cos(angle)));
					end.Y = Convert.ToSingle(m_Center.Y + Math.Round(m_Radius * Math.Sin(angle)));
					e.Graphics.DrawLine(m_TickPen, inner, end);
				}
			}

			// draw the disc outline
			e.Graphics.DrawEllipse(m_OutlinePen, m_DialBounds);

			// draw the main position indicator
			double theta = (Position - 90.0f) * (Math.PI) / 180f;
			end.X = Convert.ToSingle(m_Center.X + (m_Radius * PositionLength * Math.Cos(theta)));
			end.Y = Convert.ToSingle(m_Center.Y + (m_Radius * PositionLength * Math.Sin(theta)));
			e.Graphics.DrawLine(m_IndicatorShadowPen, m_Center, end);
			end.X = Convert.ToSingle(m_Center.X + (0.98 * m_Radius * PositionLength * Math.Cos(theta)));
			end.Y = Convert.ToSingle(m_Center.Y + (0.98 * m_Radius * PositionLength * Math.Sin(theta)));
			e.Graphics.DrawLine(m_IndicatorPen, m_Center, end);

			// draw the main position text
			var pos = Math.Round(Position);
			if (pos == 360.0) pos = 0.0;
			var pt = pos.ToString() + '\u00B0';
			var ptsz = e.Graphics.MeasureString(pt, m_PositionFont);
			var ptx = ((m_DialBounds.Width - ptsz.Width) / 2) + FontOffsetX;
			var pty = (m_DialBounds.Height * 3 / 4) - ptsz.Height;
#if USE_DROP_PANEL
			e.Graphics.FillRectangle(m_DropPanelBrush, ptx, pty, ptsz.Width, ptsz.Height);
			e.Graphics.DrawRectangle(m_ThinPen, ptx, pty, ptsz.Width, ptsz.Height);
#endif
			for (int i = 1; i <= FontDropShadowDepth; i += 1)
				e.Graphics.DrawString(pt, m_PositionFont, m_TextShadowBrush, ptx + i, pty + i);
			e.Graphics.DrawString(pt, m_PositionFont, m_TextBrush, ptx, pty);

			// if the preset is active, draw it
			if (m_PresetAngle >= 0 && m_PresetAngle < 360) {
				var pa_rad = (m_PresetAngle - 90) * (Math.PI / 180.0);
				end.X = Convert.ToSingle(m_Center.X + (0.98 * PresetLength * m_Radius * Math.Cos(pa_rad)));
				end.Y = Convert.ToSingle(m_Center.Y + (0.98 * PresetLength * m_Radius * Math.Sin(pa_rad)));
				e.Graphics.DrawLine(m_PresetShadowPen, m_Center, end);
				end.X = Convert.ToSingle(m_Center.X + (PresetLength * m_Radius * Math.Cos(pa_rad)));
				end.Y = Convert.ToSingle(m_Center.Y + (PresetLength * m_Radius * Math.Sin(pa_rad)));
				e.Graphics.DrawLine(m_PresetPen, m_Center, end);

				pos = Math.Round(m_PresetAngle);
				if (pos == 360.0) pos = 0.0;
				pt = pos.ToString() + '\u00B0';
				ptsz = e.Graphics.MeasureString(pt, m_PresetFont);
				ptx = ((m_DialBounds.Width - ptsz.Width) / 2) + FontOffsetX;
				pty = (m_DialBounds.Height * 1 / 4);
#if USE_DROP_PANEL
				e.Graphics.FillRectangle(m_DropPanelBrush, ptx, pty, ptsz.Width, ptsz.Height);
				e.Graphics.DrawRectangle(m_ThinPen, ptx, pty, ptsz.Width, ptsz.Height);
#endif
				for (int i = 1; i <= FontDropShadowDepth; i += 1)
					e.Graphics.DrawString(pt, m_PresetFont, m_TextShadowBrush, ptx + i, pty + i);
				e.Graphics.DrawString(pt, m_PresetFont, m_PresetBrush, ptx, pty);
			}

			// draw the 'hub' in the center
			e.Graphics.FillEllipse(m_OutlineBrush, m_HubBounds);

			base.OnPaint(e);
		}

		protected override void OnSizeChanged(EventArgs e) {
			MeasureDialSize();
			//Debug.WriteLine("DEBUG: dial bounds now = " + m_DialBounds.ToString());
			base.OnSizeChanged(e);
		}

		protected override void OnDoubleClick(EventArgs e) {
			var pos = PointToClient(MousePosition);
			if (m_DialBounds.Contains(pos)) {
				var bearing = ComputeBearingFromMousePosition(pos);
				if (PresetSelected != null) {
					PresetSelected(this, new PresetEventArgs { TargetAzimuth = Convert.ToInt32(bearing) });
				}
			}
			base.OnDoubleClick(e);
		}

		protected override void OnMouseLeave(EventArgs e) {
			m_PresetAngle = -1;
			Invalidate();
			base.OnMouseLeave(e);
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			EvaluateMousePresetState();
			base.OnMouseMove(e);
		}
		#endregion

		#region Utilities
		private float ComputeBearingFromMousePosition(Point pos) {
			var dx = -(double)(m_Center.X - pos.X);
			var dy = (double)(m_Center.Y - pos.Y);
			if (dx == 0 && dy >= 0) { return 0; }
			if (dx == 0 && dy < 0) { return 180; }
			var angle = Math.Atan(dy / dx);
			angle = 90.0 - (angle * 180.0 / Math.PI);
			if (dx < 0) angle += 180;
			return Convert.ToSingle(angle);
		}

		private void EvaluateMousePresetState() {
			var pos = PointToClient(MousePosition);
			var bearing = ComputeBearingFromMousePosition(pos);
			m_PresetAngle = bearing;
			Invalidate();
		}

		private void MeasureDialSize() {
			int min = Math.Min(Width, Height);
			int diameter = min - (2 * DialOutlineThickness);
			m_Center = new Point(min / 2, min / 2);
			m_Radius = diameter / 2;
			m_DialBounds = new Rectangle(
				DialOutlineThickness,
				DialOutlineThickness,
				diameter,
				diameter
			);
			m_HubBounds = new Rectangle(
				(diameter / 2) - (DialHubSize / 2) + DialOutlineThickness,
				(diameter / 2) - (DialHubSize / 2) + DialOutlineThickness,
				DialHubSize,
				DialHubSize
			);
		}
		#endregion
	}
}
