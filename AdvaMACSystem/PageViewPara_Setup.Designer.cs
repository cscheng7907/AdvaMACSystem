namespace AdvaMACSystem
{
    partial class PageViewPara_Setup
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
            this.imageButton_Finish_OK = new ComCtrls.ImageButton(this.components);
            this.imageButton_OK = new ComCtrls.ImageButton(this.components);
            this.imageButton_back = new ComCtrls.ImageButton(this.components);
            this.panel_body = new System.Windows.Forms.Panel();
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
            this.imageLabel_title.Text = "安装设定";
            this.imageLabel_title.TextX = -1F;
            this.imageLabel_title.TextY = -1F;
            this.imageLabel_title.TransParent = false;
            
            // 
            // imageButton_Finish_OK
            // 
            this.imageButton_Finish_OK.Visible = false;
            this.imageButton_Finish_OK.Checked = false;
            this.imageButton_Finish_OK.DownColor = System.Drawing.SystemColors.Control;
            this.imageButton_Finish_OK.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageButton_Finish_OK.Location = new System.Drawing.Point(266, 521);
            this.imageButton_Finish_OK.Name = "imageButton_Finish_OK";
            this.imageButton_Finish_OK.ShortcutKeys = System.Windows.Forms.Keys.None;
            this.imageButton_Finish_OK.Size = new System.Drawing.Size(210, 40);
            this.imageButton_Finish_OK.ForeColor = System.Drawing.Color.Black;
            this.imageButton_Finish_OK.TabIndex = 4;
            this.imageButton_Finish_OK.TabStop = false;
            this.imageButton_Finish_OK.Text = "安装调试完毕确认";
            this.imageButton_Finish_OK.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.imageButton_Finish_OK.Toggle = false;
            this.imageButton_Finish_OK.TransParent = false;
            this.imageButton_Finish_OK.UPImg = AdvaMACSystemRes.Dgn_up;
            this.imageButton_Finish_OK.DNImg = AdvaMACSystemRes.Dgn_down;
            this.imageButton_Finish_OK.UpColor = System.Drawing.SystemColors.Control;
            this.imageButton_Finish_OK.Click += new System.EventHandler(this.imageButton_Finish_OK_Click);
            this.imageButton_Finish_OK.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageButton_Finish_OK_MouseUp);
            this.imageButton_Finish_OK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageButton_Finish_OK_MouseDown);
            
            // 
            // imageButton_OK
            // 
            this.imageButton_OK.Checked = false;
            this.imageButton_OK.DownColor = System.Drawing.SystemColors.Control;
            this.imageButton_OK.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageButton_OK.Location = new System.Drawing.Point(523, 521);
            this.imageButton_OK.Name = "imageButton_OK";
            this.imageButton_OK.ShortcutKeys = System.Windows.Forms.Keys.None;
            this.imageButton_OK.Size = new System.Drawing.Size(210, 40);
            this.imageButton_OK.ForeColor = System.Drawing.Color.Black;
            this.imageButton_OK.TabIndex = 4;
            this.imageButton_OK.TabStop = false;
            this.imageButton_OK.Text = "确认";
            this.imageButton_OK.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.imageButton_OK.Toggle = false;
            this.imageButton_OK.TransParent = false;
            this.imageButton_OK.UPImg = AdvaMACSystemRes.Dgn_up;
            this.imageButton_OK.DNImg = AdvaMACSystemRes.Dgn_down;
            this.imageButton_OK.UpColor = System.Drawing.SystemColors.Control;
            this.imageButton_OK.Click += new System.EventHandler(this.imageButton_OK_Click);
            this.imageButton_OK.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageButton_OK_MouseUp);
            this.imageButton_OK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageButton_OK_MouseDown);

            // 
            // imageButton_back
            // 
            this.imageButton_back.Checked = false;
            this.imageButton_back.DownColor = System.Drawing.SystemColors.Control;
            this.imageButton_back.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageButton_back.Location = new System.Drawing.Point(780, 521);
            this.imageButton_back.Name = "imageButton_back";
            this.imageButton_back.ShortcutKeys = System.Windows.Forms.Keys.None;
            this.imageButton_back.ForeColor = System.Drawing.Color.Black;
            this.imageButton_back.Size = new System.Drawing.Size(210, 40);
            this.imageButton_back.TabIndex = 0;
            this.imageButton_back.TabStop = false;
            this.imageButton_back.Text = "返回";
            this.imageButton_back.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.imageButton_back.Toggle = false;
            this.imageButton_back.TransParent = false;
            this.imageButton_back.UPImg = AdvaMACSystemRes.Dgn_up;
            this.imageButton_back.DNImg = AdvaMACSystemRes.Dgn_down;
            this.imageButton_back.UpColor = System.Drawing.SystemColors.Control;
            this.imageButton_back.Click += new System.EventHandler(this.imageButton_back_Click);
            // 
            // panel_body
            // 
            this.panel_body.Controls.Add(this.imageButton_Finish_OK);
            this.panel_body.Controls.Add(this.imageButton_OK);
            this.panel_body.Controls.Add(this.imageButton_back);
            this.panel_body.Location = new System.Drawing.Point(6, 68);
            this.panel_body.Name = "panel_body";
            this.panel_body.Size = new System.Drawing.Size(1011, 580);


            // 
            // PageViewPara_Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel_body);
            this.Controls.Add(this.imageLabel_title);
            this.Name = "PageViewPara_Setup";
            this.Size = new System.Drawing.Size(1024, 674);
            this.ForeColor = System.Drawing.Color.Black;
            this.ResumeLayout(false);

        }

        #endregion

        private ComCtrls.ImageLabel imageLabel_title;
        private ComCtrls.ImageButton imageButton_Finish_OK;
        private ComCtrls.ImageButton imageButton_OK;
        private ComCtrls.ImageButton imageButton_back;
        private System.Windows.Forms.Panel panel_body;
    }
}
