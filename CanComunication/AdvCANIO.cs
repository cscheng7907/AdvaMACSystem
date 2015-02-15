// #############################################################################
// *****************************************************************************
//                  Copyright (c) 2009, Advantech Automation Corp.
//      THIS IS AN UNPUBLISHED WORK CONTAINING CONFIDENTIAL AND PROPRIETARY
//               INFORMATION WHICH IS THE PROPERTY OF ADVANTECH AUTOMATION CORP.
//
//    ANY DISCLOSURE, USE, OR REPRODUCTION, WITHOUT WRITTEN AUTHORIZATION FROM
//               ADVANTECH AUTOMATION CORP., IS STRICTLY PROHIBITED.
// *****************************************************************************

// #############################################################################
//
// File:    AdvCANIO.cs
// Created: 4/8/2009
// Version: 1.0
// Description: Implement IO function about how to access CAN WDM&CE driver
//
// -----------------------------------------------------------------------------
using System;
using System.Threading;
using System.Runtime.InteropServices;
/// <summary>
/// Summary description for Class1
/// </summary>
public class AdvCANIO
{
   private uint hDevice;
   private IntPtr orgWriteBuf = IntPtr.Zero;
   private IntPtr orgReadBuf = IntPtr.Zero;
   private IntPtr lpCommandBuffer = IntPtr.Zero;
   private IntPtr lpConfigBuffer = IntPtr.Zero;
   private IntPtr lpStatusBuffer = IntPtr.Zero;
   private AdvCan.Command_par_t Command = new AdvCan.Command_par_t();
   private AdvCan.Config_par_t Config = new AdvCan.Config_par_t();
   private int OutLen;
   private uint MaxReadMsgNumber;
   private uint MaxWriteMsgNumber;
   public const int SUCCESS = 0;                            //Status definition
   public const int OPERATION_ERROR = -1;                   //Status definition
   public const int TIME_OUT = -2;                          //Status definition         
   public const uint INVALID_HANDLE_VALUE = 0xffffffff;
   public AdvCANIO()
	{
		//
		// TODO: Add constructor logic here
		//
      hDevice = INVALID_HANDLE_VALUE;
	}
   /*****************************************************************************
   *
   *	acCanOpen
   *
   *	Purpose:
   *		open can port by name 
   *		
   *
   *	Arguments:
   *		PortName			         - port name
   *		synchronization	      - TRUE, synchronization ; FALSE, asynchronous
   *		MsgNumberOfReadBuffer	- message number of read intptr
   *		MsgNumberOfWriteBuffer	- message number of write intptr
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acCanOpen(string CanPortName, bool synchronization, uint MsgNumberOfReadBuffer, uint MsgNumberOfWriteBuffer)
   {
      CanPortName += ":";
      hDevice = AdvCan.CreateFile(CanPortName, AdvCan.GENERIC_READ + AdvCan.GENERIC_WRITE, 0, 0, AdvCan.OPEN_EXISTING, AdvCan.FILE_ATTRIBUTE_NORMAL, 0);

      if (hDevice != INVALID_HANDLE_VALUE)
      {
         MaxReadMsgNumber = MsgNumberOfReadBuffer;
         MaxWriteMsgNumber = MsgNumberOfWriteBuffer;
         orgReadBuf = Marshal.AllocHGlobal((int)(AdvCan.CAN_MSG_LENGTH * MsgNumberOfReadBuffer));
         orgWriteBuf = Marshal.AllocHGlobal((int)(AdvCan.CAN_MSG_LENGTH * MsgNumberOfWriteBuffer));
         lpCommandBuffer = Marshal.AllocHGlobal(AdvCan.CAN_COMMAND_LENGTH);
         lpConfigBuffer = Marshal.AllocHGlobal(AdvCan.CAN_CONFIG_LENGTH);
         lpStatusBuffer = Marshal.AllocHGlobal(AdvCan.CAN_CANSTATUS_LENGTH);
         return SUCCESS;
      }
      else
         return OPERATION_ERROR;
   }

   /*****************************************************************************
   *
   *	acCanClose
   *
   *	Purpose:
   *		close can port 
   *		
   *
   *	Arguments:
   *
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acCanClose()
   {
      if (hDevice != INVALID_HANDLE_VALUE)
      {
          AdvCan.CloseHandle(hDevice);
          Thread.Sleep(100);
          Marshal.FreeHGlobal(orgWriteBuf);
          Marshal.FreeHGlobal(orgReadBuf);
          Marshal.FreeHGlobal(lpCommandBuffer);
          Marshal.FreeHGlobal(lpConfigBuffer);
          Marshal.FreeHGlobal(lpStatusBuffer);
          hDevice = INVALID_HANDLE_VALUE;
     }
                            

      return SUCCESS;
   }

   /*****************************************************************************
   *
   *	acEnterResetMode
   *
   *	Purpose:
   *		Enter reset mode.
   *		
   *
   *	Arguments:
   *
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acEnterResetMode()            
   {
      bool flag;
      Command.cmd = AdvCan.CMD_STOP;
      Marshal.StructureToPtr(Command, lpCommandBuffer, false);
      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_COMMAND, lpCommandBuffer, AdvCan.CAN_COMMAND_LENGTH, IntPtr.Zero, 0, ref OutLen, 0);
      if (!flag)
      {
         return OPERATION_ERROR;
      }
      return SUCCESS;
   }

   /*****************************************************************************
   *
   *	acEnterWorkMode
   *
   *	Purpose:
   *		Enter work mode 
   *		
   *
   *	Arguments:
   *
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acEnterWorkMode()              
   {
      bool flag;
      Command.cmd = AdvCan.CMD_START;
      Marshal.StructureToPtr(Command, lpCommandBuffer, false);
      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_COMMAND, lpCommandBuffer, AdvCan.CAN_COMMAND_LENGTH, IntPtr.Zero, 0, ref OutLen, 0);
      if (!flag)
      {
         return OPERATION_ERROR;
      }
      return SUCCESS;
   }

   /*****************************************************************************
   *
   *	acSetBaud
   *
   *	Purpose:
   *		set baudrate of the CAN Controller.The two modes of configuring
   *     baud rate are custom mode and standard mode.
   *     -   Custom mode
   *         If Baud Rate value is user defined, driver will write the first 8
   *         bit of low 16 bit in BTR0 of SJA1000.
   *         The lower order 8 bit of low 16 bit will be written in BTR1 of SJA1000.
   *     -   Standard mode
   *         Target value     BTR0      BTR1      Setting value 
   *           10K            0x31      0x1c      10 
   *           20K            0x18      0x1c      20 
   *           50K            0x09      0x1c      50 
   *          100K            0x04      0x1c      100 
   *          125K            0x03      0x1c      125 
   *          250K            0x01      0x1c      250 
   *          500K            0x00      0x1c      500 
   *          800K            0x00      0x16      800 
   *         1000K            0x00      0x14      1000 
   *		
   *
   *	Arguments:
   *		BaudRateValue             - baudrate will be set
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acSetBaud(uint BaudRateValue)
   {
      bool flag;
      Config.target = AdvCan.CONF_TIMING;
      Config.val1 = BaudRateValue;
      Marshal.StructureToPtr(Config, lpConfigBuffer, false);
      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_CONFIG, lpConfigBuffer, AdvCan.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, 0);
      if (!flag)
      {
         return OPERATION_ERROR;
      }
      return SUCCESS;
   }

   /*****************************************************************************
   *
   *	acSetBaudRegister
   *
   *	Purpose:
   *		Configures baud rate by custom mode.
   *		
   *
   *	Arguments:
   *		Btr0           - BTR0 register value.
   *		Btr1           - BTR1 register value.
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acSetBaudRegister(Byte Btr0, Byte Btr1)
   {
      uint BaudRateValue = (uint)(Btr0 * 256 + Btr1);
      return acSetBaud(BaudRateValue);
   }

   /*****************************************************************************
   *
   *	acSetTimeOut
   *
   *	Purpose:
   *		set timeout for read and write  
   *		
   *
   *	Arguments:
   *		ulReadTimeOut                   - ms
   *     ulWriteTimeOut                  - ms
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acSetTimeOut(uint WriteTimeOutValue, uint ReadTimeOutValue)
   {
      bool flag;
      Config.target = AdvCan.CONF_TIMEOUT;
      Config.val1 = WriteTimeOutValue;
      Config.val2 = ReadTimeOutValue;
      Marshal.StructureToPtr(Config, lpConfigBuffer, false);
      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_CONFIG, lpConfigBuffer, AdvCan.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, 0);
      if (!flag)
      {
         return OPERATION_ERROR;
      }
      return SUCCESS;
   }

   /*****************************************************************************
   *
   *       acSetListenOnlyMode
   *
   *       Purpose:
   *               Set listen only mode of the CAN Controller
   *		
   *
   *       Arguments:
   *               bFlag           - TRUE, open only listen mode; FALSE, close only listen mode
   *       Returns:
   *       =0      succeeded; or <0 Failed 
   *
   *****************************************************************************/
   public int acSetListenOnlyMode(bool ListenOnly)
   {
      bool flag;
      Config.target = AdvCan.CONF_LISTEN_ONLY_MODE;
      if (ListenOnly)
         Config.val1 = 1;
      else
         Config.val1 = 0;
      Marshal.StructureToPtr(Config, lpConfigBuffer, false);
      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_CONFIG, lpConfigBuffer, AdvCan.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, 0);
      if (!flag)
      {
         return OPERATION_ERROR;
      }
      return SUCCESS;
   }

   /*****************************************************************************
   *
   *       acSetAcceptanceFilterMode
   *
   *       Purpose:
   *               Set acceptance filter mode of the CAN Controller
   *		
   *
   *       Arguments:
   *               nFilterMode     - 1, single filter mode; 0, dule filter mode
   *       Returns:
   *       =0      succeeded; or <0 Failed 
   *
   *****************************************************************************/
   public int acSetAcceptanceFilterMode(uint FilterMode)
   {
      bool flag = false;
      Config.target = AdvCan.CONF_ACC_FILTER;
      Config.val1 = FilterMode;
      Marshal.StructureToPtr(Config, lpConfigBuffer, false);
      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_CONFIG, lpConfigBuffer, AdvCan.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, 0);
      if (!flag)
      {
         return OPERATION_ERROR;
      }
      return SUCCESS;
   }

   /*****************************************************************************
   *
   *	acSetAcceptanceFilterMask
   *
   *	Purpose:
   *		set acceptance filter mask of the CAN Controller
   *		
   *
   *	Arguments:
   *		Mask              - acceptance filter mask
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acSetAcceptanceFilterMask(uint Mask)
   {
      bool flag = false;
      Config.target = AdvCan.CONF_ACCM;
      Config.val1 = Mask;
      Marshal.StructureToPtr(Config, lpConfigBuffer, false);
      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_CONFIG, lpConfigBuffer, AdvCan.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, 0);
      if (!flag)
      {
         return OPERATION_ERROR;
      }
      return SUCCESS;
   }

   /*****************************************************************************
   *
   *	acSetAcceptanceFilterCode
   *
   *	Purpose:
   *		set acceptance filter code of the CAN Controller
   *		
   *
   *	Arguments:
   *		Code              - acceptance filter code
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acSetAcceptanceFilterCode(uint Code)
   {
      bool flag = false;
      Config.target = AdvCan.CONF_ACCC;
      Config.val1 = Code;
      Marshal.StructureToPtr(Config, lpConfigBuffer, false);
      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_CONFIG, lpConfigBuffer, AdvCan.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, 0);
      if (!flag)
      {
         return OPERATION_ERROR;
      }
      return SUCCESS;
   }

   /*****************************************************************************
   *
   *	acSetAcceptanceFilter
   *
   *	Purpose:
   *		set acceptance filter code and mask of the CAN Controller 
   *		
   *
   *	Arguments:
   *		Mask              - acceptance filter mask
   *		Code              - acceptance filter code
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acSetAcceptanceFilter(uint Mask, uint Code)
   {
      bool flag = false;
      Config.target = AdvCan.CONF_ACC;
      Config.val1 = Mask;
      Config.val2 = Code;
      Marshal.StructureToPtr(Config, lpConfigBuffer, false);
      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_CONFIG, lpConfigBuffer, AdvCan.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, 0);
      if (!flag)
      {
         return OPERATION_ERROR;
      }
      return SUCCESS;
   }

   /*****************************************************************************
   *
   *	acGetStatus
   *
   *	Purpose:
   *		Get the current status of the driver and the CAN Controller
   *		
   *
   *	Arguments:
   *		Status   			- status buffer
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acGetStatus(ref AdvCan.CanStatusPar_t Status)
   {
      bool flag = false;
//      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_STATUS, lpStatusBuffer, AdvCan.CAN_CANSTATUS_LENGTH, lpStatusBuffer, AdvCan.CAN_CANSTATUS_LENGTH, ref OutLen, 0);
      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_STATUS, lpStatusBuffer, 0, lpStatusBuffer, AdvCan.CAN_CANSTATUS_LENGTH, ref OutLen, 0);
      if (!flag)
      {
         return OPERATION_ERROR;
      }
      Status = (AdvCan.CanStatusPar_t)(Marshal.PtrToStructure(lpStatusBuffer, typeof(AdvCan.CanStatusPar_t)));
      return SUCCESS;
   }

   /*****************************************************************************
   *
   *	acClearRxFifo
   *
   *	Purpose:
   *		clear can port rx buffer by handle 
   *		
   *
   *	Arguments:
   *
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acClearRxFifo()
   {
      bool flag = false;
      Command.cmd = AdvCan.CMD_CLEARBUFFERS;
      Marshal.StructureToPtr(Command, lpCommandBuffer, false);
      flag = AdvCan.DeviceIoControl(hDevice, AdvCan.CAN_IOCTL_COMMAND, lpCommandBuffer, AdvCan.CAN_COMMAND_LENGTH, IntPtr.Zero, 0, ref OutLen, 0);
      if (!flag)
      {
         return OPERATION_ERROR;
      }
      return SUCCESS;
   }

   /*****************************************************************************
   *
   *	acCanWrite
   *
   *	Purpose:
   *		write can msg
   *		
   *
   *	Arguments:
   *		msgWrite 				  - managed buffer for write
   *		nWriteCount			     - msg number for write
   *     pulNumberofWritten     - real msgs have written
   *		ov				           - synchronization event
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acCanWrite(AdvCan.canmsg_t[] msgWrite, uint nWriteCount, ref uint pulNumberofWritten)
   {
      bool flag;
      int nRet;

      if (nWriteCount > MaxWriteMsgNumber)
         return OPERATION_ERROR;

      pulNumberofWritten = 0;
      //Copy data from managed structure to unmanaged buffer
      for (int i = 0; i < nWriteCount; i++)
      {
         Marshal.StructureToPtr(msgWrite[i], new IntPtr(orgWriteBuf.ToInt32() + (AdvCan.CAN_MSG_LENGTH * i)), false); 
      }
      flag = AdvCan.WriteFile(hDevice, orgWriteBuf, nWriteCount, ref pulNumberofWritten, 0); //Send frame
      if (flag)
      {
         if (nWriteCount > pulNumberofWritten)
            nRet = TIME_OUT;                          //Sending data timeout
         else
            nRet = SUCCESS;                               //Sending data ok
      }
      else
      {
         nRet = OPERATION_ERROR;                            //Sending data error
      }
      return nRet;
   }

   /*****************************************************************************
   *
   *	acCanRead
   *
   *	Purpose:
   *		Read can message.
   *		
   *
   *	Arguments:
   *		msgRead           - managed buffer for read
   *		nReadCount			- msg number that unmanaged buffer can preserve
   *     pulNumberofRead   - real msgs have read
   *		ov				      - synchronization event
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acCanRead(AdvCan.canmsg_t[] msgRead, uint nReadCount, ref uint pulNumberofRead)
   {
      bool flag;
      int nRet;
      uint i;

      if (nReadCount > MaxReadMsgNumber)
         return OPERATION_ERROR;

      pulNumberofRead = 0;
      flag = AdvCan.ReadFile(hDevice, orgReadBuf, nReadCount, ref pulNumberofRead, 0); //Read frame
      if (flag)
      {
         if (pulNumberofRead == 0)
         {
            nRet = TIME_OUT;
         }
         else
         {
            for (i = 0; i < pulNumberofRead; i++)
               msgRead[i] = (AdvCan.canmsg_t)(Marshal.PtrToStructure(new IntPtr(orgReadBuf.ToInt32() + AdvCan.CAN_MSG_LENGTH * i), typeof(AdvCan.canmsg_t)));
            nRet = SUCCESS;
         }
      }
      else
      {
         nRet = OPERATION_ERROR;                                    //Package receiving error
      }
      return nRet;
   }

   /*****************************************************************************
   *
   *	acClearCommError
   *
   *	Purpose:
   *		execute ClearCommError of AdvCan.
   *		
   *
   *	Arguments:
   *     ErrorCode         - error code if the CAN Controller occur error
   * 
   * 
   *	Returns:
   *	TRUE	SUCCESS; or FALSE failure 
   *
   *****************************************************************************/
   public bool acClearCommError(ref uint ErrorCode)
   {
      AdvCan.COMSTAT lpState = new AdvCan.COMSTAT();
      return AdvCan.ClearCommError(hDevice, ref ErrorCode, ref lpState);
   }

   /*****************************************************************************
   *
   *	acSetCommMask
   *
   *	Purpose:
   *		execute SetCommMask of AdvCan.
   *		
   *
   *	Arguments:
   *     EvtMask         - event type
   * 
   * 
   *	Returns:
   *	TRUE	SUCCESS; or FALSE failure
   *
   *****************************************************************************/
   public bool acSetCommMask(uint EvtMask)
   {
      return AdvCan.SetCommMask(hDevice, EvtMask);
   }

   /*****************************************************************************
   *
   *	acGetCommMask
   *
   *	Purpose:
   *		execute GetCommMask of AdvCan.
   *		
   *
   *	Arguments:
   *     EvtMask         - event type
   * 
   * 
   *	Returns:
   *	TRUE	SUCCESS; or FALSE failure 
   *
   *****************************************************************************/
   public bool acGetCommMask(ref uint EvtMask)
   {
      return AdvCan.GetCommMask(hDevice, ref EvtMask);
   }

   /*****************************************************************************
   *
   *	acWaitEvent
   *
   *	Purpose:
   *		Wait can message or error of the CAN Controller.
   *		
   *
   *	Arguments:
   *		orgBuf            - unmanaged buffer for read
   *		nReadCount			- msg number that unmanaged buffer can preserve
   *     pulNumberofRead   - real msgs have read
   *		ov				      - synchronization event
   *     ErrorCode         - return error code of the CAN Controller when the function return OPERATION_ERROR
   *	Returns:
   *	=0	SUCCESS; or <0 failure 
   *
   *****************************************************************************/
   public int acWaitEvent(AdvCan.canmsg_t[] msgRead, uint nReadCount, ref uint pulNumberofRead, ref uint ErrorCode)
   {
      int Code = 0;
      int nRet = OPERATION_ERROR;

      if (AdvCan.WaitCommEvent(hDevice, ref Code, 0) == true)
      {
         if ((Code & AdvCan.EV_RXCHAR) != 0)
         {
            nRet = acCanRead(msgRead, nReadCount, ref pulNumberofRead);
         }
         if ((Code & AdvCan.EV_ERR) != 0)
         {
            nRet = OPERATION_ERROR;
            acClearCommError(ref ErrorCode);
         }
      }

      return nRet;
   }
}
