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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.customStatusBar1 = new ComCtrls.CustomProgressBar();
            this.labelMin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 30);
            this.label1.Text = "1#";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(80, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 30);
            this.label2.Text = "到哪里:5000kkk";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 20);
            this.label3.Text = "嗷嗷嗷:1111kkk";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(150, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 20);
            this.label4.Text = "嗷嗷嗷:1111kkk";
            // 
            // labelMax
            // 
            this.labelMax.Location = new System.Drawing.Point(200, 70);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(100, 20);
            this.labelMax.Text = "Max";
            this.labelMax.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // customStatusBar1
            // 
            this.customStatusBar1.Location = new System.Drawing.Point(0, 35);
            this.customStatusBar1.Name = "customStatusBar1";
            this.customStatusBar1.Size = new System.Drawing.Size(300, 30);
            this.customStatusBar1.TabIndex = 10;
            this.customStatusBar1.TabStop = false;
            this.customStatusBar1.Text = "customStatusBar1";
            // 
            // labelMin
            // 
            this.labelMin.Location = new System.Drawing.Point(0, 70);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(100, 20);
            this.labelMin.Text = "Min";
            // 
            // CurrentStatusBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.customStatusBar1);
            this.Controls.Add(this.labelMax);
            this.Controls.Add(this.labelMin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CurrentStatusBlock";
            this.Size = new System.Drawing.Size(300, 110);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelMax;
        private ComCtrls.CustomProgressBar customStatusBar1;
        private System.Windows.Forms.Label labelMin;

    }
}
