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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem();
            this.imageButton_back = new ComCtrls.ImageButton(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(326, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(407, 38);
            this.label1.Text = "系统故障信息-实时数据";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.ForeColor = System.Drawing.Color.Black;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.Add(this.columnHeader5);
            this.listView1.Columns.Add(this.columnHeader6);
            this.listView1.Columns.Add(this.columnHeader7);
            this.listView1.Columns.Add(this.columnHeader8);
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem1.Text = "1";
            listViewItem1.SubItems.Add("2014");
            listViewItem1.SubItems.Add("发生");
            listViewItem1.SubItems.Add("err1");
            listViewItem2.Text = "2";
            listViewItem2.SubItems.Add("2015");
            listViewItem2.SubItems.Add("消失");
            listViewItem2.SubItems.Add("err2");
            this.listView1.Items.Add(listViewItem1);
            this.listView1.Items.Add(listViewItem2);
            this.listView1.Location = new System.Drawing.Point(5, 57);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1014, 519);
            this.listView1.TabIndex = 1;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "序号";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "时间";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 200;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "事件";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "内容";
            this.columnHeader8.Width = 620;
            // 
            // imageButton_back
            // 
            this.imageButton_back.Checked = false;
            this.imageButton_back.DownColor = System.Drawing.SystemColors.Control;
            this.imageButton_back.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageButton_back.Location = new System.Drawing.Point(780, 621);
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
            // PageViewError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imageButton_back);
            this.Name = "PageViewError";
            this.Size = new System.Drawing.Size(1024, 670);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private ComCtrls.ImageButton imageButton_back;
    }
}
