namespace AdvaMACSysUpdater
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
            this.button1 = new System.Windows.Forms.Button();
            this.lb_State = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer();
            this.timer2 = new System.Windows.Forms.Timer();
            this.lb_Process = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 25F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(377, 476);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 73);
            this.button1.TabIndex = 0;
            this.button1.Text = "升级";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb_State
            // 
            this.lb_State.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold);
            this.lb_State.Location = new System.Drawing.Point(41, 292);
            this.lb_State.Name = "lb_State";
            this.lb_State.Size = new System.Drawing.Size(930, 97);
            this.lb_State.Text = "USB设备未就绪";
            this.lb_State.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(267, 392);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(436, 20);
            this.progressBar1.Value = 50;
            this.progressBar1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 300;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // lb_Process
            // 
            this.lb_Process.Location = new System.Drawing.Point(737, 392);
            this.lb_Process.Name = "lb_Process";
            this.lb_Process.Size = new System.Drawing.Size(71, 20);
            this.lb_Process.Text = "0%";
            this.lb_Process.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb_Process.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1022, 743);
            this.ControlBox = false;
            this.Controls.Add(this.lb_Process);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lb_State);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lb_State;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lb_Process;
    }
}

