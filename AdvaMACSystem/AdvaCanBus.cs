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
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DataPool;
using System.IO;

namespace AdvaMACSystem
{
    public class AdvaCanBus
    {
        private const string ConfigFile = @"\HardDisk\CanConfig.xml";

        private AdvCANIO Device = new AdvCANIO();
        private bool m_bRun1 = false;

        private Thread Thread1;
        private int ThreadInterval = 200;//500;

        private const uint M_SENDCOUNT = 164;
        private AdvCan.canmsg_t[] msgSend = new AdvCan.canmsg_t[M_SENDCOUNT];                 //Package for write 

        private const uint M_RECIEVECOUNT = 400;//100;//44;
        private AdvCan.canmsg_t[] msgRecieve = new AdvCan.canmsg_t[M_RECIEVECOUNT];                 //Package for write 

        private static AdvaCanBus AdvaCanBusObject = null;
        public static AdvaCanBus GetAdvaCanBus()
        {
            if (AdvaCanBusObject == null)
                AdvaCanBusObject = new AdvaCanBus();

            return AdvaCanBusObject;
        }


        private AdvaCanBus()
        {

        }

        ~AdvaCanBus()
        {

        }

        //通讯配置参数 XML序列化 导入
        private string CanPortName = "can1";
        private uint BaudRateValue = AdvCan.CAN_TIMING_250K;
        private uint nWriteCount = M_SENDCOUNT;
        private uint nReadCount = M_RECIEVECOUNT;

        private UInt32 ReadTimeOutValue = 3000;
        private UInt32 WriteTimeOutValue = 3000;
        private uint dwMaskCode = 0xFFFFFFFF;//0;
        private uint dwAccCode = 0xFFFFFFFF;//0;
        private bool AcceptanceFilterMode = true;//false single true dual
        private uint EventMask = 0xFFFFFFFF;//0;
        //-通讯配置参数


        public string[] canErrStrArray =
        {
            /* 0 */"",
            /* 1 */"Failed to open the CAN port, please check the CAN port name!",
            /* 2 */ "Failed to stop opertion!",
            /* 3 */"Failed to set baud!",
            /* 4 */"Failed to set acceptance filter mode!",
            /* 5 */"Failed to set acceptance mask!",
            /* 6 */"Failed to set acceptance Filter Code!",
            /* 7 */"Failed to set Timeout!",
            /* 8 */"Failed to restart operation!"  ,
            /* 9 */"Failed to get current status!",
            /* 10 */ "Failed to set Event Mask!",
            /* 11 */ "Failed to get Event Mask!",
            /* 12 */ "Failed to clear receive fifo!"
        };


        private uint canErrcode = 0;
        public uint CanErrCode
        {
            get { return canErrcode; }
        }

        private void LoadConfig()
        {
            try
            {
                if (ConfigFile != string.Empty && File.Exists(ConfigFile))
                {
                    //修正了密码没有保存到配置文件的错误 by cs at 2009-3-26 13:04:54 {B2565183-D787-4b58-829E-D913AB845FA7}
                    XmlDocument doc = new XmlDocument();

                    doc.Load(ConfigFile);

                    if (doc.DocumentElement != null)
                    {
                        XmlNode rootnode = doc.DocumentElement;

                        XmlElement rxe = (XmlElement)rootnode;

                        CanPortName = rxe.GetAttribute("CanPortName").Trim();// "can1";
                        BaudRateValue = Convert.ToUInt32(rxe.GetAttribute("BaudRate").Trim());//AdvCan.CAN_TIMING_125K;
                        nWriteCount = Convert.ToUInt32(rxe.GetAttribute("WriteCount").Trim());//M_SENDCOUNT;
                        nReadCount = Convert.ToUInt32(rxe.GetAttribute("ReadCount").Trim());//M_RECIEVECOUNT;

                        ReadTimeOutValue = Convert.ToUInt32(rxe.GetAttribute("ReadTimeOut").Trim());//3000;
                        WriteTimeOutValue = Convert.ToUInt32(rxe.GetAttribute("WriteTimeOut").Trim());//3000;
                        dwMaskCode = Convert.ToUInt32(rxe.GetAttribute("MaskCode").Trim());//0;
                        dwAccCode = Convert.ToUInt32(rxe.GetAttribute("AccCode").Trim());//0;
                        AcceptanceFilterMode = (rxe.GetAttribute("AcceptanceFilterMode").Trim().ToLower() == "true");//false single true dual
                        EventMask = Convert.ToUInt32(rxe.GetAttribute("EventMask").Trim());//0;

                        return;
                    }
                }

            }
            catch (Exception)
            {

            }
        }




        public bool Open()
        {
            int nRet = 0;
            bool flag = false;

            AdvCan.CanStatusPar_t CanStatus = new AdvCan.CanStatusPar_t();

            nRet = Device.acCanOpen(CanPortName, false, nReadCount, nWriteCount);                               //Open CAN port
            if (nRet < 0)
            {
                canErrcode = 1;//"Failed to open the CAN port, please check the CAN port name!"
                return false;
            }

            nRet = Device.acEnterResetMode();                     //Enter reset mode          
            if (nRet < 0)
            {
                canErrcode = 2;//"Failed to stop opertion!"
                Device.acCanClose();
                return false;
            }

            nRet = Device.acSetBaud(BaudRateValue);                               //Set Baud Rate
            if (nRet < 0)
            {
                canErrcode = 3;//"Failed to set baud!"
                Device.acCanClose();
                return false;
            }
            /*
            if (!AcceptanceFilterMode) // "Single"                     //Set acceptance filter mode
                nRet = Device.acSetAcceptanceFilterMode(AdvCan.PELICAN_SINGLE_FILTER);
            else
                nRet = Device.acSetAcceptanceFilterMode(AdvCan.PELICAN_DUAL_FILTER);
            if (nRet < 0)
            {
                canErrcode = 4;//"Failed to set acceptance filter mode!"
                Device.acCanClose();
                return false;
            }

            nRet = Device.acSetAcceptanceFilterMask(dwMaskCode);                        //Set acceptance mask
            if (nRet < 0)
            {
                canErrcode = 5;//"Failed to set acceptance mask!"
                Device.acCanClose();
                return false;
            }

            nRet = Device.acSetAcceptanceFilterCode(dwAccCode);                          //Set acceptance code
            if (nRet < 0)
            {
                canErrcode = 6;//"Failed to set acceptance Filter Code!"
                Device.acCanClose();
                return false;
            }
            */
            nRet = Device.acSetTimeOut(WriteTimeOutValue, ReadTimeOutValue);      //Set timeout
            if (nRet < 0)
            {
                canErrcode = 7;//"Failed to set Timeout!"
                Device.acCanClose();
                return false;
            }

            //*
            //todo
            EventMask = AdvCan.EV_ERR + AdvCan.EV_RXCHAR;
            flag = Device.acSetCommMask(EventMask);                                                                 //Set event mask
            if (!flag)
            {
                canErrcode = 10;//"Failed to set Event Mask!"
                Device.acCanClose();
                return false;
            }

            flag = Device.acGetCommMask(ref EventMask);                                                             //Get event mask
            if (!flag)
            {
                canErrcode = 11;//"Failed to get Event Mask!"
                Device.acCanClose();
                return false;
            }

            nRet = Device.acClearRxFifo();                                                                           //Clear receive fifo of driver
            if (nRet < 0)
            {
                canErrcode = 12;//"Failed to clear receive fifo!");
                Device.acCanClose();
                return false;
            }
            //*/

            nRet = Device.acEnterWorkMode();                                     //Enter work mdoe
            if (nRet < 0)
            {
                canErrcode = 8;//"Failed to restart operation!"
                Device.acCanClose();
                return false;
            }

            nRet = Device.acGetStatus(ref CanStatus);                       //Get status
            if (nRet < 0)
            {
                canErrcode = 9;//"Failed to get current status!"
                Device.acCanClose();
                return false;
            }
            Thread1 = new Thread(new ThreadStart(ThreadMethod));
            Thread1.Start();


            return true;
            //Device.acCanClose();
        }

        public void Close()
        {
            m_bRun1 = false;
            Thread.Sleep(ThreadInterval);
            Device.acCanClose();

        }

        private void ThreadMethod()
        {

            //Initialize msg
            for (int j = 0; j < M_SENDCOUNT; j++)
            {
                msgSend[j].flags = AdvCan.MSG_EXT;
                msgSend[j].cob = 0;
                msgSend[j].id = 0;
                msgSend[j].length = (short)AdvCan.DATALENGTH;
                msgSend[j].data = new byte[AdvCan.DATALENGTH];

                //if rtr
                //if (bLeftRtrFlag)
                //{
                //    msgSend[j].flags += AdvCan.MSG_RTR;
                //    msgSend[j].length = 0;
                //}

                for (int i = 0; i < msgSend[j].length; i++)
                {
                    msgSend[j].data[i] = 0;// (byte)i;
                }
            }

            //Initialize msg
            for (int j = 0; j < M_RECIEVECOUNT; j++)
            {
                msgRecieve[j].data = new byte[AdvCan.DATALENGTH];
            }

            m_bRun1 = true;
            while (m_bRun1)
            {
                Thread.Sleep(ThreadInterval);
                if (!m_bRun1)
                    break;
                canwrite();

                //todo 读写节奏的调整
                canread();
            }
        }









        private void canwrite()
        {
            int nRet = 0;
            char[] data = new char[AdvCan.DATALENGTH];
            uint pulNumberofWritten = 0;

            if (CanDatapool.IsLocked)
                return;

            // 构造发送帧
            //RTR
            //if (!bLeftRtrFlag)
            {
                for (int j = 0; j < M_SENDCOUNT; j++)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        msgSend[j].data[i] = 0;//todo 填充数据帧
                    }
                    msgSend[j].length = (short)data.Length;//todo 根据实际长度
                }
            }

            if (CanDatapool == null)
                return;

            bool Csign_View_Setup = CanDatapool.sign_View_Setup;
            bool Csign_View_Setup_Confirm = CanDatapool.sign_View_Setup_Confirm;
            bool Csign_View_Parameter = CanDatapool.sign_View_Parameter;
            bool Csign_View_Parameter_Confirm = CanDatapool.sign_View_Parameter_Confirm;
            bool Csign_View_SenserCalibration = CanDatapool.sign_View_SenserCalibration;
            bool Csign_View_SetupFinish_Confirm = CanDatapool.sign_View_SetupFinish_Confirm;
            //bool Csign_View_CylinderParameter_Confirm = CanDatapool.sign_View_CylinderParameter_Confirm;//油缸、马达参数设定确认标志位

            int id = CanDatapool.CurId;
            int subid = CanDatapool.CurSubId;

