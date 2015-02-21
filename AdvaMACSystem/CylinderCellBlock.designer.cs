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
            this.lbMaxPos = new System.Windows.Forms.Label();
            this.lbMinPos = new System.Windows.Forms.Label();
            this.lbMaxPre = new System.Windows.Forms.Label();
            this.lbMinPre = new System.Windows.Forms.Label();
            this.lbSettingPre = new System.Windows.Forms.Label();
            this.lbWarningPre = new System.Windows.Forms.Label();
            this.lbSettingPos = new System.Windows.Forms.Label();
            this.lbWarningPos = new System.Windows.Forms.Label();
            this.lbValuePre = new System.Windows.Forms.Label();
            this.lbValuePos = new System.Windows.Forms.Label();
            this.lbIndex = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.lbMaxPos);
            this.panel1.Controls.Add(this.lbMinPos);
            this.panel1.Controls.Add(this.lbMaxPre);
            this.panel1.Controls.Add(this.lbMinPre);
            this.panel1.Controls.Add(this.lbSettingPre);
            this.panel1.Controls.Add(this.lbWarningPre);
            this.panel1.Controls.Add(this.lbSettingPos);
            this.panel1.Controls.Add(this.lbWarningPos);
            this.panel1.Controls.Add(this.lbValuePre);
            this.panel1.Controls.Add(this.lbValuePos);
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
            this.lbMaxPos.Location = new System.Drawing.Point(220, 33);
            this.lbMaxPos.Name = "lbMaxDis";
            this.lbMaxPos.Size = new System.Drawing.Size(80, 15);
            this.lbMaxPos.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbMinDis
            // 
            this.lbMinPos.Location = new System.Drawing.Point(0, 33);
            this.lbMinPos.Name = "lbMinDis";
            this.lbMinPos.Size = new System.Drawing.Size(60, 15);
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
            this.lbSettingPos.ForeColor = System.Drawing.Color.Blue;
            this.lbSettingPos.Location = new System.Drawing.Point(150, 28);
            this.lbSettingPos.Name = "lbSettingDis";
            this.lbSettingPos.Size = new System.Drawing.Size(60, 15);
            this.lbSettingPos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbSettingPos.Visible = false;
            // 
            // lbWarningDis
            // 
            this.lbWarningPos.ForeColor = System.Drawing.Color.Red;
            this.lbWarningPos.Location = new System.Drawing.Point(75, 28);
            this.lbWarningPos.Name = "lbWarningDis";
            this.lbWarningPos.Size = new System.Drawing.Size(60, 15);
            this.lbWarningPos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbWarningPos.Visible = false;
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
            this.lbValuePos.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbValuePos.Location = new System.Drawing.Point(80, 1);
            this.lbValuePos.Name = "lbValueDis";
            this.lbValuePos.Size = new System.Drawing.Size(160, 20);
            this.lbValuePos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
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
        private System.Windows.Forms.Label lbValuePos;
        private System.Windows.Forms.Label lbWarningPos;
        private System.Windows.Forms.Label lbSettingPos;
        private System.Windows.Forms.Label lbMaxPre;
        private System.Windows.Forms.Label lbMinPre;
        private ComCtrls.CustomProgressBar progressBar;
        private System.Windows.Forms.Label lbMaxPos;
        private System.Windows.Forms.Label lbMinPos;
        private System.Windows.Forms.Label lbValuePre;
        private System.Windows.Forms.Label lbSettingPre;
        private System.Windows.Forms.Label lbWarningPre;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}
