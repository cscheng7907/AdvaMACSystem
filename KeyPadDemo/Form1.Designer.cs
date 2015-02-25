namespace KeyPadDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.imageLabel1 = new ComCtrls.ImageLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // imageLabel1
            // 
            this.imageLabel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imageLabel1.BackImg = null;
            this.imageLabel1.Checked = false;
            this.imageLabel1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            this.imageLabel1.IMGContainer = null;
            this.imageLabel1.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel1.Location = new System.Drawing.Point(172, 185);
            this.imageLabel1.Name = "imageLabel1";
            this.imageLabel1.Size = new System.Drawing.Size(139, 27);
            this.imageLabel1.TabIndex = 3;
            this.imageLabel1.Text = "0";
            this.imageLabel1.TextX = -1F;
            this.imageLabel1.TextY = -1F;
            this.imageLabel1.TransParent = false;
            this.imageLabel1.Click += new System.EventHandler(this.imageLabel1_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(253, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imageLabel1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private ComCtrls.ImageLabel imageLabel1;
        private System.Windows.Forms.Label label1;
    }
}

