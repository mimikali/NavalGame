﻿namespace NavalGame
{
    partial class OrdersDisplay
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MoveBox = new System.Windows.Forms.GroupBox();
            this.MovePictureBox = new System.Windows.Forms.PictureBox();
            this.MoveButton = new System.Windows.Forms.Button();
            this.NextTurnButton = new System.Windows.Forms.Button();
            this.BeginTurnButton = new System.Windows.Forms.Button();
            this.OrdersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.LightArtilleryBox = new System.Windows.Forms.GroupBox();
            this.LightArtilleryPictureBox = new System.Windows.Forms.PictureBox();
            this.LightArtilleryButton = new System.Windows.Forms.Button();
            this.HeavyArtilleryBox = new System.Windows.Forms.GroupBox();
            this.HeavyArtilleryPictureBox = new System.Windows.Forms.PictureBox();
            this.HeavyArtilleryButton = new System.Windows.Forms.Button();
            this.RepairBox = new System.Windows.Forms.GroupBox();
            this.RepairPictureBox = new System.Windows.Forms.PictureBox();
            this.RepairButton = new System.Windows.Forms.Button();
            this.BuildBox = new System.Windows.Forms.GroupBox();
            this.BuildPictureBox = new System.Windows.Forms.PictureBox();
            this.BuildButton = new System.Windows.Forms.Button();
            this.LoadBox = new System.Windows.Forms.GroupBox();
            this.LoadPictureBox = new System.Windows.Forms.PictureBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.UnloadBox = new System.Windows.Forms.GroupBox();
            this.UnloadPictureBox = new System.Windows.Forms.PictureBox();
            this.UnloadButton = new System.Windows.Forms.Button();
            this.TorpedoBox = new System.Windows.Forms.GroupBox();
            this.TorpedoPictureBox = new System.Windows.Forms.PictureBox();
            this.TorpedoButton = new System.Windows.Forms.Button();
            this.DiveBox = new System.Windows.Forms.GroupBox();
            this.DivePictureBox = new System.Windows.Forms.PictureBox();
            this.DiveButton = new System.Windows.Forms.Button();
            this.HealthBar = new System.Windows.Forms.ProgressBar();
            this.UnitPanel = new System.Windows.Forms.Panel();
            this.UnitTextBox = new System.Windows.Forms.RichTextBox();
            this.UnitPictureBox = new System.Windows.Forms.PictureBox();
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.FlagBox = new System.Windows.Forms.PictureBox();
            this.GreetingText = new System.Windows.Forms.Label();
            this.MoveBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovePictureBox)).BeginInit();
            this.OrdersPanel.SuspendLayout();
            this.LightArtilleryBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LightArtilleryPictureBox)).BeginInit();
            this.HeavyArtilleryBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeavyArtilleryPictureBox)).BeginInit();
            this.RepairBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RepairPictureBox)).BeginInit();
            this.BuildBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BuildPictureBox)).BeginInit();
            this.LoadBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoadPictureBox)).BeginInit();
            this.UnloadBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UnloadPictureBox)).BeginInit();
            this.TorpedoBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TorpedoPictureBox)).BeginInit();
            this.DiveBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DivePictureBox)).BeginInit();
            this.UnitPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UnitPictureBox)).BeginInit();
            this.InfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlagBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MoveBox
            // 
            this.MoveBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveBox.Controls.Add(this.MovePictureBox);
            this.MoveBox.Controls.Add(this.MoveButton);
            this.MoveBox.Location = new System.Drawing.Point(3, 3);
            this.MoveBox.Name = "MoveBox";
            this.MoveBox.Size = new System.Drawing.Size(241, 53);
            this.MoveBox.TabIndex = 0;
            this.MoveBox.TabStop = false;
            // 
            // MovePictureBox
            // 
            this.MovePictureBox.Location = new System.Drawing.Point(122, 10);
            this.MovePictureBox.Name = "MovePictureBox";
            this.MovePictureBox.Size = new System.Drawing.Size(97, 37);
            this.MovePictureBox.TabIndex = 1;
            this.MovePictureBox.TabStop = false;
            // 
            // MoveButton
            // 
            this.MoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MoveButton.Location = new System.Drawing.Point(6, 19);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(75, 23);
            this.MoveButton.TabIndex = 0;
            this.MoveButton.Text = "Move";
            this.MoveButton.UseVisualStyleBackColor = true;
            this.MoveButton.Click += new System.EventHandler(this.MoveButtonClicked);
            // 
            // NextTurnButton
            // 
            this.NextTurnButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NextTurnButton.Location = new System.Drawing.Point(131, 843);
            this.NextTurnButton.Name = "NextTurnButton";
            this.NextTurnButton.Size = new System.Drawing.Size(112, 53);
            this.NextTurnButton.TabIndex = 1;
            this.NextTurnButton.Text = "End Turn";
            this.NextTurnButton.UseVisualStyleBackColor = true;
            this.NextTurnButton.Click += new System.EventHandler(this.NextTurnButtonClick);
            // 
            // BeginTurnButton
            // 
            this.BeginTurnButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BeginTurnButton.Location = new System.Drawing.Point(131, 843);
            this.BeginTurnButton.Name = "BeginTurnButton";
            this.BeginTurnButton.Size = new System.Drawing.Size(119, 53);
            this.BeginTurnButton.TabIndex = 2;
            this.BeginTurnButton.Text = "Begin Turn";
            this.BeginTurnButton.UseVisualStyleBackColor = true;
            this.BeginTurnButton.Click += new System.EventHandler(this.BeginTurnButtonClick);
            // 
            // OrdersPanel
            // 
            this.OrdersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrdersPanel.Controls.Add(this.MoveBox);
            this.OrdersPanel.Controls.Add(this.LightArtilleryBox);
            this.OrdersPanel.Controls.Add(this.HeavyArtilleryBox);
            this.OrdersPanel.Controls.Add(this.RepairBox);
            this.OrdersPanel.Controls.Add(this.BuildBox);
            this.OrdersPanel.Controls.Add(this.LoadBox);
            this.OrdersPanel.Controls.Add(this.UnloadBox);
            this.OrdersPanel.Controls.Add(this.TorpedoBox);
            this.OrdersPanel.Controls.Add(this.DiveBox);
            this.OrdersPanel.Location = new System.Drawing.Point(6, 283);
            this.OrdersPanel.Name = "OrdersPanel";
            this.OrdersPanel.Size = new System.Drawing.Size(247, 545);
            this.OrdersPanel.TabIndex = 3;
            this.OrdersPanel.Visible = false;
            // 
            // LightArtilleryBox
            // 
            this.LightArtilleryBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LightArtilleryBox.Controls.Add(this.LightArtilleryPictureBox);
            this.LightArtilleryBox.Controls.Add(this.LightArtilleryButton);
            this.LightArtilleryBox.Location = new System.Drawing.Point(3, 62);
            this.LightArtilleryBox.Name = "LightArtilleryBox";
            this.LightArtilleryBox.Size = new System.Drawing.Size(241, 53);
            this.LightArtilleryBox.TabIndex = 1;
            this.LightArtilleryBox.TabStop = false;
            // 
            // LightArtilleryPictureBox
            // 
            this.LightArtilleryPictureBox.Location = new System.Drawing.Point(122, 13);
            this.LightArtilleryPictureBox.Name = "LightArtilleryPictureBox";
            this.LightArtilleryPictureBox.Size = new System.Drawing.Size(97, 34);
            this.LightArtilleryPictureBox.TabIndex = 2;
            this.LightArtilleryPictureBox.TabStop = false;
            // 
            // LightArtilleryButton
            // 
            this.LightArtilleryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LightArtilleryButton.Location = new System.Drawing.Point(6, 19);
            this.LightArtilleryButton.Name = "LightArtilleryButton";
            this.LightArtilleryButton.Size = new System.Drawing.Size(75, 23);
            this.LightArtilleryButton.TabIndex = 0;
            this.LightArtilleryButton.Text = "Light Guns";
            this.LightArtilleryButton.UseVisualStyleBackColor = true;
            this.LightArtilleryButton.Click += new System.EventHandler(this.LightArtilleryButtonClick);
            // 
            // HeavyArtilleryBox
            // 
            this.HeavyArtilleryBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HeavyArtilleryBox.Controls.Add(this.HeavyArtilleryPictureBox);
            this.HeavyArtilleryBox.Controls.Add(this.HeavyArtilleryButton);
            this.HeavyArtilleryBox.Location = new System.Drawing.Point(3, 121);
            this.HeavyArtilleryBox.Name = "HeavyArtilleryBox";
            this.HeavyArtilleryBox.Size = new System.Drawing.Size(241, 53);
            this.HeavyArtilleryBox.TabIndex = 2;
            this.HeavyArtilleryBox.TabStop = false;
            // 
            // HeavyArtilleryPictureBox
            // 
            this.HeavyArtilleryPictureBox.Location = new System.Drawing.Point(122, 14);
            this.HeavyArtilleryPictureBox.Name = "HeavyArtilleryPictureBox";
            this.HeavyArtilleryPictureBox.Size = new System.Drawing.Size(97, 33);
            this.HeavyArtilleryPictureBox.TabIndex = 2;
            this.HeavyArtilleryPictureBox.TabStop = false;
            // 
            // HeavyArtilleryButton
            // 
            this.HeavyArtilleryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.HeavyArtilleryButton.Location = new System.Drawing.Point(6, 19);
            this.HeavyArtilleryButton.Name = "HeavyArtilleryButton";
            this.HeavyArtilleryButton.Size = new System.Drawing.Size(75, 23);
            this.HeavyArtilleryButton.TabIndex = 0;
            this.HeavyArtilleryButton.Text = "Heavy Guns";
            this.HeavyArtilleryButton.UseVisualStyleBackColor = true;
            this.HeavyArtilleryButton.Click += new System.EventHandler(this.HeavyArtilleryButtonClick);
            // 
            // RepairBox
            // 
            this.RepairBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RepairBox.Controls.Add(this.RepairPictureBox);
            this.RepairBox.Controls.Add(this.RepairButton);
            this.RepairBox.Location = new System.Drawing.Point(3, 180);
            this.RepairBox.Name = "RepairBox";
            this.RepairBox.Size = new System.Drawing.Size(241, 53);
            this.RepairBox.TabIndex = 3;
            this.RepairBox.TabStop = false;
            // 
            // RepairPictureBox
            // 
            this.RepairPictureBox.Location = new System.Drawing.Point(122, 14);
            this.RepairPictureBox.Name = "RepairPictureBox";
            this.RepairPictureBox.Size = new System.Drawing.Size(97, 33);
            this.RepairPictureBox.TabIndex = 2;
            this.RepairPictureBox.TabStop = false;
            // 
            // RepairButton
            // 
            this.RepairButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RepairButton.Location = new System.Drawing.Point(6, 19);
            this.RepairButton.Name = "RepairButton";
            this.RepairButton.Size = new System.Drawing.Size(75, 23);
            this.RepairButton.TabIndex = 0;
            this.RepairButton.Text = "Repair";
            this.RepairButton.UseVisualStyleBackColor = true;
            this.RepairButton.Click += new System.EventHandler(this.RepairButtonClick);
            // 
            // BuildBox
            // 
            this.BuildBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BuildBox.Controls.Add(this.BuildPictureBox);
            this.BuildBox.Controls.Add(this.BuildButton);
            this.BuildBox.Location = new System.Drawing.Point(3, 239);
            this.BuildBox.Name = "BuildBox";
            this.BuildBox.Size = new System.Drawing.Size(241, 53);
            this.BuildBox.TabIndex = 4;
            this.BuildBox.TabStop = false;
            // 
            // BuildPictureBox
            // 
            this.BuildPictureBox.Location = new System.Drawing.Point(122, 14);
            this.BuildPictureBox.Name = "BuildPictureBox";
            this.BuildPictureBox.Size = new System.Drawing.Size(97, 33);
            this.BuildPictureBox.TabIndex = 2;
            this.BuildPictureBox.TabStop = false;
            // 
            // BuildButton
            // 
            this.BuildButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BuildButton.Location = new System.Drawing.Point(6, 19);
            this.BuildButton.Name = "BuildButton";
            this.BuildButton.Size = new System.Drawing.Size(75, 23);
            this.BuildButton.TabIndex = 0;
            this.BuildButton.Text = "Build";
            this.BuildButton.UseVisualStyleBackColor = true;
            this.BuildButton.Click += new System.EventHandler(this.BuildButtonClick);
            // 
            // LoadBox
            // 
            this.LoadBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadBox.Controls.Add(this.LoadPictureBox);
            this.LoadBox.Controls.Add(this.LoadButton);
            this.LoadBox.Location = new System.Drawing.Point(3, 298);
            this.LoadBox.Name = "LoadBox";
            this.LoadBox.Size = new System.Drawing.Size(241, 53);
            this.LoadBox.TabIndex = 5;
            this.LoadBox.TabStop = false;
            // 
            // LoadPictureBox
            // 
            this.LoadPictureBox.Location = new System.Drawing.Point(122, 14);
            this.LoadPictureBox.Name = "LoadPictureBox";
            this.LoadPictureBox.Size = new System.Drawing.Size(97, 33);
            this.LoadPictureBox.TabIndex = 2;
            this.LoadPictureBox.TabStop = false;
            // 
            // LoadButton
            // 
            this.LoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LoadButton.Location = new System.Drawing.Point(6, 19);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButtonClick);
            // 
            // UnloadBox
            // 
            this.UnloadBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UnloadBox.Controls.Add(this.UnloadPictureBox);
            this.UnloadBox.Controls.Add(this.UnloadButton);
            this.UnloadBox.Location = new System.Drawing.Point(3, 357);
            this.UnloadBox.Name = "UnloadBox";
            this.UnloadBox.Size = new System.Drawing.Size(241, 53);
            this.UnloadBox.TabIndex = 6;
            this.UnloadBox.TabStop = false;
            // 
            // UnloadPictureBox
            // 
            this.UnloadPictureBox.Location = new System.Drawing.Point(122, 14);
            this.UnloadPictureBox.Name = "UnloadPictureBox";
            this.UnloadPictureBox.Size = new System.Drawing.Size(97, 33);
            this.UnloadPictureBox.TabIndex = 2;
            this.UnloadPictureBox.TabStop = false;
            // 
            // UnloadButton
            // 
            this.UnloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.UnloadButton.Location = new System.Drawing.Point(6, 19);
            this.UnloadButton.Name = "UnloadButton";
            this.UnloadButton.Size = new System.Drawing.Size(75, 23);
            this.UnloadButton.TabIndex = 0;
            this.UnloadButton.Text = "Unload";
            this.UnloadButton.UseVisualStyleBackColor = true;
            this.UnloadButton.Click += new System.EventHandler(this.UnloadButtonClick);
            // 
            // TorpedoBox
            // 
            this.TorpedoBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TorpedoBox.Controls.Add(this.TorpedoPictureBox);
            this.TorpedoBox.Controls.Add(this.TorpedoButton);
            this.TorpedoBox.Location = new System.Drawing.Point(3, 416);
            this.TorpedoBox.Name = "TorpedoBox";
            this.TorpedoBox.Size = new System.Drawing.Size(241, 53);
            this.TorpedoBox.TabIndex = 7;
            this.TorpedoBox.TabStop = false;
            // 
            // TorpedoPictureBox
            // 
            this.TorpedoPictureBox.Location = new System.Drawing.Point(122, 14);
            this.TorpedoPictureBox.Name = "TorpedoPictureBox";
            this.TorpedoPictureBox.Size = new System.Drawing.Size(97, 33);
            this.TorpedoPictureBox.TabIndex = 2;
            this.TorpedoPictureBox.TabStop = false;
            // 
            // TorpedoButton
            // 
            this.TorpedoButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TorpedoButton.Location = new System.Drawing.Point(6, 19);
            this.TorpedoButton.Name = "TorpedoButton";
            this.TorpedoButton.Size = new System.Drawing.Size(75, 23);
            this.TorpedoButton.TabIndex = 0;
            this.TorpedoButton.Text = "Torpedo";
            this.TorpedoButton.UseVisualStyleBackColor = true;
            this.TorpedoButton.Click += new System.EventHandler(this.TorpedoButtonClick);
            // 
            // DiveBox
            // 
            this.DiveBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiveBox.Controls.Add(this.DivePictureBox);
            this.DiveBox.Controls.Add(this.DiveButton);
            this.DiveBox.Location = new System.Drawing.Point(3, 475);
            this.DiveBox.Name = "DiveBox";
            this.DiveBox.Size = new System.Drawing.Size(241, 53);
            this.DiveBox.TabIndex = 8;
            this.DiveBox.TabStop = false;
            // 
            // DivePictureBox
            // 
            this.DivePictureBox.Location = new System.Drawing.Point(122, 14);
            this.DivePictureBox.Name = "DivePictureBox";
            this.DivePictureBox.Size = new System.Drawing.Size(97, 33);
            this.DivePictureBox.TabIndex = 2;
            this.DivePictureBox.TabStop = false;
            // 
            // DiveButton
            // 
            this.DiveButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DiveButton.Location = new System.Drawing.Point(6, 19);
            this.DiveButton.Name = "DiveButton";
            this.DiveButton.Size = new System.Drawing.Size(75, 23);
            this.DiveButton.TabIndex = 0;
            this.DiveButton.Text = "Dive";
            this.DiveButton.UseVisualStyleBackColor = true;
            this.DiveButton.Click += new System.EventHandler(this.DiveButtonClick);
            // 
            // HealthBar
            // 
            this.HealthBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HealthBar.Location = new System.Drawing.Point(4, 3);
            this.HealthBar.Name = "HealthBar";
            this.HealthBar.Size = new System.Drawing.Size(241, 23);
            this.HealthBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.HealthBar.TabIndex = 4;
            // 
            // UnitPanel
            // 
            this.UnitPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UnitPanel.Controls.Add(this.UnitTextBox);
            this.UnitPanel.Controls.Add(this.UnitPictureBox);
            this.UnitPanel.Location = new System.Drawing.Point(6, 74);
            this.UnitPanel.Name = "UnitPanel";
            this.UnitPanel.Size = new System.Drawing.Size(247, 169);
            this.UnitPanel.TabIndex = 5;
            this.UnitPanel.Visible = false;
            // 
            // UnitTextBox
            // 
            this.UnitTextBox.Location = new System.Drawing.Point(3, 89);
            this.UnitTextBox.Name = "UnitTextBox";
            this.UnitTextBox.ReadOnly = true;
            this.UnitTextBox.Size = new System.Drawing.Size(241, 76);
            this.UnitTextBox.TabIndex = 2;
            this.UnitTextBox.Text = "";
            // 
            // UnitPictureBox
            // 
            this.UnitPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UnitPictureBox.Location = new System.Drawing.Point(3, 3);
            this.UnitPictureBox.Name = "UnitPictureBox";
            this.UnitPictureBox.Size = new System.Drawing.Size(241, 80);
            this.UnitPictureBox.TabIndex = 0;
            this.UnitPictureBox.TabStop = false;
            // 
            // InfoPanel
            // 
            this.InfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoPanel.Controls.Add(this.HealthBar);
            this.InfoPanel.Location = new System.Drawing.Point(6, 246);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(247, 31);
            this.InfoPanel.TabIndex = 6;
            this.InfoPanel.Visible = false;
            // 
            // FlagBox
            // 
            this.FlagBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlagBox.Location = new System.Drawing.Point(6, 843);
            this.FlagBox.Name = "FlagBox";
            this.FlagBox.Size = new System.Drawing.Size(106, 53);
            this.FlagBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FlagBox.TabIndex = 7;
            this.FlagBox.TabStop = false;
            // 
            // GreetingText
            // 
            this.GreetingText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GreetingText.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreetingText.Location = new System.Drawing.Point(6, 4);
            this.GreetingText.Name = "GreetingText";
            this.GreetingText.Size = new System.Drawing.Size(244, 67);
            this.GreetingText.TabIndex = 8;
            this.GreetingText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OrdersDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GreetingText);
            this.Controls.Add(this.FlagBox);
            this.Controls.Add(this.InfoPanel);
            this.Controls.Add(this.UnitPanel);
            this.Controls.Add(this.OrdersPanel);
            this.Controls.Add(this.BeginTurnButton);
            this.Controls.Add(this.NextTurnButton);
            this.Name = "OrdersDisplay";
            this.Size = new System.Drawing.Size(256, 899);
            this.MoveBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MovePictureBox)).EndInit();
            this.OrdersPanel.ResumeLayout(false);
            this.LightArtilleryBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LightArtilleryPictureBox)).EndInit();
            this.HeavyArtilleryBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeavyArtilleryPictureBox)).EndInit();
            this.RepairBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RepairPictureBox)).EndInit();
            this.BuildBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BuildPictureBox)).EndInit();
            this.LoadBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LoadPictureBox)).EndInit();
            this.UnloadBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UnloadPictureBox)).EndInit();
            this.TorpedoBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TorpedoPictureBox)).EndInit();
            this.DiveBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DivePictureBox)).EndInit();
            this.UnitPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UnitPictureBox)).EndInit();
            this.InfoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FlagBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button MoveButton;
        private System.Windows.Forms.GroupBox MoveBox;
        private System.Windows.Forms.Button NextTurnButton;
        private System.Windows.Forms.Button BeginTurnButton;
        private System.Windows.Forms.FlowLayoutPanel OrdersPanel;
        private System.Windows.Forms.GroupBox LightArtilleryBox;
        private System.Windows.Forms.Button LightArtilleryButton;
        private System.Windows.Forms.GroupBox HeavyArtilleryBox;
        private System.Windows.Forms.Button HeavyArtilleryButton;
        private System.Windows.Forms.ProgressBar HealthBar;
        private System.Windows.Forms.Panel UnitPanel;
        private System.Windows.Forms.PictureBox UnitPictureBox;
        private System.Windows.Forms.RichTextBox UnitTextBox;
        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.PictureBox MovePictureBox;
        private System.Windows.Forms.PictureBox LightArtilleryPictureBox;
        private System.Windows.Forms.PictureBox HeavyArtilleryPictureBox;
        private System.Windows.Forms.GroupBox RepairBox;
        private System.Windows.Forms.PictureBox RepairPictureBox;
        private System.Windows.Forms.Button RepairButton;
        private System.Windows.Forms.GroupBox BuildBox;
        private System.Windows.Forms.PictureBox BuildPictureBox;
        private System.Windows.Forms.Button BuildButton;
        private System.Windows.Forms.PictureBox FlagBox;
        private System.Windows.Forms.Label GreetingText;
        private System.Windows.Forms.Button UnloadButton;
        private System.Windows.Forms.PictureBox UnloadPictureBox;
        private System.Windows.Forms.GroupBox UnloadBox;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.PictureBox LoadPictureBox;
        private System.Windows.Forms.GroupBox LoadBox;
        private System.Windows.Forms.GroupBox TorpedoBox;
        private System.Windows.Forms.PictureBox TorpedoPictureBox;
        private System.Windows.Forms.Button TorpedoButton;
        private System.Windows.Forms.GroupBox DiveBox;
        private System.Windows.Forms.PictureBox DivePictureBox;
        private System.Windows.Forms.Button DiveButton;
    }
}
