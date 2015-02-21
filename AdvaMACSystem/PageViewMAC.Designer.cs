namespace AdvaMACSystem
{
    partial class PageViewMAC
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
            this.timer_RefreshMac = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // timer_RefreshMac
            // 
            this.timer_RefreshMac.Interval = 500;
            this.timer_RefreshMac.Tick += new System.EventHandler(this.timer_RefreshMac_Tick);
            // 
            // PageViewMAC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Name = "PageViewMAC";
            this.Size = new System.Drawing.Size(1024, 600);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_RefreshMac;
    }
}
