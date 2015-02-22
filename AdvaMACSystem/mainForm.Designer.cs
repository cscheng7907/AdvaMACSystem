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
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabelCurError = new ComCtrls.ImageLabel();
            this.label_CurWarning = new ComCtrls.ImageLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_time = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageLabel1 = new ComCtrls.ImageLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.lb_date.Size = new System.Drawing.Size(187, 27);
            this.lb_date.Text = "日期：2015-02-12";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LabelCurError);
            this.panel1.Controls.Add(this.label_CurWarning);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lb_time);
            this.panel1.Controls.Add(this.lb_date);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 94);
            // 
            // LabelCurError
            // 
            this.LabelCurError.BackImg = ((System.Drawing.Image)(resources.GetObject("LabelCurError.BackImg")));
            this.LabelCurError.Checked = false;
            this.LabelCurError.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.LabelCurError.IMGContainer = null;
            this.LabelCurError.Layout = ComCtrls.KTLayout.GlyphTop;
            this.LabelCurError.Location = new System.Drawing.Point(734, 3);
            this.LabelCurError.Name = "LabelCurError";
            this.LabelCurError.Size = new System.Drawing.Size(93, 78);
            this.LabelCurError.TabIndex = 14;
            this.LabelCurError.Text = "33";
            this.LabelCurError.TextX = 65F;
            this.LabelCurError.TextY = 58F;
            this.LabelCurError.TransParent = false;
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
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 94);
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
            this.lb_time.Size = new System.Drawing.Size(187, 30);
            this.lb_time.Text = "时间：12 : 11 : 18";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.imageLabel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 74);
            // 
            // imageLabel1
            // 
            this.imageLabel1.BackImg = ((System.Drawing.Image)(resources.GetObject("imageLabel1.BackImg")));
            this.imageLabel1.Checked = false;
            this.imageLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imageLabel1.IMGContainer = null;
            this.imageLabel1.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel1.Location = new System.Drawing.Point(3, 0);
            this.imageLabel1.Name = "imageLabel1";
            this.imageLabel1.Size = new System.Drawing.Size(85, 38);
            this.imageLabel1.TabIndex = 0;
            this.imageLabel1.Text = "实时监控";
            this.imageLabel1.TextX = -1F;
            this.imageLabel1.TextY = -1F;
            this.imageLabel1.TransParent = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.LightSalmon;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "mainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lb_date;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lb_time;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private ComCtrls.ImageLabel label_CurWarning;
        private ComCtrls.ImageLabel LabelCurError;
        private System.Windows.Forms.Panel panel2;
        private ComCtrls.ImageLabel imageLabel1;
    }
}

