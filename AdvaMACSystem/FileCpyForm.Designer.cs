namespace AdvaMACSystem
{
    partial class FileCpyForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.label_PValue = new System.Windows.Forms.Label();
            this.label_Caption = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(22, 61);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(417, 20);
            this.progressBar1.Value = 50;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(367, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_PValue
            // 
            this.label_PValue.Location = new System.Drawing.Point(455, 63);
            this.label_PValue.Name = "label_PValue";
            this.label_PValue.Size = new System.Drawing.Size(46, 20);
            this.label_PValue.Text = "0%";
            // 
            // label_Caption
            // 
            this.label_Caption.Location = new System.Drawing.Point(22, 25);
            this.label_Caption.Name = "label_Caption";
            this.label_Caption.Size = new System.Drawing.Size(350, 20);
            this.label_Caption.Text = "文件复制中...";
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FileCpyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(515, 161);
            this.ControlBox = false;
            this.Controls.Add(this.label_Caption);
            this.Controls.Add(this.label_PValue);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FileCpyForm";
            this.Text = "FileCpyForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_PValue;
        private System.Windows.Forms.Label label_Caption;
        private System.Windows.Forms.Timer timer1;

    }
}