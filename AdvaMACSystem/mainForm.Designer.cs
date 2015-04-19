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
            this.timer1 = new System.Windows.Forms.Timer();
            this.lb_date = new System.Windows.Forms.Label();
            this.panel_Head = new System.Windows.Forms.Panel();
            this.imageLabel_History = new ComCtrls.ImageLabel();
            this.imageLabel_Diagnose = new ComCtrls.ImageLabel();
            this.imageLabel_Setting = new ComCtrls.ImageLabel();
            this.Label_CurError = new ComCtrls.ImageLabel();
            this.label_CurWarning = new ComCtrls.ImageLabel();
            this.panel_LOGO = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_time = new System.Windows.Forms.Label();
            this.panel_Warn = new System.Windows.Forms.Panel();
            this.imageLabel_Warn_History = new ComCtrls.ImageLabel();
            this.imageLabel_Warn_Real = new ComCtrls.ImageLabel();
            this.panel_Para = new System.Windows.Forms.Panel();
            this.imageLabel_Sensor = new ComCtrls.ImageLabel();
            this.imageLabel_Para = new ComCtrls.ImageLabel();
            this.imageLabel_Setup = new ComCtrls.ImageLabel();
            this.panel_Err = new System.Windows.Forms.Panel();
            this.imageLabel_Err_History = new ComCtrls.ImageLabel();
            this.imageLabel_Err_Real = new ComCtrls.ImageLabel();
            this.panel_Head.SuspendLayout();
            this.panel_LOGO.SuspendLayout();
            this.panel_Warn.SuspendLayout();
            this.panel_Para.SuspendLayout();
            this.panel_Err.SuspendLayout();
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
            this.lb_date.Location = new System.Drawing.Point(889, 10);
            this.lb_date.Name = "lb_date";
            this.lb_date.Size = new System.Drawing.Size(187, 31);
            this.lb_date.Text = "2015-02-12";
            // 
            // panel_Head
            // 
            this.panel_Head.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.panel_Head.Controls.Add(this.imageLabel_History);
            this.panel_Head.Controls.Add(this.imageLabel_Diagnose);
            this.panel_Head.Controls.Add(this.imageLabel_Setting);
            this.panel_Head.Controls.Add(this.Label_CurError);
            this.panel_Head.Controls.Add(this.label_CurWarning);
            this.panel_Head.Controls.Add(this.panel_LOGO);
            this.panel_Head.Controls.Add(this.label2);
            this.panel_Head.Controls.Add(this.lb_time);
            this.panel_Head.Controls.Add(this.lb_date);
            this.panel_Head.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Head.Location = new System.Drawing.Point(0, 0);
            this.panel_Head.Name = "panel_Head";
            this.panel_Head.Size = new System.Drawing.Size(1024, 86);
            this.panel_Head.Click += new System.EventHandler(this.panel_Head_Click);
            this.panel_Head.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Head_MouseDown);
            this.panel_Head.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_Head_MouseUp);
            // 
            // imageLabel_History
            // 
            this.imageLabel_History.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.imageLabel_History.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.history_64;
            this.imageLabel_History.Checked = false;
            this.imageLabel_History.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.imageLabel_History.ForeColor = System.Drawing.Color.Red;
            this.imageLabel_History.IMGContainer = null;
            this.imageLabel_History.ImgDisable = null;
            this.imageLabel_History.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_History.Location = new System.Drawing.Point(480, 8);
            this.imageLabel_History.Name = "imageLabel_History";
            this.imageLabel_History.Size = new System.Drawing.Size(75, 70);
            this.imageLabel_History.TabIndex = 21;
            this.imageLabel_History.TextX = 45F;
            this.imageLabel_History.TextY = 0F;
            this.imageLabel_History.TransParent = true;
            this.imageLabel_History.Click += new System.EventHandler(this.Label_History_Click);
            // 
            // imageLabel_Diagnose
            // 
            this.imageLabel_Diagnose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.imageLabel_Diagnose.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.config64;
            this.imageLabel_Diagnose.Checked = false;
            this.imageLabel_Diagnose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Diagnose.ForeColor = System.Drawing.Color.Red;
            this.imageLabel_Diagnose.IMGContainer = null;
            this.imageLabel_Diagnose.ImgDisable = null;
            this.imageLabel_Diagnose.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Diagnose.Location = new System.Drawing.Point(561, 8);
            this.imageLabel_Diagnose.Name = "imageLabel_Diagnose";
            this.imageLabel_Diagnose.Size = new System.Drawing.Size(75, 70);
            this.imageLabel_Diagnose.TabIndex = 20;
            this.imageLabel_Diagnose.TextX = 45F;
            this.imageLabel_Diagnose.TextY = 0F;
            this.imageLabel_Diagnose.TransParent = true;
            this.imageLabel_Diagnose.Click += new System.EventHandler(this.imageLabel_Diagnose_Click);
            // 
            // imageLabel_Setting
            // 
            this.imageLabel_Setting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.imageLabel_Setting.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.settings64;
            this.imageLabel_Setting.Checked = false;
            this.imageLabel_Setting.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Setting.ForeColor = System.Drawing.Color.Red;
            this.imageLabel_Setting.IMGContainer = null;
            this.imageLabel_Setting.ImgDisable = null;
            this.imageLabel_Setting.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Setting.Location = new System.Drawing.Point(644, 8);
            this.imageLabel_Setting.Name = "imageLabel_Setting";
            this.imageLabel_Setting.Size = new System.Drawing.Size(75, 70);
            this.imageLabel_Setting.TabIndex = 19;
            this.imageLabel_Setting.TextX = 45F;
            this.imageLabel_Setting.TextY = 0F;
            this.imageLabel_Setting.TransParent = true;
            this.imageLabel_Setting.Click += new System.EventHandler(this.Label_Setting_Click);
            // 
            // Label_CurError
            // 
            this.Label_CurError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.Label_CurError.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.fix2_gray;
            this.Label_CurError.Checked = false;
            this.Label_CurError.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.Label_CurError.ForeColor = System.Drawing.Color.Red;
            this.Label_CurError.IMGContainer = null;
            this.Label_CurError.ImgDisable = null;
            this.Label_CurError.Layout = ComCtrls.KTLayout.GlyphTop;
            this.Label_CurError.Location = new System.Drawing.Point(802, 8);
            this.Label_CurError.Name = "Label_CurError";
            this.Label_CurError.Size = new System.Drawing.Size(75, 70);
            this.Label_CurError.TabIndex = 14;
            this.Label_CurError.TextX = 45F;
            this.Label_CurError.TextY = 0F;
            this.Label_CurError.TransParent = true;
            this.Label_CurError.Click += new System.EventHandler(this.Label_CurError_Click);
            // 
            // label_CurWarning
            // 
            this.label_CurWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(132)))));
            this.label_CurWarning.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.warning2_gray;
            this.label_CurWarning.Checked = false;
            this.label_CurWarning.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.label_CurWarning.ForeColor = System.Drawing.Color.Red;
            this.label_CurWarning.IMGContainer = null;
            this.label_CurWarning.ImgDisable = null;
            this.label_CurWarning.Layout = ComCtrls.KTLayout.GlyphTop;
            this.label_CurWarning.Location = new System.Drawing.Point(716, 8);
            this.label_CurWarning.Name = "label_CurWarning";
            this.label_CurWarning.Size = new System.Drawing.Size(75, 70);
            this.label_CurWarning.TabIndex = 7;
            this.label_CurWarning.TextX = 45F;
            this.label_CurWarning.TextY = 0F;
            this.label_CurWarning.TransParent = true;
            this.label_CurWarning.Click += new System.EventHandler(this.label_CurWarning_Click);
            // 
            // panel_LOGO
            // 
            this.panel_LOGO.Controls.Add(this.pictureBox1);
            this.panel_LOGO.Controls.Add(this.pictureBox2);
            this.panel_LOGO.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_LOGO.Location = new System.Drawing.Point(0, 0);
            this.panel_LOGO.Name = "panel_LOGO";
            this.panel_LOGO.Size = new System.Drawing.Size(119, 86);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.None;
            this.pictureBox2.Image = global::AdvaMACSystem.AdvaMACSystemRes.LogoBack;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(119, 86);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.None;
            this.pictureBox1.Image = global::AdvaMACSystem.AdvaMACSystemRes.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 86);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(131, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(292, 70);
            this.label2.Text = "HIS0350A\n钢支撑轴力补偿系统";
            // 
            // lb_time
            // 
            this.lb_time.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lb_time.ForeColor = System.Drawing.Color.White;
            this.lb_time.Location = new System.Drawing.Point(889, 49);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(187, 32);
            this.lb_time.Text = "12 : 11 : 18";
            // 
            // panel_Warn
            // 
            this.panel_Warn.BackColor = System.Drawing.Color.Silver;
            this.panel_Warn.Controls.Add(this.imageLabel_Warn_History);
            this.panel_Warn.Controls.Add(this.imageLabel_Warn_Real);
            this.panel_Warn.Location = new System.Drawing.Point(5, 95);
            this.panel_Warn.Name = "panel_Warn";
            this.panel_Warn.Size = new System.Drawing.Size(258, 45);
            this.panel_Warn.Visible = false;
            // 
            // imageLabel_Warn_History
            // 
            this.imageLabel_Warn_History.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.Menu;
            this.imageLabel_Warn_History.Checked = false;
            this.imageLabel_Warn_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageLabel_Warn_History.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Warn_History.IMGContainer = null;
            this.imageLabel_Warn_History.ImgDisable = global::AdvaMACSystem.AdvaMACSystemRes.Menu_disable;
            this.imageLabel_Warn_History.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Warn_History.Location = new System.Drawing.Point(129, 0);
            this.imageLabel_Warn_History.Name = "imageLabel_Warn_History";
            this.imageLabel_Warn_History.Size = new System.Drawing.Size(129, 45);
            this.imageLabel_Warn_History.TabIndex = 1;
            this.imageLabel_Warn_History.Tag = "1";
            this.imageLabel_Warn_History.Text = "历史数据";
            this.imageLabel_Warn_History.TextX = -1F;
            this.imageLabel_Warn_History.TextY = -1F;
            this.imageLabel_Warn_History.TransParent = false;
            this.imageLabel_Warn_History.Click += new System.EventHandler(this.imageLabel_WarnSet_Click);
            // 
            // imageLabel_Warn_Real
            // 
            this.imageLabel_Warn_Real.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.Menu;
            this.imageLabel_Warn_Real.Checked = false;
            this.imageLabel_Warn_Real.Dock = System.Windows.Forms.DockStyle.Left;
            this.imageLabel_Warn_Real.Enabled = false;
            this.imageLabel_Warn_Real.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Warn_Real.IMGContainer = null;
            this.imageLabel_Warn_Real.ImgDisable = global::AdvaMACSystem.AdvaMACSystemRes.Menu_disable;
            this.imageLabel_Warn_Real.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Warn_Real.Location = new System.Drawing.Point(0, 0);
            this.imageLabel_Warn_Real.Name = "imageLabel_Warn_Real";
            this.imageLabel_Warn_Real.Size = new System.Drawing.Size(129, 45);
            this.imageLabel_Warn_Real.TabIndex = 0;
            this.imageLabel_Warn_Real.Tag = "0";
            this.imageLabel_Warn_Real.Text = "实时数据";
            this.imageLabel_Warn_Real.TextX = -1F;
            this.imageLabel_Warn_Real.TextY = -1F;
            this.imageLabel_Warn_Real.TransParent = false;
            this.imageLabel_Warn_Real.Click += new System.EventHandler(this.imageLabel_WarnSet_Click);
            // 
            // panel_Para
            // 
            this.panel_Para.BackColor = System.Drawing.Color.Silver;
            this.panel_Para.Controls.Add(this.imageLabel_Sensor);
            this.panel_Para.Controls.Add(this.imageLabel_Para);
            this.panel_Para.Controls.Add(this.imageLabel_Setup);
            this.panel_Para.Location = new System.Drawing.Point(5, 95);
            this.panel_Para.Name = "panel_Para";
            this.panel_Para.Size = new System.Drawing.Size(387, 45);
            this.panel_Para.Visible = false;
            // 
            // imageLabel_Sensor
            // 
            this.imageLabel_Sensor.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.Menu;
            this.imageLabel_Sensor.Checked = false;
            this.imageLabel_Sensor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageLabel_Sensor.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Sensor.IMGContainer = null;
            this.imageLabel_Sensor.ImgDisable = global::AdvaMACSystem.AdvaMACSystemRes.Menu_disable;
            this.imageLabel_Sensor.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Sensor.Location = new System.Drawing.Point(258, 0);
            this.imageLabel_Sensor.Name = "imageLabel_Sensor";
            this.imageLabel_Sensor.Size = new System.Drawing.Size(129, 45);
            this.imageLabel_Sensor.TabIndex = 2;
            this.imageLabel_Sensor.Tag = "2";
            this.imageLabel_Sensor.Text = "传感器标定";
            this.imageLabel_Sensor.TextX = -1F;
            this.imageLabel_Sensor.TextY = -1F;
            this.imageLabel_Sensor.TransParent = false;
            this.imageLabel_Sensor.Click += new System.EventHandler(this.imageLabel_ParaSet_Click);
            // 
            // imageLabel_Para
            // 
            this.imageLabel_Para.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.Menu;
            this.imageLabel_Para.Checked = false;
            this.imageLabel_Para.Dock = System.Windows.Forms.DockStyle.Left;
            this.imageLabel_Para.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Para.IMGContainer = null;
            this.imageLabel_Para.ImgDisable = global::AdvaMACSystem.AdvaMACSystemRes.Menu_disable;
            this.imageLabel_Para.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Para.Location = new System.Drawing.Point(129, 0);
            this.imageLabel_Para.Name = "imageLabel_Para";
            this.imageLabel_Para.Size = new System.Drawing.Size(129, 45);
            this.imageLabel_Para.TabIndex = 1;
            this.imageLabel_Para.Tag = "1";
            this.imageLabel_Para.Text = "参数设定";
            this.imageLabel_Para.TextX = -1F;
            this.imageLabel_Para.TextY = -1F;
            this.imageLabel_Para.TransParent = false;
            this.imageLabel_Para.Click += new System.EventHandler(this.imageLabel_ParaSet_Click);
            // 
            // imageLabel_Setup
            // 
            this.imageLabel_Setup.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.Menu;
            this.imageLabel_Setup.Checked = false;
            this.imageLabel_Setup.Dock = System.Windows.Forms.DockStyle.Left;
            this.imageLabel_Setup.Enabled = false;
            this.imageLabel_Setup.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Setup.IMGContainer = null;
            this.imageLabel_Setup.ImgDisable = global::AdvaMACSystem.AdvaMACSystemRes.Menu_disable;
            this.imageLabel_Setup.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Setup.Location = new System.Drawing.Point(0, 0);
            this.imageLabel_Setup.Name = "imageLabel_Setup";
            this.imageLabel_Setup.Size = new System.Drawing.Size(129, 45);
            this.imageLabel_Setup.TabIndex = 0;
            this.imageLabel_Setup.Tag = "0";
            this.imageLabel_Setup.Text = "安装设定";
            this.imageLabel_Setup.TextX = -1F;
            this.imageLabel_Setup.TextY = -1F;
            this.imageLabel_Setup.TransParent = false;
            this.imageLabel_Setup.Click += new System.EventHandler(this.imageLabel_ParaSet_Click);
            // 
            // panel_Err
            // 
            this.panel_Err.BackColor = System.Drawing.Color.Silver;
            this.panel_Err.Controls.Add(this.imageLabel_Err_History);
            this.panel_Err.Controls.Add(this.imageLabel_Err_Real);
            this.panel_Err.Location = new System.Drawing.Point(5, 95);
            this.panel_Err.Name = "panel_Err";
            this.panel_Err.Size = new System.Drawing.Size(258, 45);
            this.panel_Err.Visible = false;
            // 
            // imageLabel_Err_History
            // 
            this.imageLabel_Err_History.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.Menu;
            this.imageLabel_Err_History.Checked = false;
            this.imageLabel_Err_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageLabel_Err_History.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Err_History.IMGContainer = null;
            this.imageLabel_Err_History.ImgDisable = global::AdvaMACSystem.AdvaMACSystemRes.Menu_disable;
            this.imageLabel_Err_History.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Err_History.Location = new System.Drawing.Point(129, 0);
            this.imageLabel_Err_History.Name = "imageLabel_Err_History";
            this.imageLabel_Err_History.Size = new System.Drawing.Size(129, 45);
            this.imageLabel_Err_History.TabIndex = 1;
            this.imageLabel_Err_History.Tag = "1";
            this.imageLabel_Err_History.Text = "历史数据";
            this.imageLabel_Err_History.TextX = -1F;
            this.imageLabel_Err_History.TextY = -1F;
            this.imageLabel_Err_History.TransParent = false;
            this.imageLabel_Err_History.Click += new System.EventHandler(this.imageLabel_ErrSet_Click);
            // 
            // imageLabel_Err_Real
            // 
            this.imageLabel_Err_Real.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.Menu;
            this.imageLabel_Err_Real.Checked = false;
            this.imageLabel_Err_Real.Dock = System.Windows.Forms.DockStyle.Left;
            this.imageLabel_Err_Real.Enabled = false;
            this.imageLabel_Err_Real.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Err_Real.IMGContainer = null;
            this.imageLabel_Err_Real.ImgDisable = global::AdvaMACSystem.AdvaMACSystemRes.Menu_disable;
            this.imageLabel_Err_Real.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Err_Real.Location = new System.Drawing.Point(0, 0);
            this.imageLabel_Err_Real.Name = "imageLabel_Err_Real";
            this.imageLabel_Err_Real.Size = new System.Drawing.Size(129, 45);
            this.imageLabel_Err_Real.TabIndex = 0;
            this.imageLabel_Err_Real.Tag = "0";
            this.imageLabel_Err_Real.Text = "实时数据";
            this.imageLabel_Err_Real.TextX = -1F;
            this.imageLabel_Err_Real.TextY = -1F;
            this.imageLabel_Err_Real.TransParent = false;
            this.imageLabel_Err_Real.Click += new System.EventHandler(this.imageLabel_ErrSet_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.panel_Err);
            this.Controls.Add(this.panel_Para);
            this.Controls.Add(this.panel_Warn);
            this.Controls.Add(this.panel_Head);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "mainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.panel_Head.ResumeLayout(false);
            this.panel_LOGO.ResumeLayout(false);
            this.panel_Warn.ResumeLayout(false);
            this.panel_Para.ResumeLayout(false);
            this.panel_Err.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lb_date;
        private System.Windows.Forms.Panel panel_Head;
        private System.Windows.Forms.Panel panel_LOGO;
        private System.Windows.Forms.Label lb_time;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private ComCtrls.ImageLabel label_CurWarning;
        private ComCtrls.ImageLabel Label_CurError;
        private ComCtrls.ImageLabel imageLabel_Diagnose;
        private ComCtrls.ImageLabel imageLabel_Setting;
        private ComCtrls.ImageLabel imageLabel_History;
        private System.Windows.Forms.Panel panel_Warn;
        private ComCtrls.ImageLabel imageLabel_Warn_History;
        private ComCtrls.ImageLabel imageLabel_Warn_Real;
        private System.Windows.Forms.Panel panel_Para;
        private ComCtrls.ImageLabel imageLabel_Para;
        private ComCtrls.ImageLabel imageLabel_Setup;
        private ComCtrls.ImageLabel imageLabel_Sensor;
        private System.Windows.Forms.Panel panel_Err;
        private ComCtrls.ImageLabel imageLabel_Err_History;
        private ComCtrls.ImageLabel imageLabel_Err_Real;
    }
}

