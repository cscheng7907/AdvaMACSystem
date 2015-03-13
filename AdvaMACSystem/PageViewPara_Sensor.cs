/*AdvaMACSystem 监控软件
 * 作者： 程慎
 *  
 * 修改记录：
 *       时间                内容                人员
 * 2015-2-15             创建                by cs 
 * 
 * copyright
 */

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataPool;
using ComCtrls;

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



            SaveViewData_Low();
        }

        private void imageButton_OK_High_Click(object sender, EventArgs e)
        {
            SaveViewData_High();
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


        private void UpdateViewData()
        {
            if (comboBox_id.SelectedIndex >= 0 &&
                comboBox_subid.SelectedIndex >= 0)
            {
                //todo
                //imageLabel1.Text =
                //imageLabel2.Text =
                //imageLabel3.Text =


                //Y1
                imageLabel4.Text = DataPool.CDataPool.GetDataPoolObject().GetRealValue(comboBox_id.SelectedIndex,
                   comboBox_subid.SelectedIndex, CmdDataType.cdtPositionSenserLow_Value).ToString("0.0");

                //Y2
                imageLabel5.Text = DataPool.CDataPool.GetDataPoolObject().GetRealValue(comboBox_id.SelectedIndex,
                   comboBox_subid.SelectedIndex, CmdDataType.cdtPositionSenserHigh_Value).ToString("0.0");
            }
        }

        private void SaveViewData_Low()
        {
            DataPool.CDataPool.GetDataPoolObject().SetRealValue(comboBox_id.SelectedIndex,
                comboBox_subid.SelectedIndex, CmdDataType.cdtPositionSenserLow_Value, Convert.ToDouble(imageLabel4.Text));
            DataPool.CDataPool.GetDataPoolObject().SavetoFile();
            MessageBox.Show(string.Format("{0:00}#泵站-{1:00}#油缸，参数已经保存！", comboBox_id.SelectedIndex + 1, comboBox_subid.SelectedIndex + 1));

        }
        private void SaveViewData_High()
        {
            DataPool.CDataPool.GetDataPoolObject().SetRealValue(comboBox_id.SelectedIndex,
                comboBox_subid.SelectedIndex, CmdDataType.cdtPositionSenserHigh_Value, Convert.ToDouble(imageLabel5.Text));
            DataPool.CDataPool.GetDataPoolObject().SavetoFile();
            MessageBox.Show(string.Format("{0:00}#泵站-{1:00}#油缸，参数已经保存！", comboBox_id.SelectedIndex + 1, comboBox_subid.SelectedIndex + 1));
        }

        private void imageLabel_Input_Click(object sender, EventArgs e)
        {
            double dv = 0;

            if (sender is ImageLabel)
            {
                ImageLabel lb = (ImageLabel)sender;

                KeypadForm f = KeypadForm.GetKeypadForm(lb.Text);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    {
                        try
                        {
                            dv = Convert.ToDouble(f.KeyText);

                            lb.Text = dv.ToString("0.0");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("输入非法！");
                        }
                    }
                }

            }
        }

    }
}
