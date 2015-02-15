using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AdvaMACSystem
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            lb_Now.Text = string.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

        }
    }
}