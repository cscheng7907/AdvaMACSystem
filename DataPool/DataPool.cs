using System;
using System.Collections.Generic;
using System.Text;

namespace DataPool
{
    public class CDataPool
    {
        private static CDataPool DataPoolObject = null;

        public static CDataPool GetDataPoolObject()
        {
            if (DataPoolObject == null)
                DataPoolObject = new CDataPool();

            return DataPoolObject;
        }


        private CDataPool()
        {
            //in_Pressure_Real = new List<int>();
            //in_Position_Real = new List<int>();
            //in_cylinderState_Real = new List<StateType>();
            //in_MachLockState_Real = new List<StateType>();

            //in_Limit_5 = new List<bool>();
            //in_Limit_10 = new List<bool>();


            //in_Warn_HighPressure = new List<bool>();
            //in_Warn_LowPressure = new List<bool>();
            //in_Warn_HighPosition = new List<bool>();
            //in_Warn_LowPosition = new List<bool>();

            //in_Error_PressureSenser = new List<bool>();
            //in_Error_PositionSenser = new List<bool>();
            //in_Error_cylinder_extend = new List<bool>();
            //in_Error_cylinder_retract = new List<bool>();
            //in_Error_MachLock_extend = new List<bool>();
            //in_Error_MachLock_retract = new List<bool>();

            //in_Pressure_Pump_Real = new List<int>();
            //in_Voltage_Real = new List<int>();
            //in_PowerSupply = new List<bool>();
            //in_Error_Pump = new List<bool>();


            for (int i = 0; i < Number_Pump; i++)
            {
                for (int j = 0; j < Number_Cylinder; j++)
                {
                    in_Pressure_Real_3001_3008.Add(0);// 油缸压力当前值 4*8
                    in_Position_Real_3101_3108.Add(0);// 油缸当前长度值 4*8

                    in_Limit_5_3301_3304.Add(false);   // 油缸5mm接近开关限位 4*8
                    in_Limit_10_3301_3304.Add(false); // 油缸10mm接近开关限位 4*8

                    in_Warn_HighPressure_3401_3404.Add(false);// 油缸压力过高 4*8
                    in_Warn_LowPressure_3401_3404.Add(false); // 油缸压力过低 4*8
                    in_Warn_HighPosition_3401_3404.Add(false);// 油缸长度过高 4*8
                    in_Warn_LowPosition_3401_3404.Add(false); // 油缸长度过低 4*8

                    in_Error_PressureSenser_3501_3504.Add(false); //油缸压力传感器故障 4*8                          
                    in_Error_PositionSenser_3501_3504.Add(false); //油缸长度传感器故障 4*8                     
                    in_Error_cylinder_extend_3511_3514.Add(false); //油缸伸出电磁阀线路短路 4*8                    
                    in_Error_cylinder_retract_3511_3514.Add(false); //油缸缩回电磁阀线路短路 4*8                     
                    in_Error_MachLock_extend_3511_3514.Add(false); //油缸机械锁伸出电磁阀线路短路 4*8                    
                    in_Error_MachLock_retract_3511_3514.Add(false); //油缸机械锁缩回电磁阀线路短路 4*8                     


                    out_Installed.Add(false); //油缸是否安装 4*8

                    out_PressureUpperLimitAlarm_Enable.Add(false);//油缸压力上限报警功能开启 4*8
                    out_PositionUpperLimitAlarm_Enable.Add(false);//油缸长度上限报警功能开启 4*8
                    out_PositionLowerLimitAlarm_Enable.Add(false);//油缸长度下限报警功能开启 4*8
                    out_PositionControl_Enable.Add(false);//油缸长度控制功能开启 4*8

                    out_PressureUpperLimitAlarm_Value.Add(0);//油缸压力上限报警值设定 4*8
                    out_PressureLowerLimitAlarm_Value.Add(0);//油缸压力下限报警值设定 4*8
                    out_PositionUpperLimitAlarm_Value.Add(0); //油缸长度上限报警值设定 4*8
                    out_PositionLowerLimitAlarm_Value.Add(0); //油缸长度下限报警值设定 4*8

                    out_Pressure_Value.Add(0);//油缸压力设定值 4*8
                    out_Position_Value.Add(0); //油缸长度设定值 4*8

                    out_PositionSenserLow_Value.Add(0);//油缸长度传感器低位值 4*8
                    out_PositionSenserHigh_Value.Add(0);//油缸长度传感器高位值 4*8
                }

                in_Pressure_Pump_Real_3301_3304.Add(0);// 泵站压力 4
                in_Voltage_Real_3301_3304.Add(0);// 控制器电压 4
                in_PowerSupply_3301_3304.Add(false);// 控制器检测当前供电 0：市电 1：发电机 4
                out_PressureAlarm_Pump.Add(0);//泵站压力报警值 4

                for (int j = 0; j < 16; j++)
                {
                    in_Error_Pump_3501_3504.Add(false); //泵站及控制器 故障 4*16
                }
            }
        }

