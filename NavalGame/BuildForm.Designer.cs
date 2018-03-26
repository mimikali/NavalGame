namespace NavalGame
{
    partial class BuildForm
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
            this.UnitList = new System.Windows.Forms.ListBox();
            this.UnitView = new System.Windows.Forms.PictureBox();
            this._CancelButton = new System.Windows.Forms.Button();
            this._BuildButton = new System.Windows.Forms.Button();
            this.UnitDescription = new System.Windows.Forms.RichTextBox();
            this.SpendText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UnitView)).BeginInit();
            this.SuspendLayout();
            // 
            // UnitList
            // 
            this.UnitList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.UnitList.DisplayMember = "Name";
            this.UnitList.IntegralHeight = false;
            this.UnitList.Location = new System.Drawing.Point(13, 57);
            this.UnitList.Name = "UnitList";
            this.UnitList.Size = new System.Drawing.Size(120, 290);
            this.UnitList.TabIndex = 0;
            this.UnitList.SelectedIndexChanged += new System.EventHandler(this.UnitListSelectedIndexChanged);
            // 
            // UnitView
            // 
            this.UnitView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UnitView.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.UnitView.Location = new System.Drawing.Point(139, 13);
            this.UnitView.Name = "UnitView";
            this.UnitView.Size = new System.Drawing.Size(309, 141);
            this.UnitView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UnitView.TabIndex = 1;
            this.UnitView.TabStop = false;
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(315, 315);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(133, 32);
            this._CancelButton.TabIndex = 2;
            this._CancelButton.Text = "Cancel";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // _BuildButton
            // 
            this._BuildButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._BuildButton.Location = new System.Drawing.Point(180, 315);
            this._BuildButton.Name = "_BuildButton";
            this._BuildButton.Size = new System.Drawing.Size(129, 32);
            this._BuildButton.TabIndex = 3;
            this._BuildButton.Text = "Build";
            this._BuildButton.UseVisualStyleBackColor = true;
            this._BuildButton.Click += new System.EventHandler(this.BuildButtonClick);
            // 
            // UnitDescription
            // 
            this.UnitDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UnitDescription.Location = new System.Drawing.Point(139, 160);
            this.UnitDescription.Name = "UnitDescription";
            this.UnitDescription.ReadOnly = true;
            this.UnitDescription.Size = new System.Drawing.Size(309, 149);
            this.UnitDescription.TabIndex = 4;
            this.UnitDescription.Text = "";
            // 
            // SpendText
            // 
            this.SpendText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SpendText.Location = new System.Drawing.Point(13, 13);
            this.SpendText.Name = "SpendText";
            this.SpendText.Size = new System.Drawing.Size(120, 41);
            this.SpendText.TabIndex = 5;
            this.SpendText.Text = "Available to spend: ";
            // 
            // BuildForm
            // 
            this.AcceptButton = this._BuildButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(460, 366);
            this.Controls.Add(this.SpendText);
            this.Controls.Add(this.UnitDescription);
            this.Controls.Add(this._BuildButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this.UnitView);
            this.Controls.Add(this.UnitList);
            this.MinimumSize = new System.Drawing.Size(476, 404);
            this.Name = "BuildForm";
            this.Text = "BuildForm";
            ((System.ComponentModel.ISupportInitialize)(this.UnitView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox UnitList;
        private System.Windows.Forms.PictureBox UnitView;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _BuildButton;
        private System.Windows.Forms.RichTextBox UnitDescription;
        private System.Windows.Forms.Label SpendText;
    }
}