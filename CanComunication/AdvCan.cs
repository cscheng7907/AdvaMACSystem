// #############################################################################
// *****************************************************************************
//                  Copyright (c) 2008, Advantech Automation Corp.
//      THIS IS AN UNPUBLISHED WORK CONTAINING CONFIDENTIAL AND PROPRIETARY
//               INFORMATION WHICH IS THE PROPERTY OF ADVANTECH AUTOMATION CORP.
//
//    ANY DISCLOSURE, USE, OR REPRODUCTION, WITHOUT WRITTEN AUTHORIZATION FROM
//               ADVANTECH AUTOMATION CORP., IS STRICTLY PROHIBITED.
// *****************************************************************************

// #############################################################################
//
// File:    AdvCan.cs
// Created: 9/22/2008
// Version: 1.0
// Description: Defines data structures and function declarations
//
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

public class AdvCan
   {
      public const int CAN_MSG_LENGTH = 22;                        //Length of canmsg_t in bytes
      public const int CAN_COMMAND_LENGTH = 24;                    //Length of Command_par_t in bytes
      public const int CAN_CONFIG_LENGTH = 24;                     //Length of Config_par_t in bytes
      public const int CAN_CANSTATUS_LENGTH = 72;                  //Length of CanStatusPar_t in bytes
      // -----------------------------------------------------------------------------
      // DESCRIPTION: Standard baud  
      // -----------------------------------------------------------------------------
      public const uint CAN_TIMING_10K = 10;
      public const uint CAN_TIMING_20K = 20;
      public const uint CAN_TIMING_50K = 50;
      public const uint CAN_TIMING_100K = 100;
      public const uint CAN_TIMING_125K = 125;
      public const uint CAN_TIMING_250K = 250;
      public const uint CAN_TIMING_500K = 500;
      public const uint CAN_TIMING_800K = 800;
      public const uint CAN_TIMING_1000K = 1000;

      // -----------------------------------------------------------------------------
      // DESCRIPTION: Acceptance filter mode  
      // -----------------------------------------------------------------------------
      public const ushort PELICAN_SINGLE_FILTER = 1;
      public const ushort PELICAN_DUAL_FILTER = 0;

      // -----------------------------------------------------------------------------
      // DESCRIPTION: Standard baud  
      // -----------------------------------------------------------------------------
      public const ushort DATALENGTH = 8;                           //CAN data length 

      // -----------------------------------------------------------------------------
      // DESCRIPTION: For CAN frame id. if flags of frame point out 
      // some errors(MSG_OVR, MSG_PASSIVE, MSG_BUSOFF, MSG_BOUR), 
      // then id of frame is equal to ERRORID 
      // -----------------------------------------------------------------------------
      public const uint ERRORID = 0xffffffff;

      // -----------------------------------------------------------------------------
      // DESCRIPTION: CAN frame flag  
      // -----------------------------------------------------------------------------
      public const ushort MSG_RTR = ( 1 << 0 );                    //RTR Message 
      public const ushort MSG_OVR = ( 1 << 1 );                    //CAN controller Msg overflow error
      public const ushort MSG_EXT = ( 1 << 2 );                    //Extended message format  
      public const ushort MSG_SELF = ( 1 << 3 );                   //Message received from own tx 
      public const ushort MSG_PASSIVE = ( 1 << 4 );                //CAN Controller in error passive
      public const ushort MSG_BUSOFF = ( 1 << 5 );                 //CAN Controller Bus Off    
      public const ushort MSG_BOVR = ( 1 << 7 );                   //Receive buffer overflow     

      // -----------------------------------------------------------------------------
      // DESCRIPTION: CAN frame use by driver 
      // -----------------------------------------------------------------------------
      [StructLayout(LayoutKind.Sequential)]
       public struct canmsg_t
       {
           public int flags;                                       //Flags, indicating or controlling special message properties 
           public int cob;                                         //CAN object number, used in Full CAN
           public uint id;                                         //CAN message ID, 4 bytes  
           public short length;                                      //Number of bytes in the CAN message 
           [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
           public byte[] data;
      }
      // -----------------------------------------------------------------------------
      // DESCRIPTION: IOCTL code 
      // -----------------------------------------------------------------------------
      public const uint ADS_TYPE = 40000;
      public const uint ADS_IOCTL_INDEX = 0x900;

      // -----------------------------------------------------------------------------
      // DESCRIPTION:IOCTL Command cmd targets
      // -----------------------------------------------------------------------------
      public const ushort CMD_START = 1;                           //Start chip 
      public const ushort CMD_STOP = 2;                            //Stop chip
      public const ushort CMD_RESET = 3;                           //Reset chip 
      public const ushort CMD_CLEARBUFFERS = 4;                    //Clear the receive buffer 

      // -----------------------------------------------------------------------------
      // DESCRIPTION: IOCTL Configure cmd targets
      // -----------------------------------------------------------------------------
      public const ushort CONF_ACC = 0;                            //Accept code and mask code
      public const ushort CONF_ACCM = 1;                           //Mask code 
      public const ushort CONF_ACCC = 2;                           //Accept code 
      public const ushort CONF_TIMING = 3;                         //Bit timing 
      public const ushort CONF_LISTEN_ONLY_MODE = 8;               //For SJA1000 PeliCAN 
      public const ushort CONF_SELF_RECEPTION = 9;                 //Self reception 
      public const ushort CONF_TIMEOUT = 13;                       //Configure read and write timeout one time 
      public const ushort CONF_ACC_FILTER  = 20;                    //Acceptance filter mode: 1-Single, 0-Dual 

      // -----------------------------------------------------------------------------
      // DESCRIPTION:For ulStatus of CanStatusPar_t
      // -----------------------------------------------------------------------------
      public const ushort STATUS_OK = 0;
      public const ushort STATUS_BUS_ERROR = 1;
      public const ushort STATUS_BUS_OFF = 2;

      //------------------------------------------------------------------------------
      // DESCRIPTION: For EventMask of CanStatusPar_t
      //------------------------------------------------------------------------------
      public const uint EV_ERR = 0x0080;             // Line status error occurred
      public const uint EV_RXCHAR = 0x0001;          // Any Character received

      //------------------------------------------------------------------------------
      // DESCRIPTION: For windows error code
      //------------------------------------------------------------------------------
      public const uint ERROR_SEM_TIMEOUT = 121;
      public const uint ERROR_IO_PENDING = 997;
      //------------------------------------------------------------------------------
      // DESCRIPTION: Define windows  macro used in widows API
      //------------------------------------------------------------------------------
      public const uint GENERIC_READ = 0x80000000;
      public const uint GENERIC_WRITE = 0x40000000;
      public const uint GENERIC_EXECUTE = 0x20000000;
      public const uint GENERIC_ALL = 0x10000000;

      public const uint FILE_SHARE_READ = 0x1;
      public const uint FILE_SHARE_WRITE = 0x2;
      public const uint FILE_SHARE_DELETE = 0x4;

      public const uint OPEN_EXISTING = 3;
      public const uint FILE_ATTRIBUTE_NORMAL = 0x80;

      public const uint CE_RXOVER = 0x0001;      //Receive Queue overflow
      public const uint CE_OVERRUN = 0x0002;     //Receive Overrun Error
      public const uint CE_FRAME = 0x0008;       //Receive Framing error
      public const uint CE_BREAK = 0x0010;       //Break Detected

      //------------------------------------------------------------------------------
      // DESCRIPTION: IOCTL code 
        //------------------------------------------------------------------------------
      public const uint CAN_IOCTL_COMMAND = 0x222540;
      public const uint CAN_IOCTL_CONFIG = 0x222544;
      public const uint CAN_IOCTL_STATUS = 0x222554;

      //----------------------------------------------------------------------------
      //DESCRIPTION: IOCTL Command request parameter structure 
      //----------------------------------------------------------------------------
      public struct Command_par_t
      {
         public int cmd;                          //special driver command
         public int target;                       //special configuration target 
         public uint val1;                        //parameter 1
         public uint val2;                        //parameter 2 
         public int errorv;                       //return value
         public int retval;                       //return value
      }

      //----------------------------------------------------------------------------
      //DESCRIPTION: IOCTL configuration request parameter structure 
      //----------------------------------------------------------------------------
      public struct Config_par_t
      {
         public int cmd;                          //special driver command
         public int target;                       //special configuration target 
         public uint val1;                        //parameter 1
         public uint val2;                        //parameter 2 
         public int errorv;                       //return value
         public int retval;                       //return value
      }

      // -----------------------------------------------------------------------------
      //DESCRIPTION:IOCTL Generic CAN controller status request parameter structure 
      // -----------------------------------------------------------------------------
      public struct CanStatusPar_t
      {
         public uint baud;                      //Actual bit rate 
         public uint status;                    //CAN controller status register 
         public uint error_warning_limit;       //The error warning limit 
         public uint rx_errors;                 //Content of RX error counter
         public uint tx_errors;                 //Content of TX error counter 
         public uint error_code;                //Content of error code register 
         public uint rx_buffer_size;            //Size of rx buffer
         public uint rx_buffer_used;            //number of messages
         public uint tx_buffer_size;            //Size of tx buffer for wince, windows not use tx buffer
         public uint tx_buffer_used;            //Number of message for wince, windows not use tx buffer s
         public uint retval;                    //Return value
         public uint type;                      //CAN controller/driver type
         public uint acceptancecode;            //Acceptance code 
         public uint acceptancemask;            //Acceptance mask 
         public uint acceptancemode;           //Acceptance Filter Mode: 1:Single 0:Dual
         public uint selfreception;             //Self reception 
         public uint readtimeout;               //Read timeout 
         public uint writetimeout;              //Write timeout 
      }

        //----------------------------------------------------------------------------
        //DESCRIPTION: COMSTAT  structure
        //----------------------------------------------------------------------------
      public struct COMSTAT
      {
         public int fCtsHold;
         public int fDsrHold;
         public int fRlsdHold;
         public int fXoffHold;
         public int fXoffSent;
         public int fEof;
         public int fTxim;
         public int fReserved;
         public int cbInQue;
         public int cbOutQue;
      }

      //----------------------------------------------------------------------------
      //DESCRIPTION: Windows API Declaration
      //----------------------------------------------------------------------------
      [DllImport("Coredll.dll")]
      public static extern uint CreateEvent(int lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);
 
      [DllImport("Coredll.dll")]
      public static extern bool CloseHandle(uint hDevice);

      [DllImport("Coredll.dll")]
      public static extern uint GetLastError();
   
      [DllImport("Coredll.dll")]
      public static extern uint CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, int hTemplateFile);

      [DllImport("Coredll.dll")]
      public static extern bool DeviceIoControl(uint hDevice, uint dwIoControlCode, IntPtr lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, ref int lpBytesReturned, int lpOverlapped);

      [DllImport("Coredll.dll")]
      public static extern bool WriteFile(uint hDevice, IntPtr pbData, uint nNumberOfFramesToWrite, ref uint lpNumberOfFramesWritten, int lpOverlapped);

      [DllImport("Coredll.dll")]
      public static extern bool ReadFile(uint hDevice, IntPtr pbData, uint nNumberOfFramesToRead, ref uint lpNumberOfFramesRead, int lpOverlapped);

      [DllImport("Coredll.dll")]
      public static extern bool SetCommMask(uint hDevice, uint dwEvtMask);
      
      [DllImport("Coredll.dll")]
      public static extern bool GetCommMask(uint hDevice, ref uint dwEvtMask);
      [DllImport("Coredll.dll")]
      public static extern bool WaitCommEvent(uint hFile, ref int lpEvtMask, int lpOverlapped);

      [DllImport("Coredll.dll")]
      public static extern bool ClearCommError(uint hFile, ref uint lpErrors, ref COMSTAT lpStat);
    }
//}
