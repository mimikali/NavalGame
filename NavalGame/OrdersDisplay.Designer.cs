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
            this.MoveBox.SuspendLayout();
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
            this.MoveBox.Size = new System.Drawing.Size(198, 53);
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
            this.MoveBar.Size = new System.Drawing.Size(100, 23);
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
            // OrdersDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BeginTurnButton);
            this.Controls.Add(this.NextTurnButton);
            this.Controls.Add(this.MoveBox);
            this.Name = "OrdersDisplay";
            this.Size = new System.Drawing.Size(214, 359);
            this.MoveBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button MoveButton;
        private System.Windows.Forms.ProgressBar MoveBar;
        private System.Windows.Forms.GroupBox MoveBox;
        private System.Windows.Forms.Button NextTurnButton;
        private System.Windows.Forms.Button BeginTurnButton;
    }
}
