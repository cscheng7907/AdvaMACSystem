namespace AdvaMACSystem
{
    partial class PageViewPara_Cylinder
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
            this.imageButton_OK = new ComCtrls.ImageButton(this.components);
            this.imageButton_back = new ComCtrls.ImageButton(this.components);
            this.panel_body = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.imageLabel_PumpTodayPositionHighout = new ComCtrls.ImageLabel();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.imageLabel_PumpPositionHighout = new ComCtrls.ImageLabel();
            this.label28 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.imageLabel_PumpPressureHighout = new ComCtrls.ImageLabel();
            this.label24 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.imageLabel_Area = new ComCtrls.ImageLabel();
            this.label22 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.imageLabel_MAXPosition_Value = new ComCtrls.ImageLabel();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.imageLabel_MAXPressure_Value = new ComCtrls.ImageLabel();
            this.label20 = new System.Windows.Forms.Label();
            this.panel_body.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageLabel_title
            // 
            this.imageLabel_title.BackColor = System.Drawing.Color.Silver;
            this.imageLabel_title.Checked = false;
            this.imageLabel_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.imageLabel_title.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.imageLabel_title.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_title.Location = new System.Drawing.Point(0, 0);
            this.imageLabel_title.Name = "imageLabel_title";
            this.imageLabel_title.Size = new System.Drawing.Size(1024, 57);
            this.imageLabel_title.TabIndex = 0;
            this.imageLabel_title.Text = "油缸参数设定";
            this.imageLabel_title.TextX = -1F;
            this.imageLabel_title.TextY = -1F;
            this.imageLabel_title.TransParent = false;
            // 
            // imageButton_OK
            // 
            this.imageButton_OK.Checked = false;
            this.imageButton_OK.DNImg = global::AdvaMACSystem.AdvaMACSystemRes.Dgn_down;
            this.imageButton_OK.DownColor = System.Drawing.SystemColors.Control;
            this.imageButton_OK.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.imageButton_OK.ForeColor = System.Drawing.Color.Black;
            this.imageButton_OK.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageButton_OK.Location = new System.Drawing.Point(523, 521);
            this.imageButton_OK.Name = "imageButton_OK";
            this.imageButton_OK.ShortcutKeys = System.Windows.Forms.Keys.None;
            this.imageButton_OK.Size = new System.Drawing.Size(210, 40);
            this.imageButton_OK.TabIndex = 4;
            this.imageButton_OK.TabStop = false;
            this.imageButton_OK.Text = "确认";
            this.imageButton_OK.Toggle = false;
            this.imageButton_OK.TransParent = false;
            this.imageButton_OK.UpColor = System.Drawing.SystemColors.Control;
            this.imageButton_OK.UPImg = global::AdvaMACSystem.AdvaMACSystemRes.Dgn_up;
            this.imageButton_OK.Click += new System.EventHandler(this.imageButton_OK_Click);
            this.imageButton_OK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageButton_OK_MouseDown);
            this.imageButton_OK.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageButton_OK_MouseUp);
            // 
            // imageButton_back
            // 
            this.imageButton_back.Checked = false;
            this.imageButton_back.DNImg = global::AdvaMACSystem.AdvaMACSystemRes.Dgn_down;
            this.imageButton_back.DownColor = System.Drawing.SystemColors.Control;
            this.imageButton_back.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.imageButton_back.ForeColor = System.Drawing.Color.Black;
            this.imageButton_back.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageButton_back.Location = new System.Drawing.Point(780, 521);
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
            // panel_body
            // 
            this.panel_body.Controls.Add(this.imageButton_OK);
            this.panel_body.Controls.Add(this.imageButton_back);
            this.panel_body.Controls.Add(this.panel1);
            this.panel_body.Location = new System.Drawing.Point(6, 68);
            this.panel_body.Name = "panel_body";
            this.panel_body.Size = new System.Drawing.Size(1011, 580);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.imageLabel_PumpTodayPositionHighout);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.imageLabel_PumpPositionHighout);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.imageLabel_PumpPressureHighout);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.imageLabel_Area);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.imageLabel_MAXPosition_Value);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.imageLabel_MAXPressure_Value);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Location = new System.Drawing.Point(157, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 512);
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label25.Location = new System.Drawing.Point(791, 286);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(46, 29);
            this.label25.Text = "mm";
            // 
            // imageLabel_PumpTodayPositionHighout
            // 
            this.imageLabel_PumpTodayPositionHighout.BackColor = System.Drawing.Color.White;
            this.imageLabel_PumpTodayPositionHighout.Checked = false;
            this.imageLabel_PumpTodayPositionHighout.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_PumpTodayPositionHighout.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_PumpTodayPositionHighout.Location = new System.Drawing.Point(662, 282);
            this.imageLabel_PumpTodayPositionHighout.Name = "imageLabel_PumpTodayPositionHighout";
            this.imageLabel_PumpTodayPositionHighout.Size = new System.Drawing.Size(123, 29);
            this.imageLabel_PumpTodayPositionHighout.TabIndex = 117;
            this.imageLabel_PumpTodayPositionHighout.Tag = DataPool.CmdDataType.cdtPumpTodayPositionHighout;
            this.imageLabel_PumpTodayPositionHighout.Text = "0";
            this.imageLabel_PumpTodayPositionHighout.TextX = -1F;
            this.imageLabel_PumpTodayPositionHighout.TextY = -1F;
            this.imageLabel_PumpTodayPositionHighout.TransParent = false;
            this.imageLabel_PumpTodayPositionHighout.Click += new System.EventHandler(this.imageLabel_Input_Click);
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label26.Location = new System.Drawing.Point(431, 285);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(236, 33);
            this.label26.Text = "当天行程最大设定值：";
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label27.Location = new System.Drawing.Point(367, 286);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(68, 29);
            this.label27.Text = "mm";
            // 
            // imageLabel_PumpPositionHighout
            // 
            this.imageLabel_PumpPositionHighout.BackColor = System.Drawing.Color.White;
            this.imageLabel_PumpPositionHighout.Checked = false;
            this.imageLabel_PumpPositionHighout.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_PumpPositionHighout.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_PumpPositionHighout.Location = new System.Drawing.Point(240, 283);
            this.imageLabel_PumpPositionHighout.Name = "imageLabel_PumpPositionHighout";
            this.imageLabel_PumpPositionHighout.Size = new System.Drawing.Size(113, 29);
            this.imageLabel_PumpPositionHighout.TabIndex = 116;
            this.imageLabel_PumpPositionHighout.Tag = DataPool.CmdDataType.cdtPumpPositionHighout;
            this.imageLabel_PumpPositionHighout.Text = "0";
            this.imageLabel_PumpPositionHighout.TextX = -1F;
            this.imageLabel_PumpPositionHighout.TextY = -1F;
            this.imageLabel_PumpPositionHighout.TransParent = false;
            this.imageLabel_PumpPositionHighout.Click += new System.EventHandler(this.imageLabel_Input_Click);
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label28.Location = new System.Drawing.Point(21, 286);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(236, 33);
            this.label28.Text = "油缸最大行程设定值：";
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label23.Location = new System.Drawing.Point(791, 234);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(46, 29);
            this.label23.Text = "bar";
            // 
            // imageLabel_PumpPressureHighout
            // 
            this.imageLabel_PumpPressureHighout.BackColor = System.Drawing.Color.White;
            this.imageLabel_PumpPressureHighout.Checked = false;
            this.imageLabel_PumpPressureHighout.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_PumpPressureHighout.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_PumpPressureHighout.Location = new System.Drawing.Point(662, 229);
            this.imageLabel_PumpPressureHighout.Name = "imageLabel_PumpPressureHighout";
            this.imageLabel_PumpPressureHighout.Size = new System.Drawing.Size(123, 29);
            this.imageLabel_PumpPressureHighout.TabIndex = 115;
            this.imageLabel_PumpPressureHighout.Tag = DataPool.CmdDataType.cdtPumpPressureHighout;
            this.imageLabel_PumpPressureHighout.Text = "0";
            this.imageLabel_PumpPressureHighout.TextX = -1F;
            this.imageLabel_PumpPressureHighout.TextY = -1F;
            this.imageLabel_PumpPressureHighout.TransParent = false;
            this.imageLabel_PumpPressureHighout.Click += new System.EventHandler(this.imageLabel_Input_Click);
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label24.Location = new System.Drawing.Point(431, 232);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(236, 33);
            this.label24.Text = "马达最大压力：";
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label21.Location = new System.Drawing.Point(361, 233);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(70, 29);
            this.label21.Text = "dm^2";
            // 
            // imageLabel_Area
            // 
            this.imageLabel_Area.BackColor = System.Drawing.Color.White;
            this.imageLabel_Area.Checked = false;
            this.imageLabel_Area.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_Area.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_Area.Location = new System.Drawing.Point(240, 230);
            this.imageLabel_Area.Name = "imageLabel_Area";
            this.imageLabel_Area.Size = new System.Drawing.Size(113, 29);
            this.imageLabel_Area.TabIndex = 114;
            this.imageLabel_Area.Tag = DataPool.CmdDataType.cdtSectionalArea_Value;
            this.imageLabel_Area.Text = "0";
            this.imageLabel_Area.TextX = -1F;
            this.imageLabel_Area.TextY = -1F;
            this.imageLabel_Area.TransParent = false;
            this.imageLabel_Area.Click += new System.EventHandler(this.imageLabel_Input_Click);
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(21, 233);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(236, 33);
            this.label22.Text = "油缸截面积：";
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label17.Location = new System.Drawing.Point(790, 181);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 29);
            this.label17.Text = "mm";
            // 
            // imageLabel_MAXPosition_Value
            // 
            this.imageLabel_MAXPosition_Value.BackColor = System.Drawing.Color.White;
            this.imageLabel_MAXPosition_Value.Checked = false;
            this.imageLabel_MAXPosition_Value.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_MAXPosition_Value.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_MAXPosition_Value.Location = new System.Drawing.Point(662, 175);
            this.imageLabel_MAXPosition_Value.Name = "imageLabel_MAXPosition_Value";
            this.imageLabel_MAXPosition_Value.Size = new System.Drawing.Size(123, 29);
            this.imageLabel_MAXPosition_Value.TabIndex = 113;
            this.imageLabel_MAXPosition_Value.Tag = DataPool.CmdDataType.cdtMAXPosition_Value;
            this.imageLabel_MAXPosition_Value.Text = "0";
            this.imageLabel_MAXPosition_Value.TextX = -1F;
            this.imageLabel_MAXPosition_Value.TextY = -1F;
            this.imageLabel_MAXPosition_Value.TransParent = false;
            this.imageLabel_MAXPosition_Value.Click += new System.EventHandler(this.imageLabel_Input_Click);
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(429, 178);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(234, 33);
            this.label18.Text = "油缸最大位移值：";
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label19.Location = new System.Drawing.Point(361, 178);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(54, 29);
            this.label19.Text = "bar";
            // 
            // imageLabel_MAXPressure_Value
            // 
            this.imageLabel_MAXPressure_Value.BackColor = System.Drawing.Color.White;
            this.imageLabel_MAXPressure_Value.Checked = false;
            this.imageLabel_MAXPressure_Value.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel_MAXPressure_Value.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_MAXPressure_Value.Location = new System.Drawing.Point(240, 175);
            this.imageLabel_MAXPressure_Value.Name = "imageLabel_MAXPressure_Value";
            this.imageLabel_MAXPressure_Value.Size = new System.Drawing.Size(113, 29);
            this.imageLabel_MAXPressure_Value.TabIndex = 112;
            this.imageLabel_MAXPressure_Value.Tag = DataPool.CmdDataType.cdtMAXPressure_Value;
            this.imageLabel_MAXPressure_Value.Text = "0";
            this.imageLabel_MAXPressure_Value.TextX = -1F;
            this.imageLabel_MAXPressure_Value.TextY = -1F;
            this.imageLabel_MAXPressure_Value.TransParent = false;
            this.imageLabel_MAXPressure_Value.Click += new System.EventHandler(this.imageLabel_Input_Click);
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(21, 178);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(236, 33);
            this.label20.Text = "油缸最大压力值：";
            // 
            // PageViewPara_Cylinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.imageLabel_title);
            this.Controls.Add(this.panel_body);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "PageViewPara_Cylinder";
            this.Size = new System.Drawing.Size(1024, 674);
            this.panel_body.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComCtrls.ImageLabel imageLabel_title;
        private ComCtrls.ImageButton imageButton_OK;
        private ComCtrls.ImageButton imageButton_back;
        private System.Windows.Forms.Panel panel_body;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label25;
        private ComCtrls.ImageLabel imageLabel_PumpTodayPositionHighout;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private ComCtrls.ImageLabel imageLabel_PumpPositionHighout;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label23;
        private ComCtrls.ImageLabel imageLabel_PumpPressureHighout;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label21;
        private ComCtrls.ImageLabel imageLabel_Area;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label17;
        private ComCtrls.ImageLabel imageLabel_MAXPosition_Value;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private ComCtrls.ImageLabel imageLabel_MAXPressure_Value;
        private System.Windows.Forms.Label label20;
    }
}
