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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.OrdersDisplay = new NavalGame.OrdersDisplay();
            this.MapDisplay = new NavalGame.MapDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.MapDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // OrdersDisplay
            // 
            this.OrdersDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrdersDisplay.Location = new System.Drawing.Point(527, 12);
            this.OrdersDisplay.MapDisplay = null;
            this.OrdersDisplay.Name = "OrdersDisplay";
            this.OrdersDisplay.Size = new System.Drawing.Size(261, 428);
            this.OrdersDisplay.TabIndex = 1;
            // 
            // MapDisplay
            // 
            this.MapDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapDisplay.CameraPosition = ((System.Drawing.PointF)(resources.GetObject("MapDisplay.CameraPosition")));
            this.MapDisplay.CameraScale = 30;
            this.MapDisplay.CurrentOrder = null;
            this.MapDisplay.Game = null;
            this.MapDisplay.Location = new System.Drawing.Point(10, 10);
            this.MapDisplay.Name = "MapDisplay";
            this.MapDisplay.OrdersDisplay = null;
            this.MapDisplay.Size = new System.Drawing.Size(511, 430);
            this.MapDisplay.TabIndex = 0;
            this.MapDisplay.TabStop = false;
            this.MapDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MapDisplay.BackColor = System.Drawing.Color.Black;// System.Drawing.SystemColors.ControlDark;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OrdersDisplay);
            this.Controls.Add(this.MapDisplay);
            this.Name = "Form1";
            this.Text = "Naval Game";
            ((System.ComponentModel.ISupportInitialize)(this.MapDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private NavalGame.MapDisplay MapDisplay;
        public System.Windows.Forms.ToolTip ToolTip;
        private OrdersDisplay OrdersDisplay;
    }
}

