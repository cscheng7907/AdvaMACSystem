using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DataPool;

namespace AdvaMACSystem
{
    public class AdvaCanBus
    {
        private AdvCANIO Device = new AdvCANIO();
        private bool m_bRun1 = false;

        private Thread Thread1;
        private int ThreadInterval = 500;

        private const uint M_SENDCOUNT = 1;
        private AdvCan.canmsg_t[] msgSend = new AdvCan.canmsg_t[M_SENDCOUNT];                 //Package for write 

        private const uint M_RECIEVECOUNT = 1;
        private AdvCan.canmsg_t[] msgRecieve = new AdvCan.canmsg_t[M_RECIEVECOUNT];                 //Package for write 

        public AdvaCanBus()
        {

        }

        ~AdvaCanBus()
        {

        }

        //通讯配置参数 XML序列化 导入
        private string CanPortName = "can1";
        private uint BaudRateValue = AdvCan.CAN_TIMING_125K;
        uint nWriteCount = M_SENDCOUNT;
        uint nReadCount = M_RECIEVECOUNT;

        private UInt32 ReadTimeOutValue = 3000;
        private UInt32 WriteTimeOutValue = 3000;
        private uint dwMaskCode = 0;
        private uint dwAccCode = 0;
        private bool AcceptanceFilterMode = false;//false single true dual
        private uint EventMask = 0;
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

        private bool Open()
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

            if (AcceptanceFilterMode) // "Single"                     //Set acceptance filter mode
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

            nRet = Device.acSetTimeOut(WriteTimeOutValue, ReadTimeOutValue);      //Set timeout
            if (nRet < 0)
            {
                canErrcode = 7;//"Failed to set Timeout!"
                Device.acCanClose();
                return false;
            }

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
                {
                    msgSend[j].flags += AdvCan.MSG_RTR;
                    msgSend[j].length = 0;
                }

                for (int i = 0; i < msgSend[j].length; i++)
                {
                    msgSend[j].data[i] = (byte)i;
                }
            }

            //Initialize msg
            for (int j = 0; j < M_SENDCOUNT; j++)
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

            nRet = Device.acCanWrite(msgSend, nWriteCount, ref pulNumberofWritten);
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
                                    case 0x00300001:
                                    case 0x00300002:
                                    case 0x00300003:
                                    case 0x00300004:
                                    case 0x00300005:
                                    case 0x00300006:
                                    case 0x00300007:
                                    case 0x00300008:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = idArray[0];

                                        Idx_Pump = (idArray0 - 1) / 2 + 1;

                                        Idx_Cylinder = 1 + (((idArray0 % 2) == 0) ? 4 : 0);
                                        CanDatapool.Pressure_Real[(int)((Idx_Pump - 1) * 8 + Idx_Cylinder - 1)] =
                                            msgRecieve[j].data[0] + (msgRecieve[j].data[1] << 8);

                                        Idx_Cylinder = 2 + (((idArray0 % 2) == 0) ? 4 : 0);
                                        CanDatapool.Pressure_Real[(int)((Idx_Pump - 1) * 8 + Idx_Cylinder - 1)] =
                                            msgRecieve[j].data[2] + (msgRecieve[j].data[3] << 8);

                                        Idx_Cylinder = 3 + (((idArray0 % 2) == 0) ? 4 : 0);
                                        CanDatapool.Pressure_Real[(int)((Idx_Pump - 1) * 8 + Idx_Cylinder - 1)] =
                                            msgRecieve[j].data[4] + (msgRecieve[j].data[5] << 8);

                                        Idx_Cylinder = 4 + (((idArray0 % 2) == 0) ? 4 : 0);
                                        CanDatapool.Pressure_Real[(int)((Idx_Pump - 1) * 8 + Idx_Cylinder - 1)] =
                                            msgRecieve[j].data[6] + (msgRecieve[j].data[7] << 8);

                                        break;

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
                                    case 0x00310001:
                                    case 0x00310002:
                                    case 0x00310003:
                                    case 0x00310004:
                                    case 0x00310005:
                                    case 0x00310006:
                                    case 0x00310007:
                                    case 0x00310008:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = idArray[0];

                                        Idx_Pump = (idArray0 - 1) / 2 + 1;

                                        Idx_Cylinder = 1 + (((idArray0 % 2) == 0) ? 4 : 0);
                                        CanDatapool.Position_Real[(int)((Idx_Pump - 1) * 8 + Idx_Cylinder - 1)] =
                                            msgRecieve[j].data[0] + (msgRecieve[j].data[1] << 8);

                                        Idx_Cylinder = 2 + (((idArray0 % 2) == 0) ? 4 : 0);
                                        CanDatapool.Position_Real[(int)((Idx_Pump - 1) * 8 + Idx_Cylinder - 1)] =
                                            msgRecieve[j].data[2] + (msgRecieve[j].data[3] << 8);

                                        Idx_Cylinder = 3 + (((idArray0 % 2) == 0) ? 4 : 0);
                                        CanDatapool.Position_Real[(int)((Idx_Pump - 1) * 8 + Idx_Cylinder - 1)] =
                                            msgRecieve[j].data[4] + (msgRecieve[j].data[5] << 8);

