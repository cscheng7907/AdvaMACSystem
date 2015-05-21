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
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
            this.Show();
            ShowLoadingInfo();
            main = new mainForm();
            ShowLoadingInfo();
            main.Create_pvMAC();
            ShowLoadingInfo();
            main.Create_pvHistory();
            ShowLoadingInfo();
            main.Create_pvDiagnose();
            ShowLoadingInfo();
            main.Create_pvError();
            main.Create_pvWarn();
            ShowLoadingInfo();
            main.Create_pvPara();
            main.Create_pvPara_Sensor();
            main.Create_pvPara_Setup();
            this.button1.Text = "进入系统";
            this.button1.Click += new System.EventHandler(this.button1_Click);
        }

        private string loadingText = "正在加载({0})";
        private uint remainItems = 6;
        private void ShowLoadingInfo()
        {
            this.button1.Text = string.Format(loadingText, remainItems.ToString());
            Application.DoEvents();
            --remainItems;
        }
        
        private mainForm main = null;
        private void button1_Click(object sender, EventArgs e)
        {
              main.imageLabel_MAC_Click(null, null);
              main.Show();
              main.BringToFront();

              this.Hide();
        }


    }
}