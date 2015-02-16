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
            Pressure_Real = new List<int>();
            Position_Real = new List<int>();
            cylinderState_Real = new List<StateType>();
            MachLockState_Real = new List<StateType>();

            Limit_5 = new List<bool>();
            Limit_10 = new List<bool>();


            Warn_HighPressure = new List<bool>();
            Warn_LowPressure = new List<bool>();
            Warn_HighPosition = new List<bool>();
            Warn_LowPosition = new List<bool>();

            Error_PressureSenser = new List<bool>();
            Error_PositionSenser = new List<bool>();
            Error_cylinder_extend = new List<bool>();
            Error_cylinder_retract = new List<bool>();
            Error_MachLock_extend = new List<bool>();
            Error_MachLock_retract = new List<bool>();


            for (int i = 0; i < Number_Pump; i++)
            {
                for (int j = 0; j < Number_Cylinder; j++)
                {
                    Pressure_Real.Add(0);
                    Position_Real.Add(0);
                    Limit_5.Add(false);
                    Limit_10.Add(false);
                    Warn_HighPressure.Add(false);
                    Warn_LowPressure.Add(false);
                    Warn_HighPosition.Add(false);
                    Warn_LowPosition.Add(false);

                    Error_PressureSenser.Add(false);
                    Error_PositionSenser.Add(false);
                    Error_cylinder_extend.Add(false);
                    Error_cylinder_retract.Add(false);
                    Error_MachLock_extend.Add(false);
                    Error_MachLock_retract.Add(false);
                }
            }

            Pressure_Pump_Real = new List<int>();
            Voltage_Real = new List<int>();
            PowerSupply = new List<bool>();
            Error_Pump = new List<bool>();


            for (int i = 0; i < Number_Pump; i++)
            {
                Pressure_Pump_Real.Add(0);
                Voltage_Real.Add(0);
                PowerSupply.Add(false);

                for (int j = 0; j < 16; j++)
                {
                    Error_Pump.Add(false);
                }
            }




        }

        private uint Number_Pump = 4;
        private uint Number_Cylinder = 8;

        public List<int> Pressure_Real = null;// 油缸压力当前值 4*8
        public List<int> Position_Real = null;// 油缸当前长度值 4*8

        public List<StateType> cylinderState_Real = null;// 油缸运行状态 4*8
        public List<StateType> MachLockState_Real = null;// 油缸机械锁运行状态 4*8

        public List<int> Pressure_Pump_Real = null;// 泵站压力 4
        public List<int> Voltage_Real = null;// 控制器电压 4
        public List<bool> PowerSupply = null;// 控制器检测当前供电 0：市电 1：发电机 4

        public List<bool> Limit_5 = null;// 油缸5mm接近开关限位 4*8
        public List<bool> Limit_10 = null;// 油缸10mm接近开关限位 4*8

        public List<bool> Warn_HighPressure = null;// 油缸压力过高 4*8
        public List<bool> Warn_LowPressure = null;// 油缸压力过低 4*8
        public List<bool> Warn_HighPosition = null;// 油缸长度过高 4*8
        public List<bool> Warn_LowPosition = null;// 油缸长度过低 4*8


        public List<bool> Error_Pump = null;//泵站及控制器 故障 4*16

        public List<bool> Error_PressureSenser = null;//油缸压力传感器故障 4*8
        public List<bool> Error_PositionSenser = null;//油缸长度传感器故障 4*8

        public List<bool> Error_cylinder_extend = null;//油缸伸出电磁阀线路短路 4*8
        public List<bool> Error_cylinder_retract = null;//油缸缩回电磁阀线路短路 4*8

        public List<bool> Error_MachLock_extend = null;//油缸机械锁伸出电磁阀线路短路 4*8
        public List<bool> Error_MachLock_retract = null;//油缸机械锁缩回电磁阀线路短路 4*8

        //Get function
        public double GetRealValue(int id, int subid, CmdDataType type)
        {
            double rtv = 0;

            switch (type)
            {
                case CmdDataType.cdtPressure_Real:// 油缸压力当前值 4*8
                    rtv = Pressure_Real[id * 8 + subid] * 0.1;

                    break;
                case CmdDataType.cdtPosition_Real:// 油缸当前长度值 4*8
                    rtv = Position_Real[id * 8 + subid] * 0.1;

                    break;
                case CmdDataType.cdtPressure_Pump_Real:// 泵站压力 4
                    rtv = Pressure_Pump_Real[id] * 0.1;

                    break;
                case CmdDataType.cdtVoltage_Real:// 控制器电压 4
                    rtv = Voltage_Real[id] * 0.1;

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
                    rtv = (int)cylinderState_Real[id * 8 + subid];
                    break;
                case CmdDataType.cdtMachLockState_Real:// 油缸机械锁运行状态 4*8
                    rtv = (int)MachLockState_Real[id * 8 + subid];
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
                    rtv = PowerSupply[id];
                    break;
                case CmdDataType.cdtLimit_5:// 油缸5mm接近开关限位 4*8
                    rtv = Limit_5[id * 8 + subid];
                    break;

                case CmdDataType.cdtLimit_10:// 油缸10mm接近开关限位 4*8
                    rtv = Limit_10[id * 8 + subid];
                    break;

                case CmdDataType.cdtWarn_HighPressure:// 油缸压力过高 4*8
                    rtv = Warn_HighPressure[id * 8 + subid];
                    break;
                case CmdDataType.cdtWarn_LowPressure:// 油缸压力过低 4*8
                    rtv = Warn_LowPressure[id * 8 + subid];
                    break;
                case CmdDataType.cdtWarn_HighPosition:// 油缸长度过高 4*8
                    rtv = Warn_HighPosition[id * 8 + subid];
                    break;
                case CmdDataType.cdtWarn_LowPosition:// 油缸长度过低 4*8
                    rtv = Warn_LowPosition[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_Pump://泵站及控制器 故障 4*16
                    rtv = Error_Pump[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_PressureSenser://油缸压力传感器故障 4*8
                    rtv = Error_PressureSenser[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_PositionSenser://油缸长度传感器故障 4*8
                    rtv = Error_PositionSenser[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_cylinder_extend://油缸伸出电磁阀线路短路 4*8
                    rtv = Error_cylinder_extend[id * 8 + subid];
                    break;

                case CmdDataType.cdtError_cylinder_retract://油缸缩回电磁阀线路短路 4*8
                    rtv = Error_cylinder_retract[id * 8 + subid];

                    break;
                case CmdDataType.cdtError_MachLock_extend://油缸机械锁伸出电磁阀线路短路 4*8
                    rtv = Error_MachLock_extend[id * 8 + subid];

                    break;
                case CmdDataType.cdtError_MachLock_retract://油缸机械锁缩回电磁阀线路短路 4*8
                    rtv = Error_MachLock_retract[id * 8 + subid];

                    break;
                default:
                    break;
            }


            return rtv;
        }

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
