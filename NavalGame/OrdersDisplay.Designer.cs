namespace NavalGame
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
            this.MoveBar = new System.Windows.Forms.ProgressBar();
            this.MoveButton = new System.Windows.Forms.Button();
            this.NextTurnButton = new System.Windows.Forms.Button();
            this.BeginTurnButton = new System.Windows.Forms.Button();
            this.OrdersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.LightArtilleryBox = new System.Windows.Forms.GroupBox();
            this.LightArtilleryBar = new System.Windows.Forms.ProgressBar();
            this.LightArtilleryButton = new System.Windows.Forms.Button();
            this.HeavyArtilleryBox = new System.Windows.Forms.GroupBox();
            this.HeavyArtilleryBar = new System.Windows.Forms.ProgressBar();
            this.HeavyArtilleryButton = new System.Windows.Forms.Button();
            this.MoveBox.SuspendLayout();
            this.OrdersPanel.SuspendLayout();
            this.LightArtilleryBox.SuspendLayout();
            this.HeavyArtilleryBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MoveBox
            // 
            this.MoveBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveBox.Controls.Add(this.MoveBar);
            this.MoveBox.Controls.Add(this.MoveButton);
            this.MoveBox.Location = new System.Drawing.Point(3, 3);
            this.MoveBox.Name = "MoveBox";
            this.MoveBox.Size = new System.Drawing.Size(205, 53);
            this.MoveBox.TabIndex = 0;
            this.MoveBox.TabStop = false;
            // 
            // MoveBar
            // 
            this.MoveBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveBar.Location = new System.Drawing.Point(87, 19);
            this.MoveBar.Name = "MoveBar";
            this.MoveBar.Size = new System.Drawing.Size(107, 23);
            this.MoveBar.TabIndex = 1;
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
            this.NextTurnButton.Location = new System.Drawing.Point(62, 315);
            this.NextTurnButton.Name = "NextTurnButton";
            this.NextTurnButton.Size = new System.Drawing.Size(75, 23);
            this.NextTurnButton.TabIndex = 1;
            this.NextTurnButton.Text = "End Turn";
            this.NextTurnButton.UseVisualStyleBackColor = true;
            this.NextTurnButton.Click += new System.EventHandler(this.NextTurnButtonClick);
            // 
            // BeginTurnButton
            // 
            this.BeginTurnButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BeginTurnButton.Location = new System.Drawing.Point(62, 315);
            this.BeginTurnButton.Name = "BeginTurnButton";
            this.BeginTurnButton.Size = new System.Drawing.Size(75, 23);
            this.BeginTurnButton.TabIndex = 2;
            this.BeginTurnButton.Text = "Begin Turn";
            this.BeginTurnButton.UseVisualStyleBackColor = true;
            this.BeginTurnButton.Click += new System.EventHandler(this.BeginTurnButtonClick);
            // 
            // OrdersPanel
            // 
            this.OrdersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrdersPanel.Controls.Add(this.MoveBox);
            this.OrdersPanel.Controls.Add(this.LightArtilleryBox);
            this.OrdersPanel.Controls.Add(this.HeavyArtilleryBox);
            this.OrdersPanel.Location = new System.Drawing.Point(3, 3);
            this.OrdersPanel.Name = "OrdersPanel";
            this.OrdersPanel.Size = new System.Drawing.Size(208, 212);
            this.OrdersPanel.TabIndex = 3;
            // 
            // LightArtilleryBox
            // 
            this.LightArtilleryBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LightArtilleryBox.Controls.Add(this.LightArtilleryBar);
            this.LightArtilleryBox.Controls.Add(this.LightArtilleryButton);
            this.LightArtilleryBox.Location = new System.Drawing.Point(3, 62);
            this.LightArtilleryBox.Name = "LightArtilleryBox";
            this.LightArtilleryBox.Size = new System.Drawing.Size(205, 53);
            this.LightArtilleryBox.TabIndex = 1;
            this.LightArtilleryBox.TabStop = false;
            // 
            // LightArtilleryBar
            // 
            this.LightArtilleryBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LightArtilleryBar.Location = new System.Drawing.Point(87, 19);
            this.LightArtilleryBar.Name = "LightArtilleryBar";
            this.LightArtilleryBar.Size = new System.Drawing.Size(107, 23);
            this.LightArtilleryBar.TabIndex = 1;
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
            this.HeavyArtilleryBox.Controls.Add(this.HeavyArtilleryBar);
            this.HeavyArtilleryBox.Controls.Add(this.HeavyArtilleryButton);
            this.HeavyArtilleryBox.Location = new System.Drawing.Point(3, 121);
            this.HeavyArtilleryBox.Name = "HeavyArtilleryBox";
            this.HeavyArtilleryBox.Size = new System.Drawing.Size(205, 53);
            this.HeavyArtilleryBox.TabIndex = 2;
            this.HeavyArtilleryBox.TabStop = false;
            // 
            // HeavyArtilleryBar
            // 
            this.HeavyArtilleryBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HeavyArtilleryBar.Location = new System.Drawing.Point(87, 19);
            this.HeavyArtilleryBar.Name = "HeavyArtilleryBar";
            this.HeavyArtilleryBar.Size = new System.Drawing.Size(107, 23);
            this.HeavyArtilleryBar.TabIndex = 1;
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
            // OrdersDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OrdersPanel);
            this.Controls.Add(this.BeginTurnButton);
            this.Controls.Add(this.NextTurnButton);
            this.Name = "OrdersDisplay";
            this.Size = new System.Drawing.Size(214, 359);
            this.MoveBox.ResumeLayout(false);
            this.OrdersPanel.ResumeLayout(false);
            this.LightArtilleryBox.ResumeLayout(false);
            this.HeavyArtilleryBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button MoveButton;
        private System.Windows.Forms.ProgressBar MoveBar;
        private System.Windows.Forms.GroupBox MoveBox;
        private System.Windows.Forms.Button NextTurnButton;
        private System.Windows.Forms.Button BeginTurnButton;
        private System.Windows.Forms.FlowLayoutPanel OrdersPanel;
        private System.Windows.Forms.GroupBox LightArtilleryBox;
        private System.Windows.Forms.ProgressBar LightArtilleryBar;
        private System.Windows.Forms.Button LightArtilleryButton;
        private System.Windows.Forms.GroupBox HeavyArtilleryBox;
        private System.Windows.Forms.ProgressBar HeavyArtilleryBar;
        private System.Windows.Forms.Button HeavyArtilleryButton;
    }
}
