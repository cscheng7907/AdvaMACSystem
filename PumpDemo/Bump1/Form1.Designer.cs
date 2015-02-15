namespace Bump1
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
            this.currentStatusBlock1 = new Bump1.CurrentStatusBlock();
            this.timer1 = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // currentStatusBlock1
            // 
            this.currentStatusBlock1.Location = new System.Drawing.Point(148, 116);
            this.currentStatusBlock1.Name = "currentStatusBlock1";
            this.currentStatusBlock1.Size = new System.Drawing.Size(300, 110);
            this.currentStatusBlock1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(598, 375);
            this.Controls.Add(this.currentStatusBlock1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private CurrentStatusBlock currentStatusBlock1;
        private System.Windows.Forms.Timer timer1;
    }
}