        private uint Number_Pump = 4;
        public uint PumpCount
        {
            get { return Number_Pump; }
        }


        private uint Number_Cylinder = 8;
        public uint CylinderCount
        {
            get { return Number_Cylinder; }
        }



        //in values
        #region CAN 读取的数据
        public List<int> in_Pressure_Real_3001_3008 = new List<int>();// 油缸压力当前值 4*8
        public List<int> in_Position_Real_3101_3108 = new List<int>();// 油缸当前长度值 4*8

        public List<MotionStateType> in_cylinderState_Real_3201_3208 = new List<MotionStateType>();// 油缸运行状态 4*8
        public List<MotionStateType> in_MachLockState_Real_3201_3208 = new List<MotionStateType>();// 油缸机械锁运行状态 4*8

        public List<int> in_Pressure_Pump_Real_3301_3304 = new List<int>();// 泵站压力 4
        public List<int> in_Voltage_Real_3301_3304 = new List<int>();// 控制器电压 4
        public List<bool> in_PowerSupply_3301_3304 = new List<bool>();// 控制器检测当前供电 0：市电 1：发电机 4

        public List<bool> in_Limit_5_3301_3304 = new List<bool>();// 油缸5mm接近开关限位 4*8
        public List<bool> in_Limit_10_3301_3304 = new List<bool>();// 油缸10mm接近开关限位 4*8

        public List<bool> in_Warn_HighPressure_3401_3404 = new List<bool>();// 油缸压力过高 4*8
        public List<bool> in_Warn_LowPressure_3401_3404 = new List<bool>();// 油缸压力过低 4*8
        public List<bool> in_Warn_HighPosition_3401_3404 = new List<bool>();// 油缸长度过高 4*8
        public List<bool> in_Warn_LowPosition_3401_3404 = new List<bool>();// 油缸长度过低 4*8


        public List<bool> in_Error_Pump_3501_3504 = new List<bool>();//泵站及控制器 故障 4*16
        public List<bool> in_Error_PressureSenser_3501_3504 = new List<bool>();//油缸压力传感器故障 4*8
        public List<bool> in_Error_PositionSenser_3501_3504 = new List<bool>();//油缸长度传感器故障 4*8

        public List<bool> in_Error_cylinder_extend_3511_3514 = new List<bool>();//油缸伸出电磁阀线路短路 4*8
        public List<bool> in_Error_cylinder_retract_3511_3514 = new List<bool>();//油缸缩回电磁阀线路短路 4*8
        public List<bool> in_Error_MachLock_extend_3511_3514 = new List<bool>();//油缸机械锁伸出电磁阀线路短路 4*8
        public List<bool> in_Error_MachLock_retract_3511_3514 = new List<bool>();//油缸机械锁缩回电磁阀线路短路 4*8

        #endregion

