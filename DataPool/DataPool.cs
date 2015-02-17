using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DataPool
{
    public class CDataPool
    {
        public CDataPool()
        {
            in_Pressure_Real = new List<int>();
            in_Position_Real = new List<int>();
            in_cylinderState_Real = new List<StateType>();
            in_MachLockState_Real = new List<StateType>();

            in_Limit_5 = new List<bool>();
            in_Limit_10 = new List<bool>();


            in_Warn_HighPressure = new List<bool>();
            in_Warn_LowPressure = new List<bool>();
            in_Warn_HighPosition = new List<bool>();
            in_Warn_LowPosition = new List<bool>();

            in_Error_PressureSenser = new List<bool>();
            in_Error_PositionSenser = new List<bool>();
            in_Error_cylinder_extend = new List<bool>();
            in_Error_cylinder_retract = new List<bool>();
            in_Error_MachLock_extend = new List<bool>();
            in_Error_MachLock_retract = new List<bool>();


            for (int i = 0; i < Number_Pump; i++)
            {
                for (int j = 0; j < Number_Cylinder; j++)
                {
                    in_Pressure_Real.Add(0);
                    in_Position_Real.Add(0);
                    in_Limit_5.Add(false);
                    in_Limit_10.Add(false);
                    in_Warn_HighPressure.Add(false);
                    in_Warn_LowPressure.Add(false);
                    in_Warn_HighPosition.Add(false);
                    in_Warn_LowPosition.Add(false);

                    in_Error_PressureSenser.Add(false);
                    in_Error_PositionSenser.Add(false);
                    in_Error_cylinder_extend.Add(false);
                    in_Error_cylinder_retract.Add(false);
                    in_Error_MachLock_extend.Add(false);
                    in_Error_MachLock_retract.Add(false);
                }
            }

            in_Pressure_Pump_Real = new List<int>();
            in_Voltage_Real = new List<int>();
            in_PowerSupply = new List<bool>();
            in_Error_Pump = new List<bool>();


            for (int i = 0; i < Number_Pump; i++)
            {
                in_Pressure_Pump_Real.Add(0);
                in_Voltage_Real.Add(0);
                in_PowerSupply.Add(false);

                for (int j = 0; j < 16; j++)
                {
                    in_Error_Pump.Add(false);
                }
            }




        }

        private uint Number_Pump = 4;
        private uint Number_Cylinder = 8;

        public List<int> in_Pressure_Real = null;// 油缸压力当前值 4*8
        public List<int> in_Position_Real = null;// 油缸当前长度值 4*8

        public List<StateType> in_cylinderState_Real = null;// 油缸运行状态 4*8
        public List<StateType> in_MachLockState_Real = null;// 油缸机械锁运行状态 4*8

        public List<int> in_Pressure_Pump_Real = null;// 泵站压力 4
        public List<int> in_Voltage_Real = null;// 控制器电压 4
        public List<bool> in_PowerSupply = null;// 控制器检测当前供电 0：市电 1：发电机 4

        public List<bool> in_Limit_5 = null;// 油缸5mm接近开关限位 4*8
        public List<bool> in_Limit_10 = null;// 油缸10mm接近开关限位 4*8

        public List<bool> in_Warn_HighPressure = null;// 油缸压力过高 4*8
        public List<bool> in_Warn_LowPressure = null;// 油缸压力过低 4*8
        public List<bool> in_Warn_HighPosition = null;// 油缸长度过高 4*8
        public List<bool> in_Warn_LowPosition = null;// 油缸长度过低 4*8


        public List<bool> in_Error_Pump = null;//泵站及控制器 故障 4*16

        public List<bool> in_Error_PressureSenser = null;//油缸压力传感器故障 4*8
        public List<bool> in_Error_PositionSenser = null;//油缸长度传感器故障 4*8

        public List<bool> in_Error_cylinder_extend = null;//油缸伸出电磁阀线路短路 4*8
        public List<bool> in_Error_cylinder_retract = null;//油缸缩回电磁阀线路短路 4*8

        public List<bool> in_Error_MachLock_extend = null;//油缸机械锁伸出电磁阀线路短路 4*8
        public List<bool> in_Error_MachLock_retract = null;//油缸机械锁缩回电磁阀线路短路 4*8

        //Get function
        public double GetRealValue(int id, int subid, CmdDataType type)
        {
            double rtv = 0;

            switch (type)
            {
                case CmdDataType.cdtPressure_Real:// 油缸压力当前值 4*8
                    rtv = in_Pressure_Real[id * 8 + subid] * 0.1;

                    break;
                case CmdDataType.cdtPosition_Real:// 油缸当前长度值 4*8
                    rtv = in_Position_Real[id * 8 + subid] * 0.1;

                    break;
                case CmdDataType.cdtPressure_Pump_Real:// 泵站压力 4
                    rtv = in_Pressure_Pump_Real[id] * 0.1;

                    break;
                case CmdDataType.cdtVoltage_Real:// 控制器电压 4
                    rtv = in_Voltage_Real[id] * 0.1;

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
                case CmdDataType.cdtcylinderState_Real:// 油缸运行状态 4*8
                    rtv = (int)in_cylinderState_Real[id * 8 + subid];
                    break;
                case CmdDataType.cdtMachLockState_Real:// 油缸机械锁运行状态 4*8
                    rtv = (int)in_MachLockState_Real[id * 8 + subid];
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
                case CmdDataType.cdtPowerSupply:// 控制器检测当前供电 0：市电 1：发电机 4
                    rtv = in_PowerSupply[id];
                    break;
                case CmdDataType.cdtLimit_5:// 油缸5mm接近开关限位 4*8
                    rtv = in_Limit_5[id * 8 + subid];
                    break;

                case CmdDataType.cdtLimit_10:// 油缸10mm接近开关限位 4*8
                    rtv = in_Limit_10[id * 8 + subid];
                    break;

                case CmdDataType.cdtWarn_HighPressure:// 油缸压力过高 4*8
                    rtv = in_Warn_HighPressure[id * 8 + subid];
                    break;
                case CmdDataType.cdtWarn_LowPressure:// 油缸压力过低 4*8
                    rtv = in_Warn_LowPressure[id * 8 + subid];
                    break;
                case CmdDataType.cdtWarn_HighPosition:// 油缸长度过高 4*8
                    rtv = in_Warn_HighPosition[id * 8 + subid];
                    break;
                case CmdDataType.cdtWarn_LowPosition:// 油缸长度过低 4*8
                    rtv = in_Warn_LowPosition[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_Pump://泵站及控制器 故障 4*16
                    rtv = in_Error_Pump[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_PressureSenser://油缸压力传感器故障 4*8
                    rtv = in_Error_PressureSenser[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_PositionSenser://油缸长度传感器故障 4*8
                    rtv = in_Error_PositionSenser[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_cylinder_extend://油缸伸出电磁阀线路短路 4*8
                    rtv = in_Error_cylinder_extend[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_cylinder_retract://油缸缩回电磁阀线路短路 4*8
                    rtv = in_Error_cylinder_retract[id * 8 + subid];

                    break;
                case CmdDataType.cdtError_MachLock_extend://油缸机械锁伸出电磁阀线路短路 4*8
                    rtv = in_Error_MachLock_extend[id * 8 + subid];

                    break;
                case CmdDataType.cdtError_MachLock_retract://油缸机械锁缩回电磁阀线路短路 4*8
                    rtv = in_Error_MachLock_retract[id * 8 + subid];

                    break;
                default:
                    break;
            }


            return rtv;
        }


        public List<bool> out_Installed  = null;//油缸是否安装 4*8

//        泵站压力报警值 4


//油缸压力上限报警功能开启

//油缸长度上限报警功能开启
//油缸长度下限报警功能开启
//油缸长度控制功能开启

//油缸压力上限报警值设定
//油缸压力下限报警值设定
//油缸长度上限报警值设定
//油缸长度下限报警值设定

//油缸压力设定值
//油缸长度设定值

//油缸长度传感器低位值
//油缸长度传感器高位值

//-------sign
//进入“安装设定”界面标志位
//安装确定标志
//进入“参数设定”界面标志位
//进入“传感器标定”界面标志位

//油缸长度传感器低位值确认
//油缸长度传感器高位值确认


        //Init XML 导入结构



    }

    //0:停，1：伸 2：缩

    public enum StateType
    {
        stsStop = 0,
        stsextend = 1,
        stsretract = 2
    }

    public enum CmdDataType
    {
        cdtPressure_Real,// 油缸压力当前值 4*8
        cdtPosition_Real,// 油缸当前长度值 4*8

        cdtcylinderState_Real,// 油缸运行状态 4*8
        cdtMachLockState_Real,// 油缸机械锁运行状态 4*8

        cdtPressure_Pump_Real,// 泵站压力 4
        cdtVoltage_Real,// 控制器电压 4
        cdtPowerSupply,// 控制器检测当前供电 0：市电 1：发电机 4

        cdtLimit_5,// 油缸5mm接近开关限位 4*8
        cdtLimit_10,// 油缸10mm接近开关限位 4*8

        cdtWarn_HighPressure,// 油缸压力过高 4*8
        cdtWarn_LowPressure,// 油缸压力过低 4*8
        cdtWarn_HighPosition,// 油缸长度过高 4*8
        cdtWarn_LowPosition,// 油缸长度过低 4*8

        cdtError_Pump,//泵站及控制器 故障 4*16

        cdtError_PressureSenser,//油缸压力传感器故障 4*8
        cdtError_PositionSenser,//油缸长度传感器故障 4*8

        cdtError_cylinder_extend,//油缸伸出电磁阀线路短路 4*8
        cdtError_cylinder_retract,//油缸缩回电磁阀线路短路 4*8

        cdtError_MachLock_extend,//油缸机械锁伸出电磁阀线路短路 4*8
        cdtError_MachLock_retract//油缸机械锁缩回电磁阀线路短路 4*8
    }

}
