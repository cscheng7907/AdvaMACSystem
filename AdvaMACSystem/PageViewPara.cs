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
            lbList.Add(imageLabel_MAXPressure_Value);
            lbList.Add(imageLabel_MAXPosition_Value);
            lbList.Add(imageLabel_Area);

            lbList.Add(imageLabel_PumpTodayPositionHighout);
            lbList.Add(imageLabel_PumpPositionHighout);
            lbList.Add(imageLabel_PumpPressureHighout);

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
                        lbList[i].Text = DataPool.CDataPool.GetDataPoolObject().GetRealValue(comboBox_id.SelectedIndex,
                          comboBox_subid.SelectedIndex, (CmdDataType)lbList[i].Tag).ToString("0.0");
                    else
                        lbList[i].Text = DataPool.CDataPool.GetDataPoolObject().GetRealValue(comboBox_id.SelectedIndex,
                          comboBox_subid.SelectedIndex, (CmdDataType)lbList[i].Tag).ToString("0.0");
                }

                for (int i = 0; i < btnList.Count; i++)
                {
                    btnList[i].Checked = DataPool.CDataPool.GetDataPoolObject().GetBoolValue(comboBox_id.SelectedIndex,
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
                        DataPool.CDataPool.GetDataPoolObject().SetRealValue(comboBox_id.SelectedIndex,
                        comboBox_subid.SelectedIndex, (CmdDataType)lbList[i].Tag, Convert.ToDouble(lbList[i].Text));
                    else
                        DataPool.CDataPool.GetDataPoolObject().SetRealValue(comboBox_id.SelectedIndex,
                            comboBox_subid.SelectedIndex, (CmdDataType)lbList[i].Tag, Convert.ToDouble(lbList[i].Text));
                }

                for (int i = 0; i < btnList.Count; i++)
                {
                    DataPool.CDataPool.GetDataPoolObject().SetboolValue(comboBox_id.SelectedIndex,
                          comboBox_subid.SelectedIndex, (CmdDataType)btnList[i].Tag, btnList[i].Checked);
                }
            }
            DataPool.CDataPool.GetDataPoolObject().SavetoFile();
            //MessageBox.Show(string.Format("#{0:00}泵站-#{1:00}油缸，参数已经保存！", comboBox_id.SelectedIndex + 1, comboBox_subid.SelectedIndex + 1));
            MessageBox.Show(string.Format("#{0:00}泵站-#{1:00}油缸，参数已经保存！", comboBox_id.SelectedIndex + 1, comboBox_subid.SelectedIndex + 1), "",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.None,
                   MessageBoxDefaultButton.Button1);


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

        private bool CheckValue(CmdDataType DataType, double val)
        {
            bool bv = false;

            switch (DataType)
            {
                case CmdDataType.cdtPressureAlarm_Pump:
                    bv = true;
                    break;
                //截面积范围0.00～99.99dm^2 
                //cdtSectionalArea_Value,//油缸截面积 4*8
                case CmdDataType.cdtSectionalArea_Value:
                    bv = (val >= 0 && val <= 99.99);
                    break;
                //最大压力范围：0.0～400.0bar 
                //cdtMAXPressure_Value,//油缸最大压力 4*8
                case CmdDataType.cdtMAXPressure_Value:
                    bv = (val >= 0 && val <= 400);
                    break;
                //最大位移范围：0.0～999.9mm
                //cdtMAXPosition_Value, //油缸最大位移 4*8
                case CmdDataType.cdtMAXPosition_Value:
                    bv = (val >= 0 && val <= 999.9);
                    break;
                //马达最大压力设定：0～400.0bar
                //cdtPumpPressureHighout,          //马达最大压力设定	       4*8
                case CmdDataType.cdtPumpPressureHighout:
                    bv = (val >= 0 && val <= 400);
                    break;
                //油缸最大行程设定值：0.0～999.9mm
                //cdtPumpPositionHighout,          //油缸最大行程设定值		   4*8
                case CmdDataType.cdtPumpPositionHighout:
                    bv = (val >= 0 && val <= 999.9);
                    break;
                //油缸当天行程最大设定值：0.0～999.9mm
                //cdtPumpTodayPositionHighout //油缸当天行程最大设定值 4*8
                case CmdDataType.cdtPumpTodayPositionHighout:
                    bv = (val >= 0 && val <= 999.9);
                    break;
                default:
                    bv = true;
                    break;
            }

            if (!bv)
                MessageBox.Show("输入数值越界！", "",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);

            return bv;
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
                    try
                    {
                        dv = Convert.ToDouble(f.KeyText);

                        if (CheckValue((CmdDataType)lb.Tag, dv))
                            lb.Text = dv.ToString("0.0");
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("输入非法！");
                        MessageBox.Show("输入非法！", "",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                    }
                }
            }
        }
    }
}