        //Get function
        #region data 读取接口
        public double GetRealValue(int id, int subid, CmdDataType type)
        {
            double rtv = 0;

            switch (type)
            {
                case CmdDataType.cdtPressure_Real_3001_3008:// 油缸压力当前值 4*8
                    rtv = in_Pressure_Real_3001_3008[id * 8 + subid] * 0.1;

                    break;
                case CmdDataType.cdtPosition_Real_3101_3108:// 油缸当前长度值 4*8
                    rtv = in_Position_Real_3101_3108[id * 8 + subid] * 0.1;

                    break;
                case CmdDataType.cdtPressure_Pump_Real_3301_3304:// 泵站压力 4
                    rtv = in_Pressure_Pump_Real_3301_3304[id] * 0.1;

                    break;
                case CmdDataType.cdtVoltage_Real_3301_3304:// 控制器电压 4
                    rtv = in_Voltage_Real_3301_3304[id] * 0.1;

                    break;

                case CmdDataType.cdtPressureUpperLimitAlarm_Value://油缸压力上限报警值设定 4*8
                    rtv = out_PressureUpperLimitAlarm_Value[id * 8 + subid] * 0.1;
                    break;
                case CmdDataType.cdtPressureLowerLimitAlarm_Value://油缸压力下限报警值设定 4*8
                    rtv = out_PressureLowerLimitAlarm_Value[id * 8 + subid] * 0.1;
                    break;
                case CmdDataType.cdtPositionUpperLimitAlarm_Value: //油缸长度上限报警值设定 4*8
                    rtv = out_PositionUpperLimitAlarm_Value[id * 8 + subid] * 0.1;
                    break;
                case CmdDataType.cdtPositionLowerLimitAlarm_Value: //油缸长度下限报警值设定 4*8
                    rtv = out_PositionLowerLimitAlarm_Value[id * 8 + subid] * 0.1;
                    break;
                case CmdDataType.cdtPressure_Value://油缸压力设定值 4*8
                    rtv = out_Pressure_Value[id * 8 + subid] * 0.1;
                    break;
                case CmdDataType.cdtPosition_Value: //油缸长度设定值 4*8
                    rtv = out_Position_Value[id * 8 + subid] * 0.1;
                    break;
                case CmdDataType.cdtPositionSenserLow_Value://油缸长度传感器低位值 4*8
                    rtv = out_PositionSenserLow_Value[id * 8 + subid] * 0.1;
                    break;
                case CmdDataType.cdtPositionSenserHigh_Value://油缸长度传感器高位值 4*8
                    rtv = out_PositionSenserHigh_Value[id * 8 + subid] * 0.1;
                    break;
                default:
                    break;
            }

            return rtv;
        }

        public int GetintValue(int id, int subid, CmdDataType type)
        {
            int rtv = 0;

            switch (type)
            {
                case CmdDataType.cdtcylinderState_Real_3201_3208:// 油缸运行状态 4*8
                    rtv = (int)in_cylinderState_Real_3201_3208[id * 8 + subid];
                    break;
                case CmdDataType.cdtMachLockState_Real_3201_3208:// 油缸机械锁运行状态 4*8
                    rtv = (int)in_MachLockState_Real_3201_3208[id * 8 + subid];
                    break;
                case CmdDataType.cdtPressureAlarm_Pump://泵站压力报警值 4
                    rtv = (int)out_PressureAlarm_Pump[id];
                    break;
                default:
                    break;
            }

            return 0;
        }

