/*
 *
 *
 *    MainForm.Designer.cs
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

namespace KK5JY.RotorCraft {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.panelControls = new System.Windows.Forms.Panel();
			this.bM15 = new System.Windows.Forms.Button();
			this.bM10 = new System.Windows.Forms.Button();
			this.bP15 = new System.Windows.Forms.Button();
			this.bP10 = new System.Windows.Forms.Button();
			this.bP5 = new System.Windows.Forms.Button();
			this.bM5 = new System.Windows.Forms.Button();
			this.panelPresets = new System.Windows.Forms.Panel();
			this.bPreset4 = new System.Windows.Forms.Button();
			this.numPreset4 = new System.Windows.Forms.NumericUpDown();
			this.bPreset8 = new System.Windows.Forms.Button();
			this.numPreset8 = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.bPreset2 = new System.Windows.Forms.Button();
			this.bPreset3 = new System.Windows.Forms.Button();
			this.numPreset1 = new System.Windows.Forms.NumericUpDown();
			this.bPreset1 = new System.Windows.Forms.Button();
			this.numPreset2 = new System.Windows.Forms.NumericUpDown();
			this.bPreset5 = new System.Windows.Forms.Button();
			this.bPreset6 = new System.Windows.Forms.Button();
			this.numPreset3 = new System.Windows.Forms.NumericUpDown();
			this.numPreset6 = new System.Windows.Forms.NumericUpDown();
			this.bPreset7 = new System.Windows.Forms.Button();
			this.numPreset7 = new System.Windows.Forms.NumericUpDown();
			this.numPreset5 = new System.Windows.Forms.NumericUpDown();
			this.tbMessage = new System.Windows.Forms.TextBox();
			this.bStop = new System.Windows.Forms.Button();
			this.bCW = new System.Windows.Forms.Button();
			this.bCCW = new System.Windows.Forms.Button();
			this.m_Dial = new KK5JY.RotorCraft.RotorDial();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.cbOnTop = new System.Windows.Forms.CheckBox();
			this.cbShowTrayIcon = new System.Windows.Forms.CheckBox();
			this.bConnect = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.cbShowNotifications = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cbShowInTaskbar = new System.Windows.Forms.CheckBox();
			this.cbPort = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.numPollDelay = new System.Windows.Forms.NumericUpDown();
			this.numSmoothing = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.bDisconnect = new System.Windows.Forms.Button();
			this.numPollTimeout = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.cbSpeed = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tbAbout = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panelControls.SuspendLayout();
			this.panelPresets.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPreset4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset5)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPollDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSmoothing)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPollTimeout)).BeginInit();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(542, 289);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.panelControls);
			this.tabPage1.Controls.Add(this.m_Dial);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(534, 263);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Operate";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// panelControls
			// 
			this.panelControls.Controls.Add(this.bM15);
			this.panelControls.Controls.Add(this.bM10);
			this.panelControls.Controls.Add(this.bP15);
			this.panelControls.Controls.Add(this.bP10);
			this.panelControls.Controls.Add(this.bP5);
			this.panelControls.Controls.Add(this.bM5);
			this.panelControls.Controls.Add(this.panelPresets);
			this.panelControls.Controls.Add(this.tbMessage);
			this.panelControls.Controls.Add(this.bStop);
			this.panelControls.Controls.Add(this.bCW);
			this.panelControls.Controls.Add(this.bCCW);
			this.panelControls.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelControls.Location = new System.Drawing.Point(268, 3);
			this.panelControls.Margin = new System.Windows.Forms.Padding(0);
			this.panelControls.Name = "panelControls";
			this.panelControls.Size = new System.Drawing.Size(263, 257);
			this.panelControls.TabIndex = 0;
			// 
			// bM15
			// 
			this.bM15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bM15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bM15.Location = new System.Drawing.Point(4, 40);
			this.bM15.Margin = new System.Windows.Forms.Padding(0);
			this.bM15.Name = "bM15";
			this.bM15.Size = new System.Drawing.Size(42, 23);
			this.bM15.TabIndex = 3;
			this.bM15.Text = "-15";
			this.bM15.UseVisualStyleBackColor = true;
			this.bM15.Click += new System.EventHandler(this.IncrementButtonClick);
			// 
			// bM10
			// 
			this.bM10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bM10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bM10.Location = new System.Drawing.Point(47, 40);
			this.bM10.Margin = new System.Windows.Forms.Padding(0);
			this.bM10.Name = "bM10";
			this.bM10.Size = new System.Drawing.Size(42, 23);
			this.bM10.TabIndex = 4;
			this.bM10.Text = "-10";
			this.bM10.UseVisualStyleBackColor = true;
			this.bM10.Click += new System.EventHandler(this.IncrementButtonClick);
			// 
			// bP15
			// 
			this.bP15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bP15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bP15.Location = new System.Drawing.Point(219, 40);
			this.bP15.Margin = new System.Windows.Forms.Padding(0);
			this.bP15.Name = "bP15";
			this.bP15.Size = new System.Drawing.Size(42, 23);
			this.bP15.TabIndex = 8;
			this.bP15.Text = "+15";
			this.bP15.UseVisualStyleBackColor = true;
			this.bP15.Click += new System.EventHandler(this.IncrementButtonClick);
			// 
			// bP10
			// 
			this.bP10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bP10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bP10.Location = new System.Drawing.Point(176, 40);
			this.bP10.Margin = new System.Windows.Forms.Padding(0);
			this.bP10.Name = "bP10";
			this.bP10.Size = new System.Drawing.Size(42, 23);
			this.bP10.TabIndex = 7;
			this.bP10.Text = "+10";
			this.bP10.UseVisualStyleBackColor = true;
			this.bP10.Click += new System.EventHandler(this.IncrementButtonClick);
			// 
			// bP5
			// 
			this.bP5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bP5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bP5.Location = new System.Drawing.Point(133, 40);
			this.bP5.Margin = new System.Windows.Forms.Padding(0);
			this.bP5.Name = "bP5";
			this.bP5.Size = new System.Drawing.Size(42, 23);
			this.bP5.TabIndex = 6;
			this.bP5.Text = "+5";
			this.bP5.UseVisualStyleBackColor = true;
			this.bP5.Click += new System.EventHandler(this.IncrementButtonClick);
			// 
			// bM5
			// 
			this.bM5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bM5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bM5.Location = new System.Drawing.Point(90, 40);
			this.bM5.Margin = new System.Windows.Forms.Padding(0);
			this.bM5.Name = "bM5";
			this.bM5.Size = new System.Drawing.Size(42, 23);
			this.bM5.TabIndex = 5;
			this.bM5.Text = "-5";
			this.bM5.UseVisualStyleBackColor = true;
			this.bM5.Click += new System.EventHandler(this.IncrementButtonClick);
			// 
			// panelPresets
			// 
			this.panelPresets.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.panelPresets.Controls.Add(this.bPreset4);
			this.panelPresets.Controls.Add(this.numPreset4);
			this.panelPresets.Controls.Add(this.bPreset8);
			this.panelPresets.Controls.Add(this.numPreset8);
			this.panelPresets.Controls.Add(this.label7);
			this.panelPresets.Controls.Add(this.bPreset2);
			this.panelPresets.Controls.Add(this.bPreset3);
			this.panelPresets.Controls.Add(this.numPreset1);
			this.panelPresets.Controls.Add(this.bPreset1);
			this.panelPresets.Controls.Add(this.numPreset2);
			this.panelPresets.Controls.Add(this.bPreset5);
			this.panelPresets.Controls.Add(this.bPreset6);
			this.panelPresets.Controls.Add(this.numPreset3);
			this.panelPresets.Controls.Add(this.numPreset6);
			this.panelPresets.Controls.Add(this.bPreset7);
			this.panelPresets.Controls.Add(this.numPreset7);
			this.panelPresets.Controls.Add(this.numPreset5);
			this.panelPresets.Location = new System.Drawing.Point(8, 72);
			this.panelPresets.Margin = new System.Windows.Forms.Padding(0);
			this.panelPresets.Name = "panelPresets";
			this.panelPresets.Size = new System.Drawing.Size(250, 150);
			this.panelPresets.TabIndex = 1;
			// 
			// bPreset4
			// 
			this.bPreset4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bPreset4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bPreset4.Location = new System.Drawing.Point(75, 121);
			this.bPreset4.Margin = new System.Windows.Forms.Padding(0);
			this.bPreset4.Name = "bPreset4";
			this.bPreset4.Size = new System.Drawing.Size(41, 26);
			this.bPreset4.TabIndex = 13;
			this.bPreset4.Text = "Go";
			this.bPreset4.UseVisualStyleBackColor = true;
			this.bPreset4.Click += new System.EventHandler(this.PresetButtonClick);
			this.bPreset4.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			// 
			// numPreset4
			// 
			this.numPreset4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numPreset4.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numPreset4.Location = new System.Drawing.Point(3, 121);
			this.numPreset4.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
			this.numPreset4.Name = "numPreset4";
			this.numPreset4.Size = new System.Drawing.Size(69, 26);
			this.numPreset4.TabIndex = 12;
			this.numPreset4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numPreset4.ValueChanged += new System.EventHandler(this.PresetTargetChanged);
			// 
			// bPreset8
			// 
			this.bPreset8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bPreset8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bPreset8.Location = new System.Drawing.Point(202, 121);
			this.bPreset8.Margin = new System.Windows.Forms.Padding(0);
			this.bPreset8.Name = "bPreset8";
			this.bPreset8.Size = new System.Drawing.Size(41, 26);
			this.bPreset8.TabIndex = 15;
			this.bPreset8.Text = "Go";
			this.bPreset8.UseVisualStyleBackColor = true;
			this.bPreset8.Click += new System.EventHandler(this.PresetButtonClick);
			this.bPreset8.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			// 
			// numPreset8
			// 
			this.numPreset8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numPreset8.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numPreset8.Location = new System.Drawing.Point(130, 121);
			this.numPreset8.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
			this.numPreset8.Name = "numPreset8";
			this.numPreset8.Size = new System.Drawing.Size(69, 26);
			this.numPreset8.TabIndex = 14;
			this.numPreset8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numPreset8.ValueChanged += new System.EventHandler(this.PresetTargetChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(61, 2);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(127, 20);
			this.label7.TabIndex = 28;
			this.label7.Text = "Preset Targets";
			this.label7.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			// 
			// bPreset2
			// 
			this.bPreset2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bPreset2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bPreset2.Location = new System.Drawing.Point(75, 57);
			this.bPreset2.Margin = new System.Windows.Forms.Padding(0);
			this.bPreset2.Name = "bPreset2";
			this.bPreset2.Size = new System.Drawing.Size(41, 26);
			this.bPreset2.TabIndex = 5;
			this.bPreset2.Text = "Go";
			this.bPreset2.UseVisualStyleBackColor = true;
			this.bPreset2.Click += new System.EventHandler(this.PresetButtonClick);
			this.bPreset2.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			// 
			// bPreset3
			// 
			this.bPreset3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bPreset3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bPreset3.Location = new System.Drawing.Point(75, 89);
			this.bPreset3.Margin = new System.Windows.Forms.Padding(0);
			this.bPreset3.Name = "bPreset3";
			this.bPreset3.Size = new System.Drawing.Size(41, 26);
			this.bPreset3.TabIndex = 9;
			this.bPreset3.Text = "Go";
			this.bPreset3.UseVisualStyleBackColor = true;
			this.bPreset3.Click += new System.EventHandler(this.PresetButtonClick);
			this.bPreset3.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			// 
			// numPreset1
			// 
			this.numPreset1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numPreset1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numPreset1.Location = new System.Drawing.Point(3, 25);
			this.numPreset1.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
			this.numPreset1.Name = "numPreset1";
			this.numPreset1.Size = new System.Drawing.Size(69, 26);
			this.numPreset1.TabIndex = 0;
			this.numPreset1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numPreset1.ValueChanged += new System.EventHandler(this.PresetTargetChanged);
			// 
			// bPreset1
			// 
			this.bPreset1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bPreset1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bPreset1.Location = new System.Drawing.Point(75, 25);
			this.bPreset1.Margin = new System.Windows.Forms.Padding(0);
			this.bPreset1.Name = "bPreset1";
			this.bPreset1.Size = new System.Drawing.Size(41, 26);
			this.bPreset1.TabIndex = 1;
			this.bPreset1.Text = "Go";
			this.bPreset1.UseVisualStyleBackColor = true;
			this.bPreset1.Click += new System.EventHandler(this.PresetButtonClick);
			this.bPreset1.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			// 
			// numPreset2
			// 
			this.numPreset2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numPreset2.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numPreset2.Location = new System.Drawing.Point(3, 57);
			this.numPreset2.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
			this.numPreset2.Name = "numPreset2";
			this.numPreset2.Size = new System.Drawing.Size(69, 26);
			this.numPreset2.TabIndex = 4;
			this.numPreset2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numPreset2.ValueChanged += new System.EventHandler(this.PresetTargetChanged);
			// 
			// bPreset5
			// 
			this.bPreset5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bPreset5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bPreset5.Location = new System.Drawing.Point(202, 25);
			this.bPreset5.Margin = new System.Windows.Forms.Padding(0);
			this.bPreset5.Name = "bPreset5";
			this.bPreset5.Size = new System.Drawing.Size(41, 26);
			this.bPreset5.TabIndex = 3;
			this.bPreset5.Text = "Go";
			this.bPreset5.UseVisualStyleBackColor = true;
			this.bPreset5.Click += new System.EventHandler(this.PresetButtonClick);
			this.bPreset5.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			// 
			// bPreset6
			// 
			this.bPreset6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bPreset6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bPreset6.Location = new System.Drawing.Point(202, 57);
			this.bPreset6.Margin = new System.Windows.Forms.Padding(0);
			this.bPreset6.Name = "bPreset6";
			this.bPreset6.Size = new System.Drawing.Size(41, 26);
			this.bPreset6.TabIndex = 7;
			this.bPreset6.Text = "Go";
			this.bPreset6.UseVisualStyleBackColor = true;
			this.bPreset6.Click += new System.EventHandler(this.PresetButtonClick);
			this.bPreset6.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			// 
			// numPreset3
			// 
			this.numPreset3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numPreset3.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numPreset3.Location = new System.Drawing.Point(3, 89);
			this.numPreset3.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
			this.numPreset3.Name = "numPreset3";
			this.numPreset3.Size = new System.Drawing.Size(69, 26);
			this.numPreset3.TabIndex = 8;
			this.numPreset3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numPreset3.ValueChanged += new System.EventHandler(this.PresetTargetChanged);
			// 
			// numPreset6
			// 
			this.numPreset6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numPreset6.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numPreset6.Location = new System.Drawing.Point(130, 57);
			this.numPreset6.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
			this.numPreset6.Name = "numPreset6";
			this.numPreset6.Size = new System.Drawing.Size(69, 26);
			this.numPreset6.TabIndex = 6;
			this.numPreset6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numPreset6.ValueChanged += new System.EventHandler(this.PresetTargetChanged);
			// 
			// bPreset7
			// 
			this.bPreset7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bPreset7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bPreset7.Location = new System.Drawing.Point(202, 89);
			this.bPreset7.Margin = new System.Windows.Forms.Padding(0);
			this.bPreset7.Name = "bPreset7";
			this.bPreset7.Size = new System.Drawing.Size(41, 26);
			this.bPreset7.TabIndex = 11;
			this.bPreset7.Text = "Go";
			this.bPreset7.UseVisualStyleBackColor = true;
			this.bPreset7.Click += new System.EventHandler(this.PresetButtonClick);
			this.bPreset7.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			// 
			// numPreset7
			// 
			this.numPreset7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numPreset7.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numPreset7.Location = new System.Drawing.Point(130, 89);
			this.numPreset7.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
			this.numPreset7.Name = "numPreset7";
			this.numPreset7.Size = new System.Drawing.Size(69, 26);
			this.numPreset7.TabIndex = 10;
			this.numPreset7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numPreset7.ValueChanged += new System.EventHandler(this.PresetTargetChanged);
			// 
			// numPreset5
			// 
			this.numPreset5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numPreset5.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numPreset5.Location = new System.Drawing.Point(130, 25);
			this.numPreset5.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
			this.numPreset5.Name = "numPreset5";
			this.numPreset5.Size = new System.Drawing.Size(69, 26);
			this.numPreset5.TabIndex = 2;
			this.numPreset5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numPreset5.ValueChanged += new System.EventHandler(this.PresetTargetChanged);
			// 
			// tbMessage
			// 
			this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbMessage.Location = new System.Drawing.Point(23, 233);
			this.tbMessage.Name = "tbMessage";
			this.tbMessage.ReadOnly = true;
			this.tbMessage.Size = new System.Drawing.Size(220, 21);
			this.tbMessage.TabIndex = 39;
			this.tbMessage.TabStop = false;
			this.tbMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.tbMessage.WordWrap = false;
			this.tbMessage.Enter += new System.EventHandler(this.MessageEnter);
			// 
			// bStop
			// 
			this.bStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bStop.BackColor = System.Drawing.Color.DarkRed;
			this.bStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bStop.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.bStop.Location = new System.Drawing.Point(91, 2);
			this.bStop.Name = "bStop";
			this.bStop.Size = new System.Drawing.Size(84, 34);
			this.bStop.TabIndex = 1;
			this.bStop.Text = "STOP";
			this.bStop.UseVisualStyleBackColor = false;
			this.bStop.Click += new System.EventHandler(this.StopClick);
			this.bStop.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			// 
			// bCW
			// 
			this.bCW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bCW.BackColor = System.Drawing.Color.Green;
			this.bCW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bCW.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bCW.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.bCW.Location = new System.Drawing.Point(179, 2);
			this.bCW.Name = "bCW";
			this.bCW.Size = new System.Drawing.Size(84, 34);
			this.bCW.TabIndex = 2;
			this.bCW.Text = ">>";
			this.bCW.UseVisualStyleBackColor = false;
			this.bCW.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			this.bCW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RotateMouseDown);
			this.bCW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RotateMouseUp);
			// 
			// bCCW
			// 
			this.bCCW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bCCW.BackColor = System.Drawing.Color.Green;
			this.bCCW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bCCW.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bCCW.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.bCCW.Location = new System.Drawing.Point(3, 2);
			this.bCCW.Name = "bCCW";
			this.bCCW.Size = new System.Drawing.Size(84, 34);
			this.bCCW.TabIndex = 0;
			this.bCCW.Text = "<<";
			this.bCCW.UseVisualStyleBackColor = false;
			this.bCCW.Paint += new System.Windows.Forms.PaintEventHandler(this.SetTextPaintHints);
			this.bCCW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RotateMouseDown);
			this.bCCW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RotateMouseUp);
			// 
			// m_Dial
			// 
			this.m_Dial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_Dial.Location = new System.Drawing.Point(3, 3);
			this.m_Dial.Name = "m_Dial";
			this.m_Dial.Position = 0F;
			this.m_Dial.PositionFont = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_Dial.PresetFont = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_Dial.Size = new System.Drawing.Size(257, 257);
			this.m_Dial.TabIndex = 0;
			this.m_Dial.TabStop = false;
			this.m_Dial.UseTextDropShadow = true;
			this.m_Dial.PresetSelected += new System.EventHandler<KK5JY.RotorCraft.PresetEventArgs>(this.DialPresetSelected);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.panel1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(534, 263);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Configure";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(528, 257);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.cbOnTop);
			this.panel2.Controls.Add(this.cbShowTrayIcon);
			this.panel2.Controls.Add(this.bConnect);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.cbShowNotifications);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.cbShowInTaskbar);
			this.panel2.Controls.Add(this.cbPort);
			this.panel2.Controls.Add(this.label8);
			this.panel2.Controls.Add(this.numPollDelay);
			this.panel2.Controls.Add(this.numSmoothing);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.bDisconnect);
			this.panel2.Controls.Add(this.numPollTimeout);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.cbSpeed);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Location = new System.Drawing.Point(137, 4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(255, 248);
			this.panel2.TabIndex = 16;
			// 
			// cbOnTop
			// 
			this.cbOnTop.AutoSize = true;
			this.cbOnTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbOnTop.Location = new System.Drawing.Point(7, 171);
			this.cbOnTop.Name = "cbOnTop";
			this.cbOnTop.Size = new System.Drawing.Size(116, 19);
			this.cbOnTop.TabIndex = 14;
			this.cbOnTop.Text = "Always Visible";
			this.cbOnTop.UseVisualStyleBackColor = true;
			this.cbOnTop.CheckedChanged += new System.EventHandler(this.OnTopCheckChanged);
			// 
			// cbShowTrayIcon
			// 
			this.cbShowTrayIcon.AutoSize = true;
			this.cbShowTrayIcon.Checked = true;
			this.cbShowTrayIcon.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbShowTrayIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbShowTrayIcon.Location = new System.Drawing.Point(7, 209);
			this.cbShowTrayIcon.Name = "cbShowTrayIcon";
			this.cbShowTrayIcon.Size = new System.Drawing.Size(123, 19);
			this.cbShowTrayIcon.TabIndex = 8;
			this.cbShowTrayIcon.Text = "Show Tray Icon";
			this.cbShowTrayIcon.UseVisualStyleBackColor = true;
			this.cbShowTrayIcon.CheckedChanged += new System.EventHandler(this.ShowTrayIconCheckedChanged);
			// 
			// bConnect
			// 
			this.bConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bConnect.Location = new System.Drawing.Point(7, 132);
			this.bConnect.Name = "bConnect";
			this.bConnect.Size = new System.Drawing.Size(119, 32);
			this.bConnect.TabIndex = 4;
			this.bConnect.Text = "Connect";
			this.bConnect.UseVisualStyleBackColor = true;
			this.bConnect.Click += new System.EventHandler(this.ConnectClick);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(180, 84);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 15);
			this.label4.TabIndex = 7;
			this.label4.Text = "sec";
			// 
			// cbShowNotifications
			// 
			this.cbShowNotifications.AutoSize = true;
			this.cbShowNotifications.Checked = true;
			this.cbShowNotifications.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbShowNotifications.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbShowNotifications.Location = new System.Drawing.Point(7, 228);
			this.cbShowNotifications.Name = "cbShowNotifications";
			this.cbShowNotifications.Size = new System.Drawing.Size(235, 19);
			this.cbShowNotifications.TabIndex = 9;
			this.cbShowNotifications.Text = "Show Preset Target Notifications";
			this.cbShowNotifications.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(180, 58);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 15);
			this.label5.TabIndex = 6;
			this.label5.Text = "msec";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(4, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Port/Socket:";
			// 
			// cbShowInTaskbar
			// 
			this.cbShowInTaskbar.AutoSize = true;
			this.cbShowInTaskbar.Checked = true;
			this.cbShowInTaskbar.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbShowInTaskbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbShowInTaskbar.Location = new System.Drawing.Point(7, 190);
			this.cbShowInTaskbar.Name = "cbShowInTaskbar";
			this.cbShowInTaskbar.Size = new System.Drawing.Size(132, 19);
			this.cbShowInTaskbar.TabIndex = 7;
			this.cbShowInTaskbar.Text = "Show in Taskbar";
			this.cbShowInTaskbar.UseVisualStyleBackColor = true;
			this.cbShowInTaskbar.CheckedChanged += new System.EventHandler(this.ShowInTaskbarCheckedChanged);
			// 
			// cbPort
			// 
			this.cbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbPort.FormattingEnabled = true;
			this.cbPort.Location = new System.Drawing.Point(103, 3);
			this.cbPort.Name = "cbPort";
			this.cbPort.Size = new System.Drawing.Size(146, 23);
			this.cbPort.TabIndex = 0;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(4, 108);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(80, 15);
			this.label8.TabIndex = 13;
			this.label8.Text = "Smoothing:";
			// 
			// numPollDelay
			// 
			this.numPollDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numPollDelay.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numPollDelay.Location = new System.Drawing.Point(103, 56);
			this.numPollDelay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numPollDelay.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.numPollDelay.Name = "numPollDelay";
			this.numPollDelay.Size = new System.Drawing.Size(71, 21);
			this.numPollDelay.TabIndex = 2;
			this.numPollDelay.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.numPollDelay.ValueChanged += new System.EventHandler(this.PollDelayValueChanged);
			// 
			// numSmoothing
			// 
			this.numSmoothing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numSmoothing.Location = new System.Drawing.Point(103, 106);
			this.numSmoothing.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numSmoothing.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numSmoothing.Name = "numSmoothing";
			this.numSmoothing.Size = new System.Drawing.Size(71, 21);
			this.numSmoothing.TabIndex = 6;
			this.numSmoothing.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.numSmoothing.ValueChanged += new System.EventHandler(this.SmoothingValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(4, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "Poll Delay:";
			// 
			// bDisconnect
			// 
			this.bDisconnect.Enabled = false;
			this.bDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bDisconnect.Location = new System.Drawing.Point(130, 132);
			this.bDisconnect.Name = "bDisconnect";
			this.bDisconnect.Size = new System.Drawing.Size(119, 32);
			this.bDisconnect.TabIndex = 5;
			this.bDisconnect.Text = "Disconnect";
			this.bDisconnect.UseVisualStyleBackColor = true;
			this.bDisconnect.Click += new System.EventHandler(this.DisconnectClick);
			// 
			// numPollTimeout
			// 
			this.numPollTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numPollTimeout.Location = new System.Drawing.Point(103, 81);
			this.numPollTimeout.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.numPollTimeout.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.numPollTimeout.Name = "numPollTimeout";
			this.numPollTimeout.Size = new System.Drawing.Size(71, 21);
			this.numPollTimeout.TabIndex = 3;
			this.numPollTimeout.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.numPollTimeout.ValueChanged += new System.EventHandler(this.PollTimeoutValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(4, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92, 15);
			this.label3.TabIndex = 5;
			this.label3.Text = "Poll Timeout:";
			// 
			// cbSpeed
			// 
			this.cbSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbSpeed.FormattingEnabled = true;
			this.cbSpeed.Location = new System.Drawing.Point(103, 29);
			this.cbSpeed.Name = "cbSpeed";
			this.cbSpeed.Size = new System.Drawing.Size(146, 23);
			this.cbSpeed.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(4, 33);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(52, 15);
			this.label6.TabIndex = 8;
			this.label6.Text = "Speed:";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.tbAbout);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(534, 263);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "About";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// tbAbout
			// 
			this.tbAbout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbAbout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbAbout.Location = new System.Drawing.Point(0, 0);
			this.tbAbout.Multiline = true;
			this.tbAbout.Name = "tbAbout";
			this.tbAbout.ReadOnly = true;
			this.tbAbout.Size = new System.Drawing.Size(534, 263);
			this.tbAbout.TabIndex = 0;
			this.tbAbout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(542, 289);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(550, 316);
			this.Name = "MainForm";
			this.Text = "RotorCraft MARKED";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.panelControls.ResumeLayout(false);
			this.panelControls.PerformLayout();
			this.panelPresets.ResumeLayout(false);
			this.panelPresets.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPreset4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPreset5)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPollDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSmoothing)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPollTimeout)).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox cbPort;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numPollTimeout;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numPollDelay;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbSpeed;
		private RotorDial m_Dial;
		private System.Windows.Forms.Button bCW;
		private System.Windows.Forms.Button bStop;
		private System.Windows.Forms.Button bCCW;
		private System.Windows.Forms.Button bPreset7;
		private System.Windows.Forms.NumericUpDown numPreset7;
		private System.Windows.Forms.Button bPreset6;
		private System.Windows.Forms.Button bPreset5;
		private System.Windows.Forms.Button bPreset3;
		private System.Windows.Forms.Button bPreset2;
		private System.Windows.Forms.Button bPreset1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numPreset6;
		private System.Windows.Forms.NumericUpDown numPreset5;
		private System.Windows.Forms.NumericUpDown numPreset3;
		private System.Windows.Forms.NumericUpDown numPreset2;
		private System.Windows.Forms.NumericUpDown numPreset1;
		private System.Windows.Forms.Button bDisconnect;
		private System.Windows.Forms.Button bConnect;
		private System.Windows.Forms.Panel panelControls;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numSmoothing;
		private System.Windows.Forms.TextBox tbMessage;
		private System.Windows.Forms.Panel panelPresets;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.CheckBox cbShowNotifications;
		private System.Windows.Forms.CheckBox cbShowInTaskbar;
		private System.Windows.Forms.CheckBox cbShowTrayIcon;
		private System.Windows.Forms.CheckBox cbOnTop;
		private System.Windows.Forms.Button bM15;
		private System.Windows.Forms.Button bM10;
		private System.Windows.Forms.Button bP15;
		private System.Windows.Forms.Button bP10;
		private System.Windows.Forms.Button bP5;
		private System.Windows.Forms.Button bM5;
		private System.Windows.Forms.Button bPreset4;
		private System.Windows.Forms.NumericUpDown numPreset4;
		private System.Windows.Forms.Button bPreset8;
		private System.Windows.Forms.NumericUpDown numPreset8;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TextBox tbAbout;
	}
}