                                        Idx_Cylinder = 4 + (((idArray0 % 2) == 0) ? 4 : 0);
                                        CanDatapool.Position_Real[(int)((Idx_Pump - 1) * 8 + Idx_Cylinder - 1)] =
                                            msgRecieve[j].data[6] + (msgRecieve[j].data[7] << 8);

                                        break;
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
                                    case 0x00320001:
                                    case 0x00320002:
                                    case 0x00320003:
                                    case 0x00320004:
                                    case 0x00320005:
                                    case 0x00320006:
                                    case 0x00320007:
                                    case 0x00320008:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = idArray[0];

                                        if ((idArray0 % 2) == 0)//偶数 机械锁运行状态
                                        {
                                            for (int k = 0; k < msgRecieve[j].data.Length; k++)
                                            {
                                                CanDatapool.MachLockState_Real[(idArray0 - 1) * 4 + k] = (StateType)msgRecieve[j].data[k];
                                            }
                                        }
                                        else //奇数 油缸运行状态
                                        {
                                            for (int k = 0; k < msgRecieve[j].data.Length; k++)
                                            {
                                                CanDatapool.cylinderState_Real[(idArray0 - 1) * 4 + k] = (StateType)msgRecieve[j].data[k];
                                            }
                                        }

                                        break;

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
                                    case 0x00330001:
                                    case 0x00330002:
                                    case 0x00330003:
                                    case 0x00330004:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = idArray[0];

                                        CanDatapool.Pressure_Pump_Real[idArray0] =
                                             msgRecieve[j].data[0] + (msgRecieve[j].data[1] << 8);
                                        CanDatapool.Voltage_Real[idArray0] =
                                             msgRecieve[j].data[2] + (msgRecieve[j].data[3] << 8);

                                        CanDatapool.PowerSupply[idArray0] = (msgRecieve[j].data[4] != 0);

                                        for (int k = 0; k < 8; k++)
                                        {
                                            CanDatapool.Limit_5[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[5] & (1 << k)) != 0);

                                            CanDatapool.Limit_10[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[6] & (1 << k)) != 0);
                                        }

                                        break;

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

                                    case 0x00340001:
                                    case 0x00340002:
                                    case 0x00340003:
                                    case 0x00340004:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = idArray[0];

                                        for (int k = 0; k < 8; k++)
                                        {
                                            CanDatapool.Warn_LowPressure[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[0] & (1 << k)) != 0);
                                            CanDatapool.Warn_HighPressure[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[1] & (1 << k)) != 0);
                                            CanDatapool.Warn_LowPosition[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[2] & (1 << k)) != 0);
                                            CanDatapool.Warn_HighPosition[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[3] & (1 << k)) != 0);

                                        }

                                        break;




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

                                    case 0x00350001:
                                    case 0x00350002:
                                    case 0x00350003:
                                    case 0x00350004:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = idArray[0];

                                        for (int k = 0; k < 8; k++)
                                        {
                                            CanDatapool.Error_Pump[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[0] & (1 << k)) != 0);
                                            CanDatapool.Error_Pump[(idArray0) * 8 + k] = ((msgRecieve[j].data[1] & (1 << k)) != 0);

                                            CanDatapool.Error_PressureSenser[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[2] & (1 << k)) != 0);
                                            CanDatapool.Error_PositionSenser[(idArray0 - 1) * 8 + k] = ((msgRecieve[j].data[3] & (1 << k)) != 0);

                                        }

                                        break;






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

                                    case 0x00350011:
                                    case 0x00350012:
                                    case 0x00350013:
                                    case 0x00350014:
                                        idArray = BitConverter.GetBytes(msgRecieve[j].id);
                                        idArray0 = Convert.ToByte(idArray[0] - 10);

                                        for (int k = 0; k < 8; k++)
                                        {
                                            if (k < 4)//油缸伸出/缩回电磁阀线路断路
                                            {
                                                for (int l = 0; l < 8; l++)
                                                {
                                                    if (l % 2 == 0)
                                                        CanDatapool.Error_cylinder_extend[(8 * k + l) / 2] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
                                                    else
                                                        CanDatapool.Error_cylinder_retract[(8 * k + l) / 2] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
                                                }
                                            }
                                            else //油缸机械锁伸出/缩回电磁阀线路断路
                                            {

                                                for (int l = 0; l < 8; l++)
                                                {
                                                    if (l % 2 == 0)
                                                        CanDatapool.Error_MachLock_extend[(8 * (k - 4) + l) / 2] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
                                                    else
                                                        CanDatapool.Error_MachLock_retract[(8 * (k - 4) + l) / 2] = ((msgRecieve[j].data[k] & (1 << l)) != 0);
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


        private CDataPool _candatapool = null;
        public CDataPool CanDatapool
        {
            get { return _candatapool; }
            set
            {
                if (_candatapool != value)
                    _candatapool = value;
            }
        }

    }
}
