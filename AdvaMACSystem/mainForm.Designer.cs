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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_time = new System.Windows.Forms.Label();
            this.panel_Tabs = new System.Windows.Forms.Panel();
            this.imageLabel_Diagnose = new ComCtrls.ImageLabel();
            this.imageLabel_Senser = new ComCtrls.ImageLabel();
            this.imageLabel_Para = new ComCtrls.ImageLabel();
            this.imageLabel_Setup = new ComCtrls.ImageLabel();
            this.imageLabel_HisError = new ComCtrls.ImageLabel();
            this.imageLabel_HisWarn = new ComCtrls.ImageLabel();
            this.imageLabel_History = new ComCtrls.ImageLabel();
            this.imageLabel_RealError = new ComCtrls.ImageLabel();
            this.imageLabel_RealWarn = new ComCtrls.ImageLabel();
            this.imageLabel_MAC = new ComCtrls.ImageLabel();
            this.Label_CurError = new ComCtrls.ImageLabel();
            this.label_CurWarning = new ComCtrls.ImageLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_Head.SuspendLayout();
            this.panel_Tabs.SuspendLayout();
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
            this.lb_date.ForeColor = System.Drawing.Color.Black;
            this.lb_date.Location = new System.Drawing.Point(834, 15);
            this.lb_date.Name = "lb_date";
            this.lb_date.Size = new System.Drawing.Size(187, 31);
            this.lb_date.Text = "日期：2015-02-12";
            // 
            // panel_Head
            // 
            this.panel_Head.Controls.Add(this.Label_CurError);
            this.panel_Head.Controls.Add(this.label_CurWarning);
            this.panel_Head.Controls.Add(this.label4);
            this.panel_Head.Controls.Add(this.label3);
            this.panel_Head.Controls.Add(this.pictureBox1);
            this.panel_Head.Controls.Add(this.label2);
            this.panel_Head.Controls.Add(this.lb_time);
            this.panel_Head.Controls.Add(this.lb_date);
            this.panel_Head.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Head.Location = new System.Drawing.Point(0, 0);
            this.panel_Head.Name = "panel_Head";
            this.panel_Head.Size = new System.Drawing.Size(1024, 94);
            this.panel_Head.Click += new System.EventHandler(this.panel_Head_Click);
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(268, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "工程编号：XXXX";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(268, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 20);
            this.label3.Text = "工程名称：XXXXXXXX";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(112, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(337, 28);
            this.label2.Text = "HIS0350A钢支撑轴力补偿系统";
            // 
            // lb_time
            // 
            this.lb_time.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lb_time.ForeColor = System.Drawing.Color.Black;
            this.lb_time.Location = new System.Drawing.Point(834, 54);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(187, 40);
            this.lb_time.Text = "时间：12 : 11 : 18";
            // 
            // panel_Tabs
            // 
            this.panel_Tabs.Controls.Add(this.imageLabel_Diagnose);
            this.panel_Tabs.Controls.Add(this.imageLabel_Senser);
            this.panel_Tabs.Controls.Add(this.imageLabel_Para);
            this.panel_Tabs.Controls.Add(this.imageLabel_Setup);
            this.panel_Tabs.Controls.Add(this.imageLabel_HisError);
            this.panel_Tabs.Controls.Add(this.imageLabel_HisWarn);
            this.panel_Tabs.Controls.Add(this.imageLabel_History);
            this.panel_Tabs.Controls.Add(this.imageLabel_RealError);
            this.panel_Tabs.Controls.Add(this.imageLabel_RealWarn);
            this.panel_Tabs.Controls.Add(this.imageLabel_MAC);
            this.panel_Tabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Tabs.Location = new System.Drawing.Point(0, 94);
            this.panel_Tabs.Name = "panel_Tabs";
            this.panel_Tabs.Size = new System.Drawing.Size(1024, 74);
            // 
            // imageLabel_Diagnose
            // 
            this.imageLabel_Diagnose.BackImg = ((System.Drawing.Image)(resources.GetObject("imageLabel_Diagnose.BackImg")));
            this.imageLabel_Diagnose.Checked = false;
            this.imageLabel_Diagnose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imageLabel_Diagnose.IMGContainer = null;
            this.imageLabel_Diagnose.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Diagnose.Location = new System.Drawing.Point(549, 17);
            this.imageLabel_Diagnose.Name = "imageLabel_Diagnose";
            this.imageLabel_Diagnose.Size = new System.Drawing.Size(85, 38);
            this.imageLabel_Diagnose.TabIndex = 9;
            this.imageLabel_Diagnose.Text = "I/O诊断";
            this.imageLabel_Diagnose.TextX = -1F;
            this.imageLabel_Diagnose.TextY = -1F;
            this.imageLabel_Diagnose.TransParent = false;
            this.imageLabel_Diagnose.Click += new System.EventHandler(this.imageLabel_Diagnose_Click);
            // 
            // imageLabel_Senser
            // 
            this.imageLabel_Senser.BackImg = ((System.Drawing.Image)(resources.GetObject("imageLabel_Senser.BackImg")));
            this.imageLabel_Senser.Checked = false;
            this.imageLabel_Senser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imageLabel_Senser.IMGContainer = null;
            this.imageLabel_Senser.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Senser.Location = new System.Drawing.Point(828, 17);
            this.imageLabel_Senser.Name = "imageLabel_Senser";
            this.imageLabel_Senser.Size = new System.Drawing.Size(85, 38);
            this.imageLabel_Senser.TabIndex = 8;
            this.imageLabel_Senser.Text = "传感器标定";
            this.imageLabel_Senser.TextX = -1F;
            this.imageLabel_Senser.TextY = -1F;
            this.imageLabel_Senser.TransParent = false;
            this.imageLabel_Senser.Click += new System.EventHandler(this.imageLabel_Senser_Click);
            // 
            // imageLabel_Para
            // 
            this.imageLabel_Para.BackImg = ((System.Drawing.Image)(resources.GetObject("imageLabel_Para.BackImg")));
            this.imageLabel_Para.Checked = false;
            this.imageLabel_Para.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imageLabel_Para.IMGContainer = null;
            this.imageLabel_Para.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Para.Location = new System.Drawing.Point(737, 17);
            this.imageLabel_Para.Name = "imageLabel_Para";
            this.imageLabel_Para.Size = new System.Drawing.Size(85, 38);
            this.imageLabel_Para.TabIndex = 7;
            this.imageLabel_Para.Text = "参数设定";
            this.imageLabel_Para.TextX = -1F;
            this.imageLabel_Para.TextY = -1F;
            this.imageLabel_Para.TransParent = false;
            this.imageLabel_Para.Click += new System.EventHandler(this.imageLabel_Para_Click);
            // 
            // imageLabel_Setup
            // 
            this.imageLabel_Setup.BackImg = ((System.Drawing.Image)(resources.GetObject("imageLabel_Setup.BackImg")));
            this.imageLabel_Setup.Checked = false;
            this.imageLabel_Setup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imageLabel_Setup.IMGContainer = null;
            this.imageLabel_Setup.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Setup.Location = new System.Drawing.Point(643, 17);
            this.imageLabel_Setup.Name = "imageLabel_Setup";
            this.imageLabel_Setup.Size = new System.Drawing.Size(85, 38);
            this.imageLabel_Setup.TabIndex = 6;
            this.imageLabel_Setup.Text = "安装设定";
            this.imageLabel_Setup.TextX = -1F;
            this.imageLabel_Setup.TextY = -1F;
            this.imageLabel_Setup.TransParent = false;
            this.imageLabel_Setup.Click += new System.EventHandler(this.imageLabel_Setup_Click);
            // 
            // imageLabel_HisError
            // 
            this.imageLabel_HisError.BackImg = ((System.Drawing.Image)(resources.GetObject("imageLabel_HisError.BackImg")));
            this.imageLabel_HisError.Checked = false;
            this.imageLabel_HisError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imageLabel_HisError.IMGContainer = null;
            this.imageLabel_HisError.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_HisError.Location = new System.Drawing.Point(458, 17);
            this.imageLabel_HisError.Name = "imageLabel_HisError";
            this.imageLabel_HisError.Size = new System.Drawing.Size(85, 38);
            this.imageLabel_HisError.TabIndex = 5;
            this.imageLabel_HisError.Text = "历史故障";
            this.imageLabel_HisError.TextX = -1F;
            this.imageLabel_HisError.TextY = -1F;
            this.imageLabel_HisError.TransParent = false;
            this.imageLabel_HisError.Click += new System.EventHandler(this.imageLabel_HisError_Click);
            // 
            // imageLabel_HisWarn
            // 
            this.imageLabel_HisWarn.BackImg = ((System.Drawing.Image)(resources.GetObject("imageLabel_HisWarn.BackImg")));
            this.imageLabel_HisWarn.Checked = false;
            this.imageLabel_HisWarn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imageLabel_HisWarn.IMGContainer = null;
            this.imageLabel_HisWarn.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_HisWarn.Location = new System.Drawing.Point(367, 17);
            this.imageLabel_HisWarn.Name = "imageLabel_HisWarn";
            this.imageLabel_HisWarn.Size = new System.Drawing.Size(85, 38);
            this.imageLabel_HisWarn.TabIndex = 4;
            this.imageLabel_HisWarn.Text = "历史报警";
            this.imageLabel_HisWarn.TextX = -1F;
            this.imageLabel_HisWarn.TextY = -1F;
            this.imageLabel_HisWarn.TransParent = false;
            this.imageLabel_HisWarn.Click += new System.EventHandler(this.imageLabel_HisWarn_Click);
            // 
            // imageLabel_History
            // 
            this.imageLabel_History.BackImg = ((System.Drawing.Image)(resources.GetObject("imageLabel_History.BackImg")));
            this.imageLabel_History.Checked = false;
            this.imageLabel_History.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imageLabel_History.IMGContainer = null;
            this.imageLabel_History.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_History.Location = new System.Drawing.Point(276, 17);
            this.imageLabel_History.Name = "imageLabel_History";
            this.imageLabel_History.Size = new System.Drawing.Size(85, 38);
            this.imageLabel_History.TabIndex = 3;
            this.imageLabel_History.Text = "历史监控";
            this.imageLabel_History.TextX = -1F;
            this.imageLabel_History.TextY = -1F;
            this.imageLabel_History.TransParent = false;
            this.imageLabel_History.Click += new System.EventHandler(this.imageLabel_History_Click);
            // 
            // imageLabel_RealError
            // 
            this.imageLabel_RealError.BackImg = ((System.Drawing.Image)(resources.GetObject("imageLabel_RealError.BackImg")));
            this.imageLabel_RealError.Checked = false;
            this.imageLabel_RealError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imageLabel_RealError.IMGContainer = null;
            this.imageLabel_RealError.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_RealError.Location = new System.Drawing.Point(185, 17);
            this.imageLabel_RealError.Name = "imageLabel_RealError";
            this.imageLabel_RealError.Size = new System.Drawing.Size(85, 38);
            this.imageLabel_RealError.TabIndex = 2;
            this.imageLabel_RealError.Text = "实时故障";
            this.imageLabel_RealError.TextX = -1F;
            this.imageLabel_RealError.TextY = -1F;
            this.imageLabel_RealError.TransParent = false;
            this.imageLabel_RealError.Click += new System.EventHandler(this.imageLabel_RealError_Click);
            // 
            // imageLabel_RealWarn
            // 
            this.imageLabel_RealWarn.BackImg = ((System.Drawing.Image)(resources.GetObject("imageLabel_RealWarn.BackImg")));
            this.imageLabel_RealWarn.Checked = false;
            this.imageLabel_RealWarn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imageLabel_RealWarn.IMGContainer = null;
            this.imageLabel_RealWarn.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_RealWarn.Location = new System.Drawing.Point(94, 17);
            this.imageLabel_RealWarn.Name = "imageLabel_RealWarn";
            this.imageLabel_RealWarn.Size = new System.Drawing.Size(85, 38);
            this.imageLabel_RealWarn.TabIndex = 1;
            this.imageLabel_RealWarn.Text = "实时报警";
            this.imageLabel_RealWarn.TextX = -1F;
            this.imageLabel_RealWarn.TextY = -1F;
            this.imageLabel_RealWarn.TransParent = false;
            this.imageLabel_RealWarn.Click += new System.EventHandler(this.imageLabel_RealWarn_Click);
            // 
            // imageLabel_MAC
            // 
            this.imageLabel_MAC.BackImg = ((System.Drawing.Image)(resources.GetObject("imageLabel_MAC.BackImg")));
            this.imageLabel_MAC.Checked = false;
            this.imageLabel_MAC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imageLabel_MAC.IMGContainer = null;
            this.imageLabel_MAC.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_MAC.Location = new System.Drawing.Point(3, 17);
            this.imageLabel_MAC.Name = "imageLabel_MAC";
            this.imageLabel_MAC.Size = new System.Drawing.Size(85, 38);
            this.imageLabel_MAC.TabIndex = 0;
            this.imageLabel_MAC.Text = "实时监控";
            this.imageLabel_MAC.TextX = -1F;
            this.imageLabel_MAC.TextY = -1F;
            this.imageLabel_MAC.TransParent = false;
            this.imageLabel_MAC.Click += new System.EventHandler(this.imageLabel_MAC_Click);
            // 
            // Label_CurError
            // 
            this.Label_CurError.BackImg = ((System.Drawing.Image)(resources.GetObject("Label_CurError.BackImg")));
            this.Label_CurError.Checked = false;
            this.Label_CurError.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.Label_CurError.IMGContainer = null;
            this.Label_CurError.Layout = ComCtrls.KTLayout.GlyphTop;
            this.Label_CurError.Location = new System.Drawing.Point(734, 3);
            this.Label_CurError.Name = "Label_CurError";
            this.Label_CurError.Size = new System.Drawing.Size(93, 78);
            this.Label_CurError.TabIndex = 14;
            this.Label_CurError.Text = "33";
            this.Label_CurError.TextX = 65F;
            this.Label_CurError.TextY = 58F;
            this.Label_CurError.TransParent = false;
            this.Label_CurError.Click += new System.EventHandler(this.Label_CurError_Click);
            // 
            // label_CurWarning
            // 
            this.label_CurWarning.BackImg = ((System.Drawing.Image)(resources.GetObject("label_CurWarning.BackImg")));
            this.label_CurWarning.Checked = false;
            this.label_CurWarning.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.label_CurWarning.IMGContainer = null;
            this.label_CurWarning.Layout = ComCtrls.KTLayout.GlyphTop;
            this.label_CurWarning.Location = new System.Drawing.Point(635, 3);
            this.label_CurWarning.Name = "label_CurWarning";
            this.label_CurWarning.Size = new System.Drawing.Size(93, 78);
            this.label_CurWarning.TabIndex = 7;
            this.label_CurWarning.Text = "60";
            this.label_CurWarning.TextX = 65F;
            this.label_CurWarning.TextY = 58F;
            this.label_CurWarning.TransParent = false;
            this.label_CurWarning.Click += new System.EventHandler(this.label_CurWarning_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 94);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.panel_Tabs);
            this.Controls.Add(this.panel_Head);
            this.ForeColor = System.Drawing.Color.LightSalmon;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "mainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.panel_Head.ResumeLayout(false);
            this.panel_Tabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lb_date;
        private System.Windows.Forms.Panel panel_Head;
        private System.Windows.Forms.Label lb_time;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private ComCtrls.ImageLabel label_CurWarning;
        private ComCtrls.ImageLabel Label_CurError;
        private System.Windows.Forms.Panel panel_Tabs;
        private ComCtrls.ImageLabel imageLabel_MAC;
        private ComCtrls.ImageLabel imageLabel_History;
        private ComCtrls.ImageLabel imageLabel_RealError;
        private ComCtrls.ImageLabel imageLabel_RealWarn;
        private ComCtrls.ImageLabel imageLabel_HisWarn;
        private ComCtrls.ImageLabel imageLabel_Senser;
        private ComCtrls.ImageLabel imageLabel_Para;
        private ComCtrls.ImageLabel imageLabel_Setup;
        private ComCtrls.ImageLabel imageLabel_HisError;
        private ComCtrls.ImageLabel imageLabel_Diagnose;
    }
}

