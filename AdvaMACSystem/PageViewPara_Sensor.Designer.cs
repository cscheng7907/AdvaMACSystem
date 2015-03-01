namespace AdvaMACSystem
{
    partial class PageViewPara_Sensor
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageLabel_title = new ComCtrls.ImageLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_id = new System.Windows.Forms.ComboBox();
            this.imageLabel_idbk = new ComCtrls.ImageLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_subid = new System.Windows.Forms.ComboBox();
            this.imageLabel_subidbk = new ComCtrls.ImageLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageLabel4 = new ComCtrls.ImageLabel();
            this.imageLabel5 = new ComCtrls.ImageLabel();
            this.imageLabel3 = new ComCtrls.ImageLabel();
            this.imageLabel2 = new ComCtrls.ImageLabel();
            this.imageLabel1 = new ComCtrls.ImageLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imageButton_OK_Low = new ComCtrls.ImageButton(this.components);
            this.imageButton_OK_High = new ComCtrls.ImageButton(this.components);
            this.imageButton_back = new ComCtrls.ImageButton(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageLabel_title
            // 
            this.imageLabel_title.BackColor = System.Drawing.Color.Silver;
            this.imageLabel_title.Checked = false;
            this.imageLabel_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.imageLabel_title.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.imageLabel_title.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imageLabel_title.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_title.Location = new System.Drawing.Point(0, 0);
            this.imageLabel_title.Name = "imageLabel_title";
            this.imageLabel_title.Size = new System.Drawing.Size(1024, 85);
            this.imageLabel_title.TabIndex = 0;
            this.imageLabel_title.Text = "传感器标定";
            this.imageLabel_title.TextX = -1F;
            this.imageLabel_title.TextY = -1F;
            this.imageLabel_title.TransParent = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.comboBox_id);
            this.panel1.Controls.Add(this.imageLabel_idbk);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBox_subid);
            this.panel1.Controls.Add(this.imageLabel_subidbk);
            this.panel1.Location = new System.Drawing.Point(7, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1010, 88);
            // 
            // comboBox_id
            // 
            this.comboBox_id.Items.Add("#1 泵站");
            this.comboBox_id.Items.Add("#2 泵站");
            this.comboBox_id.Items.Add("#3 泵站");
            this.comboBox_id.Items.Add("#4 泵站");
            this.comboBox_id.Location = new System.Drawing.Point(234, 30);
            this.comboBox_id.Name = "comboBox_id";
            this.comboBox_id.Size = new System.Drawing.Size(130, 23);
            this.comboBox_id.TabIndex = 0;
            this.comboBox_id.TabStop = false;
            this.comboBox_id.SelectedIndexChanged += new System.EventHandler(this.comboBox_id_SelectedIndexChanged);
            // 
            // imageLabel_idbk
            // 
            this.imageLabel_idbk.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.input;
            this.imageLabel_idbk.Checked = false;
            this.imageLabel_idbk.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_idbk.Location = new System.Drawing.Point(215, 25);
            this.imageLabel_idbk.Name = "imageLabel_idbk";
            this.imageLabel_idbk.Size = new System.Drawing.Size(166, 35);
            this.imageLabel_idbk.TabIndex = 1;
            this.imageLabel_idbk.Text = "imageLabel2";
            this.imageLabel_idbk.TextX = -1F;
            this.imageLabel_idbk.TextY = -1F;
            this.imageLabel_idbk.TransParent = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(93, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 33);
            this.label1.Text = "泵站选择：";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(530, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 33);
            this.label3.Text = "油缸选择：";
            // 
            // comboBox_subid
            // 
            this.comboBox_subid.Items.Add("#1 油缸");
            this.comboBox_subid.Items.Add("#2 油缸");
            this.comboBox_subid.Items.Add("#3 油缸");
            this.comboBox_subid.Items.Add("#4 油缸");
            this.comboBox_subid.Items.Add("#5 油缸");
            this.comboBox_subid.Items.Add("#6 油缸");
            this.comboBox_subid.Items.Add("#7 油缸");
            this.comboBox_subid.Items.Add("#8 油缸");
            this.comboBox_subid.Location = new System.Drawing.Point(678, 28);
            this.comboBox_subid.Name = "comboBox_subid";
            this.comboBox_subid.Size = new System.Drawing.Size(130, 23);
            this.comboBox_subid.TabIndex = 3;
            this.comboBox_subid.TabStop = false;
            this.comboBox_subid.SelectedIndexChanged += new System.EventHandler(this.comboBox_subid_SelectedIndexChanged);
            // 
            // imageLabel_subidbk
            // 
            this.imageLabel_subidbk.BackImg = global::AdvaMACSystem.AdvaMACSystemRes.input;
            this.imageLabel_subidbk.Checked = false;
            this.imageLabel_subidbk.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_subidbk.Location = new System.Drawing.Point(659, 23);
            this.imageLabel_subidbk.Name = "imageLabel_subidbk";
            this.imageLabel_subidbk.Size = new System.Drawing.Size(166, 35);
            this.imageLabel_subidbk.TabIndex = 4;
            this.imageLabel_subidbk.Text = "imageLabel3";
            this.imageLabel_subidbk.TextX = -1F;
            this.imageLabel_subidbk.TextY = -1F;
            this.imageLabel_subidbk.TransParent = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.imageLabel4);
            this.panel2.Controls.Add(this.imageLabel5);
            this.panel2.Controls.Add(this.imageLabel3);
            this.panel2.Controls.Add(this.imageLabel2);
            this.panel2.Controls.Add(this.imageLabel1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(7, 187);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1010, 333);
            // 
            // imageLabel4
            // 
            this.imageLabel4.BackColor = System.Drawing.Color.White;
            this.imageLabel4.Checked = false;
            this.imageLabel4.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel4.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel4.Location = new System.Drawing.Point(615, 128);
            this.imageLabel4.Name = "imageLabel4";
            this.imageLabel4.Size = new System.Drawing.Size(221, 33);
            this.imageLabel4.TabIndex = 9;
            this.imageLabel4.Text = "0";
            this.imageLabel4.TextX = -1F;
            this.imageLabel4.TextY = -1F;
            this.imageLabel4.TransParent = false;
            // 
            // imageLabel5
            // 
            this.imageLabel5.BackColor = System.Drawing.Color.White;
            this.imageLabel5.Checked = false;
            this.imageLabel5.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel5.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel5.Location = new System.Drawing.Point(615, 198);
            this.imageLabel5.Name = "imageLabel5";
            this.imageLabel5.Size = new System.Drawing.Size(221, 33);
            this.imageLabel5.TabIndex = 8;
            this.imageLabel5.Text = "0";
            this.imageLabel5.TextX = -1F;
            this.imageLabel5.TextY = -1F;
            this.imageLabel5.TransParent = false;
            // 
            // imageLabel3
            // 
            this.imageLabel3.BackColor = System.Drawing.Color.White;
            this.imageLabel3.Checked = false;
            this.imageLabel3.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel3.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel3.Location = new System.Drawing.Point(148, 198);
            this.imageLabel3.Name = "imageLabel3";
            this.imageLabel3.Size = new System.Drawing.Size(221, 33);
            this.imageLabel3.TabIndex = 7;
            this.imageLabel3.Text = "0";
            this.imageLabel3.TextX = -1F;
            this.imageLabel3.TextY = -1F;
            this.imageLabel3.TransParent = false;
            // 
            // imageLabel2
            // 
            this.imageLabel2.BackColor = System.Drawing.Color.White;
            this.imageLabel2.Checked = false;
            this.imageLabel2.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel2.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel2.Location = new System.Drawing.Point(148, 128);
            this.imageLabel2.Name = "imageLabel2";
            this.imageLabel2.Size = new System.Drawing.Size(221, 33);
            this.imageLabel2.TabIndex = 6;
            this.imageLabel2.Text = "0";
            this.imageLabel2.TextX = -1F;
            this.imageLabel2.TextY = -1F;
            this.imageLabel2.TransParent = false;
            // 
            // imageLabel1
            // 
            this.imageLabel1.BackColor = System.Drawing.Color.White;
            this.imageLabel1.Checked = false;
            this.imageLabel1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel1.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel1.Location = new System.Drawing.Point(490, 44);
            this.imageLabel1.Name = "imageLabel1";
            this.imageLabel1.Size = new System.Drawing.Size(221, 33);
            this.imageLabel1.TabIndex = 5;
            this.imageLabel1.Text = "0";
            this.imageLabel1.TextX = -1F;
            this.imageLabel1.TextY = -1F;
            this.imageLabel1.TransParent = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(530, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 30);
            this.label6.Text = "Y2 值：";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(530, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 30);
            this.label7.Text = "Y1 值：";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(56, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 30);
            this.label5.Text = "X2 值：";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(56, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 30);
            this.label4.Text = "X1 值：";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(352, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 33);
            this.label2.Text = "当前AD值：";
            // 
            // imageButton_OK_Low
            // 
            this.imageButton_OK_Low.Checked = false;
            this.imageButton_OK_Low.DNImg = global::AdvaMACSystem.AdvaMACSystemRes.Dgn_down;
            this.imageButton_OK_Low.DownColor = System.Drawing.SystemColors.Control;
            this.imageButton_OK_Low.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.imageButton_OK_Low.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageButton_OK_Low.Location = new System.Drawing.Point(523, 621);
            this.imageButton_OK_Low.Name = "imageButton_OK_Low";
            this.imageButton_OK_Low.ShortcutKeys = System.Windows.Forms.Keys.None;
            this.imageButton_OK_Low.Size = new System.Drawing.Size(210, 40);
            this.imageButton_OK_Low.TabIndex = 4;
            this.imageButton_OK_Low.TabStop = false;
            this.imageButton_OK_Low.Text = "低位标定";
            this.imageButton_OK_Low.Toggle = false;
            this.imageButton_OK_Low.TransParent = false;
            this.imageButton_OK_Low.UpColor = System.Drawing.SystemColors.Control;
            this.imageButton_OK_Low.UPImg = global::AdvaMACSystem.AdvaMACSystemRes.Dgn_up;
            this.imageButton_OK_Low.Click += new System.EventHandler(this.imageButton_OK_Low_Click);
            this.imageButton_OK_Low.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageButton_OK_Low_MouseDown);
            this.imageButton_OK_Low.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageButton_OK_Low_MouseUp);
            // 
            // imageButton_OK_High
            // 
            this.imageButton_OK_High.Checked = false;
            this.imageButton_OK_High.DNImg = global::AdvaMACSystem.AdvaMACSystemRes.Dgn_down;
            this.imageButton_OK_High.DownColor = System.Drawing.SystemColors.Control;
            this.imageButton_OK_High.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.imageButton_OK_High.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageButton_OK_High.Location = new System.Drawing.Point(323, 621);
            this.imageButton_OK_High.Name = "imageButton_OK_High";
            this.imageButton_OK_High.ShortcutKeys = System.Windows.Forms.Keys.None;
            this.imageButton_OK_High.Size = new System.Drawing.Size(210, 40);
            this.imageButton_OK_High.TabIndex = 4;
            this.imageButton_OK_High.TabStop = false;
            this.imageButton_OK_High.Text = "高位标定";
            this.imageButton_OK_High.Toggle = false;
            this.imageButton_OK_High.TransParent = false;
            this.imageButton_OK_High.UpColor = System.Drawing.SystemColors.Control;
            this.imageButton_OK_High.UPImg = global::AdvaMACSystem.AdvaMACSystemRes.Dgn_up;
            this.imageButton_OK_High.Click += new System.EventHandler(this.imageButton_OK_High_Click);
            this.imageButton_OK_High.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageButton_OK_High_MouseDown);
            this.imageButton_OK_High.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageButton_OK_High_MouseUp);
            // 
            // imageButton_back
            // 
            this.imageButton_back.Checked = false;
            this.imageButton_back.DNImg = global::AdvaMACSystem.AdvaMACSystemRes.Dgn_down;
            this.imageButton_back.DownColor = System.Drawing.SystemColors.Control;
            this.imageButton_back.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.imageButton_back.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageButton_back.Location = new System.Drawing.Point(780, 621);
            this.imageButton_back.Name = "imageButton_back";
            this.imageButton_back.ShortcutKeys = System.Windows.Forms.Keys.None;
            this.imageButton_back.Size = new System.Drawing.Size(210, 40);
            this.imageButton_back.TabIndex = 0;
            this.imageButton_back.TabStop = false;
            this.imageButton_back.Text = "返回";
            this.imageButton_back.Toggle = false;
            this.imageButton_back.TransParent = false;
            this.imageButton_back.UpColor = System.Drawing.SystemColors.Control;
            this.imageButton_back.UPImg = global::AdvaMACSystem.AdvaMACSystemRes.Dgn_up;
            this.imageButton_back.Click += new System.EventHandler(this.imageButton_back_Click);
            // 
            // PageViewPara_Sensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.imageButton_OK_Low);
            this.Controls.Add(this.imageButton_OK_High);
            this.Controls.Add(this.imageButton_back);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imageLabel_title);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "PageViewPara_Sensor";
            this.Size = new System.Drawing.Size(1024, 674);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComCtrls.ImageLabel imageLabel_title;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_id;
        private ComCtrls.ImageLabel imageLabel_idbk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_subid;
        private ComCtrls.ImageLabel imageLabel_subidbk;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private ComCtrls.ImageButton imageButton_back;
        private ComCtrls.ImageButton imageButton_OK_Low;
        private ComCtrls.ImageButton imageButton_OK_High;
        private ComCtrls.ImageLabel imageLabel4;
        private ComCtrls.ImageLabel imageLabel5;
        private ComCtrls.ImageLabel imageLabel3;
        private ComCtrls.ImageLabel imageLabel2;
        private ComCtrls.ImageLabel imageLabel1;
    }
}
