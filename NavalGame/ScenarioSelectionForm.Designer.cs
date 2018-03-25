namespace NavalGame
{
    partial class ScenarioSelectionForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScenarioSelectionForm));
            this.ScenarioList = new System.Windows.Forms.ListBox();
            this.ScenarioDescriptionBox = new System.Windows.Forms.RichTextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.MapView = new NavalGame.MapView();
            ((System.ComponentModel.ISupportInitialize)(this.MapView)).BeginInit();
            this.SuspendLayout();
            // 
            // ScenarioList
            // 
            this.ScenarioList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ScenarioList.FormattingEnabled = true;
            this.ScenarioList.Location = new System.Drawing.Point(7, 12);
            this.ScenarioList.Name = "ScenarioList";
            this.ScenarioList.Size = new System.Drawing.Size(190, 329);
            this.ScenarioList.TabIndex = 0;
            this.ScenarioList.SelectedIndexChanged += new System.EventHandler(this.ScenarioListSelectedIndexChanged);
            // 
            // ScenarioDescriptionBox
            // 
            this.ScenarioDescriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScenarioDescriptionBox.Location = new System.Drawing.Point(204, 203);
            this.ScenarioDescriptionBox.Name = "ScenarioDescriptionBox";
            this.ScenarioDescriptionBox.ReadOnly = true;
            this.ScenarioDescriptionBox.Size = new System.Drawing.Size(213, 97);
            this.ScenarioDescriptionBox.TabIndex = 1;
            this.ScenarioDescriptionBox.Text = "";
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Location = new System.Drawing.Point(204, 307);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(213, 34);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start Game";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // MapView
            // 
            this.MapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapView.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MapView.CameraPosition = ((System.Drawing.PointF)(resources.GetObject("MapView.CameraPosition")));
            this.MapView.CameraScale = 0;
            this.MapView.Location = new System.Drawing.Point(204, 13);
            this.MapView.Name = "MapView";
            this.MapView.Size = new System.Drawing.Size(213, 184);
            this.MapView.TabIndex = 2;
            this.MapView.TabStop = false;
            this.MapView.Terrain = null;
            // 
            // ScenarioSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 351);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.MapView);
            this.Controls.Add(this.ScenarioDescriptionBox);
            this.Controls.Add(this.ScenarioList);
            this.Name = "ScenarioSelectionForm";
            this.Text = "ScenarioSelectionForm";
            ((System.ComponentModel.ISupportInitialize)(this.MapView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ScenarioList;
        private System.Windows.Forms.RichTextBox ScenarioDescriptionBox;
        private MapView MapView;
        private System.Windows.Forms.Button StartButton;
    }
}