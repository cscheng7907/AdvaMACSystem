namespace AdvaMACSystem
{
    partial class CylinderCellBlock
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar = new ComCtrls.CustomProgressBar();
            this.lbMaxDis = new System.Windows.Forms.Label();
            this.lbMinDis = new System.Windows.Forms.Label();
            this.lbMaxPre = new System.Windows.Forms.Label();
            this.lbMinPre = new System.Windows.Forms.Label();
            this.lbSettingPre = new System.Windows.Forms.Label();
            this.lbWarningPre = new System.Windows.Forms.Label();
            this.lbSettingDis = new System.Windows.Forms.Label();
            this.lbWarningDis = new System.Windows.Forms.Label();
            this.lbValuePre = new System.Windows.Forms.Label();
            this.lbValueDis = new System.Windows.Forms.Label();
            this.lbIndex = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.lbMaxDis);
            this.panel1.Controls.Add(this.lbMinDis);
            this.panel1.Controls.Add(this.lbMaxPre);
            this.panel1.Controls.Add(this.lbMinPre);
            this.panel1.Controls.Add(this.lbSettingPre);
            this.panel1.Controls.Add(this.lbWarningPre);
            this.panel1.Controls.Add(this.lbSettingDis);
            this.panel1.Controls.Add(this.lbWarningDis);
            this.panel1.Controls.Add(this.lbValuePre);
            this.panel1.Controls.Add(this.lbValueDis);
            this.panel1.Controls.Add(this.lbIndex);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 135);
            this.panel1.Click += new System.EventHandler(this.CylinderCellBlock_Click);
            // 
            // progressBar
            // 
            this.progressBar.IMGContainer = null;
            this.progressBar.Location = new System.Drawing.Point(10, 50);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(280, 30);
            this.progressBar.TabIndex = 11;
            this.progressBar.TabStop = false;
            this.progressBar.Text = "progressBar";
            // 
            // lbMaxDis
            // 
            this.lbMaxDis.Location = new System.Drawing.Point(220, 33);
            this.lbMaxDis.Name = "lbMaxDis";
            this.lbMaxDis.Size = new System.Drawing.Size(80, 15);
            this.lbMaxDis.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbMinDis
            // 
            this.lbMinDis.Location = new System.Drawing.Point(0, 33);
            this.lbMinDis.Name = "lbMinDis";
            this.lbMinDis.Size = new System.Drawing.Size(60, 15);
            // 
            // lbMaxPre
            // 
            this.lbMaxPre.Location = new System.Drawing.Point(220, 82);
            this.lbMaxPre.Name = "lbMaxPre";
            this.lbMaxPre.Size = new System.Drawing.Size(80, 15);
            this.lbMaxPre.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbMinPre
            // 
            this.lbMinPre.Location = new System.Drawing.Point(0, 82);
            this.lbMinPre.Name = "lbMinPre";
            this.lbMinPre.Size = new System.Drawing.Size(60, 15);
            // 
            // lbSettingPre
            // 
            this.lbSettingPre.ForeColor = System.Drawing.Color.Blue;
            this.lbSettingPre.Location = new System.Drawing.Point(150, 85);
            this.lbSettingPre.Name = "lbSettingPre";
            this.lbSettingPre.Size = new System.Drawing.Size(60, 15);
            this.lbSettingPre.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbWarningPre
            // 
            this.lbWarningPre.ForeColor = System.Drawing.Color.Red;
            this.lbWarningPre.Location = new System.Drawing.Point(75, 85);
            this.lbWarningPre.Name = "lbWarningPre";
            this.lbWarningPre.Size = new System.Drawing.Size(60, 15);
            this.lbWarningPre.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbSettingDis
            // 
            this.lbSettingDis.ForeColor = System.Drawing.Color.Blue;
            this.lbSettingDis.Location = new System.Drawing.Point(150, 28);
            this.lbSettingDis.Name = "lbSettingDis";
            this.lbSettingDis.Size = new System.Drawing.Size(60, 15);
            this.lbSettingDis.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbSettingDis.Visible = false;
            // 
            // lbWarningDis
            // 
            this.lbWarningDis.ForeColor = System.Drawing.Color.Red;
            this.lbWarningDis.Location = new System.Drawing.Point(75, 28);
            this.lbWarningDis.Name = "lbWarningDis";
            this.lbWarningDis.Size = new System.Drawing.Size(60, 15);
            this.lbWarningDis.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbWarningDis.Visible = false;
            // 
            // lbValuePre
            // 
            this.lbValuePre.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbValuePre.Location = new System.Drawing.Point(80, 113);
            this.lbValuePre.Name = "lbValuePre";
            this.lbValuePre.Size = new System.Drawing.Size(160, 20);
            this.lbValuePre.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbValueDis
            // 
            this.lbValueDis.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbValueDis.Location = new System.Drawing.Point(80, 1);
            this.lbValueDis.Name = "lbValueDis";
            this.lbValueDis.Size = new System.Drawing.Size(160, 20);
            this.lbValueDis.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbIndex
            // 
            this.lbIndex.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbIndex.Location = new System.Drawing.Point(1, 1);
            this.lbIndex.Name = "lbIndex";
            this.lbIndex.Size = new System.Drawing.Size(40, 30);
            this.lbIndex.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(310, 145);
            // 
            // CylinderCellBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "CylinderCellBlock";
            this.Size = new System.Drawing.Size(310, 145);
            this.Click += new System.EventHandler(this.CylinderCellBlock_Click);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbIndex;
        private System.Windows.Forms.Label lbValueDis;
        private System.Windows.Forms.Label lbWarningDis;
        private System.Windows.Forms.Label lbSettingDis;
        private System.Windows.Forms.Label lbMaxPre;
        private System.Windows.Forms.Label lbMinPre;
        private ComCtrls.CustomProgressBar progressBar;
        private System.Windows.Forms.Label lbMaxDis;
        private System.Windows.Forms.Label lbMinDis;
        private System.Windows.Forms.Label lbValuePre;
        private System.Windows.Forms.Label lbSettingPre;
        private System.Windows.Forms.Label lbWarningPre;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}
