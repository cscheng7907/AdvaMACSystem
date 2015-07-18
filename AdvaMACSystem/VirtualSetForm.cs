using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataPool;
using ComCtrls;

namespace AdvaMACSystem
{
    public partial class VirtualSetForm : Form
    {
        private int firstcmdtype = (int)CmdDataType.cdtWarn_HighPressure_3401_3404;


        public VirtualSetForm()
        {
            InitializeComponent();

            /*
List<int> in_Pressure_Real_3001_3008 = new List<int>();// 油缸压力当前值 4*8
List<int> in_Position_Real_3101_3108 = new List<int>();// 油缸当前长度值 4*8

List<MotionStateType> in_cylinderState_Real_3201_3208 = new List<MotionStateType>();// 油缸运行状态 4*8
List<MotionStateType> in_MachLockState_Real_3201_3208 = new List<MotionStateType>();// 油缸机械锁运行状态 4*8

List<int> in_Pressure_Pump_Real_3301_3304 = new List<int>();// 泵站压力 4
List<int> in_Voltage_Real_3301_3304 = new List<int>();// 控制器电压 4
List<bool> in_PowerSupply_3301_3304 = new List<bool>();// 控制器检测当前供电 0：市电 1：发电机 4

List<bool> in_Limit_5_3301_3304 = new List<bool>();// 油缸5mm接近开关限位 4*8
List<bool> in_Limit_10_3301_3304 = new List<bool>();// 油缸10mm接近开关限位 4*8

List<bool> in_Warn_HighPressure_3401_3404 = new List<bool>();// 油缸压力过高 4*8
List<bool> in_Warn_LowPressure_3401_3404 = new List<bool>();// 油缸压力过低 4*8
List<bool> in_Warn_HighPosition_3401_3404 = new List<bool>();// 油缸长度过高 4*8
List<bool> in_Warn_LowPosition_3401_3404 = new List<bool>();// 油缸长度过低 4*8


List<bool> in_Error_Pump_3501_3504 = new List<bool>();//泵站及控制器 故障 4*16
List<bool> in_Error_PressureSenser_3501_3504 = new List<bool>();//油缸压力传感器故障 4*8
List<bool> in_Error_PositionSenser_3501_3504 = new List<bool>();//油缸长度传感器故障 4*8

List<bool> in_Error_cylinder_extend_3511_3514 = new List<bool>();//油缸伸出电磁阀线路短路 4*8
List<bool> in_Error_cylinder_retract_3511_3514 = new List<bool>();//油缸缩回电磁阀线路短路 4*8
List<bool> in_Error_MachLock_extend_3511_3514 = new List<bool>();//油缸机械锁伸出电磁阀线路短路 4*8
List<bool> in_Error_MachLock_retract_3511_3514 = new List<bool>();//油缸机械锁缩回电磁阀线路短路 4*8

*/

            id_controledPump.Add(CDataPool.GetDataPoolObject().out_id_controledPump);
            id_redundantPump.Add(CDataPool.GetDataPoolObject().out_id_redundantPump);


            this.SuspendLayout();

            IntDic.Add(il_Presure, CDataPool.GetDataPoolObject().in_Pressure_Real_3001_3008);
            IntDic.Add(il_Position, CDataPool.GetDataPoolObject().in_Position_Real_3101_3108);
            IntDic.Add(il_Pressure_Pump, CDataPool.GetDataPoolObject().in_Pressure_Pump_Real_3301_3304);
            IntDic.Add(il_Voltage, CDataPool.GetDataPoolObject().in_Voltage_Real_3301_3304);
            IntDic.Add(rb__PowerSupply, CDataPool.GetDataPoolObject().in_PowerSupply_3301_3304);
            IntDic.Add(il_idControl, id_controledPump);
            IntDic.Add(il_idrongyu, id_redundantPump);



            doubleDic.Add(comboBoxcylinderState, CDataPool.GetDataPoolObject().in_cylinderState_Real_3201_3208);
            doubleDic.Add(comboBoxMachLockState, CDataPool.GetDataPoolObject().in_MachLockState_Real_3201_3208);

            //boolDic.Add(rb__PowerSupply, CDataPool.GetDataPoolObject().in_PowerSupply_3301_3304);
            boolDic.Add(rb_Limit_5, CDataPool.GetDataPoolObject().in_Limit_5_3301_3304);
            boolDic.Add(rb_Limit_10, CDataPool.GetDataPoolObject().in_Limit_10_3301_3304);
            boolDic.Add(rb_Warn_HighPressure, CDataPool.GetDataPoolObject().in_Warn_HighPressure_3401_3404);
            boolDic.Add(rb_Warn_LowPosition, CDataPool.GetDataPoolObject().in_Warn_LowPosition_3401_3404);
            boolDic.Add(rb_Warn_HighPosition, CDataPool.GetDataPoolObject().in_Warn_HighPosition_3401_3404);
            boolDic.Add(rb_Warn_LowPressure, CDataPool.GetDataPoolObject().in_Warn_LowPressure_3401_3404);
            boolDic.Add(rb_Error_cylinder_extend, CDataPool.GetDataPoolObject().in_Error_cylinder_extend_shortcircuit_3511_3514);
            boolDic.Add(rb_Error_MachLock_retract, CDataPool.GetDataPoolObject().in_Error_MachLock_retract_shortcircuit_3511_3514);
            boolDic.Add(rb_Error_Pump, CDataPool.GetDataPoolObject().in_Error_Pump_3501_3504);
            boolDic.Add(rb_Error_cylinder_retract, CDataPool.GetDataPoolObject().in_Error_cylinder_retract_shortcircuit_3511_3514);
            boolDic.Add(rb_Error_MachLock_extend, CDataPool.GetDataPoolObject().in_Error_MachLock_extend_shortcircuit_3511_3514);
            boolDic.Add(rb_Error_PressureSenser, CDataPool.GetDataPoolObject().in_Error_PressureSenser_3501_3504);
            boolDic.Add(rb_Error_PositionSenser, CDataPool.GetDataPoolObject().in_Error_PositionSenser_3501_3504);

            boolDic.Add(rb_CompAct_Pump, CDataPool.GetDataPoolObject().in_CompAct_Pump_1010_1013);
            boolDic.Add(rb_StartFailed_Pump, CDataPool.GetDataPoolObject().in_StartFailed_Pump_1010_1013);


            WarnLst.Add(rb_Warn_HighPressure);
            WarnLst.Add(rb_Warn_LowPosition);
            WarnLst.Add(rb_Warn_HighPosition);
            WarnLst.Add(rb_Warn_LowPressure);

            ErrLst.Add(rb_Error_cylinder_extend);
            ErrLst.Add(rb_Error_MachLock_retract);
            ErrLst.Add(rb_Error_Pump);
            ErrLst.Add(rb_Error_cylinder_retract);
            ErrLst.Add(rb_Error_MachLock_extend);
            ErrLst.Add(rb_Error_PressureSenser);
            ErrLst.Add(rb_Error_PositionSenser);

            foreach (KeyValuePair<Control, List<MotionStateType>> item in doubleDic)
            {
                ((ComboBox)item.Key).SelectedIndexChanged += new EventHandler(OnSelectChanged);
            }

            foreach (KeyValuePair<Control, List<Int32>> item in IntDic)
            {
                ((ImageLabel)item.Key).Click += new EventHandler(OnClick);
            }

            foreach (KeyValuePair<Control, List<bool>> item in boolDic)
            {
#if WindowsCE

                ((CheckBox)item.Key).CheckStateChanged += new EventHandler(OnCheckedChanged);
#else
                ((CheckBox)item.Key).CheckedChanged += new EventHandler(OnCheckedChanged);
#endif
            }

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox_PumpErr.SelectedIndex = 0;
            this.ResumeLayout(false);


        }

