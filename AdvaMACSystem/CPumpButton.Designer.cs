namespace AdvaMACSystem
{
    public partial class CPumpButton
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
            this.lbName = new System.Windows.Forms.Label();
            this.lbPara = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbName
            // 
            this.lbName.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular);
            this.lbName.Location = new System.Drawing.Point(10, 65);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(60, 40);
            this.lbName.Visible = false;
            // 
            // lbPara
            // 
            this.lbPara.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular);
            this.lbPara.Location = new System.Drawing.Point(10, 95);
            this.lbPara.Name = "lbPara";
            this.lbPara.Size = new System.Drawing.Size(150, 40);
            this.lbPara.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbPara.Visible = false;
            // 
            // lbStatus
            // 
            this.lbStatus.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular);
            this.lbStatus.ForeColor = System.Drawing.Color.Blue;
            this.lbStatus.Location = new System.Drawing.Point(80, 10);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(80, 60);
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbStatus.Visible = false;
            // 
            // CPumpButton
            // 
            this.Controls.Add(lbName);
            this.Controls.Add(lbPara);
            this.Controls.Add(lbStatus);
            this.Size = new System.Drawing.Size(170, 145);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbPara;
        private System.Windows.Forms.Label lbStatus;
    }
}