            uint canmsgIndex = 0;
            {
                //2000
                //进入“安装设定”界面标志位		        0		1	1：表示进入界面；0：表示未进入界面	"安装设定界面（设定内容显示器需具备记忆保存功能，需要密码才能进入安装设定界面）"
                //安装泵站选择		                                1		1	"1：表示1#泵站；2：表示2#泵站 3：表示3#泵站；4：表示4#泵站，以此类推"	
                //油缸安装选择（选定某一泵站后）		    2	0	1	1：表示1#油缸安装；0表示没有安装	
                //                                                             1	1	1：表示2#油缸安装；0表示没有安装	
                //                                                             2	1	1：表示3#油缸安装；0表示没有安装	
                //                                                             3	1	1：表示4#油缸安装；0表示没有安装	
                //                                                             4	1	1：表示5#油缸安装；0表示没有安装	
                //                                                             5	1	1：表示6#油缸安装；0表示没有安装	
                //                                                             6	1	1：表示7#油缸安装；0表示没有安装	
                //                                                             7	1	1：表示8#油缸安装；0表示没有安装	
                //安装确定标志		                                3		1	1：确定按键按下；0：按键未按下
                //安装调试完毕确定标志		                    4		1	1：确定按键按下；0：按键未按下
                //安装调试完毕确认		4	0	1	1：1#油缸调试完毕按钮按下；0：按钮未按下	
                //                                   1	1	1：2#油缸调试完毕按钮按下；0：按钮未按下	
                //                                   2	1	1：3#油缸调试完毕按钮按下；0：按钮未按下	
                //                                   3	1	1：4#油缸调试完毕按钮按下；0：按钮未按下	
                //                                   4	1	1：5#油缸调试完毕按钮按下；0：按钮未按下	
                //                                   5	1	1：6#油缸调试完毕按钮按下；0：按钮未按下	
                //                                   6	1	1：7#油缸调试完毕按钮按下；0：按钮未按下	
                //                                   7	1	1：8#油缸调试完毕按钮按下；0：按钮未按下	


                /*
                for (int i = 0; i < CanDatapool.PumpCount; i++)
                {
                    msgSend[canmsgIndex].id = 0x2000;
                    msgSend[canmsgIndex].length = 4;//(short)AdvCan.DATALENGTH;

                    msgSend[canmsgIndex].data[0] = (Csign_View_Setup) ? (byte)1 : (byte)0;
                    msgSend[canmsgIndex].data[1] = (byte)(i + 1);

                    for (int j = 0; j < 8; j++)
                    {
                        if (CanDatapool.out_Installed[(int)(i * CanDatapool.CylinderCount + j)])
                            msgSend[canmsgIndex].data[2] |= (byte)(1 << i);
                        else
                            msgSend[canmsgIndex].data[2] &= (byte)~(1 << i);
                    }

                    msgSend[canmsgIndex].data[3] = (Csign_View_Setup_Confirm) ? (byte)1 : (byte)0;
                    canmsgIndex++;
                }
                */
                if (Csign_View_Setup)
                {
                    msgSend[canmsgIndex].id = 0x2000;
                    msgSend[canmsgIndex].length = 5;//(short)AdvCan.DATALENGTH;

                    msgSend[canmsgIndex].data[0] = (Csign_View_Setup) ? (byte)1 : (byte)0;
                    msgSend[canmsgIndex].data[1] = (byte)(id + 1);

                    for (int j = 0; j < 8; j++)
                    {
                        if (CanDatapool.out_Installed[(int)(id * CanDatapool.CylinderCount + j)])
                            msgSend[canmsgIndex].data[2] |= (byte)(1 << j);
                        else
                            msgSend[canmsgIndex].data[2] &= (byte)~(1 << j);
                    }

                    msgSend[canmsgIndex].data[3] = (Csign_View_Setup_Confirm) ? (byte)1 : (byte)0;
                    //msgSend[canmsgIndex].data[4] = (Csign_View_SetupFinish_Confirm) ? (byte)1 : (byte)0;
                    for (int j = 0; j < 8; j++)
                    {
                        if (CanDatapool.sign_View_SetupFinish_Confirm_seperate[(int)(id * CanDatapool.CylinderCount + j)])
                            msgSend[canmsgIndex].data[4] |= (byte)(1 << j);
                        else
                            msgSend[canmsgIndex].data[4] &= (byte)~(1 << j);
                    }
                    canmsgIndex++;
                }
                else
                {
                    //只发送2000 空数据
                    msgSend[canmsgIndex].id = 0x2000;
                    msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                    for (int i = 0; i < AdvCan.DATALENGTH; i++)
                    {
                        msgSend[canmsgIndex].data[i] = 0;
                    }

                    canmsgIndex++;
                }



                //2001
                //进入“参数设定”界面标志位		0		1	1：表示进入界面；0：表示未进入界面	"参数设定界面（设定内容显示器需具备记忆保存功能，需要密码才能进入参数设定界面）"
                //泵站选择		                                1		1	"1：表示1#泵站；2：表示2#泵站3：表示3#泵站；4：表示4#泵站，以此类推"	
                //油缸选择（选定某一泵站后）		2		1	1：表示1#油缸...8:表示8#油缸	
                //泵站压力报警值		                    2，3		1		
                //油缸压力上限报警功能开启		    4		1	1：开启；0：未开启	
                //油缸长度上限报警功能开启		    5		1	1：开启；0：未开启	
                //油缸长度下限报警功能开启		    6		1	1：开启；0：未开启	
                //油缸长度控制功能开启		            7		1	1：开启；0：未开启	
                //2002
                //油缸压力上限报警值设定		        0,1		0.1		
                //油缸压力下限报警值设定		        2,3		0.1		
                //油缸长度上限报警值设定		        4,5		0.1		
                //油缸长度下限报警值设定		        6,7		0.1		
                //2003
                //油缸压力设定值		                    0,1		0.1		
                //油缸长度设定值		                    2,3		0.1		
                //参数设定确认标志		                4		1	1：确定按键按下；0：按键未按下	

                /*
                for (int i = 0; i < CanDatapool.PumpCount; i++)
                {
                    for (int j = 0; j < CanDatapool.CylinderCount; j++)
                    {
                        //2001
                        msgSend[canmsgIndex].id = 0x2001;
                        msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                        msgSend[canmsgIndex].data[0] = (Csign_View_Parameter) ? (byte)1 : (byte)0;
                        msgSend[canmsgIndex].data[1] = (byte)(i + 1);
                        msgSend[canmsgIndex].data[2] = (byte)(j + 1);
                        msgSend[canmsgIndex].data[3] = CanDatapool.out_PressureAlarm_Pump[i];
                        msgSend[canmsgIndex].data[4] = (CanDatapool.out_PressureUpperLimitAlarm_Enable[(int)(i * CanDatapool.CylinderCount + j)]) ? (byte)1 : (byte)0;
                        msgSend[canmsgIndex].data[5] = (CanDatapool.out_PositionUpperLimitAlarm_Enable[(int)(i * CanDatapool.CylinderCount + j)]) ? (byte)1 : (byte)0;
                        msgSend[canmsgIndex].data[6] = (CanDatapool.out_PositionLowerLimitAlarm_Enable[(int)(i * CanDatapool.CylinderCount + j)]) ? (byte)1 : (byte)0;
                        msgSend[canmsgIndex].data[7] = (CanDatapool.out_PositionControl_Enable[(int)(i * CanDatapool.CylinderCount + j)]) ? (byte)1 : (byte)0;

                        canmsgIndex++;

                        //2002
                        msgSend[canmsgIndex].id = 0x2002;
                        msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                        msgSend[canmsgIndex].data[0] = (byte)(CanDatapool.out_PressureUpperLimitAlarm_Value[(int)(i * CanDatapool.CylinderCount + j)] & 0xFF);
                        msgSend[canmsgIndex].data[1] = (byte)(CanDatapool.out_PressureUpperLimitAlarm_Value[(int)(i * CanDatapool.CylinderCount + j)] >> 8);
                        msgSend[canmsgIndex].data[2] = (byte)(CanDatapool.out_PressureLowerLimitAlarm_Value[(int)(i * CanDatapool.CylinderCount + j)] & 0xFF);
                        msgSend[canmsgIndex].data[3] = (byte)(CanDatapool.out_PressureLowerLimitAlarm_Value[(int)(i * CanDatapool.CylinderCount + j)] >> 8);
                        msgSend[canmsgIndex].data[4] = (byte)(CanDatapool.out_PositionUpperLimitAlarm_Value[(int)(i * CanDatapool.CylinderCount + j)] & 0xFF);
                        msgSend[canmsgIndex].data[5] = (byte)(CanDatapool.out_PositionUpperLimitAlarm_Value[(int)(i * CanDatapool.CylinderCount + j)] >> 8);
                        msgSend[canmsgIndex].data[6] = (byte)(CanDatapool.out_PositionLowerLimitAlarm_Value[(int)(i * CanDatapool.CylinderCount + j)] & 0xFF);
                        msgSend[canmsgIndex].data[7] = (byte)(CanDatapool.out_PositionLowerLimitAlarm_Value[(int)(i * CanDatapool.CylinderCount + j)] >> 8);

                        canmsgIndex++;

                        //2003
                        msgSend[canmsgIndex].id = 0x2003;
                        msgSend[canmsgIndex].length = 5;//(short)AdvCan.DATALENGTH;

                        msgSend[canmsgIndex].data[0] = (byte)(CanDatapool.out_Pressure_Value[(int)(i * CanDatapool.CylinderCount + j)] & 0xFF);
                        msgSend[canmsgIndex].data[1] = (byte)(CanDatapool.out_Pressure_Value[(int)(i * CanDatapool.CylinderCount + j)] >> 8);
                        msgSend[canmsgIndex].data[2] = (byte)(CanDatapool.out_Position_Value[(int)(i * CanDatapool.CylinderCount + j)] & 0xFF);
                        msgSend[canmsgIndex].data[3] = (byte)(CanDatapool.out_Position_Value[(int)(i * CanDatapool.CylinderCount + j)] >> 8);
                        msgSend[canmsgIndex].data[4] = (byte)((Csign_View_Parameter_Confirm) ? 1 : 0);

                        canmsgIndex++;

                    }
                }
                */

                if (Csign_View_Parameter)
                {
                    //2001
                    msgSend[canmsgIndex].id = 0x2001;
                    msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                    //msgSend[canmsgIndex].data[0] = (Csign_View_Parameter) ? (byte)1 : (byte)0;
                    //msgSend[canmsgIndex].data[1] = (byte)(id + 1);
                    //msgSend[canmsgIndex].data[2] = (byte)(subid + 1);
                    //msgSend[canmsgIndex].data[3] = CanDatapool.out_PressureAlarm_Pump[id];
                    //msgSend[canmsgIndex].data[4] = (CanDatapool.out_PressureUpperLimitAlarm_Enable[(int)(id * CanDatapool.CylinderCount + subid)]) ? (byte)1 : (byte)0;
                    //msgSend[canmsgIndex].data[5] = (CanDatapool.out_PositionUpperLimitAlarm_Enable[(int)(id * CanDatapool.CylinderCount + subid)]) ? (byte)1 : (byte)0;
                    //msgSend[canmsgIndex].data[6] = (CanDatapool.out_PositionLowerLimitAlarm_Enable[(int)(id * CanDatapool.CylinderCount + subid)]) ? (byte)1 : (byte)0;
                    //msgSend[canmsgIndex].data[7] = (CanDatapool.out_PositionControl_Enable[(int)(id * CanDatapool.CylinderCount + subid)]) ? (byte)1 : (byte)0;

                    //进入“参数设定”界面标志位	2001	0		1	1：表示进入界面；0：表示未进入界面
                    //泵站选择		                                1		1	1：表示1#泵站；2：表示2#泵站 3：表示3#泵站；4：表示4#泵站，以此类推"
                    //油缸选择（选定某一泵站后）		    2		1	1：表示1#油缸...8:表示8#油缸
                    //泵站压力报警值		                    3,4		0.1
                    //油缸压力上限报警功能开启		    5	0	1	1：开启；0：未开启
                    //油缸长度上限报警功能开启			    1	1	1：开启；0：未开启
                    //油缸长度下限报警功能开启			    2	1	1：开启；0：未开启
                    //油缸长度控制功能开启			            3	1	1：开启；0：未开启

                    msgSend[canmsgIndex].data[0] = (Csign_View_Parameter) ? (byte)1 : (byte)0;
                    msgSend[canmsgIndex].data[1] = (byte)(id + 1);
                    msgSend[canmsgIndex].data[2] = (byte)(subid + 1);
                    msgSend[canmsgIndex].data[3] = (byte)(CanDatapool.out_PressureAlarm_Pump[id] & 0xFF);
                    msgSend[canmsgIndex].data[4] = (byte)(CanDatapool.out_PressureAlarm_Pump[id] >> 8);
                    msgSend[canmsgIndex].data[5] = (byte)(
                        (((CanDatapool.out_PressureUpperLimitAlarm_Enable[(int)(id * CanDatapool.CylinderCount + subid)]) ? (byte)1 : (byte)0) << 0) |
                        (((CanDatapool.out_PositionUpperLimitAlarm_Enable[(int)(id * CanDatapool.CylinderCount + subid)]) ? (byte)1 : (byte)0) << 1) |
                        (((CanDatapool.out_PositionLowerLimitAlarm_Enable[(int)(id * CanDatapool.CylinderCount + subid)]) ? (byte)1 : (byte)0) << 2) |
                        (((CanDatapool.out_PositionControl_Enable[(int)(id * CanDatapool.CylinderCount + subid)]) ? (byte)1 : (byte)0) << 3));

                    canmsgIndex++;

                    //2002
                    msgSend[canmsgIndex].id = 0x2002;
                    msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                    msgSend[canmsgIndex].data[0] = (byte)(CanDatapool.out_PressureUpperLimitAlarm_Value[(int)(id * CanDatapool.CylinderCount + subid)] & 0xFF);
                    msgSend[canmsgIndex].data[1] = (byte)(CanDatapool.out_PressureUpperLimitAlarm_Value[(int)(id * CanDatapool.CylinderCount + subid)] >> 8);
                    msgSend[canmsgIndex].data[2] = (byte)(CanDatapool.out_PressureLowerLimitAlarm_Value[(int)(id * CanDatapool.CylinderCount + subid)] & 0xFF);
                    msgSend[canmsgIndex].data[3] = (byte)(CanDatapool.out_PressureLowerLimitAlarm_Value[(int)(id * CanDatapool.CylinderCount + subid)] >> 8);
                    msgSend[canmsgIndex].data[4] = (byte)(CanDatapool.out_PositionUpperLimitAlarm_Value[(int)(id * CanDatapool.CylinderCount + subid)] & 0xFF);
                    msgSend[canmsgIndex].data[5] = (byte)(CanDatapool.out_PositionUpperLimitAlarm_Value[(int)(id * CanDatapool.CylinderCount + subid)] >> 8);
                    msgSend[canmsgIndex].data[6] = (byte)(CanDatapool.out_PositionLowerLimitAlarm_Value[(int)(id * CanDatapool.CylinderCount + subid)] & 0xFF);
                    msgSend[canmsgIndex].data[7] = (byte)(CanDatapool.out_PositionLowerLimitAlarm_Value[(int)(id * CanDatapool.CylinderCount + subid)] >> 8);

                    canmsgIndex++;

                    //2003
                    msgSend[canmsgIndex].id = 0x2003;
                    msgSend[canmsgIndex].length = 5;//(short)AdvCan.DATALENGTH;

                    msgSend[canmsgIndex].data[0] = (byte)(CanDatapool.out_Pressure_Value[(int)(id * CanDatapool.CylinderCount + subid)] & 0xFF);
                    msgSend[canmsgIndex].data[1] = (byte)(CanDatapool.out_Pressure_Value[(int)(id * CanDatapool.CylinderCount + subid)] >> 8);
                    msgSend[canmsgIndex].data[2] = (byte)(CanDatapool.out_Position_Value[(int)(id * CanDatapool.CylinderCount + subid)] & 0xFF);
                    msgSend[canmsgIndex].data[3] = (byte)(CanDatapool.out_Position_Value[(int)(id * CanDatapool.CylinderCount + subid)] >> 8);
                    msgSend[canmsgIndex].data[4] = (byte)((Csign_View_Parameter_Confirm) ? 1 : 0);
                    //msgSend[canmsgIndex].data[5] = (byte)((Csign_View_CylinderParameter_Confirm) ? 1 : 0);

                    canmsgIndex++;
                }
                else
                {
                    //只发送2001 空数据
                    msgSend[canmsgIndex].id = 0x2001;
                    msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                    for (int i = 0; i < AdvCan.DATALENGTH; i++)
                    {
                        msgSend[canmsgIndex].data[i] = 0;
                    }

                    canmsgIndex++;
                }


                //2004
                //进入“传感器标定”界面标志位		0		1	1：表示进入界面；0：表示未进入界面	"传感器标定界面（设定内容显示器需具备记忆保存功能，需要密码才能进入参数设定界面）"
                //泵站选择		                                1		1	"1：表示1#泵站；2：表示2#泵站3：表示3#泵站；4：表示4#泵站，以此类推"	
                //油缸选择（选定某一泵站后）		2		1	1：表示1#油缸...8:表示8#油缸	
                //油缸长度传感器低位值		            3		0.1		
                //油缸长度传感器高位值		            4		0.1		
                //单独/统一标定标志位		            5		1	0：每个油缸单独标定；1：所有油缸按同一值标定	
                //油缸长度传感器低位值确认		    6		1	1：确定按键按下；0：按键未按下	
                //油缸长度传感器高位值确认		    7		1	1：确定按键按下；0：按键未按下

                /*
                if (CanDatapool.sign_isSame)
                {
                    msgSend[canmsgIndex].id = 0x2004;
                    msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;
                    msgSend[canmsgIndex].data[0] = (byte)((Csign_View_SenserCalibration) ? 1 : 0);
                    msgSend[canmsgIndex].data[1] = 1;
                    msgSend[canmsgIndex].data[2] = 1;
                    msgSend[canmsgIndex].data[3] = CanDatapool.out_PositionSenserLow_Value[0];
                    msgSend[canmsgIndex].data[4] = CanDatapool.out_PositionSenserHigh_Value[0];
                    msgSend[canmsgIndex].data[5] = 0;
                    msgSend[canmsgIndex].data[6] = (byte)((CanDatapool.sign_View_PositionSenserLow_Confirm) ? 1 : 0);
                    msgSend[canmsgIndex].data[7] = (byte)((CanDatapool.sign_View_PositionSenserHigh_Confirm) ? 1 : 0);

                    canmsgIndex++;

                }
                else
                {
                    for (int i = 0; i < CanDatapool.PumpCount; i++)
                    {
                        for (int j = 0; j < CanDatapool.CylinderCount; j++)
                        {
                            msgSend[canmsgIndex].id = 0x2004;
                            msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                            msgSend[canmsgIndex].data[0] = (byte)((Csign_View_SenserCalibration) ? 1 : 0);
                            msgSend[canmsgIndex].data[1] = (byte)(i + 1);
                            msgSend[canmsgIndex].data[2] = (byte)(j + 1);
                            msgSend[canmsgIndex].data[3] = CanDatapool.out_PositionSenserLow_Value[(int)(i * CanDatapool.CylinderCount + j)];
                            msgSend[canmsgIndex].data[4] = CanDatapool.out_PositionSenserHigh_Value[(int)(i * CanDatapool.CylinderCount + j)];
                            msgSend[canmsgIndex].data[5] = 0;
                            msgSend[canmsgIndex].data[6] = (byte)((CanDatapool.sign_View_PositionSenserLow_Confirm) ? 1 : 0);
                            msgSend[canmsgIndex].data[7] = (byte)((CanDatapool.sign_View_PositionSenserHigh_Confirm) ? 1 : 0);

                            canmsgIndex++;

                        }
                    }
                }
                 */
                if (Csign_View_SenserCalibration)
                {
                    msgSend[canmsgIndex].id = 0x2004;
                    msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                    msgSend[canmsgIndex].data[0] = (byte)((Csign_View_SenserCalibration) ? 1 : 0);
                    msgSend[canmsgIndex].data[1] = (byte)(id + 1);
                    msgSend[canmsgIndex].data[2] = (byte)(subid + 1);
                    msgSend[canmsgIndex].data[3] = CanDatapool.out_PositionSenserLow_Value[(int)(id * CanDatapool.CylinderCount + subid)];
                    msgSend[canmsgIndex].data[4] = CanDatapool.out_PositionSenserHigh_Value[(int)(id * CanDatapool.CylinderCount + subid)];
                    msgSend[canmsgIndex].data[5] = (CanDatapool.sign_isSame) ? (byte)1 : (byte)0;
                    msgSend[canmsgIndex].data[6] = (byte)((CanDatapool.sign_View_PositionSenserLow_Confirm) ? 1 : 0);
                    msgSend[canmsgIndex].data[7] = (byte)((CanDatapool.sign_View_PositionSenserHigh_Confirm) ? 1 : 0);

                    canmsgIndex++;

                }
                else
                {
                    //只发送2004 空数据
                    msgSend[canmsgIndex].id = 0x2004;
                    msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                    for (int i = 0; i < AdvCan.DATALENGTH; i++)
                    {
                        msgSend[canmsgIndex].data[i] = 0;
                    }

                    canmsgIndex++;
                }



                //2005
                //油缸控制方式		                            0		1	"0：表示自动控制方式
                //                                                                1：表示油缸伸缩手动控制
                //                                                                2：表示机械锁手动控制"	手动控制
                //手动控制泵站		                            1		1	"1：表示1#泵站；2：表示2#泵站3：表示3#泵站；4：表示4#泵站，以此类推"	
                //手动控制油缸		                            2		1	1：表示1#油缸...8:表示8#油缸	
                //油缸伸缩控制		                            3		1	"0：表示停止
                //                                                                 1：表示伸出控制
                //                                                                 2：表示缩回控制"	
                //控显通讯正常标志位		                    4		1	固定值19	

                /*
                for (int i = 0; i < CanDatapool.PumpCount; i++)
                {
                    for (int j = 0; j < CanDatapool.CylinderCount; j++)
                    {
                        msgSend[canmsgIndex].id = 0x2005;
                        msgSend[canmsgIndex].length = 5;// (short)AdvCan.DATALENGTH;

                        //todo 手动控制 通过单独发送还是遍历设备？
                        //msgSend[canmsgIndex].data[0] = //(Csign_View_SenserCalibration) ? 1 : 0;
                        //msgSend[canmsgIndex].data[1] = i + 1;
                        //msgSend[canmsgIndex].data[2] = j + 1;
                        //msgSend[canmsgIndex].data[3] = //CanDatapool.out_PositionSenserLow_Value[i * CanDatapool.CylinderCount + j];
                        //msgSend[canmsgIndex].data[4] = //CanDatapool.out_PositionSenserHigh_Value[i * CanDatapool.CylinderCount + j];

                        canmsgIndex++;
                    }
                }
                */
                {
                    msgSend[canmsgIndex].id = 0x2005;
                    msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                    //todo 手动控制 通过单独发送还是遍历设备？
                    msgSend[canmsgIndex].data[0] = (byte)CanDatapool.ControlMode[id];
                    msgSend[canmsgIndex].data[1] = (byte)(id + 1);
                    msgSend[canmsgIndex].data[2] = (byte)(subid + 1);
                    msgSend[canmsgIndex].data[3] = (byte)CanDatapool.out_MotionState;
                    msgSend[canmsgIndex].data[4] = 19;
                    msgSend[canmsgIndex].data[5] = (byte)(CanDatapool.out_StartPressure_Pump[id] & 0xFF);
                    msgSend[canmsgIndex].data[6] = (byte)(CanDatapool.out_StartPressure_Pump[id] >> 8);
                    msgSend[canmsgIndex].data[7] = (byte)((CanDatapool.out_ManualStart_Pump[id]) ? 1 : 0);
                    canmsgIndex++;
                }

                //2006
                //油缸截面积值设定		0,1		0.01		
                //油缸最大压力值设定		2,3		0.1		
                //油缸最大位移值设定		4,5		0.1		
                {
                    msgSend[canmsgIndex].id = 0x2006;
                    msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                    msgSend[canmsgIndex].data[0] = (byte)(CanDatapool.out_SectionalArea_Value[id] & 0xFF);
                    msgSend[canmsgIndex].data[1] = (byte)(CanDatapool.out_SectionalArea_Value[id] >> 8);

                    msgSend[canmsgIndex].data[2] = (byte)(CanDatapool.out_MAXPressure_Value[id] & 0xFF);
                    msgSend[canmsgIndex].data[3] = (byte)(CanDatapool.out_MAXPressure_Value[id] >> 8);

                    msgSend[canmsgIndex].data[4] = (byte)(CanDatapool.out_MAXPosition_Value[id] & 0xFF);
                    msgSend[canmsgIndex].data[5] = (byte)(CanDatapool.out_MAXPosition_Value[id] >> 8);

                    //泵站压力传感器最大量程 4
                    msgSend[canmsgIndex].data[6] = (byte)(CanDatapool.out_MAXPressure_Pump_Value[id] & 0xFF);
                    msgSend[canmsgIndex].data[7] = (byte)(CanDatapool.out_MAXPressure_Pump_Value[id] >> 8);

                    canmsgIndex++;
                }

                //2007
                //马达最大压力设定	            0,1		0.1		
                //油缸最大行程设定值		        2,3		0.1		
                //油缸当天行程最大设定值		4,5		0.1		
                {
                    msgSend[canmsgIndex].id = 0x2007;
                    msgSend[canmsgIndex].length = (short)AdvCan.DATALENGTH;

                    msgSend[canmsgIndex].data[0] = (byte)(CanDatapool.out_PumpPressureHighout[id] & 0xFF);
                    msgSend[canmsgIndex].data[1] = (byte)(CanDatapool.out_PumpPressureHighout[id] >> 8);

                    msgSend[canmsgIndex].data[2] = (byte)(CanDatapool.out_PumpPositionHighout[id] & 0xFF);
                    msgSend[canmsgIndex].data[3] = (byte)(CanDatapool.out_PumpPositionHighout[id] >> 8);

                    msgSend[canmsgIndex].data[4] = (byte)(CanDatapool.out_PumpTodayPositionHighout[id] & 0xFF);
                    msgSend[canmsgIndex].data[5] = (byte)(CanDatapool.out_PumpTodayPositionHighout[id] >> 8);

                    //msgSend[canmsgIndex].data[6] = (byte)((Csign_View_CylinderParameter_Confirm) ? 1 : 0);

                    //油缸最大轴力 4
                    msgSend[canmsgIndex].data[6] = (byte)(CanDatapool.out_MAXPressure2_Value[id] & 0xFF);
                    msgSend[canmsgIndex].data[7] = (byte)(CanDatapool.out_MAXPressure2_Value[id] >> 8);

                    canmsgIndex++;
                }

                //4：显示器发送给GPS						
                //功能名称	ID号	字节	位	系数	备注	说明
                //4050
                //11#油缸所在层数		0		1		油缸所在层数和支数
                //11#油缸所在支数		1		1		
                //12#油缸所在层数		2		1		
                //12#油缸所在支数		3		1		
                //13#油缸所在层数		4		1		
                //13#油缸所在支数		5		1		
                //14#油缸所在层数		6		1		
                //14#油缸所在支数		7		1		
                //4051
                //15#油缸所在层数		0		1		
                //15#油缸所在支数		1		1		
                //16#油缸所在层数		2		1		
                //16#油缸所在支数		3		1		
                //17#油缸所在层数		4		1		
                //17#油缸所在支数		5		1		
                //18#油缸所在层数		6		1		
                //18#油缸所在支数		7		1		
                //4052
                //21#油缸所在层数		0		1		
                //21#油缸所在支数		1		1		
                //22#油缸所在层数		2		1		
                //22#油缸所在支数		3		1		
                //23#油缸所在层数		4		1		
                //23#油缸所在支数		5		1		
                //24#油缸所在层数		6		1		
                //24#油缸所在支数		7		1		
                //4053
                //25#油缸所在层数		0		1		
                //25#油缸所在支数		1		1		
                //26#油缸所在层数		2		1		
                //26#油缸所在支数		3		1		
                //27#油缸所在层数		4		1		
                //27#油缸所在支数		5		1		
                //28#油缸所在层数		6		1		
                //28#油缸所在支数		7		1		
                //4054
                //31#油缸所在层数		0		1		
                //31#油缸所在支数		1		1		
                //32#油缸所在层数		2		1		
                //32#油缸所在支数		3		1		
                //33#油缸所在层数		4		1		
                //33#油缸所在支数		5		1		
                //34#油缸所在层数		6		1		
                //34#油缸所在支数		7		1		
                //4055
                //35#油缸所在层数		0		1		
                //35#油缸所在支数		1		1		
                //36#油缸所在层数		2		1		
                //36#油缸所在支数		3		1		
                //37#油缸所在层数		4		1		
                //37#油缸所在支数		5		1		
                //38#油缸所在层数		6		1		
                //38#油缸所在支数		7		1		
                //4056
                //41#油缸所在层数		0		1		
                //41#油缸所在支数		1		1		
                //42#油缸所在层数		2		1		
                //42#油缸所在支数		3		1		
                //43#油缸所在层数		4		1		
                //43#油缸所在支数		5		1		
                //44#油缸所在层数		6		1		
                //44#油缸所在支数		7		1		
                //4057
                //45#油缸所在层数		0		1		
                //45#油缸所在支数		1		1		
                //46#油缸所在层数		2		1		
                //46#油缸所在支数		3		1		
                //47#油缸所在层数		4		1		
                //47#油缸所在支数		5		1		
                //48#油缸所在层数		6		1		
                //48#油缸所在支数		7		1		
                for (int i = 0; i < 8; i++)
                {
                    msgSend[canmsgIndex].id = (0x4050 + (uint)i);

                    for (int j = 0; j < 8; j++)
                    {
                        if (j % 2 == 0)
                            msgSend[canmsgIndex].data[j] = CanDatapool.View_SetupPosition_Row[i * 4 + j / 2];//油缸安装所在的层数 4*8
                        else
                            msgSend[canmsgIndex].data[j] = CanDatapool.View_SetupPosition_Col[i * 4 + j / 2]; //油缸安装所在的支数 4*8
                    }

                    canmsgIndex++;
                }

                //当前泵站无补偿动作	1000	0		1	"值为0：否，值为1：是
                //计算规则：
                //1#泵站无补偿动作 AND 2#泵站无补偿动作 AND
                //3#泵站无补偿动作 AND 4#泵站无补偿动作"	"
                //根据接收到的1010、1011、1012、1013帧进行控制，并以100ms周期发送给控制器(注意：在手动模式下，第1、2、3、4字节全发送0）"

                //开启冗余控制		1		1	"值为0：否，值为1：是
                //计算规则：
                //某一或几#泵站建压失败，且其它泵站无补偿动作，
                //如2#泵站建压失败，1#、3#、4#无补偿动作；
                //或2#、3#泵站建压失败，1#、4#无补偿动作。"	

                //被控泵站		2		1	"1：1号泵站，2:2号泵站，3:3号…
                //计算规则：
                //开启冗余控制后，如当前只有1个泵站建压失败，则该泵站为被控泵站，如当前有多个泵站建压失败，则按1-2-3-4的优先级确定被控泵站。"	

                //冗余泵站		3		1	"1：1号泵站，2:2号泵站，3:3号…
                //计算规则：
                //按1-2-3-4-1确定冗余泵站，成为冗余泵站条件（泵站无补偿动作 AND 泵站未建压失败）
                //如：被控泵站为2，如3无补偿动作且未建压失败，则3为冗余泵站，如3有补偿动作或建压失败，则4为冗余泵站，以此类推。"	

                //冗余设定压力		4,5		0.1	"根据1011帧内容确定以及被控泵站内容确定。
                //如被控泵站为2，则冗余设定压力为“2#泵站当前设定压力”"	
                bool bval = true;
                byte id_controledPump = 0;//被控泵站
                byte id_redundantPump = 0;//冗余泵站
                byte tp_id_redundantPump = 0;


                {
                    msgSend[canmsgIndex].id = 0x1000;
                    msgSend[canmsgIndex].length = 6;// (short)AdvCan.DATALENGTH;

                    //(注意：在手动模式下，第1、2、3、4字节全发送0）
                    if (CanDatapool.ControlMode[id] != ControlModeType.Auto)
                    {
                        msgSend[canmsgIndex].data[0] = 0;
                        msgSend[canmsgIndex].data[1] = 0;
                        msgSend[canmsgIndex].data[2] = 0;
                        msgSend[canmsgIndex].data[3] = 0;

                        if (CanDatapool.ControlMode[id] == ControlModeType.MachLockManual)
                        {
                            for (int i = 0; i < CanDatapool.PumpCount; i++)
                            {
                                switch (CanDatapool.in_Listento_id_1010_1013[i])
                                {
                                    case ListentoidType.ltpNone:
                                        break;
                                    case ListentoidType.ltpredundant:
                                        id_redundantPump = (byte)(i + 1);
                                        break;
                                    case ListentoidType.ltpcontroled:
                                        id_controledPump = (byte)(i + 1);
                                        break;
                                }

                            }
                            msgSend[canmsgIndex].data[2] = id_controledPump;
                            msgSend[canmsgIndex].data[3] = id_redundantPump;

                        }


                        //CanDatapool.out_id_controledPump = 0;
                        //CanDatapool.out_id_redundantPump = 0;
                    }
                    else
                    {
                        //*当前泵站无补偿动作
                        msgSend[canmsgIndex].data[0] = (
                            !CanDatapool.in_CompAct_Pump_1010_1013[0] &&
                            !CanDatapool.in_CompAct_Pump_1010_1013[1] &&
                            !CanDatapool.in_CompAct_Pump_1010_1013[2] &&
                            !CanDatapool.in_CompAct_Pump_1010_1013[3]
                            ) ? (byte)1 : (byte)0;


                        /* for (int i = 0; i < CanDatapool.PumpCount; i++)
                         {
                             switch (CanDatapool.in_Listento_id_1010_1013[i])
                             {
                                 case ListentoidType.ltpNone:
                                     break;
                                 case ListentoidType.ltpredundant:
                                     id_redundantPump = (byte)(i + 1);
                                     break;
                                 case ListentoidType.ltpcontroled:
                                     id_controledPump = (byte)(i + 1);
                                     break;
                             }

                         }*/

                        if (id_controledPump == 0 &&
                            id_redundantPump == 0)
                        {
                            //*开启冗余控制
                            //某一或几#泵站建压失败，且其它泵站无补偿动作
                            //如2#泵站建压失败，1#、3#、4#无补偿动作；
                            //或2#、3#泵站建压失败，1#、4#无补偿动作。

                            //即，当某一泵站没有建压失败，且它有补偿动作，则开启冗余控制不成立
                            //边界条件，1 没有泵站建压失败，且所有泵站无补偿动作 ，目前结果为否//是
                            //          2 所有泵站建压失败，目前结果为否//是

                            //for (int i = 0; i < CanDatapool.PumpCount; i++)
                            //{
                            //    if (!CanDatapool.in_StartFailed_Pump_1010_1013[i] &&
                            //        CanDatapool.in_CompAct_Pump_1010_1013[i])
                            //        bval = false;
                            //}

                            //是否有建圧失败的，若有 ，继续判断；没有，则 不开启
                            bool hasjianya = false;
                            for (int i = 0; i < CanDatapool.PumpCount; i++)
                            {
                                if (CanDatapool.in_StartFailed_Pump_1010_1013[i])
                                    hasjianya = true;
                            }

                            if (hasjianya)
                            {
                                /*
                                //非建圧 && 无补偿的，则开启
                                bval = false;
                                for (int i = 0; i < CanDatapool.PumpCount; i++)
                                {
                                    if (!CanDatapool.in_StartFailed_Pump_1010_1013[i] &&
                                        !CanDatapool.in_CompAct_Pump_1010_1013[i])
                                        bval = true;
                                }
                                */
                                bval = true;
                                for (int i = 0; i < CanDatapool.PumpCount; i++)
                                {
                                    if (CanDatapool.in_StartFailed_Pump_1010_1013[i])
                                    {
                                        for (int j = 0; j < CanDatapool.PumpCount; j++)
                                        {
                                            if (i != j)
                                            {
                                                if (CanDatapool.in_CompAct_Pump_1010_1013[i])
                                                    bval = false;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                                bval = false;

                            msgSend[canmsgIndex].data[1] = (bval) ? (byte)1 : (byte)0;


                            //*被控泵站
                            //开启冗余控制后，如当前只有1个泵站建压失败，则该泵站为被控泵站，如当前有多个泵站建压失败，则按1-2-3-4的优先级确定被控泵站。
                            if (bval)
                            {
                                for (int i = 0; i < CanDatapool.PumpCount; i++)
                                {
                                    if (CanDatapool.in_StartFailed_Pump_1010_1013[i])
                                    {
                                        id_controledPump = (byte)(i + 1);
                                        break;
                                    }
                                }

                                msgSend[canmsgIndex].data[2] = id_controledPump;
                            }
                            else
                                msgSend[canmsgIndex].data[2] = 0;

                            //*冗余泵站
                            //按1-2-3-4-1确定冗余泵站，成为冗余泵站条件（泵站无补偿动作 AND 泵站未建压失败）
                            //如：被控泵站为2，如3无补偿动作且未建压失败，则3为冗余泵站，如3有补偿动作或建压失败，则4为冗余泵站，以此类推。"	
                            //边界条件，1 如没有被控泵站，从1-4判断，没有则为0
                            //          2 如有被控泵站， 没有符合条件的冗余泵站，目前为0
                            if (id_controledPump > 0)
                                for (byte i = msgSend[canmsgIndex].data[2]; i < msgSend[canmsgIndex].data[2] + CanDatapool.PumpCount; i++)
                                {
                                    if (i < CanDatapool.PumpCount)
                                        tp_id_redundantPump = i;
                                    else
                                        tp_id_redundantPump = (byte)(i - CanDatapool.PumpCount);

                                    if (CanDatapool.GetBoolValue(tp_id_redundantPump, 0, CmdDataType.cdt_PumpInstalled) &&
                                        !CanDatapool.in_CompAct_Pump_1010_1013[tp_id_redundantPump] &&
                                        !CanDatapool.in_StartFailed_Pump_1010_1013[tp_id_redundantPump])
                                        id_redundantPump = (byte)(tp_id_redundantPump + 1);
                                }
                            else
                                id_redundantPump = 0;//如没有被控泵站，从1-4判断，没有则为0

                            msgSend[canmsgIndex].data[3] = id_redundantPump;
                        }
                        else
                        {
                            msgSend[canmsgIndex].data[1] = (byte)1;
                            msgSend[canmsgIndex].data[2] = id_controledPump;
                            msgSend[canmsgIndex].data[3] = id_redundantPump;
                        }
                    }


                    //冗余设定压力
                    //如被控泵站为2，则冗余设定压力为“2#泵站当前设定压力”"
                    if (id_controledPump > 0)
                    {
                        msgSend[canmsgIndex].data[4] = CanDatapool.in_CurPressureLow_Pump_Real_1010_1013[id_controledPump - 1];
                        msgSend[canmsgIndex].data[5] = CanDatapool.in_CurPressureHigh_Pump_Real_1010_1013[id_controledPump - 1];
                    }
                    else
                    {
                        msgSend[canmsgIndex].data[4] = 0;
                        msgSend[canmsgIndex].data[5] = 0;
                    }
                    CanDatapool.out_id_controledPump = id_controledPump;
                    CanDatapool.out_id_redundantPump = id_redundantPump;

                    canmsgIndex++;
                }
            }

            nRet = Device.acCanWrite(msgSend, canmsgIndex, ref pulNumberofWritten);
            if (nRet == AdvCANIO.TIME_OUT)
            {
                //"Package  sending timeout!";               
            }
            else if (nRet == AdvCANIO.OPERATION_ERROR)
            {
                //"Package sending error!";
            }
            else
            {
                //" packages have been sent!";
            }



        }

        private void canread()
        {
            int nRet = 0;
            uint pulNumberofRead = 0;
            uint ErrorCode = 0;

            int Idx_Pump = 0;
            int Idx_Cylinder = 0;

            byte[] idArray = null;
            byte idArray0 = 0;

            short vs_High = 0;
            short vs_low = 0;

            if (CanDatapool == null)
                return;

            //nRet = Device.acWaitEvent(msgRecieve, nReadCount, ref pulNumberofRead, ref ErrorCode);
            nRet = Device.acCanRead(msgRecieve, nReadCount, ref pulNumberofRead);
            if (nRet == AdvCANIO.TIME_OUT)
            {
                //"Package receiving timeout!";
            }
            else if (nRet == AdvCANIO.OPERATION_ERROR)
            {
                if ((ErrorCode & AdvCan.CE_RXOVER) != 0)
                {
                    //SendStatus = "Receive buffer overflow!";
                }
                if ((ErrorCode & AdvCan.CE_OVERRUN) != 0)
                {
                    //SendStatus = "CAN controller message overflow";
                }
                if ((ErrorCode & AdvCan.CE_BREAK) != 0)
                {
                    //SendStatus = "CAN Controller Bus Off!";
                }
                if ((ErrorCode & AdvCan.CE_FRAME) != 0)
                {
                    //SendStatus = "CAN Controller in error passive!";
                }
                //RightStatus.Invoke(SetList, SendStatus);
            }
            else
            {
                for (int j = 0; j < pulNumberofRead; j++)
                {
                    if (msgRecieve[j].id == AdvCan.ERRORID)
                    {
                        //SendStatus += "a incorrect package";
                    }
                    else
                    {
                        if ((msgRecieve[j].flags & AdvCan.MSG_RTR) > 0)
                        {
                            //SendStatus += "a RTR package";
                        }
                        else
                        {
                            //for (int i = 0; i < msgRecieve[j].length; i++)                           //Package receiving ok
                            {
                                //todo 正确包的解析

                                switch (msgRecieve[j].id)
                                {
                                    //1#泵站当前设定压力	1010	0,1	0.1
                                    //1#泵站建压失败		        2		1	0:否，1：是
                                    //1#泵站补偿动作情况		    3		1	0：无动作，1：有动作
                                    //1#运行模式		                    4		1	0：自动模式，1：手动模式；2：无线遥控	
                                    //1#急停按钮		                    5		1	0:否，1：是	
                                    //2#泵站当前设定压力	1011	0,1	0.1
                                    //2#泵站建压失败		        2		1	0:否，1：是
                                    //2#泵站补偿动作情况		    3		1	0：无动作，1：有动作
                                    //2#运行模式		                    4		1	0：自动模式，1：手动模式；2：无线遥控	
                                    //2#急停按钮		                    5		1	0:否，1：是	
                                    //3#泵站当前设定压力	1012	0,1	0.1
                                    //3#泵站建压失败		        2		1	0:否，1：是
                                    //3#泵站补偿动作情况		    3		1	0：无动作，1：有动作
                                    //3#运行模式		                    4		1	0：自动模式，1：手动模式；2：无线遥控	
                                    //3#急停按钮		                    5		1	0:否，1：是	
                                    //4#泵站当前设定压力	1013	0,1	0.1
                                    //4#泵站建压失败		        2		1	0:否，1：是
                                    //4#泵站补偿动作情况		    3		1	0：无动作，1：有动作
                                    //4#运行模式		                    4		1	0：自动模式，1：手动模式；2：无线遥控	
                                    //4#急停按钮		                    5		1	0:否，1：是	
                                    case 0x1010:
                                    case 0x1011:
                                    case 0x1012:
                                    case 0x1013:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = Convert.ToByte(idArray[0] - 0x10);

                                        CanDatapool.in_CurPressureLow_Pump_Real_1010_1013[idArray0] = msgRecieve[j].data[0];
                                        CanDatapool.in_CurPressureHigh_Pump_Real_1010_1013[idArray0] = msgRecieve[j].data[1];
                                        CanDatapool.in_StartFailed_Pump_1010_1013[idArray0] = msgRecieve[j].data[2] != 0;
                                        CanDatapool.in_CompAct_Pump_1010_1013[idArray0] = msgRecieve[j].data[3] != 0;
                                        CanDatapool.ControlMode[idArray0] = (ControlModeType)msgRecieve[j].data[4];
                                        CanDatapool.in_EStop_1010_1013[idArray0] = msgRecieve[j].data[5] != 0;
                                        CanDatapool.in_Listento_id_1010_1013[idArray0] = (ListentoidType)msgRecieve[j].data[6];

                                        CanDatapool.in_Pump_HartBeating_1010_1013[idArray0] = DateTime.Now;

                                        break;
                                    #region 3001-3008
                                    //3001
                                    //11#油缸压力当前值	    	0,1		0.1	
                                    //12#油缸压力当前值	    	2,3		0.1	
                                    //13#油缸压力当前值	    	4,5		0.1	
                                    //14#油缸压力当前值	    	6,7		0.1

                                    //3002
                                    //15#油缸压力当前值		0,1		0.1
                                    //16#油缸压力当前值		2,3		0.1
                                    //17#油缸压力当前值		4,5		0.1
                                    //18#油缸压力当前值		6,7		0.1

                                    //3003
                                    //21#油缸压力当前值		0,1		0.1
                                    //22#油缸压力当前值		2,3		0.1
                                    //23#油缸压力当前值		4,5		0.1
                                    //24#油缸压力当前值		6,7		0.1

                                    //3004
                                    //25#油缸压力当前值		0,1		0.1
                                    //26#油缸压力当前值		2,3		0.1
                                    //27#油缸压力当前值		4,5		0.1
                                    //28#油缸压力当前值		6,7		0.1

                                    //3005
                                    //31#油缸压力当前值		0,1		0.1
                                    //32#油缸压力当前值		2,3		0.1
                                    //33#油缸压力当前值		4,5		0.1
                                    //34#油缸压力当前值		6,7		0.1

                                    //3006
                                    //35#油缸压力当前值		0,1		0.1
                                    //36#油缸压力当前值		2,3		0.1
                                    //37#油缸压力当前值		4,5		0.1
                                    //38#油缸压力当前值		6,7		0.1

                                    //3007
                                    //41#油缸压力当前值		0,1		0.1
                                    //42#油缸压力当前值		2,3		0.1
                                    //43#油缸压力当前值		4,5		0.1
                                    //44#油缸压力当前值		6,7		0.1

                                    //3008
                                    //45#油缸压力当前值		0,1		0.1
                                    //46#油缸压力当前值		2,3		0.1
                                    //47#油缸压力当前值		4,5		0.1
                                    //48#油缸压力当前值		6,7		0.1

                                    #endregion
                                    case 0x3001:
                                    case 0x3002:
                                    case 0x3003:
                                    case 0x3004:
                                    case 0x3005:
                                    case 0x3006:
                                    case 0x3007:
                                    case 0x3008:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = Convert.ToByte(idArray[0] - 1);//0~7

                                        Idx_Pump = idArray0 / 2;//0~3
                                        Idx_Cylinder = (((idArray0 % 2) != 0) ? 4 : 0);

                                        vs_High = (short)msgRecieve[j].data[1];
                                        vs_High = (short)(vs_High << 8);
                                        vs_low = msgRecieve[j].data[0];
                                        CanDatapool.in_Pressure_Real_3001_3008[(int)(Idx_Pump * 8 + Idx_Cylinder + 0)] =
                                            (vs_High | vs_low);
                                        //msgRecieve[j].data[0] + (msgRecieve[j].data[1] << 8);

                                        vs_High = (short)msgRecieve[j].data[3];
                                        vs_High = (short)(vs_High << 8);
                                        vs_low = msgRecieve[j].data[2];
                                        CanDatapool.in_Pressure_Real_3001_3008[(int)(Idx_Pump * 8 + Idx_Cylinder + 1)] =
                                            (vs_High | vs_low);
                                        //msgRecieve[j].data[2] + (msgRecieve[j].data[3] << 8);

                                        vs_High = (short)msgRecieve[j].data[5];
                                        vs_High = (short)(vs_High << 8);
                                        vs_low = msgRecieve[j].data[4];
                                        CanDatapool.in_Pressure_Real_3001_3008[(int)(Idx_Pump * 8 + Idx_Cylinder + 2)] =
                                            (vs_High | vs_low);
                                        //msgRecieve[j].data[4] + (msgRecieve[j].data[5] << 8);

                                        vs_High = (short)msgRecieve[j].data[7];
                                        vs_High = (short)(vs_High << 8);
                                        vs_low = msgRecieve[j].data[6];
                                        CanDatapool.in_Pressure_Real_3001_3008[(int)(Idx_Pump * 8 + Idx_Cylinder + 3)] =
                                                (vs_High | vs_low);
                                        //msgRecieve[j].data[6] + (msgRecieve[j].data[7] << 8);

                                        break;

                                    #region 3101-3108
                                    //3101
                                    //11#油缸当前长度值		0,1		0.1
                                    //12#油缸当前长度值		2,3		0.1
                                    //13#油缸当前长度值		4,5		0.1
                                    //14#油缸当前长度值		6,7		0.1
                                    //3102
                                    //15#油缸当前长度值		0,1		0.1
                                    //16#油缸当前长度值		2,3		0.1
                                    //17#油缸当前长度值		4,5		0.1
                                    //18#油缸当前长度值		6,7		0.1
                                    //3103
                                    //21#油缸当前长度值		0,1		0.1
                                    //22#油缸当前长度值		2,3		0.1
                                    //23#油缸当前长度值		4,5		0.1
                                    //24#油缸当前长度值		6,7		0.1
                                    //3104
                                    //25#油缸当前长度值		0,1		0.1
                                    //26#油缸当前长度值		2,3		0.1
                                    //27#油缸当前长度值		4,5		0.1
                                    //28#油缸当前长度值		6,7		0.1
                                    //3105
                                    //31#油缸当前长度值		0,1		0.1
                                    //32#油缸当前长度值		2,3		0.1
                                    //33#油缸当前长度值		4,5		0.1
                                    //34#油缸当前长度值		6,7		0.1
                                    //3106
                                    //35#油缸当前长度值		0,1		0.1
                                    //36#油缸当前长度值		2,3		0.1
                                    //37#油缸当前长度值		4,5		0.1
                                    //38#油缸当前长度值		6,7		0.1
                                    //3107
                                    //41#油缸当前长度值		0,1		0.1
                                    //42#油缸当前长度值		2,3		0.1
                                    //43#油缸当前长度值		4,5		0.1
                                    //44#油缸当前长度值		6,7		0.1
                                    //3108
                                    //45#油缸当前长度值		0,1		0.1
                                    //46#油缸当前长度值		2,3		0.1
                                    //47#油缸当前长度值		4,5		0.1
                                    //48#油缸当前长度值		6,7		0.1

                                    #endregion
                                    case 0x3101:
                                    case 0x3102:
                                    case 0x3103:
                                    case 0x3104:
                                    case 0x3105:
                                    case 0x3106:
                                    case 0x3107:
                                    case 0x3108:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = Convert.ToByte(idArray[0] - 1);//0~7

                                        Idx_Pump = idArray0 / 2;//0~3
                                        Idx_Cylinder = (((idArray0 % 2) != 0) ? 4 : 0);

                                        vs_High = (short)msgRecieve[j].data[1];
                                        vs_High = (short)(vs_High << 8);
                                        vs_low = msgRecieve[j].data[0];
                                        CanDatapool.in_Position_Real_3101_3108[(int)(Idx_Pump * 8 + Idx_Cylinder + 0)] =
                                               (vs_High | vs_low);
                                        //msgRecieve[j].data[0] + (msgRecieve[j].data[1] << 8);

                                        //Idx_Cylinder = 2 + (((idArray0 % 2) == 0) ? 4 : 0);
                                        vs_High = (short)msgRecieve[j].data[3];
                                        vs_High = (short)(vs_High << 8);
                                        vs_low = msgRecieve[j].data[2];
                                        CanDatapool.in_Position_Real_3101_3108[(int)(Idx_Pump * 8 + Idx_Cylinder + 1)] =
                                               (vs_High | vs_low);
                                        //msgRecieve[j].data[2] + (msgRecieve[j].data[3] << 8);

                                        //Idx_Cylinder = 3 + (((idArray0 % 2) == 0) ? 4 : 0);
                                        vs_High = (short)msgRecieve[j].data[5];
                                        vs_High = (short)(vs_High << 8);
                                        vs_low = msgRecieve[j].data[4];
                                        CanDatapool.in_Position_Real_3101_3108[(int)(Idx_Pump * 8 + Idx_Cylinder + 2)] =
                                               (vs_High | vs_low);
                                        //msgRecieve[j].data[4] + (msgRecieve[j].data[5] << 8);

                                        //Idx_Cylinder = 4 + (((idArray0 % 2) == 0) ? 4 : 0);
                                        vs_High = (short)msgRecieve[j].data[7];
                                        vs_High = (short)(vs_High << 8);
                                        vs_low = msgRecieve[j].data[6];
                                        CanDatapool.in_Position_Real_3101_3108[(int)(Idx_Pump * 8 + Idx_Cylinder + 3)] =
                                               (vs_High | vs_low);
                                        //msgRecieve[j].data[6] + (msgRecieve[j].data[7] << 8);

                                        break;
                                    #region 3201-3208
                                    //0:停，1：伸 2：缩
                                    //3201
                                    //11#油缸运行状态		0		1
                                    //12#油缸运行状态		1		1
                                    //13#油缸运行状态		2		1
                                    //14#油缸运行状态		3		1
                                    //15#油缸运行状态		4		1
                                    //16#油缸运行状态		5		1
                                    //17#油缸运行状态		6		1
                                    //18#油缸运行状态		7		1
                                    //3202
                                    //11#油缸机械锁运行状态		0		1
                                    //12#油缸机械锁运行状态		1		1
                                    //13#油缸机械锁运行状态		2		1
                                    //14#油缸机械锁运行状态		3		1
                                    //15#油缸机械锁运行状态		4		1
                                    //16#油缸机械锁运行状态		5		1
                                    //17#油缸机械锁运行状态		6		1
                                    //18#油缸机械锁运行状态		7		1
                                    //3203
                                    //21#油缸运行状态		0		1
                                    //22#油缸运行状态		1		1
                                    //23#油缸运行状态		2		1
                                    //24#油缸运行状态		3		1
                                    //25#油缸运行状态		4		1
                                    //26#油缸运行状态		5		1
                                    //27#油缸运行状态		6		1
                                    //28#油缸运行状态		7		1
                                    //3204
                                    //21#油缸机械锁运行状态		0		1
                                    //22#油缸机械锁运行状态		1		1
                                    //23#油缸机械锁运行状态		2		1
                                    //24#油缸机械锁运行状态		3		1
                                    //25#油缸机械锁运行状态		4		1
                                    //26#油缸机械锁运行状态		5		1
                                    //27#油缸机械锁运行状态		6		1
                                    //28#油缸机械锁运行状态		7		1
                                    //3205
                                    //31#油缸运行状态		0		1
                                    //32#油缸运行状态		1		1
                                    //33#油缸运行状态		2		1
                                    //34#油缸运行状态		3		1
                                    //35#油缸运行状态		4		1
                                    //36#油缸运行状态		5		1
                                    //37#油缸运行状态		6		1
                                    //38#油缸运行状态		7		1
                                    //3206
                                    //31#油缸机械锁运行状态		0		1
                                    //32#油缸机械锁运行状态		1		1
                                    //33#油缸机械锁运行状态		2		1
                                    //34#油缸机械锁运行状态		3		1
                                    //35#油缸机械锁运行状态		4		1
                                    //36#油缸机械锁运行状态		5		1
                                    //37#油缸机械锁运行状态		6		1
                                    //38#油缸机械锁运行状态		7		1
                                    //3207
                                    //41#油缸运行状态		0		1
                                    //42#油缸运行状态		1		1
                                    //33#油缸运行状态		2		1
                                    //44#油缸运行状态		3		1
                                    //45#油缸运行状态		4		1
                                    //46#油缸运行状态		5		1
                                    //47#油缸运行状态		6		1
                                    //48#油缸运行状态		7		1
                                    //3208
                                    //41#油缸机械锁运行状态		0		1
                                    //42#油缸机械锁运行状态		1		1
                                    //43#油缸机械锁运行状态		2		1
                                    //44#油缸机械锁运行状态		3		1
                                    //45#油缸机械锁运行状态		4		1
                                    //46#油缸机械锁运行状态		5		1
                                    //47#油缸机械锁运行状态		6		1
                                    //48#油缸机械锁运行状态		7		1

                                    #endregion
                                    case 0x3201:
                                    case 0x3202:
                                    case 0x3203:
                                    case 0x3204:
                                    case 0x3205:
                                    case 0x3206:
                                    case 0x3207:
                                    case 0x3208:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = idArray[0];

                                        if ((idArray0 % 2) == 0)//偶数 机械锁运行状态
                                        {
                                            for (int k = 0; k < msgRecieve[j].data.Length; k++)
                                            {
                                                CanDatapool.in_MachLockState_Real_3201_3208[(idArray0 / 2 - 1) * 8 + k] = (MotionStateType)msgRecieve[j].data[k];
                                            }
                                        }
                                        else //奇数 油缸运行状态
                                        {
                                            for (int k = 0; k < msgRecieve[j].data.Length; k++)
                                            {
                                                CanDatapool.in_cylinderState_Real_3201_3208[(idArray0 / 2 - 1) * 8 + k] = (MotionStateType)msgRecieve[j].data[k];
                                            }
                                        }

                                        break;

                                    #region 3301-3304
                                    //泵站压力及接近开关状态
                                    //3301
                                    //1#泵站压力		                    0,1		0.1		
                                    //1#控制器电压		                    2,3		0.1		
                                    //1#控制器检测当前供电		    4		    1	0：市电 1：发电机	
                                    //11#油缸5mm接近开关限位		5	0		0:限位 1：未限位	
                                    //12#油缸5mm接近开关限位			1		0:限位 1：未限位	
                                    //13#油缸5mm接近开关限位			2		0:限位 1：未限位	
                                    //14#油缸5mm接近开关限位			3		0:限位 1：未限位	
                                    //15#油缸5mm接近开关限位			4		0:限位 1：未限位	
                                    //16#油缸5mm接近开关限位			5		0:限位 1：未限位	
                                    //17#油缸5mm接近开关限位			6		0:限位 1：未限位	
                                    //18#油缸5mm接近开关限位			7		0:限位 1：未限位	
                                    //11#油缸10mm接近开关限位	6	0		0:限位 1：未限位	
                                    //12#油缸10mm接近开关限位		1		0:限位 1：未限位	
                                    //13#油缸10mm接近开关限位		2		0:限位 1：未限位	
                                    //14#油缸10mm接近开关限位		3		0:限位 1：未限位	
                                    //15#油缸10mm接近开关限位		4		0:限位 1：未限位	
                                    //16#油缸10mm接近开关限位		5		0:限位 1：未限位	
                                    //17#油缸10mm接近开关限位		6		0:限位 1：未限位	
                                    //18#油缸10mm接近开关限位		7		0:限位 1：未限位	
                                    //3302
                                    //2#泵站压力		                    0,1		0.1		
                                    //2#控制器电压		                    2,3		0.1		
                                    //2#控制器检测当前供电		    4		    1	0：市电 1：发电机	
                                    //21#油缸5mm接近开关限位		5	0		0:限位 1：未限位	
                                    //22#油缸5mm接近开关限位			1		0:限位 1：未限位	
                                    //23#油缸5mm接近开关限位			2		0:限位 1：未限位	
                                    //24#油缸5mm接近开关限位			3		0:限位 1：未限位	
                                    //25#油缸5mm接近开关限位			4		0:限位 1：未限位	
                                    //26#油缸5mm接近开关限位			5		0:限位 1：未限位	
                                    //27#油缸5mm接近开关限位			6		0:限位 1：未限位	
                                    //28#油缸5mm接近开关限位			7		0:限位 1：未限位	
                                    //21#油缸10mm接近开关限位	6	0		0:限位 1：未限位	
                                    //22#油缸10mm接近开关限位		1		0:限位 1：未限位	
                                    //23#油缸10mm接近开关限位		2		0:限位 1：未限位	
                                    //24#油缸10mm接近开关限位		3		0:限位 1：未限位	
                                    //25#油缸10mm接近开关限位		4		0:限位 1：未限位	
                                    //26#油缸10mm接近开关限位		5		0:限位 1：未限位	
                                    //27#油缸10mm接近开关限位		6		0:限位 1：未限位	
                                    //28#油缸10mm接近开关限位		7		0:限位 1：未限位
                                    //    3303
                                    //3#泵站压力		                    0,1		0.1		
                                    //3#控制器电压		                    2,3		0.1		
                                    //3#控制器检测当前供电		    4		    1	0：市电 1：发电机	
                                    //31#油缸5mm接近开关限位		5	0		0:限位 1：未限位	
                                    //32#油缸5mm接近开关限位			1		0:限位 1：未限位	
                                    //33#油缸5mm接近开关限位			2		0:限位 1：未限位	
                                    //34#油缸5mm接近开关限位			3		0:限位 1：未限位	
                                    //35#油缸5mm接近开关限位			4		0:限位 1：未限位	
                                    //36#油缸5mm接近开关限位			5		0:限位 1：未限位	
                                    //37#油缸5mm接近开关限位			6		0:限位 1：未限位	
                                    //38#油缸5mm接近开关限位			7		0:限位 1：未限位	
                                    //31#油缸10mm接近开关限位	6	0		0:限位 1：未限位	
                                    //32#油缸10mm接近开关限位		1		0:限位 1：未限位	
                                    //33#油缸10mm接近开关限位		2		0:限位 1：未限位	
                                    //34#油缸10mm接近开关限位		3		0:限位 1：未限位	
                                    //35#油缸10mm接近开关限位		4		0:限位 1：未限位	
                                    //36#油缸10mm接近开关限位		5		0:限位 1：未限位	
                                    //37#油缸10mm接近开关限位		6		0:限位 1：未限位	
                                    //38#油缸10mm接近开关限位		7		0:限位 1：未限位	
                                    //3304
                                    //4#泵站压力		                    0,1		0.1		
                                    //4#控制器电压		                    2,3		0.1		
                                    //4#控制器检测当前供电		    4		1	0：市电 1：发电机	
                                    //41#油缸5mm接近开关限位		5	0		0:限位 1：未限位	
                                    //42#油缸5mm接近开关限位			1		0:限位 1：未限位	
                                    //43#油缸5mm接近开关限位			2		0:限位 1：未限位	
                                    //44#油缸5mm接近开关限位			3		0:限位 1：未限位	
                                    //45#油缸5mm接近开关限位			4		0:限位 1：未限位	
                                    //46#油缸5mm接近开关限位			5		0:限位 1：未限位	
                                    //47#油缸5mm接近开关限位			6		0:限位 1：未限位	
                                    //48#油缸5mm接近开关限位			7		0:限位 1：未限位	
                                    //41#油缸10mm接近开关限位	6	0		0:限位 1：未限位	
                                    //42#油缸10mm接近开关限位		1		0:限位 1：未限位	
                                    //43#油缸10mm接近开关限位		2		0:限位 1：未限位	
                                    //44#油缸10mm接近开关限位		3		0:限位 1：未限位	
                                    //45#油缸10mm接近开关限位		4		0:限位 1：未限位	
                                    //46#油缸10mm接近开关限位		5		0:限位 1：未限位	
                                    //47#油缸10mm接近开关限位		6		0:限位 1：未限位	
                                    //48#油缸10mm接近开关限位		7		0:限位 1：未限位	

                                    #endregion
                                    case 0x3301:
                                    case 0x3302:
                                    case 0x3303:
                                    case 0x3304:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = Convert.ToByte(idArray[0] - 1);

                                        vs_High = (short)msgRecieve[j].data[1];
                                        vs_High = (short)(vs_High << 8);
                                        vs_low = msgRecieve[j].data[0];
                                        CanDatapool.in_Pressure_Pump_Real_3301_3304[idArray0] =
                                            (vs_High | vs_low);
                                        //msgRecieve[j].data[0] + (msgRecieve[j].data[1] << 8);

                                        vs_High = (short)msgRecieve[j].data[3];
                                        vs_High = (short)(vs_High << 8);
                                        vs_low = msgRecieve[j].data[2];
                                        CanDatapool.in_Voltage_Real_3301_3304[idArray0] =
                                            (vs_High | vs_low);
                                        //msgRecieve[j].data[2] + (msgRecieve[j].data[3] << 8);

                                        //CanDatapool.in_PowerSupply_3301_3304[idArray0] = (msgRecieve[j].data[4] != 0);
                                        CanDatapool.in_PowerSupply_3301_3304[idArray0] = msgRecieve[j].data[4];

                                        for (int k = 0; k < 8; k++)
                                        {
                                            CanDatapool.in_Limit_5_3301_3304[idArray0 * 8 + k] = ((msgRecieve[j].data[5] & (1 << k)) != 0);

                                            CanDatapool.in_Limit_10_3301_3304[idArray0 * 8 + k] = ((msgRecieve[j].data[6] & (1 << k)) != 0);
                                        }

                                        break;

                                    #region 3401-3404
                                    //报警信息

                                    //0:无报警 1：报警
                                    //3401
                                    //11#油缸压力过低		0	0	
                                    //12#油缸压力过低			1	
                                    //13#油缸压力过低			2	
                                    //14#油缸压力过低			3	
                                    //15#油缸压力过低			4	
                                    //16#油缸压力过低			5	
                                    //17#油缸压力过低			6	
                                    //18#油缸压力过低			7	
                                    //11#油缸压力过高		1	0	
                                    //12#油缸压力过高			1	
                                    //13#油缸压力过高			2	
                                    //14#油缸压力过高			3	
                                    //15#油缸压力过高			4	
                                    //16#油缸压力过高			5	
                                    //17#油缸压力过高			6	
                                    //18#油缸压力过高			7	
                                    //11#油缸长度过低		2	0	
                                    //12#油缸长度过低			1	
                                    //13#油缸长度过低			2	
                                    //14#油缸长度过低			3	
                                    //15#油缸长度过低			4	
                                    //16#油缸长度过低			5	
                                    //17#油缸长度过低			6	
                                    //18#油缸长度过低			7	
                                    //11#油缸长度过高		3	0	
                                    //12#油缸长度过高			1	
                                    //13#油缸长度过高			2	
                                    //14#油缸长度过高			3	
                                    //15#油缸长度过高			4	
                                    //16#油缸长度过高			5	
                                    //17#油缸长度过高			6	
                                    //18#油缸长度过高			7	
                                    //液压油位过低		    4	0		0:无报警 1：报警	
                                    //液压油位过高			    1		0:无报警 1：报警	
                                    //1#泵站压力不足			2		0:无报警 1：报警	
                                    //11#马达压力过大		5	0		0:无报警 1：报警	
                                    //12#马达压力过大			1		0:无报警 1：报警	
                                    //13#马达压力过大			2		0:无报警 1：报警	
                                    //14#马达压力过大			3		0:无报警 1：报警	
                                    //15#马达压力过大			4		0:无报警 1：报警	
                                    //16#马达压力过大			5		0:无报警 1：报警	
                                    //17#马达压力过大			6		0:无报警 1：报警	
                                    //18#马达压力过大			7		0:无报警 1：报警	
                                    //11#油缸行程过大		6	0		0:无报警 1：报警	
                                    //12#油缸行程过大			1		0:无报警 1：报警	
                                    //13#油缸行程过大			2		0:无报警 1：报警	
                                    //14#油缸行程过大			3		0:无报警 1：报警	
                                    //15#油缸行程过大			4		0:无报警 1：报警	
                                    //16#油缸行程过大			5		0:无报警 1：报警	
                                    //17#油缸行程过大			6		0:无报警 1：报警	
                                    //18#油缸行程过大			7		0:无报警 1：报警	
                                    //11#油缸当天位移过大7	0		0:无报警 1：报警	
                                    //12#油缸当天位移过大	1		0:无报警 1：报警	
                                    //13#油缸当天位移过大	2		0:无报警 1：报警	
                                    //14#油缸当天位移过大	3		0:无报警 1：报警	
                                    //15#油缸当天位移过大	4		0:无报警 1：报警	
                                    //16#油缸当天位移过大	5		0:无报警 1：报警	
                                    //17#油缸当天位移过大	6		0:无报警 1：报警	
                                    //18#油缸当天位移过大	7		0:无报警 1：报警	
                                    //3402
                                    //21#油缸压力过低		0	0	
                                    //22#油缸压力过低			1	
                                    //23#油缸压力过低			2	
                                    //24#油缸压力过低			3	
                                    //25#油缸压力过低			4	
                                    //26#油缸压力过低			5	
                                    //27#油缸压力过低			6	
                                    //28#油缸压力过低			7	
                                    //21#油缸压力过高		1	0	
                                    //22#油缸压力过高			1	
                                    //23#油缸压力过高			2	
                                    //24#油缸压力过高			3	
                                    //25#油缸压力过高			4	
                                    //26#油缸压力过高			5	
                                    //27#油缸压力过高			6	
                                    //28#油缸压力过高			7	
                                    //21#油缸长度过低		2	0	
                                    //22#油缸长度过低			1	
                                    //23#油缸长度过低			2	
                                    //24#油缸长度过低			3	
                                    //25#油缸长度过低			4	
                                    //26#油缸长度过低			5	
                                    //27#油缸长度过低			6	
                                    //28#油缸长度过低			7	
                                    //21#油缸长度过高		3	0	
                                    //22#油缸长度过高			1	
                                    //23#油缸长度过高			2	
                                    //24#油缸长度过高			3	
                                    //25#油缸长度过高			4	
                                    //26#油缸长度过高			5	
                                    //27#油缸长度过高			6	
                                    //28#油缸长度过高			7	
                                    //液压油位过低		    4	0		0:无报警 1：报警	
                                    //液压油位过高			    1		0:无报警 1：报警	
                                    //2#泵站压力不足			2		0:无报警 1：报警	
                                    //21#马达压力过大		5	0		0:无报警 1：报警	
                                    //22#马达压力过大			1		0:无报警 1：报警	
                                    //23#马达压力过大			2		0:无报警 1：报警	
                                    //24#马达压力过大			3		0:无报警 1：报警	
                                    //25#马达压力过大			4		0:无报警 1：报警	
                                    //26#马达压力过大			5		0:无报警 1：报警	
                                    //27#马达压力过大			6		0:无报警 1：报警	
                                    //28#马达压力过大			7		0:无报警 1：报警	
                                    //21#油缸行程过大		6	0		0:无报警 1：报警	
                                    //22#油缸行程过大			1		0:无报警 1：报警	
                                    //23#油缸行程过大			2		0:无报警 1：报警	
                                    //24#油缸行程过大			3		0:无报警 1：报警	
                                    //25#油缸行程过大			4		0:无报警 1：报警	
                                    //26#油缸行程过大			5		0:无报警 1：报警	
                                    //27#油缸行程过大			6		0:无报警 1：报警	
                                    //28#油缸行程过大			7		0:无报警 1：报警	
                                    //21#油缸当天位移过大7	0		0:无报警 1：报警	
                                    //22#油缸当天位移过大	1		0:无报警 1：报警	
                                    //23#油缸当天位移过大	2		0:无报警 1：报警	
                                    //24#油缸当天位移过大	3		0:无报警 1：报警	
                                    //25#油缸当天位移过大	4		0:无报警 1：报警	
                                    //26#油缸当天位移过大	5		0:无报警 1：报警	
                                    //27#油缸当天位移过大	6		0:无报警 1：报警	
                                    //28#油缸当天位移过大	7		0:无报警 1：报警	
                                    //3403
                                    //31#油缸压力过低		0	0	
                                    //32#油缸压力过低			1	
                                    //33#油缸压力过低			2	
                                    //34#油缸压力过低			3	
                                    //35#油缸压力过低			4	
                                    //36#油缸压力过低			5	
                                    //37#油缸压力过低			6	
                                    //38#油缸压力过低			7	
                                    //31#油缸压力过高		1	0	
                                    //32#油缸压力过高			1	
                                    //33#油缸压力过高			2	
                                    //34#油缸压力过高			3	
                                    //35#油缸压力过高			4	
                                    //36#油缸压力过高			5	
                                    //37#油缸压力过高			6	
                                    //38#油缸压力过高			7	
                                    //31#油缸长度过低		2	0	
                                    //32#油缸长度过低			1	
                                    //33#油缸长度过低			2	
                                    //34#油缸长度过低			3	
                                    //35#油缸长度过低			4	
                                    //36#油缸长度过低			5	
                                    //37#油缸长度过低			6	
                                    //38#油缸长度过低			7	
                                    //31#油缸长度过高		3	0	
                                    //32#油缸长度过高			1	
                                    //33#油缸长度过高			2	
                                    //34#油缸长度过高			3	
                                    //35#油缸长度过高			4	
                                    //36#油缸长度过高			5	
                                    //37#油缸长度过高			6	
                                    //38#油缸长度过高			7
                                    //液压油位过低		    4	0		0:无报警 1：报警	
                                    //液压油位过高			    1		0:无报警 1：报警	
                                    //3#泵站压力不足			2		0:无报警 1：报警	
                                    //31#马达压力过大		5	0		0:无报警 1：报警	
                                    //32#马达压力过大			1		0:无报警 1：报警	
                                    //33#马达压力过大			2		0:无报警 1：报警	
                                    //34#马达压力过大			3		0:无报警 1：报警	
                                    //35#马达压力过大			4		0:无报警 1：报警	
                                    //36#马达压力过大			5		0:无报警 1：报警	
                                    //37#马达压力过大			6		0:无报警 1：报警	
                                    //38#马达压力过大			7		0:无报警 1：报警	
                                    //31#油缸行程过大		6	0		0:无报警 1：报警	
                                    //32#油缸行程过大			1		0:无报警 1：报警	
                                    //33#油缸行程过大			2		0:无报警 1：报警	
                                    //34#油缸行程过大			3		0:无报警 1：报警	
                                    //35#油缸行程过大			4		0:无报警 1：报警	
                                    //36#油缸行程过大			5		0:无报警 1：报警	
                                    //37#油缸行程过大			6		0:无报警 1：报警	
                                    //38#油缸行程过大			7		0:无报警 1：报警	
                                    //31#油缸当天位移过大7	0		0:无报警 1：报警	
                                    //32#油缸当天位移过大	1		0:无报警 1：报警	
                                    //33#油缸当天位移过大	2		0:无报警 1：报警	
                                    //34#油缸当天位移过大	3		0:无报警 1：报警	
                                    //35#油缸当天位移过大	4		0:无报警 1：报警	
                                    //36#油缸当天位移过大	5		0:无报警 1：报警	
                                    //37#油缸当天位移过大	6		0:无报警 1：报警	
                                    //38#油缸当天位移过大	7		0:无报警 1：报警	
                                    //    3404
                                    //41#油缸压力过低		0	0	
                                    //42#油缸压力过低			1	
                                    //43#油缸压力过低			2	
                                    //44#油缸压力过低			3	
                                    //45#油缸压力过低			4	
                                    //46#油缸压力过低			5	
                                    //47#油缸压力过低			6	
                                    //48#油缸压力过低			7	
                                    //41#油缸压力过高		1	0	
                                    //42#油缸压力过高			1	
                                    //43#油缸压力过高			2	
                                    //44#油缸压力过高			3	
                                    //45#油缸压力过高			4	
                                    //46#油缸压力过高			5	
                                    //47#油缸压力过高			6	
                                    //48#油缸压力过高			7	
                                    //41#油缸长度过低		2	0	
                                    //42#油缸长度过低			1	
                                    //43#油缸长度过低			2	
                                    //44#油缸长度过低			3	
                                    //45#油缸长度过低			4	
                                    //46#油缸长度过低			5	
                                    //47#油缸长度过低			6	
                                    //48#油缸长度过低			7	
                                    //41#油缸长度过高		3	0	
                                    //42#油缸长度过高			1	
                                    //43#油缸长度过高			2	
                                    //44#油缸长度过高			3	
                                    //45#油缸长度过高			4	
                                    //46#油缸长度过高			5	
                                    //47#油缸长度过高			6	
                                    //48#油缸长度过高			7	
                                    //液压油位过低		    4	0		0:无报警 1：报警	
                                    //液压油位过高			    1		0:无报警 1：报警	
                                    //4#泵站压力不足			2		0:无报警 1：报警	
                                    //41#马达压力过大		5	0		0:无报警 1：报警	
                                    //42#马达压力过大			1		0:无报警 1：报警	
                                    //43#马达压力过大			2		0:无报警 1：报警	
                                    //44#马达压力过大			3		0:无报警 1：报警	
                                    //45#马达压力过大			4		0:无报警 1：报警	
                                    //46#马达压力过大			5		0:无报警 1：报警	
                                    //47#马达压力过大			6		0:无报警 1：报警	
                                    //48#马达压力过大			7		0:无报警 1：报警	
                                    //41#油缸行程过大		6	0		0:无报警 1：报警	
                                    //42#油缸行程过大			1		0:无报警 1：报警	
                                    //43#油缸行程过大			2		0:无报警 1：报警	
                                    //44#油缸行程过大			3		0:无报警 1：报警	
                                    //45#油缸行程过大			4		0:无报警 1：报警	
                                    //46#油缸行程过大			5		0:无报警 1：报警	
                                    //47#油缸行程过大			6		0:无报警 1：报警	
                                    //48#油缸行程过大			7		0:无报警 1：报警	
                                    //41#油缸当天位移过大7	0		0:无报警 1：报警	
                                    //42#油缸当天位移过大	1		0:无报警 1：报警	
                                    //43#油缸当天位移过大	2		0:无报警 1：报警	
                                    //44#油缸当天位移过大	3		0:无报警 1：报警	
                                    //45#油缸当天位移过大	4		0:无报警 1：报警	
                                    //46#油缸当天位移过大	5		0:无报警 1：报警	
                                    //47#油缸当天位移过大	6		0:无报警 1：报警	
                                    //48#油缸当天位移过大	7		0:无报警 1：报警	
                                    #endregion
                                    case 0x3401:
                                    case 0x3402:
                                    case 0x3403:
                                    case 0x3404:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = idArray[0];

                                        for (int k = 0; k < 8; k++)
                                        {
                                            CanDatapool.in_Warn_LowPressure_3401_3404[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[0] & (1 << k)) != 0);
                                            CanDatapool.in_Warn_HighPressure_3401_3404[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[1] & (1 << k)) != 0);
                                            CanDatapool.in_Warn_LowPosition_3401_3404[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[2] & (1 << k)) != 0);
                                            CanDatapool.in_Warn_HighPosition_3401_3404[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[3] & (1 << k)) != 0);

                                            CanDatapool.in_Warn_PumpPressureHighout_3401_3404[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[5] & (1 << k)) != 0);//泵站马达压力过大 4*8
                                            CanDatapool.in_Warn_PumpPositionHighout_3401_3404[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[6] & (1 << k)) != 0);//泵站油缸行程过大 4*8
                                            CanDatapool.in_Warn_PumpTodayPositionHighout_3401_3404[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[7] & (1 << k)) != 0);//泵站油缸当天位移过大 4*8
                                        }

                                        CanDatapool.in_Warn_PumpLevelLowout_3401_3404[(idArray0 - 1)] = ((msgRecieve[j].data[4] & (1 << 0)) != 0);//泵站油位过低 4
                                        CanDatapool.in_Warn_PumpLevelHighout_3401_3404[(idArray0 - 1)] = ((msgRecieve[j].data[4] & (1 << 1)) != 0);//泵站油位过高 4
                                        CanDatapool.in_Warn_PumpNotReach_3401_3404[(idArray0 - 1)] = ((msgRecieve[j].data[4] & (1 << 2)) != 0);//泵站达不到设定值 4

                                        break;
                                    #region 3501-3504
                                    //故障信息
                                    //0：无故障 1：故障
                                    //3501
                                    //1#泵站电动机启动线路短路		           0	0	
                                    //1#泵站电动机启动线路断路			            1	
                                    //1#泵站冗余电磁阀线路短路			            2	
                                    //1#泵站冗余电磁阀线路断路			            3	
                                    //1#泵站机械锁马达电磁阀线路短路		    	4	
                                    //1#泵站机械锁马达电磁阀线路断路			    5	
                                    //1#控制器发电机启动线路短路			        6	
                                    //1#控制器发电机启动线路断路			        7	
                                    //1#泵站比例阀线路短路		                    1	0	
                                    //1#泵站比例阀线路断路			                    1	
                                    //11#油缸压力传感器故障		                2	0	
                                    //12#油缸压力传感器故障			                1	
                                    //13#油缸压力传感器故障			                2	
                                    //14#油缸压力传感器故障			                3	
                                    //15#油缸压力传感器故障			                4	
                                    //16#油缸压力传感器故障			                5	
                                    //17#油缸压力传感器故障			                6	
                                    //18#油缸压力传感器故障			                7	
                                    //11#油缸长度传感器故障		                3	0	
                                    //12#油缸长度传感器故障			                1	
                                    //13#油缸长度传感器故障			                2	
                                    //14#油缸长度传感器故障			                3	
                                    //15#油缸长度传感器故障			                4	
                                    //16#油缸长度传感器故障			                5	
                                    //17#油缸长度传感器故障			                6	
                                    //18#油缸长度传感器故障			                7	
                                    //3502
                                    //2#泵站电动机启动线路短路		           0	0	
                                    //2#泵站电动机启动线路断路		              	1	
                                    //2#泵站冗余电磁阀线路短路		              	2	
                                    //2#泵站冗余电磁阀线路断路		              	3	
                                    //2#泵站机械锁马达电磁阀线路短路		    	4	
                                    //2#泵站机械锁马达电磁阀线路断路		    	5	
                                    //2#控制器发电机启动线路短路			        6	
                                    //2#控制器发电机启动线路断路			        7	
                                    //2#泵站比例阀线路短路              		    1	0	
                                    //2#泵站比例阀线路断路              			    1	
                                    //21#油缸压力传感器故障                 	   2	0	
                                    //22#油缸压力传感器故障              	    		1	
                                    //23#油缸压力传感器故障               			2	
                                    //24#油缸压力传感器故障               			3	
                                    //25#油缸压力传感器故障                			4	
                                    //26#油缸压力传感器故障	                  		5	
                                    //27#油缸压力传感器故障	                  		6	
                                    //28#油缸压力传感器故障	                  		7	
                                    //21#油缸长度传感器故障	                  	3	0	
                                    //22#油缸长度传感器故障	                  		1	
                                    //23#油缸长度传感器故障			              	2	
                                    //24#油缸长度传感器故障			              	3	
                                    //25#油缸长度传感器故障			              	4	
                                    //26#油缸长度传感器故障			              	5	
                                    //27#油缸长度传感器故障			              	6	
                                    //28#油缸长度传感器故障		    		        7	
                                    //3503                                     
                                    //3#泵站电动机启动线路短路	    	        0	0	
                                    //3#泵站电动机启动线路断路	    	    	    1	
                                    //3#泵站冗余电磁阀线路短路	    	    	    2	
                                    //3#泵站冗余电磁阀线路断路			    	    3	
                                    //3#泵站机械锁马达电磁阀线路短路		    	4	
                                    //3#泵站机械锁马达电磁阀线路断路		    	5	
                                    //3#控制器发电机启动线路短路				    6	
                                    //3#控制器发电机启动线路断路			    	7	
                                    //3#泵站比例阀线路短路	                   	    1	0	
                                    //3#泵站比例阀线路断路	                      		1		
                                    //31#油缸压力传感器故障	                	2	0		
                                    //32#油缸压力传感器故障	                		1	
                                    //33#油缸压力传感器故障	                		2	
                                    //34#油缸压力传感器故障							3	
                                    //35#油缸压力传感器故障							4	
                                    //36#油缸压力传感器故障							5	
                                    //37#油缸压力传感器故障							6	
                                    //38#油缸压力传感器故障							7	
                                    //31#油缸长度传感器故障						3	0	
                                    //32#油缸长度传感器故障			                1	
                                    //33#油缸长度传感器故障			                2	
                                    //34#油缸长度传感器故障			                3	
                                    //35#油缸长度传感器故障			                4	
                                    //36#油缸长度传感器故障			                5	
                                    //37#油缸长度传感器故障							6	
                                    //38#油缸长度传感器故障							7	
                                    //3504                                    			 	
                                    //4#泵站电动机启动线路短路					0	0	
                                    //4#泵站电动机启动线路断路						1	
                                    //4#泵站冗余电磁阀线路短路						2	
                                    //4#泵站冗余电磁阀线路断路			            3	
                                    //4#泵站机械锁马达电磁阀线路短路	    		4	
                                    //4#泵站机械锁马达电磁阀线路断路		    	5	
                                    //4#控制器发电机启动线路短路			        6	
                                    //4#控制器发电机启动线路断路			        7	
                                    //4#泵站比例阀线路短路	                     	1	0	
                                    //4#泵站比例阀线路断路	                	    	1	
                                    //41#油缸压力传感器故障	                	2	0	
                                    //42#油缸压力传感器故障	                		1	
                                    //43#油缸压力传感器故障	                		2	
                                    //44#油缸压力传感器故障							3	
                                    //45#油缸压力传感器故障							4	
                                    //46#油缸压力传感器故障							5	
                                    //47#油缸压力传感器故障							6	
                                    //48#油缸压力传感器故障							7	
                                    //41#油缸长度传感器故障						3	0	
                                    //42#油缸长度传感器故障		                	1	
                                    //43#油缸长度传感器故障		                	2	
                                    //44#油缸长度传感器故障		                	3	
                                    //45#油缸长度传感器故障		                	4	
                                    //46#油缸长度传感器故障		                	5	
                                    //47#油缸长度传感器故障							6	
                                    //48#油缸长度传感器故障							7	

                                    #endregion
                                    case 0x3501:
                                    case 0x3502:
                                    case 0x3503:
                                    case 0x3504:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = Convert.ToByte(idArray[0] - 1);

                                        for (int k = 0; k < 8; k++)//位
                                        {
                                            //0
                                            //0泵站电动机启动线路短路
                                            //1泵站电动机启动线路断路
                                            //2泵站冗余电磁阀线路短路
                                            //3泵站冗余电磁阀线路断路
                                            //4泵站机械锁马达电磁阀线路短路
                                            //5泵站机械锁马达电磁阀线路断路
                                            //6控制器发电机启动线路短路
                                            //7控制器发电机启动线路断路

                                            //1
                                            //0控制器发电机停止线路短路
                                            //1控制器发电机停止线路断路
                                            //2控制器声光报警灯线路短路
                                            //3控制器声光报警灯线路断路
                                            //4泵站比例阀线路短路
                                            //5泵站比例阀线路断路

                                            switch (k)
                                            {
                                                case 0:
                                                    CanDatapool.in_Error_pump_motor_shortcircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);                       //泵站电动机启动线路短路  // 4
                                                    CanDatapool.in_Error_controller_dynamo_Stop_shortcircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);        //控制器发电机停止线路短路  // 4
                                                    break;
                                                case 1:
                                                    CanDatapool.in_Error_pump_motor_opencircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);                        //泵站电动机启动线路断路  // 4
                                                    CanDatapool.in_Error_controller_dynamo_Stop_opencircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);         //控制器发电机停止线路断路  // 4
                                                    break;
                                                case 2:
                                                    CanDatapool.in_Error_pump_electromagneticvalve_shortcircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);        //泵站冗余电磁阀线路短路  // 4
                                                    CanDatapool.in_Error_controller_warnlight_shortcircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);         //控制器声光报警灯线路短路  // 4
                                                    break;
                                                case 3:
                                                    CanDatapool.in_Error_pump_electromagneticvalve_opencircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);         //泵站冗余电磁阀线路断路  // 4
                                                    CanDatapool.in_Error_controller_warnlight_opencircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);          //控制器声光报警灯线路断路  // 4
                                                    break;
                                                case 4:
                                                    CanDatapool.in_Error_pump_MachLock_proportionalvalve_shortcircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0); //泵站机械锁马达电磁阀线路短路  // 4
                                                    CanDatapool.in_Error_pump_proportionalvalve_shortcircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);           //泵站比例阀线路短路      // 4   
                                                    break;
                                                case 5:
                                                    CanDatapool.in_Error_pump_MachLock_proportionalvalve_opencircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);  //泵站机械锁马达电磁阀线路断路  // 4
                                                    CanDatapool.in_Error_pump_proportionalvalve_opencircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);            //泵站比例阀线路断路      // 4
                                                    break;
                                                case 6:
                                                    CanDatapool.in_Error_controller_dynamo_Start_shortcircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);      //控制器发电机启动线路短路  // 4
                                                    break;
                                                case 7:
                                                    CanDatapool.in_Error_controller_dynamo_Start_opencircuit_3501_3504[idArray0] = ((msgRecieve[j].data[0] & (1 << k)) != 0);       //控制器发电机启动线路断路  // 4
                                                    break;
                                            }
                                            //CanDatapool.in_Error_Pump_3501_3504[idArray0 * 8 + k] = ((msgRecieve[j].data[0] & (1 << k)) != 0);
                                            //CanDatapool.in_Error_Pump_3501_3504[idArray0 * 8 + k] = ((msgRecieve[j].data[1] & (1 << k)) != 0);

                                            CanDatapool.in_Error_PressureSenser_3501_3504[idArray0 * 8 + k] = ((msgRecieve[j].data[2] & (1 << k)) != 0);
                                            CanDatapool.in_Error_PositionSenser_3501_3504[idArray0 * 8 + k] = ((msgRecieve[j].data[3] & (1 << k)) != 0);
                                        }

                                        break;
                                    #region 3511-3514
                                    //3511
                                    //1#泵站1#油缸伸出电磁阀线路短路        		0	0	
                                    //1#泵站1#油缸缩回电磁阀线路短路        			1	
                                    //1#泵站2#油缸伸出电磁阀线路短路        			2	
                                    //1#泵站2#油缸缩回电磁阀线路短路        			3	
                                    //1#泵站3#油缸伸出电磁阀线路短路        			4	
                                    //1#泵站3#油缸缩回电磁阀线路短路					5	
                                    //1#泵站4#油缸伸出电磁阀线路短路					6	
                                    //1#泵站4#油缸缩回电磁阀线路短路					7	
                                    //1#泵站5#油缸伸出电磁阀线路短路				1	0	
                                    //1#泵站5#油缸缩回电磁阀线路短路					1	
                                    //1#泵站6#油缸伸出电磁阀线路短路					2	
                                    //1#泵站6#油缸缩回电磁阀线路短路	        		3	
                                    //1#泵站7#油缸伸出电磁阀线路短路	        		4	
                                    //1#泵站7#油缸缩回电磁阀线路短路	        		5	
                                    //1#泵站8#油缸伸出电磁阀线路短路	        		6	
                                    //1#泵站8#油缸缩回电磁阀线路短路	        		7	
                                    //1#泵站1#油缸伸出电磁阀线路断路				2	0	
                                    //1#泵站1#油缸缩回电磁阀线路断路					1	
                                    //1#泵站2#油缸伸出电磁阀线路断路					2	
                                    //1#泵站2#油缸缩回电磁阀线路断路					3	
                                    //1#泵站3#油缸伸出电磁阀线路断路					4	
                                    //1#泵站3#油缸缩回电磁阀线路断路					5	
                                    //1#泵站4#油缸伸出电磁阀线路断路	        		6	
                                    //1#泵站4#油缸缩回电磁阀线路断路	        		7	
                                    //1#泵站5#油缸伸出电磁阀线路断路	        	3	0	
                                    //1#泵站5#油缸缩回电磁阀线路断路	        		1	
                                    //1#泵站6#油缸伸出电磁阀线路断路	        		2	
                                    //1#泵站6#油缸缩回电磁阀线路断路					3	
                                    //1#泵站7#油缸伸出电磁阀线路断路					4	
                                    //1#泵站7#油缸缩回电磁阀线路断路					5	
                                    //1#泵站8#油缸伸出电磁阀线路断路					6	
                                    //1#泵站8#油缸缩回电磁阀线路断路					7	
                                    //1#泵站1#油缸机械锁伸出电磁阀线路短路		4	0	
                                    //1#泵站1#油缸机械锁缩回电磁阀线路短路			1	
                                    //1#泵站2#油缸机械锁伸出电磁阀线路短路			2	
                                    //1#泵站2#油缸机械锁缩回电磁阀线路短路			3	
                                    //1#泵站3#油缸机械锁伸出电磁阀线路短路			4	
                                    //1#泵站3#油缸机械锁缩回电磁阀线路短路			5	
                                    //1#泵站4#油缸机械锁伸出电磁阀线路短路			6	
                                    //1#泵站4#油缸机械锁缩回电磁阀线路短路			7	
                                    //1#泵站5#油缸机械锁伸出电磁阀线路短路		5	0	
                                    //1#泵站5#油缸机械锁缩回电磁阀线路短路			1	
                                    //1#泵站6#油缸机械锁伸出电磁阀线路短路			2	
                                    //1#泵站6#油缸机械锁缩回电磁阀线路短路			3	
                                    //1#泵站7#油缸机械锁伸出电磁阀线路短路			4	
                                    //1#泵站7#油缸机械锁缩回电磁阀线路短路			5	
                                    //1#泵站8#油缸机械锁伸出电磁阀线路短路			6	
                                    //1#泵站8#油缸机械锁缩回电磁阀线路短路			7	
                                    //1#泵站1#油缸机械锁伸出电磁阀线路断路		6	0	
                                    //1#泵站1#油缸机械锁缩回电磁阀线路断路			1	
                                    //1#泵站2#油缸机械锁伸出电磁阀线路断路			2	
                                    //1#泵站2#油缸机械锁缩回电磁阀线路断路			3	
                                    //1#泵站3#油缸机械锁伸出电磁阀线路断路			4	
                                    //1#泵站3#油缸机械锁缩回电磁阀线路断路			5	
                                    //1#泵站4#油缸机械锁伸出电磁阀线路断路			6	
                                    //1#泵站4#油缸机械锁缩回电磁阀线路断路			7	
                                    //1#泵站5#油缸机械锁伸出电磁阀线路断路		7	0	
                                    //1#泵站5#油缸机械锁缩回电磁阀线路断路			1	
                                    //1#泵站6#油缸机械锁伸出电磁阀线路断路			2	
                                    //1#泵站6#油缸机械锁缩回电磁阀线路断路			3	
                                    //1#泵站7#油缸机械锁伸出电磁阀线路断路			4	
                                    //1#泵站7#油缸机械锁缩回电磁阀线路断路			5	
                                    //1#泵站8#油缸机械锁伸出电磁阀线路断路			6	
                                    //1#泵站8#油缸机械锁缩回电磁阀线路断路			7	
                                    //3512
                                    //2#泵站1#油缸伸出电磁阀线路短路		        0	0	
                                    //2#泵站1#油缸缩回电磁阀线路短路		        	1	
                                    //2#泵站2#油缸伸出电磁阀线路短路		        	2	
                                    //2#泵站2#油缸缩回电磁阀线路短路		        	3	
                                    //2#泵站3#油缸伸出电磁阀线路短路		        	4	
                                    //2#泵站3#油缸缩回电磁阀线路短路					5	
                                    //2#泵站4#油缸伸出电磁阀线路短路					6	
                                    //2#泵站4#油缸缩回电磁阀线路短路					7	
                                    //2#泵站5#油缸伸出电磁阀线路短路				1	0	
                                    //2#泵站5#油缸缩回电磁阀线路短路					1	
                                    //2#泵站6#油缸伸出电磁阀线路短路					2	
                                    //2#泵站6#油缸缩回电磁阀线路短路	        		3	
                                    //2#泵站7#油缸伸出电磁阀线路短路	        		4	
                                    //2#泵站7#油缸缩回电磁阀线路短路	        		5	
                                    //2#泵站8#油缸伸出电磁阀线路短路	        		6	
                                    //2#泵站8#油缸缩回电磁阀线路短路	        		7	
                                    //2#泵站1#油缸伸出电磁阀线路断路				2	0	
                                    //2#泵站1#油缸缩回电磁阀线路断路					1	
                                    //2#泵站2#油缸伸出电磁阀线路断路					2	
                                    //2#泵站2#油缸缩回电磁阀线路断路					3	
                                    //2#泵站3#油缸伸出电磁阀线路断路					4	
                                    //2#泵站3#油缸缩回电磁阀线路断路					5	
                                    //2#泵站4#油缸伸出电磁阀线路断路	        		6	
                                    //2#泵站4#油缸缩回电磁阀线路断路	        		7	
                                    //2#泵站5#油缸伸出电磁阀线路断路	        	3	0	
                                    //2#泵站5#油缸缩回电磁阀线路断路	        		1	
                                    //2#泵站6#油缸伸出电磁阀线路断路	        		2	
                                    //2#泵站6#油缸缩回电磁阀线路断路					3	
                                    //2#泵站7#油缸伸出电磁阀线路断路					4	
                                    //2#泵站7#油缸缩回电磁阀线路断路					5	
                                    //2#泵站8#油缸伸出电磁阀线路断路					6	
                                    //2#泵站8#油缸缩回电磁阀线路断路					7	
                                    //2#泵站1#油缸机械锁伸出电磁阀线路短路		4	0	
                                    //2#泵站1#油缸机械锁缩回电磁阀线路短路			1	
                                    //2#泵站2#油缸机械锁伸出电磁阀线路短路			2	
                                    //2#泵站2#油缸机械锁缩回电磁阀线路短路			3	
                                    //2#泵站3#油缸机械锁伸出电磁阀线路短路			4	
                                    //2#泵站3#油缸机械锁缩回电磁阀线路短路			5	
                                    //2#泵站4#油缸机械锁伸出电磁阀线路短路			6	
                                    //2#泵站4#油缸机械锁缩回电磁阀线路短路			7	
                                    //2#泵站5#油缸机械锁伸出电磁阀线路短路		5	0	
                                    //2#泵站5#油缸机械锁缩回电磁阀线路短路			1	
                                    //2#泵站6#油缸机械锁伸出电磁阀线路短路			2	
                                    //2#泵站6#油缸机械锁缩回电磁阀线路短路			3	
                                    //2#泵站7#油缸机械锁伸出电磁阀线路短路			4	
                                    //2#泵站7#油缸机械锁缩回电磁阀线路短路			5	
                                    //2#泵站8#油缸机械锁伸出电磁阀线路短路			6	
                                    //2#泵站8#油缸机械锁缩回电磁阀线路短路			7	
                                    //2#泵站1#油缸机械锁伸出电磁阀线路断路		6	0	
                                    //2#泵站1#油缸机械锁缩回电磁阀线路断路			1	
                                    //2#泵站2#油缸机械锁伸出电磁阀线路断路			2	
                                    //2#泵站2#油缸机械锁缩回电磁阀线路断路			3	
                                    //2#泵站3#油缸机械锁伸出电磁阀线路断路			4	
                                    //2#泵站3#油缸机械锁缩回电磁阀线路断路			5	
                                    //2#泵站4#油缸机械锁伸出电磁阀线路断路			6	
                                    //2#泵站4#油缸机械锁缩回电磁阀线路断路			7	
                                    //2#泵站5#油缸机械锁伸出电磁阀线路断路		7	0	
                                    //2#泵站5#油缸机械锁缩回电磁阀线路断路			1	
                                    //2#泵站6#油缸机械锁伸出电磁阀线路断路			2	
                                    //2#泵站6#油缸机械锁缩回电磁阀线路断路			3	
                                    //2#泵站7#油缸机械锁伸出电磁阀线路断路			4	
                                    //2#泵站7#油缸机械锁缩回电磁阀线路断路			5	
                                    //2#泵站8#油缸机械锁伸出电磁阀线路断路			6	
                                    //2#泵站8#油缸机械锁缩回电磁阀线路断路			7	
                                    //3513
                                    //3#泵站1#油缸伸出电磁阀线路短路	        	0	0	
                                    //3#泵站1#油缸缩回电磁阀线路短路	        		1	
                                    //3#泵站2#油缸伸出电磁阀线路短路	        		2	
                                    //3#泵站2#油缸缩回电磁阀线路短路	        		3	
                                    //3#泵站3#油缸伸出电磁阀线路短路	        		4	
                                    //3#泵站3#油缸缩回电磁阀线路短路					5	
                                    //3#泵站4#油缸伸出电磁阀线路短路					6	
                                    //3#泵站4#油缸缩回电磁阀线路短路					7	
                                    //3#泵站5#油缸伸出电磁阀线路短路				1	0	
                                    //3#泵站5#油缸缩回电磁阀线路短路					1	
                                    //3#泵站6#油缸伸出电磁阀线路短路					2	
                                    //3#泵站6#油缸缩回电磁阀线路短路		        	3	
                                    //3#泵站7#油缸伸出电磁阀线路短路		        	4	
                                    //3#泵站7#油缸缩回电磁阀线路短路		        	5	
                                    //3#泵站8#油缸伸出电磁阀线路短路		        	6	
                                    //3#泵站8#油缸缩回电磁阀线路短路		        	7	
                                    //3#泵站1#油缸伸出电磁阀线路断路				2	0	
                                    //3#泵站1#油缸缩回电磁阀线路断路					1	
                                    //3#泵站2#油缸伸出电磁阀线路断路					2	
                                    //3#泵站2#油缸缩回电磁阀线路断路					3	
                                    //3#泵站3#油缸伸出电磁阀线路断路					4	
                                    //3#泵站3#油缸缩回电磁阀线路断路					5	
                                    //3#泵站4#油缸伸出电磁阀线路断路	        		6	
                                    //3#泵站4#油缸缩回电磁阀线路断路	        		7	
                                    //3#泵站5#油缸伸出电磁阀线路断路	        	3	0	
                                    //3#泵站5#油缸缩回电磁阀线路断路	        		1	
                                    //3#泵站6#油缸伸出电磁阀线路断路	        		2	
                                    //3#泵站6#油缸缩回电磁阀线路断路					3	
                                    //3#泵站7#油缸伸出电磁阀线路断路					4	
                                    //3#泵站7#油缸缩回电磁阀线路断路					5	
                                    //3#泵站8#油缸伸出电磁阀线路断路					6	
                                    //3#泵站8#油缸缩回电磁阀线路断路					7	
                                    //3#泵站1#油缸机械锁伸出电磁阀线路短路		4	0	
                                    //3#泵站1#油缸机械锁缩回电磁阀线路短路			1	
                                    //3#泵站2#油缸机械锁伸出电磁阀线路短路			2	
                                    //3#泵站2#油缸机械锁缩回电磁阀线路短路			3	
                                    //3#泵站3#油缸机械锁伸出电磁阀线路短路			4	
                                    //3#泵站3#油缸机械锁缩回电磁阀线路短路			5	
                                    //3#泵站4#油缸机械锁伸出电磁阀线路短路			6	
                                    //3#泵站4#油缸机械锁缩回电磁阀线路短路			7	
                                    //3#泵站5#油缸机械锁伸出电磁阀线路短路		5	0	
                                    //3#泵站5#油缸机械锁缩回电磁阀线路短路			1	
                                    //3#泵站6#油缸机械锁伸出电磁阀线路短路			2	
                                    //3#泵站6#油缸机械锁缩回电磁阀线路短路			3	
                                    //3#泵站7#油缸机械锁伸出电磁阀线路短路			4	
                                    //3#泵站7#油缸机械锁缩回电磁阀线路短路			5	
                                    //3#泵站8#油缸机械锁伸出电磁阀线路短路			6	
                                    //3#泵站8#油缸机械锁缩回电磁阀线路短路			7	
                                    //3#泵站1#油缸机械锁伸出电磁阀线路断路		6	0	
                                    //3#泵站1#油缸机械锁缩回电磁阀线路断路			1	
                                    //3#泵站2#油缸机械锁伸出电磁阀线路断路			2	
                                    //3#泵站2#油缸机械锁缩回电磁阀线路断路			3	
                                    //3#泵站3#油缸机械锁伸出电磁阀线路断路			4	
                                    //3#泵站3#油缸机械锁缩回电磁阀线路断路			5	
                                    //3#泵站4#油缸机械锁伸出电磁阀线路断路			6	
                                    //3#泵站4#油缸机械锁缩回电磁阀线路断路			7	
                                    //3#泵站5#油缸机械锁伸出电磁阀线路断路		7	0	
                                    //3#泵站5#油缸机械锁缩回电磁阀线路断路			1	
                                    //3#泵站6#油缸机械锁伸出电磁阀线路断路			2	
                                    //3#泵站6#油缸机械锁缩回电磁阀线路断路			3	
                                    //3#泵站7#油缸机械锁伸出电磁阀线路断路			4	
                                    //3#泵站7#油缸机械锁缩回电磁阀线路断路			5	
                                    //3#泵站8#油缸机械锁伸出电磁阀线路断路			6	
                                    //3#泵站8#油缸机械锁缩回电磁阀线路断路			7
                                    //    3514
                                    //4#泵站1#油缸伸出电磁阀线路短路	        	0	0	
                                    //4#泵站1#油缸缩回电磁阀线路短路	        		1	
                                    //4#泵站2#油缸伸出电磁阀线路短路	        		2	
                                    //4#泵站2#油缸缩回电磁阀线路短路	        		3	
                                    //4#泵站3#油缸伸出电磁阀线路短路	        		4	
                                    //4#泵站3#油缸缩回电磁阀线路短路					5	
                                    //4#泵站4#油缸伸出电磁阀线路短路					6	
                                    //4#泵站4#油缸缩回电磁阀线路短路					7	
                                    //4#泵站5#油缸伸出电磁阀线路短路				1	0	
                                    //4#泵站5#油缸缩回电磁阀线路短路					1	
                                    //4#泵站6#油缸伸出电磁阀线路短路					2	
                                    //4#泵站6#油缸缩回电磁阀线路短路	        		3	
                                    //4#泵站7#油缸伸出电磁阀线路短路	        		4	
                                    //4#泵站7#油缸缩回电磁阀线路短路	        		5	
                                    //4#泵站8#油缸伸出电磁阀线路短路	        		6	
                                    //4#泵站8#油缸缩回电磁阀线路短路	        		7	
                                    //4#泵站1#油缸伸出电磁阀线路断路				2	0	
                                    //4#泵站1#油缸缩回电磁阀线路断路					1	
                                    //4#泵站2#油缸伸出电磁阀线路断路					2	
                                    //4#泵站2#油缸缩回电磁阀线路断路					3	
                                    //4#泵站3#油缸伸出电磁阀线路断路					4	
                                    //4#泵站3#油缸缩回电磁阀线路断路					5	
                                    //4#泵站4#油缸伸出电磁阀线路断路	        		6	
                                    //4#泵站4#油缸缩回电磁阀线路断路	        		7	
                                    //4#泵站5#油缸伸出电磁阀线路断路	        	3	0	
                                    //4#泵站5#油缸缩回电磁阀线路断路	        		1	
                                    //4#泵站6#油缸伸出电磁阀线路断路	        		2	
                                    //4#泵站6#油缸缩回电磁阀线路断路					3	
                                    //4#泵站7#油缸伸出电磁阀线路断路					4	
                                    //4#泵站7#油缸缩回电磁阀线路断路					5	
                                    //4#泵站8#油缸伸出电磁阀线路断路					6	
                                    //4#泵站8#油缸缩回电磁阀线路断路					7	
                                    //4#泵站1#油缸机械锁伸出电磁阀线路短路		4	0	
                                    //4#泵站1#油缸机械锁缩回电磁阀线路短路			1	
                                    //4#泵站2#油缸机械锁伸出电磁阀线路短路			2	
                                    //4#泵站2#油缸机械锁缩回电磁阀线路短路			3	
                                    //4#泵站3#油缸机械锁伸出电磁阀线路短路			4	
                                    //4#泵站3#油缸机械锁缩回电磁阀线路短路			5	
                                    //4#泵站4#油缸机械锁伸出电磁阀线路短路			6	
                                    //4#泵站4#油缸机械锁缩回电磁阀线路短路			7	
                                    //4#泵站5#油缸机械锁伸出电磁阀线路短路		5	0	
                                    //4#泵站5#油缸机械锁缩回电磁阀线路短路			1	
                                    //4#泵站6#油缸机械锁伸出电磁阀线路短路			2	
                                    //4#泵站6#油缸机械锁缩回电磁阀线路短路			3	
                                    //4#泵站7#油缸机械锁伸出电磁阀线路短路			4	
                                    //4#泵站7#油缸机械锁缩回电磁阀线路短路			5	
                                    //4#泵站8#油缸机械锁伸出电磁阀线路短路			6	
                                    //4#泵站8#油缸机械锁缩回电磁阀线路短路			7	
                                    //4#泵站1#油缸机械锁伸出电磁阀线路断路		6	0	
                                    //4#泵站1#油缸机械锁缩回电磁阀线路断路			1	
                                    //4#泵站2#油缸机械锁伸出电磁阀线路断路			2	
                                    //4#泵站2#油缸机械锁缩回电磁阀线路断路			3	
                                    //4#泵站3#油缸机械锁伸出电磁阀线路断路			4	
                                    //4#泵站3#油缸机械锁缩回电磁阀线路断路			5	
                                    //4#泵站4#油缸机械锁伸出电磁阀线路断路			6	
                                    //4#泵站4#油缸机械锁缩回电磁阀线路断路			7	
                                    //4#泵站5#油缸机械锁伸出电磁阀线路断路		7	0	
                                    //4#泵站5#油缸机械锁缩回电磁阀线路断路			1	
                                    //4#泵站6#油缸机械锁伸出电磁阀线路断路			2	
                                    //4#泵站6#油缸机械锁缩回电磁阀线路断路			3	
                                    //4#泵站7#油缸机械锁伸出电磁阀线路断路			4	
                                    //4#泵站7#油缸机械锁缩回电磁阀线路断路			5	
                                    //4#泵站8#油缸机械锁伸出电磁阀线路断路			6	
                                    //4#泵站8#油缸机械锁缩回电磁阀线路断路			7	

                                    #endregion
                                    case 0x3511:
                                    case 0x3512:
                                    case 0x3513:
                                    case 0x3514:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = Convert.ToByte(idArray[0] - 0x10 - 1);
                                        /*
                                        for (int k = 0; k < 8; k++)
                                        {
                                            if (k < 4)//油缸伸出/缩回电磁阀线路断路
                                            {
                                                for (int l = 0; l < 8; l++)
                                                {
                                                    if (l % 2 == 0)
                                                        CanDatapool.in_Error_cylinder_extend_shortcircuit_3511_3514[(8 * k + l) / 2] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
                                                    else
                                                        CanDatapool.in_Error_cylinder_retract_shortcircuit_3511_3514[(8 * k + l) / 2] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
                                                }
                                            }
                                            else //油缸机械锁伸出/缩回电磁阀线路断路
                                            {

                                                for (int l = 0; l < 8; l++)
                                                {
                                                    if (l % 2 == 0)
                                                        CanDatapool.in_Error_MachLock_extend_shortcircuit_3511_3514[(8 * (k - 4) + l) / 2] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
                                                    else
                                                        CanDatapool.in_Error_MachLock_retract_shortcircuit_3511_3514[(8 * (k - 4) + l) / 2] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
                                                }
                                            }
                                        }
                                        */
                                        for (int k = 0; k < 8; k++)//字节数 
                                        {
                                            for (int l = 0; l < 8; l++)//位数
                                            {
                                                switch (k)
                                                {
                                                    case 0:
                                                    case 1:
                                                        CanDatapool.in_Error_cylinder_extend_shortcircuit_3511_3514[(int)(idArray0 * 8 + ((l % 2 == 0) ? l / 2 : l / 2 + 4))] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
                                                        break;
                                                    case 2:
                                                    case 3:
                                                        CanDatapool.in_Error_cylinder_extend_opencircuit_3511_3514[(int)(idArray0 * 8 + ((l % 2 == 0) ? l / 2 : l / 2 + 4))] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
                                                        break;
                                                    case 4:
                                                    case 5:
                                                        CanDatapool.in_Error_cylinder_retract_shortcircuit_3511_3514[(int)(idArray0 * 8 + ((l % 2 == 0) ? l / 2 : l / 2 + 4))] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
                                                        break;
                                                    case 6:
                                                    case 7:
                                                        CanDatapool.in_Error_cylinder_retract_opencircuit_3511_3514[(int)(idArray0 * 8 + ((l % 2 == 0) ? l / 2 : l / 2 + 4))] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
                                                        break;
                                                }
                                            }
                                        }

                                        break;
                                    default:
                                        break;
                                }
                                //SendStatus += msgRecieve[j].data[i].ToString();
                                //SendStatus += " ";
                            }
                        }
                    }
                }
            }

        }


        // private CDataPool _candatapool = null;
        public CDataPool CanDatapool
        {
            //get { return _candatapool; }
            //set
            //{
            //    if (_candatapool != value)
            //        _candatapool = value;
            //}
            get { return CDataPool.GetDataPoolObject(); }
        }

    }
}