        public bool GetBoolValue(int id, int subid, CmdDataType type)
        {
            bool rtv = false;

            switch (type)
            {
                case CmdDataType.cdtPowerSupply_3301_3304:// 控制器检测当前供电 0：市电 1：发电机 4
                    rtv = in_PowerSupply_3301_3304[id];
                    break;
                case CmdDataType.cdtLimit_5_3301_3304:// 油缸5mm接近开关限位 4*8
                    rtv = in_Limit_5_3301_3304[id * 8 + subid];
                    break;

                case CmdDataType.cdtLimit_10_3301_3304:// 油缸10mm接近开关限位 4*8
                    rtv = in_Limit_10_3301_3304[id * 8 + subid];
                    break;

                case CmdDataType.cdtWarn_HighPressure_3401_3404:// 油缸压力过高 4*8
                    rtv = in_Warn_HighPressure_3401_3404[id * 8 + subid];
                    break;
                case CmdDataType.cdtWarn_LowPressure_3401_3404:// 油缸压力过低 4*8
                    rtv = in_Warn_LowPressure_3401_3404[id * 8 + subid];
                    break;
                case CmdDataType.cdtWarn_HighPosition_3401_3404:// 油缸长度过高 4*8
                    rtv = in_Warn_HighPosition_3401_3404[id * 8 + subid];
                    break;
                case CmdDataType.cdtWarn_LowPosition_3401_3404:// 油缸长度过低 4*8
                    rtv = in_Warn_LowPosition_3401_3404[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_Pump_3501_3504://泵站及控制器 故障 4*16
                    rtv = in_Error_Pump_3501_3504[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_PressureSenser_3501_3504://油缸压力传感器故障 4*8
                    rtv = in_Error_PressureSenser_3501_3504[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_PositionSenser_3501_3504://油缸长度传感器故障 4*8
                    rtv = in_Error_PositionSenser_3501_3504[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_cylinder_extend_3511_3514://油缸伸出电磁阀线路短路 4*8
                    rtv = in_Error_cylinder_extend_3511_3514[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_cylinder_retract_3511_3514://油缸缩回电磁阀线路短路 4*8
                    rtv = in_Error_cylinder_retract_3511_3514[id * 8 + subid];

                    break;
                case CmdDataType.cdtError_MachLock_extend_3511_3514://油缸机械锁伸出电磁阀线路短路 4*8
                    rtv = in_Error_MachLock_extend_3511_3514[id * 8 + subid];

                    break;
                case CmdDataType.cdtError_MachLock_retract_3511_3514://油缸机械锁缩回电磁阀线路短路 4*8
                    rtv = in_Error_MachLock_retract_3511_3514[id * 8 + subid];

                    break;
                case CmdDataType.cdtInstalled://油缸是否安装 4*8
                    rtv = out_Installed[id * 8 + subid];
                    break;
                case CmdDataType.cdtPressureUpperLimitAlarm_Enable://油缸压力上限报警功能开启 4*8
                    rtv = out_PressureUpperLimitAlarm_Enable[id * 8 + subid];
                    break;
                case CmdDataType.cdtPositionUpperLimitAlarm_Enable://油缸长度上限报警功能开启 4*8
                    rtv = out_PositionUpperLimitAlarm_Enable[id * 8 + subid];
                    break;
                case CmdDataType.cdtPositionLowerLimitAlarm_Enable://油缸长度下限报警功能开启 4*8
                    rtv = out_PositionLowerLimitAlarm_Enable[id * 8 + subid];
                    break;
                case CmdDataType.cdtPositionControl_Enable://油缸长度控制功能开启 4*8
                    rtv = out_PositionControl_Enable[id * 8 + subid];
                    break;
                default:
                    break;
            }

            return rtv;
        }

        #endregion

        #region data 发送接口
        public void SetRealValue(int id, int subid, CmdDataType type, double value)
        { }

        public void SetintValue(int id, int subid, CmdDataType type, int value)
        { }

        public void SetboolValue(int id, int subid, CmdDataType type, bool value)
        { }



        #endregion



        //out values
        #region CAN 发送的数据
        public List<bool> out_Installed = new List<bool>();//油缸是否安装 4*8

        public List<byte> out_PressureAlarm_Pump = new List<byte>();//泵站压力报警值 4


        public List<bool> out_PressureUpperLimitAlarm_Enable = new List<bool>();//油缸压力上限报警功能开启 4*8

        public List<bool> out_PositionUpperLimitAlarm_Enable = new List<bool>();//油缸长度上限报警功能开启 4*8
        public List<bool> out_PositionLowerLimitAlarm_Enable = new List<bool>();//油缸长度下限报警功能开启 4*8
        public List<bool> out_PositionControl_Enable = new List<bool>();//油缸长度控制功能开启 4*8

        public List<short> out_PressureUpperLimitAlarm_Value = new List<short>();//油缸压力上限报警值设定 4*8
        public List<short> out_PressureLowerLimitAlarm_Value = new List<short>();//油缸压力下限报警值设定 4*8
        public List<short> out_PositionUpperLimitAlarm_Value = new List<short>(); //油缸长度上限报警值设定 4*8
        public List<short> out_PositionLowerLimitAlarm_Value = new List<short>(); //油缸长度下限报警值设定 4*8

        public List<short> out_Pressure_Value = new List<short>();//油缸压力设定值 4*8
        public List<short> out_Position_Value = new List<short>(); //油缸长度设定值 4*8

        public List<byte> out_PositionSenserLow_Value = new List<byte>();//油缸长度传感器低位值 4*8
        public List<byte> out_PositionSenserHigh_Value = new List<byte>();//油缸长度传感器高位值 4*8

        //-------sign
        public bool sign_View_Setup = false;//进入“安装设定”界面标志位
        public bool sign_View_Setup_Confirm = false;//安装确定标志
        public bool sign_View_Parameter = false; //进入“参数设定”界面标志位
        public bool sign_View_Parameter_Confirm = false; //参数设定确认标志位
        public bool sign_View_SenserCalibration = false;  //进入“传感器标定”界面标志位

        public bool sign_View_PositionSenserLow_Confirm = false;//油缸长度传感器低位值确认
        public bool sign_View_PositionSenserHigh_Confirm = false;//油缸长度传感器高位值确认
        public bool sign_isSame = false;    //单独/统一标定标志位		            5		1	0：每个油缸单独标定；1：所有油缸按同一值标定	
        public int CurId = 0;
        public int CurSubId = 0;
        public ControlModeType ControlMode = ControlModeType.Auto;
        public MotionStateType out_MotionState = MotionStateType.stsStop;

        public Queue<CmdDataSetType> cansendFiFO = new Queue<CmdDataSetType>();
        public void UpdateDevice(int id, int subid, CmdDataType cmd)
        {
            //添加到 队列
        }
        #endregion

        //Init XML 导入结构

        //线程锁
        #region 线程锁
        private bool _locked = false;
        public void Lock()
        { _locked = true; }

        public void UnLock()
        { _locked = false; }

        public bool IsLocked { get { return _locked; } }


        #endregion
    }

    //0:停，1：伸 2：缩

    public enum MotionStateType
    {
        stsStop = 0,
        stsextend = 1,
        stsretract = 2
    }

    //0：表示自动控制方式
    //1：表示油缸伸缩手动控制
    //2：表示机械锁手动控制"	手动控制
    public enum ControlModeType
    {
        Auto = 0,
        CylinderManual = 1,
        MachLockManual = 2
    }

    public enum CmdDataType
    {
        cdtNone,

        cdtPressure_Real_3001_3008,// 油缸压力当前值 4*8
        cdtPosition_Real_3101_3108,// 油缸当前长度值 4*8

        cdtcylinderState_Real_3201_3208,// 油缸运行状态 4*8
        cdtMachLockState_Real_3201_3208,// 油缸机械锁运行状态 4*8

        cdtPressure_Pump_Real_3301_3304,// 泵站压力 4
        cdtVoltage_Real_3301_3304,// 控制器电压 4
        cdtPowerSupply_3301_3304,// 控制器检测当前供电 0：市电 1：发电机 4

        cdtLimit_5_3301_3304,// 油缸5mm接近开关限位 4*8
        cdtLimit_10_3301_3304,// 油缸10mm接近开关限位 4*8

        cdtWarn_HighPressure_3401_3404,// 油缸压力过高 4*8
        cdtWarn_LowPressure_3401_3404,// 油缸压力过低 4*8
        cdtWarn_HighPosition_3401_3404,// 油缸长度过高 4*8
        cdtWarn_LowPosition_3401_3404,// 油缸长度过低 4*8


        cdtError_Pump_3501_3504,//泵站及控制器 故障 4*16
        cdtError_PressureSenser_3501_3504,//油缸压力传感器故障 4*8
        cdtError_PositionSenser_3501_3504,//油缸长度传感器故障 4*8

        cdtError_cylinder_extend_3511_3514,//油缸伸出电磁阀线路短路 4*8
        cdtError_cylinder_retract_3511_3514,//油缸缩回电磁阀线路短路 4*8
        cdtError_MachLock_extend_3511_3514,//油缸机械锁伸出电磁阀线路短路 4*8
        cdtError_MachLock_retract_3511_3514,//油缸机械锁缩回电磁阀线路短路 4*8


        cdtInstalled,//油缸是否安装 4*8

        cdtPressureAlarm_Pump,//泵站压力报警值 4


        cdtPressureUpperLimitAlarm_Enable,//油缸压力上限报警功能开启 4*8

        cdtPositionUpperLimitAlarm_Enable,//油缸长度上限报警功能开启 4*8
        cdtPositionLowerLimitAlarm_Enable,//油缸长度下限报警功能开启 4*8
        cdtPositionControl_Enable,//油缸长度控制功能开启 4*8

        cdtPressureUpperLimitAlarm_Value,//油缸压力上限报警值设定 4*8
        cdtPressureLowerLimitAlarm_Value,//油缸压力下限报警值设定 4*8
        cdtPositionUpperLimitAlarm_Value, //油缸长度上限报警值设定 4*8
        cdtPositionLowerLimitAlarm_Value, //油缸长度下限报警值设定 4*8

        cdtPressure_Value,//油缸压力设定值 4*8
        cdtPosition_Value, //油缸长度设定值 4*8

        cdtPositionSenserLow_Value,//油缸长度传感器低位值 4*8
        cdtPositionSenserHigh_Value//油缸长度传感器高位值 4*8
    }

    public struct CmdDataSetType
    {
        CmdDataType command;
        int id;
        int subid;
    }

}
