namespace AdvaMACSystem
{
    partial class PageViewError
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.imageButton1 = new ComCtrls.ImageButton(this.components);
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.Add(this.columnHeader1);
            this.listView1.Columns.Add(this.columnHeader2);
            this.listView1.Columns.Add(this.columnHeader3);
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(16, 52);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1067, 394);
            this.listView1.TabIndex = 0;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 60;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "时间";
            this.columnHeader2.Width = 60;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "内容";
            this.columnHeader3.Width = 60;
            // 
            // imageButton1
            // 
            this.imageButton1.Checked = false;
            this.imageButton1.DownColor = System.Drawing.SystemColors.Control;
            this.imageButton1.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageButton1.Location = new System.Drawing.Point(883, 452);
            this.imageButton1.Name = "imageButton1";
            this.imageButton1.ShortcutKeys = System.Windows.Forms.Keys.None;
            this.imageButton1.Size = new System.Drawing.Size(200, 52);
            this.imageButton1.TabIndex = 1;
            this.imageButton1.TabStop = false;
            this.imageButton1.Text = "返回";
            this.imageButton1.Toggle = false;
            this.imageButton1.TransParent = false;
            this.imageButton1.UpColor = System.Drawing.SystemColors.Control;
            // 
            // PageViewError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.imageButton1);
            this.Controls.Add(this.listView1);
            this.Name = "PageViewError";
            this.Size = new System.Drawing.Size(1099, 514);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private ComCtrls.ImageButton imageButton1;
    }
}
