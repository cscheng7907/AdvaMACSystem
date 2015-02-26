namespace AdvaMACSystem
{
    partial class FormTimeSetting
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.lbHour = new System.Windows.Forms.Label();
            this.lbMinute = new System.Windows.Forms.Label();
            this.lbHours = new System.Windows.Forms.Label();
            this.lbMinutes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(67, 60);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.Size = new System.Drawing.Size(332, 185);
            this.monthCalendar1.TabIndex = 0;
            // 
            // lbHour
            // 
            this.lbHour.Location = new System.Drawing.Point(82, 288);
            this.lbHour.Name = "lbHour";
            this.lbHour.Size = new System.Drawing.Size(50, 30);
            this.lbHour.Text = "时";
            // 
            // lbMinute
            // 
            this.lbMinute.Location = new System.Drawing.Point(174, 288);
            this.lbMinute.Name = "lbMinute";
            this.lbMinute.Size = new System.Drawing.Size(50, 30);
            this.lbMinute.Text = "分起";
            // 
            // lbHours
            // 
            this.lbHours.Location = new System.Drawing.Point(266, 288);
            this.lbHours.Name = "lbHours";
            this.lbHours.Size = new System.Drawing.Size(50, 30);
            this.lbHours.Text = "小时";
            // 
            // lbMinutes
            // 
            this.lbMinutes.Location = new System.Drawing.Point(358, 288);
            this.lbMinutes.Name = "lbMinutes";
            this.lbMinutes.Size = new System.Drawing.Size(50, 30);
            this.lbMinutes.Text = "分钟";
            // 
            // FormTimeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(448, 455);
            this.Controls.Add(this.lbMinutes);
            this.Controls.Add(this.lbHours);
            this.Controls.Add(this.lbMinute);
            this.Controls.Add(this.lbHour);
            this.Controls.Add(this.monthCalendar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormTimeSetting";
            this.Text = "FormTimeSetting";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label lbHour;
        private System.Windows.Forms.Label lbMinute;
        private System.Windows.Forms.Label lbHours;
        private System.Windows.Forms.Label lbMinutes;
    }
}