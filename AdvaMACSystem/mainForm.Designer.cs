namespace AdvaMACSystem
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.timer1 = new System.Windows.Forms.Timer();
            this.lb_date = new System.Windows.Forms.Label();
            this.panel_Head = new System.Windows.Forms.Panel();
            this.Label_CurError = new ComCtrls.ImageLabel();
            this.label_CurWarning = new ComCtrls.ImageLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_time = new System.Windows.Forms.Label();
            this.imageLabel_Diagnose = new ComCtrls.ImageLabel();
            this.imageLabel_Setting = new ComCtrls.ImageLabel();
            this.imageLabel_History = new ComCtrls.ImageLabel();
            this.panel_Head.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lb_date
            // 
            this.lb_date.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lb_date.ForeColor = System.Drawing.Color.White;
            this.lb_date.Location = new System.Drawing.Point(834, 10);
            this.lb_date.Name = "lb_date";
            this.lb_date.Size = new System.Drawing.Size(187, 31);
            this.lb_date.Text = "日期：2015-02-12";
            // 
            // panel_Head
            // 
            this.panel_Head.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.panel_Head.Controls.Add(this.imageLabel_History);
            this.panel_Head.Controls.Add(this.imageLabel_Diagnose);
            this.panel_Head.Controls.Add(this.imageLabel_Setting);
            this.panel_Head.Controls.Add(this.Label_CurError);
            this.panel_Head.Controls.Add(this.label_CurWarning);
            this.panel_Head.Controls.Add(this.pictureBox1);
            this.panel_Head.Controls.Add(this.label2);
            this.panel_Head.Controls.Add(this.lb_time);
            this.panel_Head.Controls.Add(this.lb_date);
            this.panel_Head.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Head.Location = new System.Drawing.Point(0, 0);
            this.panel_Head.Name = "panel_Head";
            this.panel_Head.Size = new System.Drawing.Size(1024, 85);
            this.panel_Head.Click += new System.EventHandler(this.panel_Head_Click);
            this.panel_Head.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Head_MouseDown);
            this.panel_Head.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_Head_MouseUp);
            // 
            // Label_CurError
            // 
            this.Label_CurError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.Label_CurError.BackImg = ((System.Drawing.Image)(resources.GetObject("Label_CurError.BackImg")));
            this.Label_CurError.Checked = false;
            this.Label_CurError.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.Label_CurError.ForeColor = System.Drawing.Color.Red;
            this.Label_CurError.IMGContainer = null;
            this.Label_CurError.Layout = ComCtrls.KTLayout.GlyphTop;
            this.Label_CurError.Location = new System.Drawing.Point(757, 8);
            this.Label_CurError.Name = "Label_CurError";
            this.Label_CurError.Size = new System.Drawing.Size(75, 70);
            this.Label_CurError.TabIndex = 14;
            this.Label_CurError.Text = "45";
            this.Label_CurError.TextX = 45F;
            this.Label_CurError.TextY = 0F;
            this.Label_CurError.TransParent = true;
            this.Label_CurError.Click += new System.EventHandler(this.Label_CurError_Click);
            // 
            // label_CurWarning
            // 
            this.label_CurWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.label_CurWarning.BackImg = ((System.Drawing.Image)(resources.GetObject("label_CurWarning.BackImg")));
            this.label_CurWarning.Checked = false;
            this.label_CurWarning.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.label_CurWarning.ForeColor = System.Drawing.Color.Red;
            this.label_CurWarning.IMGContainer = null;
            this.label_CurWarning.Layout = ComCtrls.KTLayout.GlyphTop;
            this.label_CurWarning.Location = new System.Drawing.Point(671, 8);
            this.label_CurWarning.Name = "label_CurWarning";
            this.label_CurWarning.Size = new System.Drawing.Size(75, 70);
            this.label_CurWarning.TabIndex = 7;
            this.label_CurWarning.Text = "60";
            this.label_CurWarning.TextX = 45F;
            this.label_CurWarning.TextY = 0F;
            this.label_CurWarning.TransParent = true;
            this.label_CurWarning.Click += new System.EventHandler(this.label_CurWarning_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(85, 85);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(92, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(347, 37);
            this.label2.Text = "HIS0350A钢支撑轴力补偿系统";
            // 
            // lb_time
            // 
            this.lb_time.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lb_time.ForeColor = System.Drawing.Color.White;
            this.lb_time.Location = new System.Drawing.Point(834, 49);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(187, 32);
            this.lb_time.Text = "时间：12 : 11 : 18";
            // 
            // imageLabel_debug
            // 
            this.imageLabel_Diagnose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.imageLabel_Diagnose.BackImg = AdvaMACSystemRes.config64;
            this.imageLabel_Diagnose.Checked = false;
            this.imageLabel_Diagnose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Diagnose.ForeColor = System.Drawing.Color.Red;
            this.imageLabel_Diagnose.IMGContainer = null;
            this.imageLabel_Diagnose.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Diagnose.Location = new System.Drawing.Point(516, 8);
            this.imageLabel_Diagnose.Name = "imageLabel_debug";
            this.imageLabel_Diagnose.Size = new System.Drawing.Size(75, 70);
            this.imageLabel_Diagnose.TabIndex = 20;
            this.imageLabel_Diagnose.Text = "";
            this.imageLabel_Diagnose.TextX = 45F;
            this.imageLabel_Diagnose.TextY = 0F;
            this.imageLabel_Diagnose.TransParent = true;
            this.imageLabel_Diagnose.Click += new System.EventHandler(this.imageLabel_Diagnose_Click);
            // 
            // imageLabel_Setting
            // 
            this.imageLabel_Setting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.imageLabel_Setting.BackImg = AdvaMACSystemRes.settings64;
            this.imageLabel_Setting.Checked = false;
            this.imageLabel_Setting.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Setting.ForeColor = System.Drawing.Color.Red;
            this.imageLabel_Setting.IMGContainer = null;
            this.imageLabel_Setting.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Setting.Location = new System.Drawing.Point(599, 8);
            this.imageLabel_Setting.Name = "imageLabel_Setting";
            this.imageLabel_Setting.Size = new System.Drawing.Size(75, 70);
            this.imageLabel_Setting.TabIndex = 19;
            this.imageLabel_Setting.Text = "";
            this.imageLabel_Setting.TextX = 45F;
            this.imageLabel_Setting.TextY = 0F;
            this.imageLabel_Setting.TransParent = true;
            this.imageLabel_Setting.Click += new System.EventHandler(this.Label_Setting_Click);
            // 
            // imageLabel_History
            // 
            this.imageLabel_History.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.imageLabel_History.BackImg = AdvaMACSystemRes.history_64;
            this.imageLabel_History.Checked = false;
            this.imageLabel_History.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.imageLabel_History.ForeColor = System.Drawing.Color.Red;
            this.imageLabel_History.IMGContainer = null;
            this.imageLabel_History.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_History.Location = new System.Drawing.Point(435, 8);
            this.imageLabel_History.Name = "imageLabel_History";
            this.imageLabel_History.Size = new System.Drawing.Size(75, 70);
            this.imageLabel_History.TabIndex = 21;
            this.imageLabel_History.Text = "";
            this.imageLabel_History.TextX = 45F;
            this.imageLabel_History.TextY = 0F;
            this.imageLabel_History.TransParent = true;
            this.imageLabel_History.Click += new System.EventHandler(this.Label_History_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.panel_Head);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "mainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.panel_Head.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lb_date;
        private System.Windows.Forms.Panel panel_Head;
        private System.Windows.Forms.Label lb_time;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private ComCtrls.ImageLabel label_CurWarning;
        private ComCtrls.ImageLabel Label_CurError;
        private ComCtrls.ImageLabel imageLabel_Diagnose;
        private ComCtrls.ImageLabel imageLabel_Setting;
        private ComCtrls.ImageLabel imageLabel_History;
    }
}