        private Dictionary<Control, List<MotionStateType>> doubleDic = new Dictionary<Control, List<MotionStateType>>();
        private Dictionary<Control, List<Int32>> IntDic = new Dictionary<Control, List<Int32>>();
        private Dictionary<Control, List<bool>> boolDic = new Dictionary<Control, List<bool>>();

        private List<Control> WarnLst = new List<Control>();
        private List<Control> ErrLst = new List<Control>();

        private List<int> id_controledPump = new List<int>();
        private List<int> id_redundantPump = new List<int>();


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoUpdate();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoUpdate();
        }

        private void DoUpdate()
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex >= 0 &&
                 comboBox_PumpErr.SelectedIndex >= 0)
            {
                foreach (KeyValuePair<Control, List<MotionStateType>> item in doubleDic)
                {
                    ((ComboBox)item.Key).SelectedIndex = CDataPool.GetDataPoolObject().GetintValue(
                         comboBox1.SelectedIndex,
                         comboBox2.SelectedIndex,
                         (CmdDataType)item.Key.Tag);
                }

                foreach (KeyValuePair<Control, List<Int32>> item in IntDic)
                {
                    item.Key.Text = CDataPool.GetDataPoolObject().GetRealValue(
                       comboBox1.SelectedIndex,
                       comboBox2.SelectedIndex,
                       (CmdDataType)item.Key.Tag).ToString();
                }

                foreach (KeyValuePair<Control, List<bool>> item in boolDic)
                {
                    if (item.Key == rb_Error_Pump)
                        ((CheckBox)item.Key).Checked = CDataPool.GetDataPoolObject().GetBoolValue(
                                      comboBox1.SelectedIndex,
                                      comboBox_PumpErr.SelectedIndex,
                                      (CmdDataType)item.Key.Tag);

                    else
                        ((CheckBox)item.Key).Checked = CDataPool.GetDataPoolObject().GetBoolValue(
                                      comboBox1.SelectedIndex,
                                      comboBox2.SelectedIndex,
                                      (CmdDataType)item.Key.Tag);
                }


                comboBox_PumpErr.SelectedIndex = 0;
            }
        }


        private void OnClick(object Sender, EventArgs e)
        {
            KeypadForm f = KeypadForm.GetKeypadForm("0", KeypadMode.Normal);
            if (f.ShowDialog() == DialogResult.OK)
            {
                ((ImageLabel)Sender).Text = f.KeyText;

                if (Sender == il_Pressure_Pump ||// 泵站压力 4
                   Sender == rb__PowerSupply ||
                   Sender == il_Voltage)// 控制器电压 4
                {
                    IntDic[(Control)Sender]
                        [comboBox1.SelectedIndex] =
                        Convert.ToInt32(f.KeyText);
                }
                else if (Sender == il_idControl)
                {
                    CDataPool.GetDataPoolObject().out_id_controledPump =
                         Convert.ToByte(f.KeyText);
                }
                else if (Sender == il_idrongyu)
                {                                   
                    CDataPool.GetDataPoolObject().out_id_redundantPump =
                         Convert.ToByte(f.KeyText);
                }
                else
                    //in_Pressure_Pump_Real_3301_3304.Add(0);
                    //in_Voltage_Real_3301_3304.Add(0);
                    IntDic[(Control)Sender]
                        [comboBox1.SelectedIndex * 8 + comboBox2.SelectedIndex] =
                        Convert.ToInt32(f.KeyText);
            }
        }

        private void OnSelectChanged(object Sender, EventArgs e)
        {

            doubleDic[(Control)Sender]
                [comboBox1.SelectedIndex * 8 + comboBox2.SelectedIndex] =
               (MotionStateType)((ComboBox)Sender).SelectedIndex;
        }
        private void OnCheckedChanged(object Sender, EventArgs e)
        {
            if (Sender == rb__PowerSupply)
                boolDic[(Control)Sender]
                   [comboBox1.SelectedIndex] =
                   ((CheckBox)Sender).Checked;
            else
                if (Sender == rb_Error_Pump)
                    boolDic[(Control)Sender]
                        [comboBox1.SelectedIndex * 16 + comboBox_PumpErr.SelectedIndex] =
                        ((CheckBox)Sender).Checked;
                else
                    boolDic[(Control)Sender]
                        [comboBox1.SelectedIndex * 8 + comboBox2.SelectedIndex] =
                        ((CheckBox)Sender).Checked;
        }
        private void comboBox_PumpErr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex >= 0 &&
                 comboBox_PumpErr.SelectedIndex >= 0)
                ((CheckBox)rb_Error_Pump).Checked = CDataPool.GetDataPoolObject().GetBoolValue(
                  comboBox1.SelectedIndex,
                  comboBox_PumpErr.SelectedIndex,
                  (CmdDataType)rb_Error_Pump.Tag);
        }

        private void VirtualSetForm_Click(object sender, EventArgs e)
        {
            label11.Text = "被控泵站 " + CDataPool.GetDataPoolObject().out_id_controledPump.ToString();
            label12.Text = "冗余泵站" + CDataPool.GetDataPoolObject().out_id_redundantPump.ToString();
        }
    }
}