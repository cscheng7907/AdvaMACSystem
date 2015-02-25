using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AdvaMACSystem
{
    public partial class PageViewPara_Sensor : UIControlbase
    {
        public PageViewPara_Sensor()
        {
            InitializeComponent();
        }

        public override void DoEnter()
        {
            base.DoEnter();


            comboBox_id.Items.Clear();
            for (int i = 0; i < DataPool.CDataPool.GetDataPoolObject().PumpCount; i++)
            {
                comboBox_id.Items.Add(string.Format("{0:0}# 泵站", i + 1));
            }

            comboBox_subid.Items.Clear();
            for (int i = 0; i < DataPool.CDataPool.GetDataPoolObject().CylinderCount; i++)
            {
                comboBox_subid.Items.Add(string.Format("{0:0}# 油缸", i + 1));
            }


            comboBox_id.SelectedIndex = 0;
            comboBox_subid.SelectedIndex = 0;

            UpdateViewData();
        }

        private void comboBox_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateViewData();
        }

        private void comboBox_subid_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateViewData();
        }
        private void imageButton_back_Click(object sender, EventArgs e)
        {
            this.DoExit();
        }

        private void imageButton_OK_Click(object sender, EventArgs e)
        {
            SaveViewData();
        }

        private void UpdateViewData() { }
        private void SaveViewData() { }
    }
}
