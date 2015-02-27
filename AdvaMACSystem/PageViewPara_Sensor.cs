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
            DataPool.CDataPool.GetDataPoolObject().CurId = comboBox_id.SelectedIndex;
            DataPool.CDataPool.GetDataPoolObject().CurSubId = comboBox_subid.SelectedIndex;

            UpdateViewData();
            DataPool.CDataPool.GetDataPoolObject().sign_View_SenserCalibration = true;
        }

        private void comboBox_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().CurId = comboBox_id.SelectedIndex;
            UpdateViewData();
        }

        private void comboBox_subid_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().CurSubId = comboBox_subid.SelectedIndex;
            UpdateViewData();
        }
        private void imageButton_back_Click(object sender, EventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_SenserCalibration = false;
            this.DoExit();
        }

        private void imageButton_OK_Low_Click(object sender, EventArgs e)
        {
            SaveViewData();
        }

        private void imageButton_OK_High_Click(object sender, EventArgs e)
        {
            SaveViewData();
        }

        private void imageButton_OK_Low_MouseUp(object sender, MouseEventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_PositionSenserLow_Confirm = false;
        }
        private void imageButton_OK_Low_MouseDown(object sender, MouseEventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_PositionSenserLow_Confirm = true;
        }


        private void imageButton_OK_High_MouseUp(object sender, MouseEventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_PositionSenserHigh_Confirm = false;
        }
        private void imageButton_OK_High_MouseDown(object sender, MouseEventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_PositionSenserHigh_Confirm = true;
        }


        private void UpdateViewData() { }
        private void SaveViewData()
        {
            DataPool.CDataPool.GetDataPoolObject().SavetoFile();
            MessageBox.Show(string.Format("#{0:00}泵站-#{1:00}油缸，参数已经保存！", comboBox_id.SelectedIndex + 1, comboBox_subid.SelectedIndex + 1));

        }
    }
}
