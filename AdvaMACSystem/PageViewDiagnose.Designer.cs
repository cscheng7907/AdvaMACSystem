namespace AdvaMACSystem
{
    partial class PageViewDiagnose
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
            this.timer_Refresh = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // timer_Refresh
            // 
            this.timer_Refresh.Interval = 500;
            this.timer_Refresh.Tick += new System.EventHandler(this.timer_Refresh_Tick);
            // 
            // PageViewDiagnose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Name = "PageViewDiagnose";
            this.Size = new System.Drawing.Size(1024, 674);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_Refresh;
    }
}
