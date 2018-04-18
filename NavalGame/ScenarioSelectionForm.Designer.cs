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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.MapView = new NavalGame.MapView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapView)).BeginInit();
            this.SuspendLayout();
            // 
            // ScenarioList
            // 
            this.ScenarioList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ScenarioList.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScenarioList.FormattingEnabled = true;
            this.ScenarioList.IntegralHeight = false;
            this.ScenarioList.ItemHeight = 21;
            this.ScenarioList.Location = new System.Drawing.Point(829, 433);
            this.ScenarioList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ScenarioList.Name = "ScenarioList";
            this.ScenarioList.Size = new System.Drawing.Size(201, 179);
            this.ScenarioList.TabIndex = 0;
            this.ScenarioList.SelectedIndexChanged += new System.EventHandler(this.ScenarioListSelectedIndexChanged);
            // 
            // ScenarioDescriptionBox
            // 
            this.ScenarioDescriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScenarioDescriptionBox.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScenarioDescriptionBox.Location = new System.Drawing.Point(1046, 433);
            this.ScenarioDescriptionBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ScenarioDescriptionBox.Name = "ScenarioDescriptionBox";
            this.ScenarioDescriptionBox.ReadOnly = true;
            this.ScenarioDescriptionBox.Size = new System.Drawing.Size(227, 179);
            this.ScenarioDescriptionBox.TabIndex = 1;
            this.ScenarioDescriptionBox.Text = "";
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Location = new System.Drawing.Point(829, 620);
            this.StartButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(215, 39);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start Game";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NavalGame.Properties.Resources.Title;
            this.pictureBox1.Location = new System.Drawing.Point(16, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(805, 642);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // LoadButton
            // 
            this.LoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadButton.Location = new System.Drawing.Point(1052, 620);
            this.LoadButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(223, 39);
            this.LoadButton.TabIndex = 5;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButtonClick);
            // 
            // MapView
            // 
            this.MapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapView.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MapView.CameraPosition = ((System.Drawing.PointF)(resources.GetObject("MapView.CameraPosition")));
            this.MapView.CameraScale = 0;
            this.MapView.Location = new System.Drawing.Point(829, 15);
            this.MapView.Margin = new System.Windows.Forms.Padding(4);
            this.MapView.Name = "MapView";
            this.MapView.Size = new System.Drawing.Size(445, 411);
            this.MapView.TabIndex = 2;
            this.MapView.TabStop = false;
            this.MapView.Terrain = null;
            // 
            // ScenarioSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 674);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.MapView);
            this.Controls.Add(this.ScenarioDescriptionBox);
            this.Controls.Add(this.ScenarioList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "ScenarioSelectionForm";
            this.Text = "Scenario Selection";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ScenarioList;
        private System.Windows.Forms.RichTextBox ScenarioDescriptionBox;
        private MapView MapView;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button LoadButton;
    }
}