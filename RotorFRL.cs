/*
 *
 *
 *    MainForm.cs
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

// define this to sort the presets attached to the notification icon
#define USE_SORTED_PRESET_MENU

// define this symbol to run unit tests instead of the main form
//#define UNIT_TESTS

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using KK5JY.IO;

namespace KK5JY.RotorCraft {
	/// <summary>
	/// The project's main window form.
	/// </summary>
	public partial class MainForm : Form {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
#if DEBUG && UNIT_TESTS
			BearingIntegrator.Main();
#else
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
#endif
		}

		#region Private Fields
		/// <summary>
		/// The rotor object.
		/// </summary>
		private IRotor m_Rotor;

		/// <summary>
		/// Update timer.
		/// </summary>
		private Timer m_UpdateTimer;

		/// <summary>
		/// Integrator for smoothing update jitter.
		/// </summary>
		private BearingIntegrator m_Boxcar;

		/// <summary>
		/// Root of context menu.
		/// </summary>
		private ContextMenu m_TaskbarIconContextMenu;

		/// <summary>
		/// The taskbar notification icon
		/// </summary>
		private NotifyIcon m_NotifyIcon;

		/// <summary>
		/// Convenience ordered list of numeric up/down controls used for presets
		/// </summary>
		private NumericUpDown[] m_PresetNumericUpDowns;

		/// <summary>
		/// The last-displayed title position.
		/// </summary>
		private int m_TitlePosition;

		/// <summary>
		/// Message for momentary display in the main UI window.
		/// </summary>
		private string m_Message;

		/// <summary>
		/// The main UI message timeout
		/// </summary>
		private const int m_MessageTimeout = 3;

		/// <summary>
		/// Time when the message was posted.
		/// </summary>
		private DateTimeOffset m_MessagePosted;

		/// <summary>
		/// The notification icon baloon text timeout.
		/// </summary>
		private const int m_BaloonTimeout = 5;
		#endregion

		/// <summary>
		/// Default ctor.
		/// </summary>
		public MainForm() {
			InitializeComponent();

			// decorate the form with the proper version number
			Text = Platform.VersionString;

			// populate the serial speed selections
			foreach (var speed in new[] { 300, 1200, 4800, 9600, 19200, 38400, 56700, 115200 }) {
				cbSpeed.Items.Add(speed.ToString());
			}
			cbSpeed.Text = "9600";

			// populate the serial port presets
			PopulateSerialPortNames();

			// allocate the boxcar
			m_Boxcar = new BearingIntegrator(Convert.ToInt32(numSmoothing.Value));

			// allocate the timer
			m_UpdateTimer = new Timer();
			m_UpdateTimer.Interval = 250;
			m_UpdateTimer.Tick += TimerTick;

			m_PresetNumericUpDowns = new[] { numPreset1, numPreset2, numPreset3, numPreset4, numPreset5, numPreset6, numPreset7, numPreset8 };
			for (int i = 0; i != m_PresetNumericUpDowns.Length; ++i) {
				m_PresetNumericUpDowns[i].Tag = i;
			}

			if (components == null) {
				components = new System.ComponentModel.Container();
			}
			m_TaskbarIconContextMenu = new ContextMenu();
			var show = new MenuItem("&Show", NotifyIconDoubleClick);
			var cw = new MenuItem("&CW");
			var ccw = new MenuItem("CC&W");		
			m_TaskbarIconContextMenu.MenuItems.Add(show);
			m_TaskbarIconContextMenu.MenuItems.Add(new MenuItem("-"));
			m_TaskbarIconContextMenu.MenuItems.Add(cw);
			m_TaskbarIconContextMenu.MenuItems.Add(ccw);
			foreach (var inc in new[] { 5, 10, 15, 30, 45, 90 }) {
				var up = new MenuItem("+" + inc.ToString() + "\u00B0") { Tag = inc };
				up.Click += IncrementMenuClick;
				cw.MenuItems.Add(up);
				var dn = new MenuItem("-" + inc.ToString() + "\u00B0") { Tag = -inc };
				dn.Click += IncrementMenuClick;
				ccw.MenuItems.Add(dn);
			}
			m_TaskbarIconContextMenu.MenuItems.Add(new MenuItem("-"));
			for (int i = 0; i != m_PresetNumericUpDowns.Length; ++i) {
				var psItem = new MenuItem("0\u00b0", PresetButtonClick);
				psItem.Tag = i;
				m_TaskbarIconContextMenu.MenuItems.Add(psItem);
			}
			m_TaskbarIconContextMenu.MenuItems.Add(new MenuItem("-"));
			var exit = new MenuItem("E&xit", this.CloseClick);
			m_TaskbarIconContextMenu.MenuItems.Add(exit);

			m_NotifyIcon = new NotifyIcon(this.components);
			m_NotifyIcon.Icon = new Icon("AppIcon.ico", new Size(16, 16));
			m_NotifyIcon.ContextMenu = m_TaskbarIconContextMenu;
			m_NotifyIcon.Text = "RotorCraft";
			m_NotifyIcon.Visible = true;
			m_NotifyIcon.DoubleClick += new EventHandler(NotifyIconDoubleClick);
			m_NotifyIcon.Text = "Azimuth: 0\u00B0";
			m_NotifyIcon.BalloonTipTitle = "RotorCraft";
			m_NotifyIcon.BalloonTipText = m_NotifyIcon.Text;

			this.TopMost = false;

			tbAbout.Text = "\r\n" + Platform.VersionString + "\r\n";
			tbAbout.Text += "\r\nCopyright (C) 2014-2015 by Matt Roberts, KK5JY.\r\n";
			tbAbout.Text += "All rights reserved.\r\n";
			tbAbout.Text += "\r\nLicense: GNU GPL 3.0\r\n";
			tbAbout.Text += "www.gnu.org\r\n";

			bM15.Tag = -15;
			bM10.Tag = -10;
			bM5.Tag = -5;
			bP5.Tag = +5;
			bP10.Tag = +10;
			bP15.Tag = +15;
		}

		#region Utilities
		/// <summary>
		/// Go to a preset/target.
		/// </summary>
		private void RunPreset(int angle) {
			// adjust angle to fit within command range
			while (angle < 0) angle += 360;
			angle = angle % 360;

			// MOVE: execute the preset
			var rotor = m_Rotor;
			if (ReferenceEquals(rotor, null)) return;
			Debug.WriteLine("DEBUG: UI Preset/Increment Request = {0}\u00b0", angle);
			rotor.Preset(angle);

			// update notification
			var msg = String.Format("Rotation Target: {0}\u00b0", angle);
			m_NotifyIcon.BalloonTipText = msg;
			if (cbShowNotifications.Checked) {
				m_NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
				m_NotifyIcon.ShowBalloonTip(m_BaloonTimeout);
			}

			// update the message box
			m_MessagePosted = DateTimeOffset.UtcNow;
			m_Message = msg;
		}

		/// <summary>
		/// Populates the serial port combo box.
		/// </summary>
		private void PopulateSerialPortNames() {
			cbPort.Items.Clear();
			foreach (var port in SerialPortHelper.GetPortNames()) {
				cbPort.Items.Add(port);
			}
		}

		/// <summary>
		/// Save the current configuration.
		/// </summary>
		/// <param name="path">Optional path for the file.</param>
		private void SaveConfig(string path = null) {
			bool complainOnFailure = true;
			try {
				if (String.IsNullOrEmpty(path)) {
					path = Platform.ConfigFilePath;
					complainOnFailure = false;
				}
				var folder = System.IO.Path.GetDirectoryName(path);
				if (!String.IsNullOrEmpty(folder) && !System.IO.Directory.Exists(folder))
					System.IO.Directory.CreateDirectory(folder);

				XmlDocument doc = new XmlDocument();
				var cfg = doc.CreateElement("Config");
				cfg.SetAttribute("Version", Platform.VersionNumberString);
				cfg.SetAttribute("ShowNotifications", cbShowNotifications.Checked.ToString());
				cfg.SetAttribute("ShowInTaskbar", cbShowInTaskbar.Checked.ToString());
				cfg.SetAttribute("ShowTrayIcon", cbShowTrayIcon.Checked.ToString());
				cfg.SetAttribute("AlwaysVisible", cbOnTop.Checked.ToString());
				cfg.SetAttribute("X", this.Location.X.ToString());
				cfg.SetAttribute("Y", this.Location.Y.ToString());
				cfg.SetAttribute("W", this.Width.ToString());
				cfg.SetAttribute("H", this.Height.ToString());
				doc.AppendChild(cfg);
				var conn = doc.CreateElement("Connection");
				conn.SetAttribute("Target", cbPort.Text);
				conn.SetAttribute("Speed", cbSpeed.Text);
				conn.SetAttribute("PollDelay", Convert.ToInt32(numPollDelay.Value).ToString());
				conn.SetAttribute("PollTimeout", Convert.ToInt32(numPollTimeout.Value).ToString());
				conn.SetAttribute("Smoothing", Convert.ToInt32(numSmoothing.Value).ToString());
				conn.SetAttribute("Connected", (m_Rotor != null).ToString());
				cfg.AppendChild(conn);
				var pre = doc.CreateElement("Presets");
				cfg.AppendChild(pre);
				foreach (var ps in m_PresetNumericUpDowns) {
					var item = doc.CreateElement("Preset");
					item.SetAttribute("Azimuth", Convert.ToInt32(ps.Value).ToString());
					pre.AppendChild(item);
				}

				using (var s = System.IO.File.Create(path)) {
					doc.Save(s);
				}
			} catch {
				if (complainOnFailure) {
					MessageBox.Show(
						String.Format("Could not store the configuration in {0}", path),
						"Error"
					);
				}
				return;
			}
			Debug.WriteLine(String.Format("DEBUG: Saved current configuration to: {0}", path));

		}

		/// <summary>
		/// Load a new configuration.
		/// </summary>
		/// <param name="path">Optional path for the file.</param>
		private void LoadConfig(string path = null) {
			bool complainOnFailure = true;
			XmlDocument doc = new XmlDocument();
			try {
				if (String.IsNullOrEmpty(path)) {
					path = Platform.ConfigFilePath;
					complainOnFailure = false;
				}

				doc.Load(path);
				if (!String.Equals(doc.DocumentElement.LocalName, "Config"))
					return;

				// NOTE: due to dependencies between these three CB's, they should be restored in this order
				if (doc.DocumentElement.HasAttribute("ShowNotifications")) {
					cbShowNotifications.Checked = Boolean.Parse(doc.DocumentElement.GetAttribute("ShowNotifications"));
				}
				if (doc.DocumentElement.HasAttribute("ShowTrayIcon")) {
					cbShowTrayIcon.Checked = Boolean.Parse(doc.DocumentElement.GetAttribute("ShowTrayIcon"));
				}
				if (doc.DocumentElement.HasAttribute("ShowInTaskbar")) {
					cbShowInTaskbar.Checked = Boolean.Parse(doc.DocumentElement.GetAttribute("ShowInTaskbar"));
				}
				if (doc.DocumentElement.HasAttribute("AlwaysVisible")) {
					cbOnTop.Checked = Boolean.Parse(doc.DocumentElement.GetAttribute("AlwaysVisible"));
				}
				if (doc.DocumentElement.HasAttribute("X") && doc.DocumentElement.HasAttribute("Y")) {
					int x = Int32.Parse(doc.DocumentElement.GetAttribute("X"));
					int y = Int32.Parse(doc.DocumentElement.GetAttribute("Y"));

					if (SystemInformation.VirtualScreen.IntersectsWith(new Rectangle(new Point(x, y), this.Size))) {
						this.Location = new Point(x, y);
					}
				}
				if (doc.DocumentElement.HasAttribute("H") && doc.DocumentElement.HasAttribute("W")) {
					int w = Int32.Parse(doc.DocumentElement.GetAttribute("W"));
					int h = Int32.Parse(doc.DocumentElement.GetAttribute("H"));

					if (w >= this.MinimumSize.Width && h >= this.MinimumSize.Height) {
						this.Size = new Size(w, h);
					}
				}

				var go = false;
				foreach (var child in doc.DocumentElement.ChildNodes.OfType<XmlElement>()) {
					switch (child.LocalName) {
						case "Connection":
							cbPort.Text = child.GetAttribute("Target");
							cbSpeed.Text = child.GetAttribute("Speed");
							numPollDelay.Value = Int32.Parse(child.GetAttribute("PollDelay"));
							numPollTimeout.Value = Int32.Parse(child.GetAttribute("PollTimeout"));
							if (child.HasAttribute("Smoothing"))
								numSmoothing.Value = Int32.Parse(child.GetAttribute("Smoothing"));
							go = child.GetAttribute("Connected").Equals("True", StringComparison.InvariantCultureIgnoreCase);
							break;
						case "Presets":
							int i = 0;
							foreach(var ps in child.ChildNodes.OfType<XmlElement>().Where(ps => ps.LocalName.Equals("Preset"))) {
								m_PresetNumericUpDowns[i++].Value = Decimal.Parse(ps.GetAttribute("Azimuth"));
							}
							break;
					}
				}

				if (go)
					ConnectClick(this, new EventArgs());
			} catch {
				if (complainOnFailure) {
					MessageBox.Show(
						String.Format("Could not restore the configuration from {0}", path),
						"Error"
					);
				}
				return;
			}
			Debug.WriteLine(String.Format("DEBUG: Loaded configuration from: {0}", path));
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Raised when the form is initially shown.
		/// </summary>
		protected override void OnShown(EventArgs e) {
			LoadConfig();
			m_UpdateTimer.Start();

			base.OnShown(e);
		}

		/// <summary>
		/// Raised when the form is closing.
		/// </summary>
		protected override void OnClosing(CancelEventArgs e) {
			// stop the updates
			m_UpdateTimer.Stop();

			// persist the current configuration
			SaveConfig();

			// disconnect the controller
#if USE_OLD_TASK_SHUTDOWN_CODE
			// (TODO: FIXME: this shouldn't require a Task)
			Task t = new Task(() => StopRotor());
			t.Start();
#else
			StopRotor();
#endif
			base.OnClosing(e);
		}

		/// <summary>
		/// Raised when the user commands a preset using the dial.
		/// </summary>
		private void DialPresetSelected(object sender, PresetEventArgs e) {
			// move the rotor
			RunPreset(e.TargetAzimuth);
		}

		/// <summary>
		/// Raised when STOP is clicked.
		/// </summary>
		private void StopClick(object sender, EventArgs e) {
			if (ReferenceEquals(m_Rotor, null)) return;
			
			m_Rotor.Move(Directions.Stop);
		}

		/// <summary>
		/// Raised when one of the CW/CCW buttons is pressed.
		/// </summary>
		private void RotateMouseDown(object sender, MouseEventArgs e) {
			if (ReferenceEquals(m_Rotor, null)) return;

			if (ReferenceEquals(sender, bCCW)) {
				bCCW.Capture = true;
				m_Rotor.Move(Directions.CounterClockwise);
			} else if (ReferenceEquals(sender, bCW)) {
				bCW.Capture = true;
				m_Rotor.Move(Directions.Clockwise);
			}
		}

		/// <summary>
		/// Raised when one of the CW/CCW buttons is released.
		/// </summary>
		private void RotateMouseUp(object sender, MouseEventArgs e) {
			if (ReferenceEquals(m_Rotor, null)) return;
			if (ReferenceEquals(sender, bCCW) || (ReferenceEquals(sender, bCW))) {
				(sender as Button).Capture = false;
				m_Rotor.Move(Directions.Stop);
			}
		}

		/// <summary>
		/// Raised when CONNECT is clicked.
		/// </summary>
		private void ConnectClick(object sender, EventArgs e) {
			if (m_Rotor != null) return;
			try {
				IDeviceHelper device = null;
				var parts = cbPort.Text.Split(':');
				if (parts.Length == 2) {
					var ips = System.Net.Dns.GetHostAddresses(parts[0]);
					int tcp = 0;
					Int32.TryParse(parts[1], out tcp);
					if (ips != null && ips.Length != 0 && tcp > 0) {
						device = new SocketHelper(new System.Net.IPEndPoint(ips[0], tcp));
					}
				} else {
					var sport = new SerialPort(cbPort.Text);
					sport.WriteTimeout = 1000;
					device = new SerialPortHelper(sport);
				}

				// if the config is bad, complain and give up
				if (device == null) {
					MessageBox.Show("There is a problem with your configuration.  Please change settings and try again.", "Configuration Error");
					return;
				}

				m_Rotor = new YaesuRotor(device);
				m_Rotor.Timeout = Convert.ToInt32(numPollTimeout.Value) * 1000;
				m_Rotor.UpdateInterval = Convert.ToInt32(numPollDelay.Value);
				m_Rotor.Start();
				bDisconnect.Enabled = true;
				bConnect.Enabled = false;
				cbPort.Enabled = false;
				cbSpeed.Enabled = false;
			} catch (Exception ex) {
				MessageBox.Show("Could not open the configured port: " + ex.Message, "Error");
			}
		}

		/// <summary>
		/// Utility to shut down the rotor interface.
		/// </summary>
		private void StopRotor() {
			if (m_Rotor != null) {
				m_Rotor.Dispose();
				m_Rotor = null;
			}
		}

		/// <summary>
		/// Raised when DISCONNECT is clicked.
		/// </summary>
		private void DisconnectClick(object sender, EventArgs e) {
			StopRotor();

			m_Dial.Position = -1;
			bDisconnect.Enabled = false;
			bConnect.Enabled = true;
			cbPort.Enabled = true;
			cbSpeed.Enabled = true;
		}

		/// <summary>
		/// Raised when the smoothing value changes.
		/// </summary>
		private void SmoothingValueChanged(object sender, EventArgs e) {
			m_Boxcar = new BearingIntegrator(Convert.ToInt32(numSmoothing.Value));
		}

		/// <summary>
		/// Raised when the poll delay value is changed.
		/// </summary>
		private void PollDelayValueChanged(object sender, EventArgs e) {
			if (ReferenceEquals(m_Rotor, null)) return;

			m_Rotor.UpdateInterval = Convert.ToInt32(numPollDelay.Value);
		}

		/// <summary>
		/// Raised when the poll timeout value is changed.
		/// </summary>
		private void PollTimeoutValueChanged(object sender, EventArgs e) {
			if (ReferenceEquals(m_Rotor, null)) return;

			m_Rotor.Timeout = Convert.ToInt32(numPollTimeout.Value) * 1000;
		}

		/// <summary>
		/// Raised by the timer.
		/// </summary>
		private void TimerTick(object sender, EventArgs e) {
			var rotor = m_Rotor;
			if (ReferenceEquals(rotor, null)) {
				m_Dial.Position = -1;
				if (tbMessage.Text != "Disconnected") {
					tbMessage.Text = "Disconnected";
					tbMessage.BackColor = SystemColors.Control;
				}
				if (Text.StartsWith("[")) {
					Text = Platform.VersionString;
				}
			} else {
				if (m_Rotor.IsOpen) {
					var bc = m_Boxcar.Update(rotor.Position);
					m_Dial.Position = bc;

					// now update the message box
					if (!String.IsNullOrEmpty(m_Message)) {
						if (!String.Equals(tbMessage.Text, m_Message)) {
							tbMessage.Text = m_Message;
							tbMessage.BackColor = Color.LightGreen;
						}
						if ((DateTimeOffset.UtcNow - m_MessagePosted).TotalSeconds >= m_MessageTimeout) {
							m_Message = null;
						}
					} else {
						tbMessage.Text = String.Format("Update Rate: {0:0.0} Hz", rotor.UpdateRate);
						tbMessage.BackColor = SystemColors.Control;
					}

					// now update the title bar
					var bci = Convert.ToInt32(Math.Round(bc));
					if (m_TitlePosition != bci) {
						m_TitlePosition = bci;
						Text = String.Format("[{0}\u00B0] {1}", m_TitlePosition, Platform.VersionString);
						m_NotifyIcon.Text = String.Format("Azimuth: {0}\u00B0", m_TitlePosition);
					}
				} else {
					m_Dial.Position = -1;
					tbMessage.Text = "Connecting...";
					tbMessage.BackColor = Color.LightYellow;
					if (m_Rotor.Errors != 0) {
						tbMessage.Text += String.Format(" ({0}) errors", m_Rotor.Errors);
					}
					if (Text.StartsWith("[")) {
						Text = Platform.VersionString;
					}
				}
			}
		}

		/// <summary>
		/// Use anti-aliasing on attached Control.
		/// </summary>
		private void SetTextPaintHints(object sender, PaintEventArgs e) {
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
		}

		/// <summary>
		/// Raised when one of the preset GO buttons is pressed.
		/// </summary>
		private void PresetButtonClick(object sender, EventArgs e) {
			if (ReferenceEquals(m_Rotor, null)) return;
			int angle = -1;

			var mi = sender as MenuItem;
			if (!ReferenceEquals(mi, null)) {
				if (mi.Tag == null)
					return;

				try {
					angle = (int)(mi.Tag);
				} catch {
					return;
				}
			} else {
				foreach (var kvp in new[] {
					new KeyValuePair<Control, NumericUpDown>(bPreset1, numPreset1),
					new KeyValuePair<Control, NumericUpDown>(bPreset2, numPreset2),
					new KeyValuePair<Control, NumericUpDown>(bPreset3, numPreset3),
					new KeyValuePair<Control, NumericUpDown>(bPreset4, numPreset4),
					new KeyValuePair<Control, NumericUpDown>(bPreset5, numPreset5),
					new KeyValuePair<Control, NumericUpDown>(bPreset6, numPreset6),
					new KeyValuePair<Control, NumericUpDown>(bPreset7, numPreset7),
					new KeyValuePair<Control, NumericUpDown>(bPreset8, numPreset8),
				}) {
					if (ReferenceEquals(sender, kvp.Key)) {
						angle = Convert.ToInt32(kvp.Value.Value);
						break;
					}
				}
			}

			if (angle >= 0) {
				RunPreset(angle);
			}
		}

		/// <summary>
		/// Raised when an incremental context item is clicked.
		/// </summary>
		private void IncrementMenuClick(object sender, EventArgs e) {
			var inc = (int)((sender as MenuItem).Tag);
			var newPos = Convert.ToInt32(m_Dial.Position + inc) % 360;
			RunPreset(newPos);
		}

		/// <summary>
		/// Raised when a main form incremental button is clicked.
		/// </summary>
		private void IncrementButtonClick(object sender, EventArgs e) {
			var inc = (int)((sender as Button).Tag);
			var newPos = Convert.ToInt32(m_Dial.Position + inc) % 360;
			RunPreset(newPos);
		}

		/// <summary>
		/// Raised whenever a preset value is changed.
		/// </summary>
		private void PresetTargetChanged(object sender, EventArgs e) {
#if USE_SORTED_PRESET_MENU
			var presets = new List<int>(m_PresetNumericUpDowns.Length);
			foreach(var num in m_PresetNumericUpDowns) {
				presets.Add(Convert.ToInt32(num.Value));
			}
			presets.Sort();

			int j = 0;
			foreach(var mi in m_TaskbarIconContextMenu.MenuItems.OfType<MenuItem>().Where(i => i.Tag != null)) {
				mi.Text = presets[j].ToString() + '\u00b0';
				mi.Tag = presets[j++];
			}
#else
			var num = sender as NumericUpDown;
			if (ReferenceEquals(num, null)) {
				return;
			}
			if (num.Tag == null) {
				return;
			}
			int index = (int)(num.Tag);
			foreach (var mi in m_TaskbarIconContextMenu.MenuItems.OfType<MenuItem>()) {
				if (mi.Tag == null) {
					continue;
				}
				int mIndex = (int)(mi.Tag);
				if (mIndex == index) {
					mi.Text = Convert.ToInt32(num.Value).ToString() + '\u00b0';
					break;
				}
			}
#endif
		}

		/// <summary>
		/// Raised when the 'show in taskbar' checkbox is changed.
		/// </summary>
		private void ShowInTaskbarCheckedChanged(object sender, EventArgs e) {
			this.ShowInTaskbar = cbShowInTaskbar.Checked;
			cbShowTrayIcon.Enabled = cbShowInTaskbar.Checked;
			if (!cbShowInTaskbar.Checked) cbShowTrayIcon.Checked = true;
		}

		/// <summary>
		/// Raised when the tray icon is double-clicked.
		/// </summary>
		private void NotifyIconDoubleClick(object sender, EventArgs e) {
			if (WindowState == FormWindowState.Minimized) {
				WindowState = FormWindowState.Normal;
			}
			Activate();
		}

		/// <summary>
		/// Raised when the close menu item is selected.
		/// </summary>
		private void CloseClick(object sender, EventArgs args) {
			this.Close();
		}

		/// <summary>
		/// Raised when 'show tray icon' checkbox is changed.
		/// </summary>
		private void ShowTrayIconCheckedChanged(object sender, EventArgs e) {
			cbShowNotifications.Enabled = m_NotifyIcon.Visible = cbShowTrayIcon.Checked;
		}

		/// <summary>
		/// Raised when the cursor tries to enter the message text box.
		/// </summary>
		private void MessageEnter(object sender, EventArgs e) {
			bStop.Select();
		}

		/// <summary>
		/// Raised when 'always visible' state is changed.
		/// </summary>
		private void OnTopCheckChanged(object sender, EventArgs e) {
			this.TopMost = cbOnTop.Checked;
		}
        #endregion

        private void m_Dial_Load(object sender, EventArgs e)
        {

        }
    }
}
