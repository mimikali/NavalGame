namespace NavalGame
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.MapDisplay = new NavalGame.MapDisplay();
            this.OrdersDisplay = new NavalGame.OrdersDisplay(MapDisplay);
            this.ToolTip = new System.Windows.Forms.ToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.MapDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdersDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // OrdersDisplay
            // 
            this.OrdersDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrdersDisplay.BackColor = System.Drawing.SystemColors.HotTrack;
            this.OrdersDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OrdersDisplay.Location = new System.Drawing.Point(620, 10);
            this.OrdersDisplay.Name = "OrdersDisplay";
            this.OrdersDisplay.Size = new System.Drawing.Size(170, 430);
            this.OrdersDisplay.InitialWidth = 170;
            this.OrdersDisplay.InitialHeight = 430;
            this.OrdersDisplay.TabIndex = 0;
            this.OrdersDisplay.TabStop = false;
            // 
            // MapDisplay
            // 
            this.MapDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapDisplay.CameraPosition = ((System.Drawing.PointF)(resources.GetObject("MapDisplay.CameraPosition")));
            this.MapDisplay.CameraScale = 20;
            this.MapDisplay.Game = null;
            this.MapDisplay.Location = new System.Drawing.Point(10, 10);
            this.MapDisplay.Name = "MapDisplay";
            this.MapDisplay.Size = new System.Drawing.Size(600, 430);
            this.MapDisplay.TabIndex = 0;
            this.MapDisplay.TabStop = false;
            this.MapDisplay.OrdersDisplay = OrdersDisplay;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MapDisplay);
            this.Controls.Add(this.OrdersDisplay);
            this.Name = "Form1";
            this.Text = "Form";
            ((System.ComponentModel.ISupportInitialize)(this.MapDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdersDisplay)).EndInit();
            this.ResumeLayout(false);
        }


        #endregion

        private NavalGame.MapDisplay MapDisplay;
        public NavalGame.OrdersDisplay OrdersDisplay;
        public System.Windows.Forms.ToolTip ToolTip;
    }
}

