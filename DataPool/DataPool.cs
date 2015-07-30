using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DataPool
{
    public class CDataPool
    {
#if WindowsCE
        public const string DataPoolRecFileName = @"\HardDisk\Record\DataPool.Rec";
#else
        public string DataPoolRecFileName = Application.StartupPath + @"\Record\DataPool.Rec";
#endif

        #region Singleton
        private static CDataPool DataPoolObject = null;

        public static CDataPool GetDataPoolObject()
        {
            if (DataPoolObject == null)
                DataPoolObject = new CDataPool();

            return DataPoolObject;
        }

        private CDataPool()
        {
            Initial_in();
            Initial_out();
        }

        #endregion

        #region Initial vars
        private void Initial_in()
        {
            for (int i = 0; i < Number_Pump; i++)
            {
                for (int j = 0; j < Number_Cylinder; j++)
                {
                    in_Pressure_Real_3001_3008.Add(0);// 油缸压力当前值 4*8
                    in_Position_Real_3101_3108.Add(0);// 油缸当前长度值 4*8

                    in_cylinderState_Real_3201_3208.Add(MotionStateType.stsStop);// 油缸运行状态 4*8
                    in_MachLockState_Real_3201_3208.Add(MotionStateType.stsStop);// 油缸机械锁运行状态 4*8

                    in_Limit_5_3301_3304.Add(false);   // 油缸5mm接近开关限位 4*8
                    in_Limit_10_3301_3304.Add(false); // 油缸10mm接近开关限位 4*8

                    in_Warn_HighPressure_3401_3404.Add(false);// 油缸压力过高 4*8
                    in_Warn_LowPressure_3401_3404.Add(false); // 油缸压力过低 4*8
                    in_Warn_HighPosition_3401_3404.Add(false);// 油缸长度过高 4*8
                    in_Warn_LowPosition_3401_3404.Add(false); // 油缸长度过低 4*8

                    in_Warn_PumpPressureHighout_3401_3404.Add(false);         //泵站马达压力过大 4*8
                    in_Warn_PumpPositionHighout_3401_3404.Add(false);        //泵站油缸行程过大 4*8
                    in_Warn_PumpTodayPositionHighout_3401_3404.Add(false);//泵站油缸当天位移过大 4*8

                    in_Error_PressureSenser_3501_3504.Add(false); //油缸压力传感器故障 4*8                          
                    in_Error_PositionSenser_3501_3504.Add(false); //油缸长度传感器故障 4*8                     
                    in_Error_cylinder_extend_shortcircuit_3511_3514.Add(false); //油缸伸出电磁阀线路短路 4*8                    
                    in_Error_cylinder_retract_shortcircuit_3511_3514.Add(false); //油缸缩回电磁阀线路短路 4*8                     
                    in_Error_MachLock_extend_shortcircuit_3511_3514.Add(false); //油缸机械锁伸出电磁阀线路短路 4*8                    
                    in_Error_MachLock_retract_shortcircuit_3511_3514.Add(false); //油缸机械锁缩回电磁阀线路短路 4*8

                    in_Error_cylinder_extend_opencircuit_3511_3514.Add(false); //油缸伸出电磁阀线路断路 4*8                    
                    in_Error_cylinder_retract_opencircuit_3511_3514.Add(false); //油缸缩回电磁阀线路断路 4*8                     
                    in_Error_MachLock_extend_opencircuit_3511_3514.Add(false); //油缸机械锁伸出电磁阀线路断路 4*8                    
                    in_Error_MachLock_retract_opencircuit_3511_3514.Add(false); //油缸机械锁缩回电磁阀线路断路 4*8                     
                }


                in_Error_pump_motor_shortcircuit_3501_3504.Add(false);                      //泵站电动机启动线路短路  // 4
                in_Error_pump_motor_opencircuit_3501_3504.Add(false);                       //泵站电动机启动线路断路  // 4

                in_Error_pump_electromagneticvalve_shortcircuit_3501_3504.Add(false);       //泵站冗余电磁阀线路短路  // 4
                in_Error_pump_electromagneticvalve_opencircuit_3501_3504.Add(false);        //泵站冗余电磁阀线路断路  // 4

                in_Error_pump_proportionalvalve_shortcircuit_3501_3504.Add(false);          //泵站比例阀线路短路      // 4   
                in_Error_pump_proportionalvalve_opencircuit_3501_3504.Add(false);           //泵站比例阀线路断路      // 4

                in_Error_pump_MachLock_proportionalvalve_shortcircuit_3501_3504.Add(false);//泵站机械锁马达电磁阀线路短路  // 4
                in_Error_pump_MachLock_proportionalvalve_opencircuit_3501_3504.Add(false); //泵站机械锁马达电磁阀线路断路  // 4

                in_Error_controller_dynamo_Start_shortcircuit_3501_3504.Add(false);     //控制器发电机启动线路短路  // 4
                in_Error_controller_dynamo_Start_opencircuit_3501_3504.Add(false);      //控制器发电机启动线路断路  // 4

                in_Error_controller_dynamo_Stop_shortcircuit_3501_3504.Add(false);       //控制器发电机停止线路短路  // 4
                in_Error_controller_dynamo_Stop_opencircuit_3501_3504.Add(false);        //控制器发电机停止线路断路  // 4

                in_Error_controller_warnlight_shortcircuit_3501_3504.Add(false);        //控制器声光报警灯线路短路  // 4
                in_Error_controller_warnlight_opencircuit_3501_3504.Add(false);         //控制器声光报警灯线路断路  // 4


                in_Pressure_Pump_Real_3301_3304.Add(0);// 泵站压力 4
                in_Voltage_Real_3301_3304.Add(0);// 控制器电压 4
                in_PowerSupply_3301_3304.Add(0);// 控制器检测当前供电 0：市电 1：发电机 4

                in_CurPressureHigh_Pump_Real_1010_1013.Add(0);// 泵站当前设定压力高位 4
                in_CurPressureLow_Pump_Real_1010_1013.Add(0); // 泵站当前设定压力低位 4
                in_StartFailed_Pump_1010_1013.Add(false);// 泵站建压失败 4
                in_CompAct_Pump_1010_1013.Add(false); // 泵站补偿动作情况 4

                in_EStop_1010_1013.Add(false); //急停按钮 // 4

                in_Warn_PumpLevelLowout_3401_3404.Add(false);  //泵站油位过低 4
                in_Warn_PumpLevelHighout_3401_3404.Add(false);//泵站油位过高 4
                in_Warn_PumpNotReach_3401_3404.Add(false);//泵站达不到设定值 4


                for (int j = 0; j < 16; j++)
                {
                    in_Error_Pump_3501_3504.Add(false); //泵站及控制器 故障 4*16
                }
            }
        }


        private void Initial_out()
        {
            this.Lock();

            try
            {
                out_Installed.Clear(); //油缸是否安装 4*8

                out_PressureUpperLimitAlarm_Enable.Clear();//油缸压力上限报警功能开启 4*8
                out_PositionUpperLimitAlarm_Enable.Clear();//油缸长度上限报警功能开启 4*8
                out_PositionLowerLimitAlarm_Enable.Clear();//油缸长度下限报警功能开启 4*8
                out_PositionControl_Enable.Clear();//油缸长度控制功能开启 4*8

                out_PressureUpperLimitAlarm_Value.Clear();//油缸压力上限报警值设定 4*8
                out_PressureLowerLimitAlarm_Value.Clear();//油缸压力下限报警值设定 4*8
                out_PositionUpperLimitAlarm_Value.Clear(); //油缸长度上限报警值设定 4*8
                out_PositionLowerLimitAlarm_Value.Clear(); //油缸长度下限报警值设定 4*8

                out_Pressure_Value.Clear();//油缸压力设定值 4*8
                out_Position_Value.Clear(); //油缸长度设定值 4*8

                out_PositionSenserLow_Value.Clear();//油缸长度传感器低位值 4*8
                out_PositionSenserHigh_Value.Clear();//油缸长度传感器高位值 4*8

                View_SetupPosition_Row.Clear();//油缸安装所在的层数 4*8
                View_SetupPosition_Col.Clear();//油缸安装所在的支数 4*8

                out_PressureAlarm_Pump.Clear();//泵站压力报警值 4
                out_Pressure_Pump.Clear();//泵站压力设定值 4

                out_StartPressure_Pump.Clear(); //泵站启动压力设定值 4
                out_ManualStart_Pump.Clear(); //手动启动泵站  4
                sign_View_SetupFinish_Confirm_seperate.Clear();//安装调试完毕确认_油缸 8

                out_PumpPressureHighout.Clear();  //马达最大压力设定	       4*8
                out_PumpPositionHighout.Clear(); //油缸最大行程设定值		   4*8
                out_PumpTodayPositionHighout.Clear(); //油缸当天行程最大设定值 4*8

                ControlMode.Clear();

                for (int i = 0; i < Number_Pump; i++)
                {
                    for (int j = 0; j < Number_Cylinder; j++)
                    {
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


                        View_SetupPosition_Row.Add(0);//油缸安装所在的层数 4*8
                        View_SetupPosition_Col.Add(0);//油缸安装所在的支数 4*8
                        sign_View_SetupFinish_Confirm_seperate.Add(false);//安装调试完毕确认_油缸 4*8

                    }

                    out_PressureAlarm_Pump.Add(0);//泵站压力报警值 4
                    out_Pressure_Pump.Add(0);//泵站压力设定值 4
                    out_StartPressure_Pump.Add(0); //泵站启动压力设定值 4
                    out_ManualStart_Pump.Add(false); //手动启动泵站  4

                    out_SectionalArea_Value.Add(0);  //油缸截面积 4
                    out_MAXPressure_Value.Add(0);  //油缸最大压力 4
                    out_MAXPosition_Value.Add(0);   //油缸最大位移 4

                    out_PumpPressureHighout.Add(0);  //马达最大压力设定	       4
                    out_PumpPositionHighout.Add(0); //油缸最大行程设定值		   4
                    out_PumpTodayPositionHighout.Add(0); //油缸当天行程最大设定值 4

                    ControlMode.Add(ControlModeType.MachLockManual);
                }
            }
            finally
            {
                this.UnLock();
            }
        }

        #endregion

        #region Count属性
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

        #endregion

        //in values
        #region CAN 读取的数据
        public List<int> in_Pressure_Real_3001_3008 = new List<int>();// 油缸压力当前值 4*8
        public List<int> in_Position_Real_3101_3108 = new List<int>();// 油缸当前长度值 4*8

        public List<MotionStateType> in_cylinderState_Real_3201_3208 = new List<MotionStateType>();// 油缸运行状态 4*8
        public List<MotionStateType> in_MachLockState_Real_3201_3208 = new List<MotionStateType>();// 油缸机械锁运行状态 4*8

        public List<int> in_Pressure_Pump_Real_3301_3304 = new List<int>();// 泵站压力 4

        public List<byte> in_CurPressureHigh_Pump_Real_1010_1013 = new List<byte>();// 泵站当前设定压力高位 4
        public List<byte> in_CurPressureLow_Pump_Real_1010_1013 = new List<byte>();// 泵站当前设定压力低位 4
        public List<bool> in_StartFailed_Pump_1010_1013 = new List<bool>();// 泵站建压失败		   4
        public List<bool> in_CompAct_Pump_1010_1013 = new List<bool>();// 泵站补偿动作情况 4


        public List<int> in_Voltage_Real_3301_3304 = new List<int>();// 控制器电压 4
        public List<int> in_PowerSupply_3301_3304 = new List<int>();// 控制器检测当前供电 0：市电 1：发电机 4

        public List<bool> in_Limit_5_3301_3304 = new List<bool>();// 油缸5mm接近开关限位 4*8
        public List<bool> in_Limit_10_3301_3304 = new List<bool>();// 油缸10mm接近开关限位 4*8

        public List<bool> in_Warn_HighPressure_3401_3404 = new List<bool>();// 油缸压力过高 4*8
        public List<bool> in_Warn_LowPressure_3401_3404 = new List<bool>();// 油缸压力过低 4*8
        public List<bool> in_Warn_HighPosition_3401_3404 = new List<bool>();// 油缸长度过高 4*8
        public List<bool> in_Warn_LowPosition_3401_3404 = new List<bool>();// 油缸长度过低 4*8

        public List<bool> in_Warn_PumpLevelLowout_3401_3404 = new List<bool>();//泵站油位过低 4
        public List<bool> in_Warn_PumpLevelHighout_3401_3404 = new List<bool>();//泵站油位过高 4
        public List<bool> in_Warn_PumpNotReach_3401_3404 = new List<bool>();//泵站达不到设定值 4

        public List<bool> in_Warn_PumpPressureHighout_3401_3404 = new List<bool>();//泵站马达压力过大 4*8
        public List<bool> in_Warn_PumpPositionHighout_3401_3404 = new List<bool>();//泵站油缸行程过大 4*8
        public List<bool> in_Warn_PumpTodayPositionHighout_3401_3404 = new List<bool>();//泵站油缸当天位移过大 4*8


        public List<bool> in_Error_Pump_3501_3504 = new List<bool>();//泵站及控制器 故障 4*16


        public List<bool> in_Error_pump_motor_shortcircuit_3501_3504 = new List<bool>();                       //泵站电动机启动线路短路  // 4
        public List<bool> in_Error_pump_motor_opencircuit_3501_3504 = new List<bool>();                        //泵站电动机启动线路断路  // 4

        public List<bool> in_Error_pump_electromagneticvalve_shortcircuit_3501_3504 = new List<bool>();        //泵站冗余电磁阀线路短路  // 4
        public List<bool> in_Error_pump_electromagneticvalve_opencircuit_3501_3504 = new List<bool>();         //泵站冗余电磁阀线路断路  // 4

        public List<bool> in_Error_pump_proportionalvalve_shortcircuit_3501_3504 = new List<bool>();           //泵站比例阀线路短路      // 4   
        public List<bool> in_Error_pump_proportionalvalve_opencircuit_3501_3504 = new List<bool>();            //泵站比例阀线路断路      // 4

        public List<bool> in_Error_pump_MachLock_proportionalvalve_shortcircuit_3501_3504 = new List<bool>(); //泵站机械锁马达电磁阀线路短路  // 4
        public List<bool> in_Error_pump_MachLock_proportionalvalve_opencircuit_3501_3504 = new List<bool>();  //泵站机械锁马达电磁阀线路断路  // 4


        public List<bool> in_Error_controller_dynamo_Start_shortcircuit_3501_3504 = new List<bool>();      //控制器发电机启动线路短路  // 4
        public List<bool> in_Error_controller_dynamo_Start_opencircuit_3501_3504 = new List<bool>();       //控制器发电机启动线路断路  // 4

        public List<bool> in_Error_controller_dynamo_Stop_shortcircuit_3501_3504 = new List<bool>();        //控制器发电机停止线路短路  // 4
        public List<bool> in_Error_controller_dynamo_Stop_opencircuit_3501_3504 = new List<bool>();         //控制器发电机停止线路断路  // 4

        public List<bool> in_Error_controller_warnlight_shortcircuit_3501_3504 = new List<bool>();         //控制器声光报警灯线路短路  // 4
        public List<bool> in_Error_controller_warnlight_opencircuit_3501_3504 = new List<bool>();          //控制器声光报警灯线路断路  // 4


        public List<bool> in_Error_PressureSenser_3501_3504 = new List<bool>();//油缸压力传感器故障 4*8
        public List<bool> in_Error_PositionSenser_3501_3504 = new List<bool>();//油缸长度传感器故障 4*8

        public List<bool> in_Error_cylinder_extend_shortcircuit_3511_3514 = new List<bool>();//油缸伸出电磁阀线路短路 4*8
        public List<bool> in_Error_cylinder_retract_shortcircuit_3511_3514 = new List<bool>();//油缸缩回电磁阀线路短路 4*8
        public List<bool> in_Error_MachLock_extend_shortcircuit_3511_3514 = new List<bool>();//油缸机械锁伸出电磁阀线路短路 4*8
        public List<bool> in_Error_MachLock_retract_shortcircuit_3511_3514 = new List<bool>();//油缸机械锁缩回电磁阀线路短路 4*8

        public List<bool> in_Error_cylinder_extend_opencircuit_3511_3514 = new List<bool>();//油缸伸出电磁阀线路断路 4*8
        public List<bool> in_Error_cylinder_retract_opencircuit_3511_3514 = new List<bool>();//油缸缩回电磁阀线路断路 4*8
        public List<bool> in_Error_MachLock_extend_opencircuit_3511_3514 = new List<bool>();//油缸机械锁伸出电磁阀线路断路 4*8
        public List<bool> in_Error_MachLock_retract_opencircuit_3511_3514 = new List<bool>();//油缸机械锁缩回电磁阀线路断路 4*8

        public List<bool> in_EStop_1010_1013 = new List<bool>();//急停按钮 // 4
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
                case CmdDataType.cdtStartPressure_Pump: //泵站启动轴力设定值 4
                    rtv = out_StartPressure_Pump[id] * 0.1;
                    break;
                case CmdDataType.cdtPressureAlarm_Pump://泵站压力报警值 4
                    rtv = out_PressureAlarm_Pump[id] * 0.1;
                    break;
                case CmdDataType.cdtSectionalArea_Value://油缸截面积 4
                    rtv = out_SectionalArea_Value[id] * 0.01;
                    break;
                case CmdDataType.cdtMAXPressure_Value://油缸最大压力 4
                    rtv = out_MAXPressure_Value[id] * 0.1;
                    break;
                case CmdDataType.cdtMAXPosition_Value: //油缸最大位移 4
                    rtv = out_MAXPosition_Value[id] * 0.1;
                    break;

                case CmdDataType.cdtPumpPressureHighout:          //马达最大压力设定	       4
                    rtv = out_PumpPressureHighout[id] * 0.1;
                    break;
                case CmdDataType.cdtPumpPositionHighout:          //油缸最大行程设定值		   4
                    rtv = out_PumpPositionHighout[id] * 0.1;
                    break;
                case CmdDataType.cdtPumpTodayPositionHighout: //油缸当天行程最大设定值 4
                    rtv = out_PumpTodayPositionHighout[id] * 0.1;
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
                case CmdDataType.cdtPowerSupply_3301_3304:// 控制器检测当前供电 0：市电 1：发电机 4
                    rtv = in_PowerSupply_3301_3304[id];
                    break;
                case CmdDataType.cdtcylinderState_Real_3201_3208:// 油缸运行状态 4*8
                    rtv = (int)in_cylinderState_Real_3201_3208[id * 8 + subid];
                    break;
                case CmdDataType.cdtMachLockState_Real_3201_3208:// 油缸机械锁运行状态 4*8
                    rtv = (int)in_MachLockState_Real_3201_3208[id * 8 + subid];
                    break;
                //case CmdDataType.cdtPressureAlarm_Pump://泵站压力报警值 4
                //    rtv = (int)out_PressureAlarm_Pump[id];
                //    break;
                case CmdDataType.cdtPressure_Pump://泵站压力设定值 4
                    rtv = (int)out_Pressure_Pump[id];
                    break;
                case CmdDataType.cdtView_SetupPosition_Row://油缸安装所在的层数 4*8
                    rtv = (int)View_SetupPosition_Row[id * 8 + subid];
                    break;
                case CmdDataType.cdtView_SetupPosition_Col://油缸安装所在的支数 4*8
                    rtv = (int)View_SetupPosition_Col[id * 8 + subid];
                    break;
                case CmdDataType.cdtid_controledPump://被控泵站
                    rtv = out_id_controledPump;
                    break;
                case CmdDataType.cdtid_redundantPump://冗余泵站
                    rtv = out_id_redundantPump;
                    break;
                default:
                    break;
            }

            return rtv;
        }

        public bool GetBoolValue(int id, int subid, CmdDataType type)
        {
            bool rtv = false;

            switch (type)
            {
                //case CmdDataType.cdtPowerSupply_3301_3304:// 控制器检测当前供电 0：市电 1：发电机 4
                //    rtv = in_PowerSupply_3301_3304[id];
                //    break;
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


                case CmdDataType.cdtError_pump_motor_shortcircuit_3501_3504:                       //泵站电动机启动线路短路  // 4
                    rtv = in_Error_pump_motor_shortcircuit_3501_3504[id];
                    break;
                case CmdDataType.cdtError_pump_motor_opencircuit_3501_3504:                        //泵站电动机启动线路断路  // 4
                    rtv = in_Error_pump_motor_opencircuit_3501_3504[id];
                    break;

                case CmdDataType.cdtError_pump_electromagneticvalve_shortcircuit_3501_3504:        //泵站冗余电磁阀线路短路  // 4
                    rtv = in_Error_pump_electromagneticvalve_shortcircuit_3501_3504[id];
                    break;
                case CmdDataType.cdtError_pump_electromagneticvalve_opencircuit_3501_3504:         //泵站冗余电磁阀线路断路  // 4
                    rtv = in_Error_pump_electromagneticvalve_opencircuit_3501_3504[id];
                    break;

                case CmdDataType.cdtError_pump_proportionalvalve_shortcircuit_3501_3504:           //泵站比例阀线路短路      // 4   
                    rtv = in_Error_pump_proportionalvalve_shortcircuit_3501_3504[id];
                    break;
                case CmdDataType.cdtError_pump_proportionalvalve_opencircuit_3501_3504:            //泵站比例阀线路断路      // 4
                    rtv = in_Error_pump_proportionalvalve_opencircuit_3501_3504[id];
                    break;

                case CmdDataType.cdtError_pump_MachLock_proportionalvalve_shortcircuit_3501_3504: //泵站机械锁马达电磁阀线路短路  // 4
                    rtv = in_Error_pump_MachLock_proportionalvalve_shortcircuit_3501_3504[id];
                    break;
                case CmdDataType.cdtError_pump_MachLock_proportionalvalve_opencircuit_3501_3504:  //泵站机械锁马达电磁阀线路断路  // 4
                    rtv = in_Error_pump_MachLock_proportionalvalve_opencircuit_3501_3504[id];
                    break;


                case CmdDataType.cdtError_controller_dynamo_Start_shortcircuit_3501_3504:      //控制器发电机启动线路短路  // 4
                    rtv = in_Error_controller_dynamo_Start_shortcircuit_3501_3504[id];
                    break;
                case CmdDataType.cdtError_controller_dynamo_Start_opencircuit_3501_3504:       //控制器发电机启动线路断路  // 4
                    rtv = in_Error_controller_dynamo_Start_opencircuit_3501_3504[id];
                    break;

                case CmdDataType.cdtError_controller_dynamo_Stop_shortcircuit_3501_3504:        //控制器发电机停止线路短路  // 4
                    rtv = in_Error_controller_dynamo_Stop_shortcircuit_3501_3504[id];
                    break;
                case CmdDataType.cdtError_controller_dynamo_Stop_opencircuit_3501_3504:         //控制器发电机停止线路断路  // 4
                    rtv = in_Error_controller_dynamo_Stop_opencircuit_3501_3504[id];
                    break;

                case CmdDataType.cdtError_controller_warnlight_shortcircuit_3501_3504:         //控制器声光报警灯线路短路  // 4
                    rtv = in_Error_controller_warnlight_shortcircuit_3501_3504[id];
                    break;
                case CmdDataType.cdtError_controller_warnlight_opencircuit_3501_3504:          //控制器声光报警灯线路断路  // 4
                    rtv = in_Error_controller_warnlight_opencircuit_3501_3504[id];
                    break;


                case CmdDataType.cdtError_PressureSenser_3501_3504://油缸压力传感器故障 4*8
                    rtv = in_Error_PressureSenser_3501_3504[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_PositionSenser_3501_3504://油缸长度传感器故障 4*8
                    rtv = in_Error_PositionSenser_3501_3504[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_cylinder_extend_shortcircuit_3511_3514://油缸伸出电磁阀线路短路 4*8
                    rtv = in_Error_cylinder_extend_shortcircuit_3511_3514[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_cylinder_retract_shortcircuit_3511_3514://油缸缩回电磁阀线路短路 4*8
                    rtv = in_Error_cylinder_retract_shortcircuit_3511_3514[id * 8 + subid];

                    break;
                case CmdDataType.cdtError_MachLock_extend_shortcircuit_3511_3514://油缸机械锁伸出电磁阀线路短路 4*8
                    rtv = in_Error_MachLock_extend_shortcircuit_3511_3514[id * 8 + subid];

                    break;
                case CmdDataType.cdtError_MachLock_retract_shortcircuit_3511_3514://油缸机械锁缩回电磁阀线路短路 4*8
                    rtv = in_Error_MachLock_retract_shortcircuit_3511_3514[id * 8 + subid];

                    break;

                case CmdDataType.cdtError_cylinder_extend_opencircuit_3511_3514://油缸伸出电磁阀线路断路 4*8
                    rtv = in_Error_cylinder_extend_opencircuit_3511_3514[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_cylinder_retract_opencircuit_3511_3514://油缸缩回电磁阀线路断路 4*8
                    rtv = in_Error_cylinder_retract_opencircuit_3511_3514[id * 8 + subid];

                    break;
                case CmdDataType.cdtError_MachLock_extend_opencircuit_3511_3514://油缸机械锁伸出电磁阀线路断路 4*8
                    rtv = in_Error_MachLock_extend_opencircuit_3511_3514[id * 8 + subid];

                    break;
                case CmdDataType.cdtError_MachLock_retract_opencircuit_3511_3514://油缸机械锁缩回电磁阀线路断路 4*8
                    rtv = in_Error_MachLock_retract_opencircuit_3511_3514[id * 8 + subid];

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
                case CmdDataType.cdtManualStart_Pump://手动启动泵站  4
                    rtv = out_ManualStart_Pump[id];
                    break;
                case CmdDataType.cdtEStop_1010_1013:
                    rtv = in_EStop_1010_1013[id];
                    break;
                case CmdDataType.cdtSetupFinish_Confirm_seperate:
                    rtv = sign_View_SetupFinish_Confirm_seperate[id * 8 + subid];
                    break;
                case CmdDataType.cdtStartFailed_Pump_1010_1013:// 泵站建压失败 4
                    rtv = in_StartFailed_Pump_1010_1013[id];
                    break;
                case CmdDataType.cdtCompAct_Pump_1010_1013: // 泵站补偿动作情况 4
                    rtv = in_CompAct_Pump_1010_1013[id];
                    break;
                case CmdDataType.cdt_PumpInstalled:
                    //for (int i = 0; i < 8; i++)
                    //{
                    //    rtv = rtv | out_Installed[id * 8 + i];
                    //}

                    rtv = out_PressureAlarm_Pump[id] != 0;

                    break;
                default:
                    break;
            }

            return rtv;
        }

        #endregion

        #region data 发送接口
        public void SetRealValue(int id, int subid, CmdDataType type, double value)
        {
            switch (type)
            {
                case CmdDataType.cdtPressureUpperLimitAlarm_Value://油缸压力上限报警值设定 4*8
                    out_PressureUpperLimitAlarm_Value[id * 8 + subid] = Convert.ToUInt16(value * 10);
                    break;
                case CmdDataType.cdtPressureLowerLimitAlarm_Value://油缸压力下限报警值设定 4*8
                    out_PressureLowerLimitAlarm_Value[id * 8 + subid] = Convert.ToUInt16(value * 10);
                    break;
                case CmdDataType.cdtPositionUpperLimitAlarm_Value: //油缸长度上限报警值设定 4*8
                    out_PositionUpperLimitAlarm_Value[id * 8 + subid] = Convert.ToInt16(value * 10);
                    break;
                case CmdDataType.cdtPositionLowerLimitAlarm_Value: //油缸长度下限报警值设定 4*8
                    out_PositionLowerLimitAlarm_Value[id * 8 + subid] = Convert.ToInt16(value * 10);
                    break;

                case CmdDataType.cdtPressure_Value://油缸压力设定值 4*8
                    out_Pressure_Value[id * 8 + subid] = Convert.ToUInt16(value * 10);
                    break;
                case CmdDataType.cdtPosition_Value: //油缸长度设定值 4*8
                    out_Position_Value[id * 8 + subid] = Convert.ToInt16(value * 10);
                    break;

                case CmdDataType.cdtPositionSenserLow_Value://油缸长度传感器低位值 4*8
                    out_PositionSenserLow_Value[id * 8 + subid] = Convert.ToByte(value * 10);
                    break;
                case CmdDataType.cdtPositionSenserHigh_Value://油缸长度传感器高位值 4*8
                    out_PositionSenserHigh_Value[id * 8 + subid] = Convert.ToByte(value * 10);
                    break;
                case CmdDataType.cdtStartPressure_Pump://泵站启动压力设定值 4
                    out_StartPressure_Pump[id] = Convert.ToUInt16(value * 10);
                    break;
                case CmdDataType.cdtPressureAlarm_Pump://泵站压力报警值 4
                    out_PressureAlarm_Pump[id] = Convert.ToInt16(value * 10);
                    break;
                case CmdDataType.cdtSectionalArea_Value://油缸截面积 4
                    out_SectionalArea_Value[id] = Convert.ToInt16(value * 100);
                    break;
                case CmdDataType.cdtMAXPressure_Value://油缸最大压力 4
                    out_MAXPressure_Value[id] = Convert.ToInt16(value * 10);
                    break;
                case CmdDataType.cdtMAXPosition_Value: //油缸最大位移 4
                    out_MAXPosition_Value[id] = Convert.ToInt16(value * 10);
                    break;

                case CmdDataType.cdtPumpPressureHighout:          //马达最大压力设定	       4
                    out_PumpPressureHighout[id] = Convert.ToUInt16(value * 10);
                    break;
                case CmdDataType.cdtPumpPositionHighout:          //油缸最大行程设定值		   4
                    out_PumpPositionHighout[id] = Convert.ToUInt16(value * 10);
                    break;
                case CmdDataType.cdtPumpTodayPositionHighout: //油缸当天行程最大设定值 4
                    out_PumpTodayPositionHighout[id] = Convert.ToUInt16(value * 10);
                    break;

                default:
                    break;
            }
        }

        public void SetintValue(int id, int subid, CmdDataType type, int value)
        {
            switch (type)
            {
                //case CmdDataType.cdtPressureAlarm_Pump://泵站压力报警值 4
                //    out_PressureAlarm_Pump[id] = (short)value;
                //    break;
                case CmdDataType.cdtPressure_Pump://泵站压力设定值 4
                    out_Pressure_Pump[id] = (short)value;
                    break;
                case CmdDataType.cdtView_SetupPosition_Row://油缸安装所在的层数 4*8
                    View_SetupPosition_Row[id * 8 + subid] = (byte)value;
                    break;
                case CmdDataType.cdtView_SetupPosition_Col://油缸安装所在的支数 4*8
                    View_SetupPosition_Col[id * 8 + subid] = (byte)value;
                    break;
#if WindowsCE
#else

                case CmdDataType.cdtid_controledPump://被控泵站
                    out_id_controledPump = (byte)value;
                    break;
                case CmdDataType.cdtid_redundantPump://冗余泵站
                    out_id_redundantPump = (byte)value;
                    break;
#endif
                default:
                    break;
            }
        }

        public void SetboolValue(int id, int subid, CmdDataType type, bool value)
        {
            switch (type)
            {
                case CmdDataType.cdtInstalled://油缸是否安装 4*8
                    out_Installed[id * 8 + subid] = value;
                    break;
                case CmdDataType.cdtPressureUpperLimitAlarm_Enable://油缸压力上限报警功能开启 4*8
                    out_PressureUpperLimitAlarm_Enable[id * 8 + subid] = value;
                    break;
                case CmdDataType.cdtPositionUpperLimitAlarm_Enable://油缸长度上限报警功能开启 4*8
                    out_PositionUpperLimitAlarm_Enable[id * 8 + subid] = value;
                    break;
                case CmdDataType.cdtPositionLowerLimitAlarm_Enable://油缸长度下限报警功能开启 4*8
                    out_PositionLowerLimitAlarm_Enable[id * 8 + subid] = value;
                    break;
                case CmdDataType.cdtPositionControl_Enable://油缸长度控制功能开启 4*8
                    out_PositionControl_Enable[id * 8 + subid] = value;
                    break;
                case CmdDataType.cdtManualStart_Pump://手动启动泵站  4
                    out_ManualStart_Pump[id] = value;
                    break;
                case CmdDataType.cdtSetupFinish_Confirm_seperate:
                    sign_View_SetupFinish_Confirm_seperate[id * 8 + subid] = value;
                    break;

                case CmdDataType.cdtStartFailed_Pump_1010_1013:// 泵站建压失败 4
                    in_StartFailed_Pump_1010_1013[id] = value;
                    break;
                case CmdDataType.cdtCompAct_Pump_1010_1013: // 泵站补偿动作情况 4
                    in_CompAct_Pump_1010_1013[id] = value;
                    break;
#if WindowsCE
#else
                case CmdDataType.cdtEStop_1010_1013:
                    in_EStop_1010_1013[id] = value;
                    break;
#endif
                default:
                    break;
            }
        }

        #endregion

        //out values
        #region CAN 发送的数据
        public List<bool> out_Installed = new List<bool>();//油缸是否安装 4*8

        public List<short> out_PressureAlarm_Pump = new List<short>();//泵站压力报警值 4
        public List<short> out_Pressure_Pump = new List<short>();//泵站压力设定值 4

        public List<UInt16> out_StartPressure_Pump = new List<UInt16>();//泵站启动轴力设定值 4
        public List<bool> out_ManualStart_Pump = new List<bool>();//手动启动泵站  4

        public List<bool> out_PressureUpperLimitAlarm_Enable = new List<bool>();//油缸压力上限报警功能开启 4*8

        public List<bool> out_PositionUpperLimitAlarm_Enable = new List<bool>();//油缸长度上限报警功能开启 4*8
        public List<bool> out_PositionLowerLimitAlarm_Enable = new List<bool>();//油缸长度下限报警功能开启 4*8
        public List<bool> out_PositionControl_Enable = new List<bool>();//油缸长度控制功能开启 4*8

        public List<UInt16> out_PressureUpperLimitAlarm_Value = new List<UInt16>();//油缸轴力上限报警值设定 4*8
        public List<UInt16> out_PressureLowerLimitAlarm_Value = new List<UInt16>();//油缸轴力下限报警值设定 4*8
        public List<short> out_PositionUpperLimitAlarm_Value = new List<short>(); //油缸长度上限报警值设定 4*8
        public List<short> out_PositionLowerLimitAlarm_Value = new List<short>(); //油缸长度下限报警值设定 4*8

        public List<UInt16> out_Pressure_Value = new List<UInt16>();//油缸轴力设定值 4*8
        public List<short> out_Position_Value = new List<short>(); //油缸长度设定值 4*8

        public List<byte> out_PositionSenserLow_Value = new List<byte>();//油缸长度传感器低位值 4*8
        public List<byte> out_PositionSenserHigh_Value = new List<byte>();//油缸长度传感器高位值 4*8

        public List<short> out_SectionalArea_Value = new List<short>();//油缸截面积 4
        public List<short> out_MAXPressure_Value = new List<short>();//油缸最大压力 4
        public List<short> out_MAXPosition_Value = new List<short>();//油缸最大位移 4

        public byte out_id_controledPump = 0;//被控泵站
        public byte out_id_redundantPump = 0;//冗余泵站

        public List<UInt16> out_PumpPressureHighout = new List<UInt16>();           //马达最大压力设定	       4*8
        public List<UInt16> out_PumpPositionHighout = new List<UInt16>();           //油缸最大行程设定值		   4*8
        public List<UInt16> out_PumpTodayPositionHighout = new List<UInt16>(); //油缸当天行程最大设定值 4*8

        //-------sign
        public bool sign_View_Setup = false;//进入“安装设定”界面标志位
        public bool sign_View_Setup_Confirm = false;//安装确定标志
        public bool sign_View_Parameter = false; //进入“参数设定”界面标志位
        public bool sign_View_Parameter_Confirm = false; //参数设定确认标志位
        public bool sign_View_SenserCalibration = false;  //进入“传感器标定”界面标志位

        public bool sign_View_PositionSenserLow_Confirm = false;//油缸长度传感器低位值确认
        public bool sign_View_PositionSenserHigh_Confirm = false;//油缸长度传感器高位值确认
        public bool sign_isSame = false;    //单独/统一标定标志位		            5		1	0：每个油缸单独标定；1：所有油缸按同一值标定	
        public bool sign_View_SetupFinish_Confirm = false;//安装调试完毕确定标志
        public List<bool> sign_View_SetupFinish_Confirm_seperate = new List<bool>();//安装调试完毕确认_油缸 4*8

        public bool sign_View_CylinderParameter_Confirm = false; //油缸、马达参数设定确认标志位

        public int CurId = 0;
        public int CurSubId = 0;
#if WindowsCE

        public List<ControlModeType> ControlMode = new List<ControlModeType>();//ControlModeType.Auto;
#else
        public List<ControlModeType> ControlMode = new List<ControlModeType>();//ControlModeType.MachLockManual;

#endif
        public MotionStateType out_MotionState = MotionStateType.stsStop;

        public List<byte> View_SetupPosition_Row = new List<byte>();//油缸安装所在的层数 4*8
        public List<byte> View_SetupPosition_Col = new List<byte>();//油缸安装所在的支数 4*8

        public int View_SetupPosition_RowCount = 0;//油缸安装总层数
        public int View_SetupPosition_ColCount = 0;//油缸安装总支数
        /*
        public Queue<CmdDataSetType> cansendFiFO = new Queue<CmdDataSetType>();
        public void UpdateDevice(int id, int subid, CmdDataType cmd)
        {
            //添加到 队列
        }
         */

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

        #region data 持久化
        /// <summary>
        /// 导入
        /// </summary>
        public void LoadFromFile()
        {
            if (File.Exists(DataPoolRecFileName))
            {
                FileStream fs = new FileStream(DataPoolRecFileName, FileMode.Open);

                long dataSum =
                    //public int View_SetupPosition_RowCount = 0;//油缸安装总层数
                    //public int View_SetupPosition_ColCount = 0; //油缸安装总支数
                            sizeof(int) * 2 +
                    //public List<byte> out_PressureAlarm_Pump  //泵站压力报警值 4
                            sizeof(short) * Number_Pump +
                    //public List<short> out_Pressure_Pump //泵站压力设定值 4
                            sizeof(short) * Number_Pump +
                    //public List<short> out_StartPressure_Pump = new List<short>();//泵站启动压力设定值 4
                            sizeof(short) * Number_Pump +
                    //public List<bool> out_Installed  //油缸是否安装 4*8
                            sizeof(bool) * Number_Pump * Number_Cylinder +
                    //public List<bool> out_PressureUpperLimitAlarm_Enable  //油缸压力上限报警功能开启 4*8
                            sizeof(bool) * Number_Pump * Number_Cylinder +
                    //public List<bool> out_PositionUpperLimitAlarm_Enable  //油缸长度上限报警功能开启 4*8
                            sizeof(bool) * Number_Pump * Number_Cylinder +
                    //public List<bool> out_PositionLowerLimitAlarm_Enable  //油缸长度下限报警功能开启 4*8
                            sizeof(bool) * Number_Pump * Number_Cylinder +
                    //public List<bool> out_PositionControl_Enable  //油缸长度控制功能开启 4*8
                            sizeof(bool) * Number_Pump * Number_Cylinder +
                    //public List<short> out_PressureUpperLimitAlarm_Value  //油缸压力上限报警值设定 4*8
                            sizeof(short) * Number_Pump * Number_Cylinder +
                    //public List<short> out_PressureLowerLimitAlarm_Value  //油缸压力下限报警值设定 4*8
                            sizeof(short) * Number_Pump * Number_Cylinder +
                    //public List<short> out_PositionUpperLimitAlarm_Value   //油缸长度上限报警值设定 4*8
                            sizeof(short) * Number_Pump * Number_Cylinder +
                    //public List<short> out_PositionLowerLimitAlarm_Value //油缸长度下限报警值设定 4*8
                            sizeof(short) * Number_Pump * Number_Cylinder +
                    //public List<short> out_Pressure_Value  //油缸压力设定值 4*8
                            sizeof(short) * Number_Pump * Number_Cylinder +
                    //public List<short> out_Position_Value //油缸长度设定值 4*8
                            sizeof(short) * Number_Pump * Number_Cylinder +
                    //public List<byte> out_PositionSenserLow_Value  //油缸长度传感器低位值 4*8
                            sizeof(byte) * Number_Pump * Number_Cylinder +
                    //public List<byte> out_PositionSenserHigh_Value  //油缸长度传感器高位值 4*8
                            sizeof(byte) * Number_Pump * Number_Cylinder +
                    //public List<byte> View_SetupPosition_Row = new List<byte>();//油缸安装所在的层数 4*8
                           sizeof(byte) * Number_Pump * Number_Cylinder +
                    //public List<byte> View_SetupPosition_Col = new List<byte>();//油缸安装所在的支数 4*8
                           sizeof(byte) * Number_Pump * Number_Cylinder +
                    //public List<short> out_SectionalArea_Value = new List<short>();//油缸截面积 4
                            sizeof(short) * Number_Pump +//* Number_Cylinder +
                    //public List<short> out_MAXPressure_Value = new List<short>();//油缸最大压力 4
                            sizeof(short) * Number_Pump +//* Number_Cylinder +
                    //public List<short> out_MAXPosition_Value = new List<short>();//油缸最大位移 4
                            sizeof(short) * Number_Pump +//* Number_Cylinder +
                    //public List<bool> sign_View_SetupFinish_Confirm_seperate = new List<bool>();//安装调试完毕确认_油缸 4*8
                            sizeof(bool) * Number_Pump * Number_Cylinder +
                    //public List<UInt16> out_PumpPressureHighout = new List<UInt16>();           //马达最大压力设定	       4
                            sizeof(UInt16) * Number_Pump +//* Number_Cylinder +
                    //public List<UInt16> out_PumpPositionHighout = new List<UInt16>();           //油缸最大行程设定值		   4
                            sizeof(UInt16) * Number_Pump +//* Number_Cylinder +
                    //public List<UInt16> out_PumpTodayPositionHighout = new List<UInt16>(); //油缸当天行程最大设定值 4
                            sizeof(UInt16) * Number_Pump;//* Number_Cylinder;

                try
                {
                    if (dataSum == fs.Length)
                    {
                        fs.Position = 0;

                        BinaryReader br = new BinaryReader(fs);
                        try
                        {
                            View_SetupPosition_RowCount = br.ReadInt32();//油缸安装总层数
                            View_SetupPosition_ColCount = br.ReadInt32(); //油缸安装总支数

                            for (int i = 0; i < Number_Pump; i++)
                            {
                                /*List<short>*/
                                out_PressureAlarm_Pump[i] = br.ReadInt16();  //泵站压力报警值 4
                                out_Pressure_Pump[i] = br.ReadInt16();  //泵站压力设定值 4
                                out_StartPressure_Pump[i] = br.ReadUInt16();//泵站启动压力设定值 4
                                out_SectionalArea_Value[i] = br.ReadInt16();//油缸截面积 4
                                out_MAXPressure_Value[i] = br.ReadInt16(); //油缸最大压力 4
                                out_MAXPosition_Value[i] = br.ReadInt16(); //油缸最大位移 4
                                out_PumpPressureHighout[i] = br.ReadUInt16();  //马达最大压力设定	       4
                                out_PumpPositionHighout[i] = br.ReadUInt16(); //油缸最大行程设定值		   4
                                out_PumpTodayPositionHighout[i] = br.ReadUInt16(); //油缸当天行程最大设定值 4
                            }

                            for (int i = 0; i < Number_Pump; i++)
                            {
                                for (int j = 0; j < Number_Cylinder; j++)
                                {
                                    /*List<bool>*/
                                    out_Installed[i * 8 + j] = br.ReadBoolean();   //油缸是否安装 4*8
                                    /*List<bool>*/
                                    out_PressureUpperLimitAlarm_Enable[i * 8 + j] = br.ReadBoolean();   //油缸压力上限报警功能开启 4*8
                                    /*List<bool>*/
                                    out_PositionUpperLimitAlarm_Enable[i * 8 + j] = br.ReadBoolean();   //油缸长度上限报警功能开启 4*8
                                    /*List<bool>*/
                                    out_PositionLowerLimitAlarm_Enable[i * 8 + j] = br.ReadBoolean();   //油缸长度下限报警功能开启 4*8
                                    /*List<bool>*/
                                    out_PositionControl_Enable[i * 8 + j] = br.ReadBoolean();   //油缸长度控制功能开启 4*8
                                    /*List<short>*/
                                    out_PressureUpperLimitAlarm_Value[i * 8 + j] = br.ReadUInt16();  //油缸压力上限报警值设定 4*8
                                    /*List<short>*/
                                    out_PressureLowerLimitAlarm_Value[i * 8 + j] = br.ReadUInt16();   //油缸压力下限报警值设定 4*8
                                    /*List<short>*/
                                    out_PositionUpperLimitAlarm_Value[i * 8 + j] = br.ReadInt16();    //油缸长度上限报警值设定 4*8
                                    /*List<short>*/
                                    out_PositionLowerLimitAlarm_Value[i * 8 + j] = br.ReadInt16();  //油缸长度下限报警值设定 4*8
                                    /*List<short>*/
                                    out_Pressure_Value[i * 8 + j] = br.ReadUInt16();   //油缸压力设定值 4*8
                                    /*List<short>*/
                                    out_Position_Value[i * 8 + j] = br.ReadInt16();  //油缸长度设定值 4*8
                                    /*List<byte>*/
                                    out_PositionSenserLow_Value[i * 8 + j] = br.ReadByte();  //油缸长度传感器低位值 4*8
                                    /*List<byte>*/
                                    out_PositionSenserHigh_Value[i * 8 + j] = br.ReadByte();   //油缸长度传感器高位值 4*8
                                    /*List<byte>*/
                                    View_SetupPosition_Row[i * 8 + j] = br.ReadByte();   //油缸安装所在的层数 4*8
                                    /*List<byte>*/
                                    View_SetupPosition_Col[i * 8 + j] = br.ReadByte();   //油缸安装所在的支数 4*8

                                    sign_View_SetupFinish_Confirm_seperate[i * 8 + j] = br.ReadBoolean();  //安装调试完毕确认_油缸 4*8

                                }
                            }
                        }
                        finally
                        {
                            br.Close();
                        }
                    }
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        public void SavetoFile()
        {
            FileStream fs = new FileStream(DataPoolRecFileName, FileMode.Create);

            BinaryWriter bw = new BinaryWriter(fs);

            try
            {
                bw.Write(View_SetupPosition_RowCount);//油缸安装总层数
                bw.Write(View_SetupPosition_ColCount); //油缸安装总支数

                for (int i = 0; i < Number_Pump; i++)
                {
                    /*List<short>*/
                    bw.Write(out_PressureAlarm_Pump[i]); //泵站压力报警值 4
                    bw.Write(out_Pressure_Pump[i]); //泵站压力设定值 4
                    bw.Write(out_StartPressure_Pump[i]); //泵站启动压力设定值 4
                    bw.Write(out_SectionalArea_Value[i]);//油缸截面积 4
                    bw.Write(out_MAXPressure_Value[i]); //油缸最大压力 4
                    bw.Write(out_MAXPosition_Value[i]); //油缸最大位移 4
                    bw.Write(out_PumpPressureHighout[i]);  //马达最大压力设定	       4
                    bw.Write(out_PumpPositionHighout[i]); //油缸最大行程设定值		   4
                    bw.Write(out_PumpTodayPositionHighout[i]); //油缸当天行程最大设定值 4
                }

                for (int i = 0; i < Number_Pump; i++)
                {
                    for (int j = 0; j < Number_Cylinder; j++)
                    {
                        /*List<bool>*/
                        bw.Write(out_Installed[i * 8 + j]);   //油缸是否安装 4*8
                        /*List<bool>*/
                        bw.Write(out_PressureUpperLimitAlarm_Enable[i * 8 + j]);   //油缸压力上限报警功能开启 4*8
                        /*List<bool>*/
                        bw.Write(out_PositionUpperLimitAlarm_Enable[i * 8 + j]);   //油缸长度上限报警功能开启 4*8
                        /*List<bool>*/
                        bw.Write(out_PositionLowerLimitAlarm_Enable[i * 8 + j]);   //油缸长度下限报警功能开启 4*8
                        /*List<bool>*/
                        bw.Write(out_PositionControl_Enable[i * 8 + j]);   //油缸长度控制功能开启 4*8
                        /*List<short>*/
                        bw.Write(out_PressureUpperLimitAlarm_Value[i * 8 + j]);  //油缸压力上限报警值设定 4*8
                        /*List<short>*/
                        bw.Write(out_PressureLowerLimitAlarm_Value[i * 8 + j]);   //油缸压力下限报警值设定 4*8
                        /*List<short>*/
                        bw.Write(out_PositionUpperLimitAlarm_Value[i * 8 + j]);    //油缸长度上限报警值设定 4*8
                        /*List<short>*/
                        bw.Write(out_PositionLowerLimitAlarm_Value[i * 8 + j]);  //油缸长度下限报警值设定 4*8
                        /*List<short>*/
                        bw.Write(out_Pressure_Value[i * 8 + j]);   //油缸压力设定值 4*8
                        /*List<short>*/
                        bw.Write(out_Position_Value[i * 8 + j]);  //油缸长度设定值 4*8
                        /*List<byte>*/
                        bw.Write(out_PositionSenserLow_Value[i * 8 + j]);  //油缸长度传感器低位值 4*8
                        /*List<byte>*/
                        bw.Write(out_PositionSenserHigh_Value[i * 8 + j]);   //油缸长度传感器高位值 4*8
                        /*List<byte>*/
                        bw.Write(View_SetupPosition_Row[i * 8 + j]);   //油缸安装所在的层数 4*8
                        /*List<byte>*/
                        bw.Write(View_SetupPosition_Col[i * 8 + j]);   //油缸安装所在的支数 4*8

                        bw.Write(sign_View_SetupFinish_Confirm_seperate[i * 8 + j]);  //安装调试完毕确认_油缸 4*8

                    }
                }
            }
            finally
            {
                bw.Flush();
                bw.Close();
                fs.Close();
            }

        }
        #endregion

        #region 复位
        public void Reset()
        {
            Initial_out();
            File.Delete(DataPoolRecFileName);
        }
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
    //2：表示无线遥控
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

        cdtWarn_PumpLevelLowout_3401_3404,//泵站油位过低 4
        cdtWarn_PumpLevelHighout_3401_3404,//泵站油位过高 4
        cdtWarn_PumpNotReach_3401_3404,//泵站达不到设定值 4

        cdtWarn_PumpPressureHighout_3401_3404,//泵站马达压力过大 4*8
        cdtWarn_PumpPositionHighout_3401_3404,//泵站油缸行程过大 4*8
        cdtWarn_PumpTodayPositionHighout_3401_3404,//泵站油缸当天位移过大 4*8


        cdtError_Pump_3501_3504,//泵站及控制器 故障 4*16

        cdtError_pump_motor_shortcircuit_3501_3504,                       //泵站电动机启动线路短路  // 4
        cdtError_pump_motor_opencircuit_3501_3504,                        //泵站电动机启动线路断路  // 4

        cdtError_pump_electromagneticvalve_shortcircuit_3501_3504,        //泵站冗余电磁阀线路短路  // 4
        cdtError_pump_electromagneticvalve_opencircuit_3501_3504,         //泵站冗余电磁阀线路断路  // 4

        cdtError_pump_proportionalvalve_shortcircuit_3501_3504,           //泵站比例阀线路短路      // 4   
        cdtError_pump_proportionalvalve_opencircuit_3501_3504,            //泵站比例阀线路断路      // 4

        cdtError_pump_MachLock_proportionalvalve_shortcircuit_3501_3504, //泵站机械锁马达电磁阀线路短路  // 4
        cdtError_pump_MachLock_proportionalvalve_opencircuit_3501_3504,  //泵站机械锁马达电磁阀线路断路  // 4


        cdtError_controller_dynamo_Start_shortcircuit_3501_3504,      //控制器发电机启动线路短路  // 4
        cdtError_controller_dynamo_Start_opencircuit_3501_3504,       //控制器发电机启动线路断路  // 4

        cdtError_controller_dynamo_Stop_shortcircuit_3501_3504,        //控制器发电机停止线路短路  // 4
        cdtError_controller_dynamo_Stop_opencircuit_3501_3504,         //控制器发电机停止线路断路  // 4

        cdtError_controller_warnlight_shortcircuit_3501_3504,         //控制器声光报警灯线路短路  // 4
        cdtError_controller_warnlight_opencircuit_3501_3504,          //控制器声光报警灯线路断路  // 4


        cdtError_PressureSenser_3501_3504,//油缸压力传感器故障 4*8
        cdtError_PositionSenser_3501_3504,//油缸长度传感器故障 4*8

        cdtError_cylinder_extend_shortcircuit_3511_3514,//油缸伸出电磁阀线路短路 4*8
        cdtError_cylinder_retract_shortcircuit_3511_3514,//油缸缩回电磁阀线路短路 4*8
        cdtError_MachLock_extend_shortcircuit_3511_3514,//油缸机械锁伸出电磁阀线路短路 4*8
        cdtError_MachLock_retract_shortcircuit_3511_3514,//油缸机械锁缩回电磁阀线路短路 4*8

        cdtError_cylinder_extend_opencircuit_3511_3514,//"油缸伸出电磁阀线路断路",// 4*8
        cdtError_cylinder_retract_opencircuit_3511_3514,//"油缸缩回电磁阀线路断路",// 4*8
        cdtError_MachLock_extend_opencircuit_3511_3514,//"油缸机械锁伸出电磁阀线路断路",// 4*8
        cdtError_MachLock_retract_opencircuit_3511_3514,//"油缸机械锁缩回电磁阀线路断路"// 4*8

        cdtEStop_1010_1013,//急停按钮 // 4

        cdtInstalled,//油缸是否安装 4*8

        cdtPressureAlarm_Pump,//泵站压力报警值 4
        cdtPressure_Pump,//泵站压力设定值 4

        cdtStartPressure_Pump, //泵站启动压力设定值 4
        cdtManualStart_Pump,//手动启动泵站  4

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
        cdtPositionSenserHigh_Value,//油缸长度传感器高位值 4*8

        cdtView_SetupPosition_Row,//油缸安装所在的层数 4*8
        cdtView_SetupPosition_Col,//油缸安装所在的支数 4*8

        cdtSectionalArea_Value,//油缸截面积 4*8
        cdtMAXPressure_Value,//油缸最大压力 4*8
        cdtMAXPosition_Value, //油缸最大位移 4*8

        cdtSetupFinish_Confirm_seperate,//安装调试完毕确认_油缸 4*8

        cdtid_controledPump,//被控泵站
        cdtid_redundantPump,//冗余泵站
        cdtStartFailed_Pump_1010_1013,// 泵站建压失败 4
        cdtCompAct_Pump_1010_1013, // 泵站补偿动作情况 4
        cdt_PumpInstalled,//泵站是否安装 4

        cdtPumpPressureHighout,          //马达最大压力设定	       4*8
        cdtPumpPositionHighout,          //油缸最大行程设定值		   4*8
        cdtPumpTodayPositionHighout //油缸当天行程最大设定值 4*8
    }

    /*
    public struct CmdDataSetType
    {
        CmdDataType command;
        int id;
        int subid;
    }
    */

}
