﻿/*AdvaMACSystem 监控软件
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
using System.Text;
using DataPool;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace AdvaMACSystem
{
    public class WarnErrOperator
    {
#if WindowsCE
        public string WarningRecFileName = @"\HardDisk\Record\Warning.Rec";
        public string ErrorRecFileName = @"\HardDisk\Record\Error.Rec";
#else
        public string WarningRecFileName = Application.StartupPath + @"\Record\Warning.Rec";
        public string ErrorRecFileName = Application.StartupPath + @"\Record\Error.Rec";
#endif

        public WarnErrOperator()
        {
            dic.Add(CmdDataType.cdtWarn_HighPressure_3401_3404, _Warn_HighPressure_3401_3404);// 油缸压力过高 4*8
            dic.Add(CmdDataType.cdtWarn_LowPressure_3401_3404, _Warn_LowPressure_3401_3404);// 油缸压力过低 4*8
            dic.Add(CmdDataType.cdtWarn_HighPosition_3401_3404, _Warn_HighPosition_3401_3404);// 油缸长度过高 4*8
            dic.Add(CmdDataType.cdtWarn_LowPosition_3401_3404, _Warn_LowPosition_3401_3404);// 油缸长度过低 4*8

            //dic.Add(CmdDataType.cdtError_Pump_3501_3504,_Error_Pump_3501_3504);//泵站及控制器 故障 4*16
            dic.Add(CmdDataType.cdtError_PressureSenser_3501_3504, _Error_PressureSenser_3501_3504);//油缸压力传感器故障 4*8
            dic.Add(CmdDataType.cdtError_PositionSenser_3501_3504, _Error_PositionSenser_3501_3504);//油缸长度传感器故障 4*8



            dic4.Add(CmdDataType.cdtError_pump_motor_shortcircuit_3501_3504, _Error_pump_motor_shortcircuit_3501_3504);                    //泵站电动机启动线路短路  // 4
            dic4.Add(CmdDataType.cdtError_pump_motor_opencircuit_3501_3504, _Error_pump_motor_opencircuit_3501_3504);                      //泵站电动机启动线路断路  // 4

            dic4.Add(CmdDataType.cdtError_pump_electromagneticvalve_shortcircuit_3501_3504, _Error_pump_electromagneticvalve_shortcircuit_3501_3504);      //泵站冗余电磁阀线路短路  // 4
            dic4.Add(CmdDataType.cdtError_pump_electromagneticvalve_opencircuit_3501_3504, _Error_pump_electromagneticvalve_opencircuit_3501_3504);       //泵站冗余电磁阀线路断路  // 4

            dic4.Add(CmdDataType.cdtError_pump_proportionalvalve_shortcircuit_3501_3504, _Error_pump_proportionalvalve_shortcircuit_3501_3504);         //泵站比例阀线路短路      // 4   
            dic4.Add(CmdDataType.cdtError_pump_proportionalvalve_opencircuit_3501_3504, _Error_pump_proportionalvalve_opencircuit_3501_3504);           //泵站比例阀线路断路      // 4

            dic4.Add(CmdDataType.cdtError_pump_MachLock_proportionalvalve_shortcircuit_3501_3504, _Error_pump_MachLock_proportionalvalve_shortcircuit_3501_3504);//泵站机械锁马达电磁阀线路短路  // 4
            dic4.Add(CmdDataType.cdtError_pump_MachLock_proportionalvalve_opencircuit_3501_3504, _Error_pump_MachLock_proportionalvalve_opencircuit_3501_3504); //泵站机械锁马达电磁阀线路断路  // 4


            dic4.Add(CmdDataType.cdtError_controller_dynamo_Start_shortcircuit_3501_3504, _Error_controller_dynamo_Start_shortcircuit_3501_3504);    //控制器发电机启动线路短路  // 4
            dic4.Add(CmdDataType.cdtError_controller_dynamo_Start_opencircuit_3501_3504, _Error_controller_dynamo_Start_opencircuit_3501_3504);     //控制器发电机启动线路断路  // 4

            dic4.Add(CmdDataType.cdtError_controller_dynamo_Stop_shortcircuit_3501_3504, _Error_controller_dynamo_Stop_shortcircuit_3501_3504);     //控制器发电机停止线路短路  // 4
            dic4.Add(CmdDataType.cdtError_controller_dynamo_Stop_opencircuit_3501_3504, _Error_controller_dynamo_Stop_opencircuit_3501_3504);     //控制器发电机停止线路断路  // 4

            dic4.Add(CmdDataType.cdtError_controller_warnlight_shortcircuit_3501_3504, _Error_controller_warnlight_shortcircuit_3501_3504);       //控制器声光报警灯线路短路  // 4
            dic4.Add(CmdDataType.cdtError_controller_warnlight_opencircuit_3501_3504, _Error_controller_warnlight_opencircuit_3501_3504);        //控制器声光报警灯线路断路  // 4


            dic.Add(CmdDataType.cdtError_cylinder_extend_shortcircuit_3511_3514, _Error_cylinder_extend_3511_3514);//油缸伸出电磁阀线路短路 4*8
            dic.Add(CmdDataType.cdtError_cylinder_retract_shortcircuit_3511_3514, _Error_cylinder_retract_3511_3514);//油缸缩回电磁阀线路短路 4*8
            dic.Add(CmdDataType.cdtError_MachLock_extend_shortcircuit_3511_3514, _Error_MachLock_extend_3511_3514);//油缸机械锁伸出电磁阀线路短路 4*8
            dic.Add(CmdDataType.cdtError_MachLock_retract_shortcircuit_3511_3514, _Error_MachLock_retract_3511_3514);//油缸机械锁缩回电磁阀线路短路 4*8
        }

        #region 缓存
        private List<bool> _Warn_HighPressure_3401_3404 = new List<bool>();// 油缸压力过高 4*8
        private List<bool> _Warn_LowPressure_3401_3404 = new List<bool>();// 油缸压力过低 4*8
        private List<bool> _Warn_HighPosition_3401_3404 = new List<bool>();// 油缸长度过高 4*8
        private List<bool> _Warn_LowPosition_3401_3404 = new List<bool>();// 油缸长度过低 4*8


        private List<bool> _Error_Pump_3501_3504 = new List<bool>();//泵站及控制器 故障 4*16
        private List<bool> _Error_PressureSenser_3501_3504 = new List<bool>();//油缸压力传感器故障 4*8
        private List<bool> _Error_PositionSenser_3501_3504 = new List<bool>();//油缸长度传感器故障 4*8

        public List<bool> _Error_pump_motor_shortcircuit_3501_3504 = new List<bool>();                       //泵站电动机启动线路短路  // 4
        public List<bool> _Error_pump_motor_opencircuit_3501_3504 = new List<bool>();                        //泵站电动机启动线路断路  // 4

        public List<bool> _Error_pump_electromagneticvalve_shortcircuit_3501_3504 = new List<bool>();        //泵站冗余电磁阀线路短路  // 4
        public List<bool> _Error_pump_electromagneticvalve_opencircuit_3501_3504 = new List<bool>();         //泵站冗余电磁阀线路断路  // 4

        public List<bool> _Error_pump_proportionalvalve_shortcircuit_3501_3504 = new List<bool>();           //泵站比例阀线路短路      // 4   
        public List<bool> _Error_pump_proportionalvalve_opencircuit_3501_3504 = new List<bool>();            //泵站比例阀线路断路      // 4

        public List<bool> _Error_pump_MachLock_proportionalvalve_shortcircuit_3501_3504 = new List<bool>(); //泵站机械锁马达电磁阀线路短路  // 4
        public List<bool> _Error_pump_MachLock_proportionalvalve_opencircuit_3501_3504 = new List<bool>();  //泵站机械锁马达电磁阀线路断路  // 4


        public List<bool> _Error_controller_dynamo_Start_shortcircuit_3501_3504 = new List<bool>();      //控制器发电机启动线路短路  // 4
        public List<bool> _Error_controller_dynamo_Start_opencircuit_3501_3504 = new List<bool>();       //控制器发电机启动线路断路  // 4

        public List<bool> _Error_controller_dynamo_Stop_shortcircuit_3501_3504 = new List<bool>();        //控制器发电机停止线路短路  // 4
        public List<bool> _Error_controller_dynamo_Stop_opencircuit_3501_3504 = new List<bool>();         //控制器发电机停止线路断路  // 4

        public List<bool> _Error_controller_warnlight_shortcircuit_3501_3504 = new List<bool>();         //控制器声光报警灯线路短路  // 4
        public List<bool> _Error_controller_warnlight_opencircuit_3501_3504 = new List<bool>();          //控制器声光报警灯线路断路  // 4










        private List<bool> _Error_cylinder_extend_3511_3514 = new List<bool>();//油缸伸出电磁阀线路短路 4*8
        private List<bool> _Error_cylinder_retract_3511_3514 = new List<bool>();//油缸缩回电磁阀线路短路 4*8
        private List<bool> _Error_MachLock_extend_3511_3514 = new List<bool>();//油缸机械锁伸出电磁阀线路短路 4*8
        private List<bool> _Error_MachLock_retract_3511_3514 = new List<bool>();//油缸机械锁缩回电磁阀线路短路 4*8

        Dictionary<CmdDataType, List<bool>> dic = new Dictionary<CmdDataType, List<bool>>();
        Dictionary<CmdDataType, List<bool>> dic4 = new Dictionary<CmdDataType, List<bool>>();

        #endregion


        private CDataPool _candatapool = null;
        public CDataPool CanDatapool
        {
            set
            {
                if (_candatapool != value)
                {

                    _candatapool = value;

                    if (_candatapool != null)
                        InitData();
                }
            }
        }


        /// <summary>
        /// key 组合件 
        ///                 id      subid       CmdDataType
        /// 数据位      XX        XX                XX
        /// </summary>
        private Dictionary<int, DateTime> _curwarninglist = new Dictionary<int, DateTime>();
        public Dictionary<int, DateTime> CurWarningList
        {
            get { return _curwarninglist; }
        }


        /// <summary>
        /// key 组合件 
        ///                 id      subid       CmdDataType
        /// 数据位      XX        XX                XX
        /// </summary>
        private Dictionary<int, DateTime> _curerrorlist = new Dictionary<int, DateTime>();
        public Dictionary<int, DateTime> CurErrorList
        {
            get { return _curerrorlist; }
        }





        private void InitData()
        {
            _Warn_HighPressure_3401_3404.Clear();
            _Warn_LowPressure_3401_3404.Clear();
            _Warn_HighPosition_3401_3404.Clear();
            _Warn_LowPosition_3401_3404.Clear();

            _Error_Pump_3501_3504.Clear();
            _Error_PressureSenser_3501_3504.Clear();
            _Error_PositionSenser_3501_3504.Clear();

            _Error_cylinder_extend_3511_3514.Clear();
            _Error_cylinder_retract_3511_3514.Clear();
            _Error_MachLock_extend_3511_3514.Clear();
            _Error_MachLock_retract_3511_3514.Clear();

            for (int i = 0; i < _candatapool.PumpCount; i++)
            {
                for (int j = 0; j < _candatapool.CylinderCount; j++)
                {

                    _Warn_HighPressure_3401_3404.Add(false);// 油缸压力过高 4*8
                    _Warn_LowPressure_3401_3404.Add(false); // 油缸压力过低 4*8
                    _Warn_HighPosition_3401_3404.Add(false);// 油缸长度过高 4*8
                    _Warn_LowPosition_3401_3404.Add(false); // 油缸长度过低 4*8

                    _Error_PressureSenser_3501_3504.Add(false); //油缸压力传感器故障 4*8                          
                    _Error_PositionSenser_3501_3504.Add(false); //油缸长度传感器故障 4*8                     
                    _Error_cylinder_extend_3511_3514.Add(false); //油缸伸出电磁阀线路短路 4*8                    
                    _Error_cylinder_retract_3511_3514.Add(false); //油缸缩回电磁阀线路短路 4*8                     
                    _Error_MachLock_extend_3511_3514.Add(false); //油缸机械锁伸出电磁阀线路短路 4*8                    
                    _Error_MachLock_retract_3511_3514.Add(false); //油缸机械锁缩回电磁阀线路短路 4*8
                }

                _Error_pump_motor_shortcircuit_3501_3504.Add(false);                      //泵站电动机启动线路短路  // 4
                _Error_pump_motor_opencircuit_3501_3504.Add(false);                       //泵站电动机启动线路断路  // 4

                _Error_pump_electromagneticvalve_shortcircuit_3501_3504.Add(false);       //泵站冗余电磁阀线路短路  // 4
                _Error_pump_electromagneticvalve_opencircuit_3501_3504.Add(false);        //泵站冗余电磁阀线路断路  // 4

                _Error_pump_proportionalvalve_shortcircuit_3501_3504.Add(false);          //泵站比例阀线路短路      // 4   
                _Error_pump_proportionalvalve_opencircuit_3501_3504.Add(false);           //泵站比例阀线路断路      // 4

                _Error_pump_MachLock_proportionalvalve_shortcircuit_3501_3504.Add(false);//泵站机械锁马达电磁阀线路短路  // 4
                _Error_pump_MachLock_proportionalvalve_opencircuit_3501_3504.Add(false); //泵站机械锁马达电磁阀线路断路  // 4

                _Error_controller_dynamo_Start_shortcircuit_3501_3504.Add(false);     //控制器发电机启动线路短路  // 4
                _Error_controller_dynamo_Start_opencircuit_3501_3504.Add(false);      //控制器发电机启动线路断路  // 4

                _Error_controller_dynamo_Stop_shortcircuit_3501_3504.Add(false);       //控制器发电机停止线路短路  // 4
                _Error_controller_dynamo_Stop_opencircuit_3501_3504.Add(false);        //控制器发电机停止线路断路  // 4

                _Error_controller_warnlight_shortcircuit_3501_3504.Add(false);        //控制器声光报警灯线路短路  // 4
                _Error_controller_warnlight_opencircuit_3501_3504.Add(false);         //控制器声光报警灯线路断路  // 4

                for (int j = 0; j < 16; j++)
                {
                    _Error_Pump_3501_3504.Add(false); //泵站及控制器 故障 4*16
                }
            }

            id_controledPump = _candatapool.out_id_controledPump; //被控泵站            

            id_redundantPump = _candatapool.out_id_redundantPump;//冗余泵站

        }

        public void Update()
        {
            bool cur = false;
            bool ischanged = false;

            if (_candatapool != null)
            {
                foreach (KeyValuePair<CmdDataType, List<bool>> item in dic)
                {
                    for (int i = 0; i < _candatapool.PumpCount; i++)
                    {
                        for (int j = 0; j < _candatapool.CylinderCount; j++)
                        {
                            cur = _candatapool.GetBoolValue(i, j, item.Key);
                            if (cur != item.Value[i * 8 + j])
                            {
                                item.Value[i * 8 + j] = cur;

                                SaveChangedData(i, j, item.Key, cur);

                                ischanged = true;
                            }
                        }
                    }
                }

                foreach (KeyValuePair<CmdDataType, List<bool>> item in dic4)
                {
                    for (int i = 0; i < _candatapool.PumpCount; i++)
                    {
                        cur = _candatapool.GetBoolValue(i, 0, item.Key);
                        if (cur != item.Value[i])
                        {
                            item.Value[i] = cur;

                            SaveChangedData(i, 0, item.Key, cur);

                            ischanged = true;
                        }
                    }
                }



                //for (int i = 0; i < _candatapool.PumpCount; i++)
                //{
                //    for (int j = 0; j < 16; j++)
                //    {
                //        cur = _candatapool.GetBoolValue(i, j, CmdDataType.cdtError_Pump_3501_3504);//泵站及控制器 故障 4*16

                //        if (cur != _Error_Pump_3501_3504[i * 16 + j])
                //        {
                //            _Error_Pump_3501_3504[i * 16 + j] = cur;

                //            SaveChangedData(i, j, CmdDataType.cdtError_Pump_3501_3504, cur);
                //            ischanged = true;
                //        }
                //    }
                //}
                ID_controledPump = _candatapool.out_id_controledPump;//被控泵站

                ID_redundantPump = _candatapool.out_id_redundantPump;//冗余泵站

                if (ischanged)
                    DoDataChanged();
            }

        }

        private CmdDataType[] WarnDataTypeList = new CmdDataType[4]{
                CmdDataType.cdtWarn_HighPressure_3401_3404,// 油缸压力过高 4*8
                CmdDataType.cdtWarn_LowPressure_3401_3404,// 油缸压力过低 4*8
                CmdDataType.cdtWarn_HighPosition_3401_3404,// 油缸长度过高 4*8
                CmdDataType.cdtWarn_LowPosition_3401_3404// 油缸长度过低 4*8   
        };

        private void SaveChangedData(int id, int subid, CmdDataType type, bool val)
        {
            //1.存文件

            FileStream RecFile;
            DateTime t = DateTime.Now;
            int listkey = id * 10000 + subid * 100 + (int)type;


            //报警系列
            if (Array.IndexOf<CmdDataType>(WarnDataTypeList, type) >= 0)
            {
                RecFile = new FileStream(WarningRecFileName, FileMode.Append);

                if (val)
                    _curwarninglist.Add(listkey, t);
                else
                {
                    _curwarninglist.Remove(listkey);
                }
            }
            else if (type == CmdDataType.cdtid_controledPump)
            {
                RecFile = new FileStream(WarningRecFileName, FileMode.Append);
                listkey = -100 * id;
            }
            else if (type == CmdDataType.cdtid_redundantPump)
            {
                RecFile = new FileStream(WarningRecFileName, FileMode.Append);
                listkey = -200 * id;
            }
            else//故障系列
            {
                RecFile = new FileStream(ErrorRecFileName, FileMode.Append);

                if (val)
                {
                    if (!_curerrorlist.ContainsKey(listkey))
                        _curerrorlist.Add(listkey, t);
                }
                else
                {
                    _curerrorlist.Remove(listkey);
                }
            }

            BinaryWriter bw = new BinaryWriter(RecFile);

            try
            {
                bw.Write((int)listkey);//4
                bw.Write((val) ? 1 : 0);//4
                bw.Write(DateTime.Now.Ticks);//8
            }
            finally
            {
                bw.Flush();
                bw.Close();
                RecFile.Close();
            }

            //2. 事件通知界面
        }

        public EventHandler OnWarnErrChanged = null;
        private void DoDataChanged()
        {
            if (OnWarnErrChanged != null)
                OnWarnErrChanged(this, new EventArgs());
        }

        public void Reset()
        {
            File.Delete(WarningRecFileName);
            File.Delete(ErrorRecFileName);
        }


        private byte id_controledPump = 0;//被控泵站
        protected byte ID_controledPump
        {
            set
            {
                if (id_controledPump != value)
                {
                    id_controledPump = value;
                    SaveChangedData(id_controledPump, 0, CmdDataType.cdtid_controledPump, true);

                }
            }
        }

        private byte id_redundantPump = 0;//冗余泵站
        protected byte ID_redundantPump
        {
            set
            {
                if (id_redundantPump != value)
                {
                    id_redundantPump = value;
                    SaveChangedData(id_redundantPump, 0, CmdDataType.cdtid_redundantPump, true);

                }
            }
        }


    }

}
