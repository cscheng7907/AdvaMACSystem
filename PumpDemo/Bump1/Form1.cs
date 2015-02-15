using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bump1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.currentStatusBlock1.MinValue = 100;
            this.currentStatusBlock1.MaxValue = 300;
            this.currentStatusBlock1.WarningValue = 200;
            this.currentStatusBlock1.SettingValue = 250;
            this.timer1.Enabled = true;
        }
        private double cnt = 0.1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            cnt += 0.1;
            this.currentStatusBlock1.CurrentValue = (float)(200 + 50 * Math.Sin(cnt));
        }

    }
}