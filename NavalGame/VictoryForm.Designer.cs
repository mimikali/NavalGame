namespace NavalGame
{
    partial class VictoryForm
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
            this.VictoryPictureBox = new System.Windows.Forms.PictureBox();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.EndGameButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VictoryPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // VictoryPictureBox
            // 
            this.VictoryPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.VictoryPictureBox.Location = new System.Drawing.Point(101, 12);
            this.VictoryPictureBox.Name = "VictoryPictureBox";
            this.VictoryPictureBox.Size = new System.Drawing.Size(600, 750);
            this.VictoryPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VictoryPictureBox.TabIndex = 0;
            this.VictoryPictureBox.TabStop = false;
            // 
            // ContinueButton
            // 
            this.ContinueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ContinueButton.Location = new System.Drawing.Point(101, 768);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(302, 65);
            this.ContinueButton.TabIndex = 1;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButtonClick);
            // 
            // EndGameButton
            // 
            this.EndGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EndGameButton.Location = new System.Drawing.Point(409, 768);
            this.EndGameButton.Name = "EndGameButton";
            this.EndGameButton.Size = new System.Drawing.Size(292, 65);
            this.EndGameButton.TabIndex = 2;
            this.EndGameButton.Text = "End Game";
            this.EndGameButton.UseVisualStyleBackColor = true;
            this.EndGameButton.Click += new System.EventHandler(this.EndGameButtonClick);
            // 
            // VictoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 853);
            this.Controls.Add(this.EndGameButton);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.VictoryPictureBox);
            this.MaximumSize = new System.Drawing.Size(800, 900);
            this.Name = "VictoryForm";
            this.Text = "Victory";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.VictoryPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox VictoryPictureBox;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Button EndGameButton;
    }
}