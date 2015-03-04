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
    public partial class PageViewPara : UIControlbase
    {
        private List<ImageLabel> lbList = new List<ImageLabel>();
        private List<ImageButton> btnList = new List<ImageButton>();


        public PageViewPara()
        {
            InitializeComponent();


            lbList.Add(imageLabel_PressureAlarm_Pump);
            lbList.Add(imageLabel_PositionLowerLimitAlarm_Value);
            lbList.Add(imageLabel_PositionUpperLimitAlarm_Value);
            lbList.Add(imageLabel_Position_Value);
            lbList.Add(imageLabel_PressureLowerLimitAlarm_Value);
            lbList.Add(imageLabel_PressureUpperLimitAlarm_Value);
            lbList.Add(imageLabel_Pressure_Value);


            btnList.Add(imageButton_PositionControl_Enable);
            btnList.Add(imageButton_PressureUpperLimitAlarm_Enable);
            btnList.Add(imageButton_PositionLowerLimitAlarm_Enable);
            btnList.Add(imageButton_PositionUpperLimitAlarm_Enable);

            for (int i = 0; i < btnList.Count; i++)
            {
                btnList[i].Toggle = true;
            }

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
            DataPool.CDataPool.GetDataPoolObject().sign_View_Parameter = true;

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

        private void UpdateViewData()
        {
            if (comboBox_id.SelectedIndex >= 0 &&
            comboBox_subid.SelectedIndex >= 0)
            {
                for (int i = 0; i < lbList.Count; i++)
                {
                    if ((CmdDataType)lbList[i].Tag == CmdDataType.cdtPressureAlarm_Pump)
                        lbList[i].Text = DataPool.CDataPool.GetDataPoolObject().GetintValue(comboBox_id.SelectedIndex,
                          comboBox_subid.SelectedIndex, (CmdDataType)lbList[i].Tag).ToString();
                    else
                        lbList[i].Text = DataPool.CDataPool.GetDataPoolObject().GetRealValue(comboBox_id.SelectedIndex,
                          comboBox_subid.SelectedIndex, (CmdDataType)lbList[i].Tag).ToString("0.0");
                }

                for (int i = 0; i < btnList.Count; i++)
                {
                    btnList[i].Checked = !DataPool.CDataPool.GetDataPoolObject().GetBoolValue(comboBox_id.SelectedIndex,
                          comboBox_subid.SelectedIndex, (CmdDataType)btnList[i].Tag);
                }
            }
        }

        private void SaveViewData()
        {
            if (comboBox_id.SelectedIndex >= 0 &&
                comboBox_subid.SelectedIndex >= 0)
            {
                for (int i = 0; i < lbList.Count; i++)
                {
                    if ((CmdDataType)lbList[i].Tag == CmdDataType.cdtPressureAlarm_Pump)
                        DataPool.CDataPool.GetDataPoolObject().SetintValue(comboBox_id.SelectedIndex,
                        comboBox_subid.SelectedIndex, (CmdDataType)lbList[i].Tag, Convert.ToInt32(lbList[i].Text));
                    else
                        DataPool.CDataPool.GetDataPoolObject().SetRealValue(comboBox_id.SelectedIndex,
                            comboBox_subid.SelectedIndex, (CmdDataType)lbList[i].Tag, Convert.ToDouble(lbList[i].Text));
                }

                for (int i = 0; i < btnList.Count; i++)
                {
                    DataPool.CDataPool.GetDataPoolObject().SetboolValue(comboBox_id.SelectedIndex,
                          comboBox_subid.SelectedIndex, (CmdDataType)btnList[i].Tag, !btnList[i].Checked);
                }
            }
            DataPool.CDataPool.GetDataPoolObject().SavetoFile();
            MessageBox.Show(string.Format("#{0:00}泵站-#{1:00}油缸，参数已经保存！", comboBox_id.SelectedIndex + 1, comboBox_subid.SelectedIndex + 1));
        }


        private void imageButton_back_Click(object sender, EventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_Parameter = false;
            this.DoExit();
        }

        private void imageButton_OK_Click(object sender, EventArgs e)
        {
            SaveViewData();
        }
        private void imageButton_OK_MouseUp(object sender, MouseEventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_Parameter_Confirm = false;
        }
        private void imageButton_OK_MouseDown(object sender, MouseEventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_Parameter_Confirm = true;
        }

        private void imageButton_Input_Click(object sender, EventArgs e)
        {


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
                    if ((CmdDataType)lb.Tag == CmdDataType.cdtPressureAlarm_Pump)
                    {
                        try
                        {
                            dv = Convert.ToByte (f.KeyText);

                            lb.Text = dv.ToString();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("输入非法！");
                        }
                    }
                    else
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
