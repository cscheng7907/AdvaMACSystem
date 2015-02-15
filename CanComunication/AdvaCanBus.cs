using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CanComunication
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

            //nRet = Device.acWaitEvent(msgRecieve, nReadCount, ref pulNumberofRead, ref ErrorCode);
            nRet = Device.acCanRead(msgRead, nReadCount, ref pulNumberofRead);
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
                            for (int i = 0; i < msgRecieve[j].length; i++)                           //Package receiving ok
                            {
                                //todo 正确包的解析

                                //SendStatus += msgRecieve[j].data[i].ToString();
                                //SendStatus += " ";
                            }
                        }
                    }
                }
            }

        }


        private DataPool _candatapool = null;
        public DataPool CanDatapool
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
