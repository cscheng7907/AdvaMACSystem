/*AdvaMACSystem 监控软件
 * 
 * 版本号 V1.0
 * 作者： 程慎
 *  
 * 修改记录：
 *       时间                内容                人员
 * 2015-2-15             创建                by cs 
 * 
 * copyright
 */

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using DataPool;
using ComCtrls;

namespace AdvaMACSystem
{
    public partial class mainForm : Form
    {
        private System.Windows.Forms.Timer timerLoad;

        private System.Drawing.Point bigviewLocation;
        private System.Drawing.Point smallviewLocation;
        private System.Drawing.Size bigviewsize;
        private System.Drawing.Size smallviewsize;
        private AdvaCanBus AdvaCanBusObj = null;
#if WindowsCE
#else
        private VirtualSetForm _VirtualSetForm = null;
#endif
        private const int mouselast = 3000;//ms

        //for testing
        private const string password_EnterWinCE = "111";//进wince桌面密码
        private const string password_Test = "444";//测试密码

        //password
        private const string password_Update = "222";//系统升级密码
        private const string password_Reset = "234";//系统复位密码

        private const string password_PageError = "333";//故障界面密码
        private const string password_Setting = "555";//参数设置密码
        private const string password_Diagnose = "666";//诊断界面密码
        private const string password_Para_Cylinder = "123123";//设备参数界面密码

        private const string password_Backup_History = "777";//历史记录备份密码


        private Bitmap estop = new Bitmap(AdvaMACSystem.AdvaMACSystemRes.estop64);
        private Bitmap estop_1 = new Bitmap(AdvaMACSystem.AdvaMACSystemRes.estop64_1);

        //private const string password_PageSetup = "555555";
        //private const string password_PagePara = "666666";
        //private const string password_PagePara_Sensor = "777777";

        public mainForm()
        {
            InitializeComponent();

#if WindowsCE

#else
            panel_Head.MouseMove += new MouseEventHandler(panel_Head_MouseMove);

#endif
            InitAppDirs();

            timerLoad_Tick(null, null);

        }

        private void InitAppDirs()
        {
#if WindowsCE
            if (!Directory.Exists(@"\HardDisk\History"))
                Directory.CreateDirectory(@"\HardDisk\History");

            if (!Directory.Exists(@"\HardDisk\Record"))
                Directory.CreateDirectory(@"\HardDisk\Record");
#else
            if (!Directory.Exists(Application.StartupPath + @"\History"))
                Directory.CreateDirectory(Application.StartupPath + @"\History");

            if (!Directory.Exists(Application.StartupPath + @"\Record"))
                Directory.CreateDirectory(Application.StartupPath + @"\Record");

#endif

        }


        private void mainForm_Load(object sender, EventArgs e)
        {
            //this.timerLoad = new System.Windows.Forms.Timer();

            //this.timerLoad.Interval = 500;
            //this.timerLoad.Tick += new System.EventHandler(this.timerLoad_Tick);

            //this.timerLoad.Enabled = true;


            InitAppDirs();


            timer1.Enabled = true;


            UIControlbase.OnKTUIControlChanged += new KTUIControlChangedEventHandler(OnPageViewChanged);

        }

        private void timerLoad_Tick(object sender, EventArgs e)
        {
            //this.timerLoad.Enabled = false;

            if (isFontExists())
                LoadFont();

            UpdateWarnErrorLabel(0, 0);

            bigviewLocation = new System.Drawing.Point(0, panel_Head.Height);
            smallviewLocation = new System.Drawing.Point(0, panel_Head.Height);
            smallviewsize = new System.Drawing.Size(this.Width, this.Height - panel_Head.Height);
            bigviewsize = new System.Drawing.Size(this.Width, this.Height - panel_Head.Height);


#if WindowsCE
            AdvaCanBusObj = AdvaCanBus.GetAdvaCanBus();
            CDataPool.GetDataPoolObject().LoadFromFile();
            //AdvaCanBusObj.CanDatapool = CDataPool.GetDataPoolObject();
            if (!AdvaCanBusObj.Open())
            {
                //MessageBox.Show(AdvaCanBusObj.canErrStrArray[AdvaCanBusObj.CanErrCode]);
                MessageBox.Show(AdvaCanBusObj.canErrStrArray[AdvaCanBusObj.CanErrCode], "",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Hand,
                     MessageBoxDefaultButton.Button1);

                return;
            }
#else
            //this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.ControlBox = true;
            this.MinimizeBox = true;
            this.MaximizeBox = false;
            CDataPool.GetDataPoolObject().LoadFromFile();
#endif
            //imageLabel_MAC_Click(null, new EventArgs());

            Application.DoEvents();

            Create_WarnErrOper();
            Create_historyOper();

            WarnErrThreadStart();
            historyRecordThreadStart();


        }


        private int timer1_count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            // lb_date.Text = string.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            lb_date.Text = string.Format("{0:0000}-{1:00}-{2:00}", dt.Year, dt.Month, dt.Day);//"日期：2015-02-12";

            lb_time.Text = string.Format("{0:00} : {1:00} : {2:00}", dt.Hour, dt.Minute, dt.Second);//"时间：12 : 11 : 18";

            //hartbeating
#if WindowsCE
            //500ms * 4 = 2s
            if (timer1_count > 4)
            {
                for (int i = 0; i < dataPool.PumpCount; i++)
                {
                    TimeSpan sub = DateTime.Now.Subtract(dataPool.in_Pump_HartBeating_1010_1013[i]);

                    if (sub.TotalMilliseconds > 2000)
                        dataPool.ResetPump(i);
                }

                timer1_count = 0;
            }
            else
                timer1_count++;
#endif

            //刷新紧停标志
            int pumpCount = (int)dataPool.PumpCount;
            isEmergencyStop = false;
            for (int i = 0; i < pumpCount; i++)
            {
                isEmergencyStop = isEmergencyStop || dataPool.GetBoolValue(i, 0, CmdDataType.cdtEStop_1010_1013);
            }
            if (isEmergencyStop)
            {
                imageLabel_Estop.Visible = true;
                if (showEStopIcon)
                {
                    imageLabel_Estop.BackImg = estop;
                }
                else
                {
                    imageLabel_Estop.BackImg = estop_1;
                }
                showEStopIcon = !showEStopIcon;
            }
            else
            {
                imageLabel_Estop.Visible = false;
                showEStopIcon = true;
            }
        }

        private CDataPool dataPool = CDataPool.GetDataPoolObject();
        private bool isEmergencyStop = false;
        private bool showEStopIcon = true;

        #region 加载字体
#if WindowsCE
        //新增字体
        [DllImport("coredll", EntryPoint = "AddFontResource")]
        private static extern int AddFontResource([In, MarshalAs(UnmanagedType.BStr)]string fontSource);

        [DllImport("coredll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
#endif

        private bool isFontExists()
        {
            System.Drawing.Text.InstalledFontCollection fonts = new System.Drawing.Text.InstalledFontCollection();
            foreach (System.Drawing.FontFamily family in fonts.Families)
            {
                if (family.Name == "微软雅黑")
                    return true;
            }
            return false;
        }

        private void LoadFont()
        {
#if WindowsCE
            if (!isFontExists())
            {
                if (File.Exists(@"HardDisk2\MSYH.ttf"))
                {
                    int installFont = AddFontResource(@"HardDisk2\MSYH.ttf");
                    SendMessage((IntPtr)0xffff, 0x001d, IntPtr.Zero, IntPtr.Zero);
                }
            }
#endif
        }
        #endregion

        #region 报警和故障的扫描
        private WarnErrOperator WarnErrOper = null;

        private void Create_WarnErrOper()
        {

            if (WarnErrOper == null)
            {
                WarnErrOper = new WarnErrOperator();

                WarnErrOper.CanDatapool = CDataPool.GetDataPoolObject();

                WarnErrOper.OnWarnErrChanged += new EventHandler(DoWarn);
            }
        }

        private Thread WarnErrThread;
        private int WarnErrThreadInterval = 2000;
        private bool WarnErrThreadrunning = false;
        private void WarnErrThreadStart()
        {
            if (WarnErrThreadrunning)
                return;

            SetList = new UpdateWarnErrorDelegate(UpdateWarnErrorLabel);
            WarnErrThread = new Thread(new ThreadStart(WarnErrThreadMethod));
            WarnErrThread.Start();

        }

        private void WarnErrThreadStop()
        {
            WarnErrThreadrunning = false;

        }

        private void WarnErrThreadMethod()
        {
            WarnErrThreadrunning = true;
            while (WarnErrThreadrunning)
            {
                Thread.Sleep(WarnErrThreadInterval);

                if (WarnErrOper != null)
                    WarnErrOper.Update();
            }
        }

        private void DoWarn(object sender, EventArgs e)
        {
            if (WarnErrOper != null)
            {
                label_CurWarning.Invoke(SetList, WarnErrOper.CurWarningList.Count, WarnErrOper.CurErrorList.Count);

            }
        }
        public delegate void UpdateWarnErrorDelegate(int warncount, int errcount);
        //Controls can be used in thread by delegate
        private UpdateWarnErrorDelegate SetList;

        public void UpdateWarnErrorLabel(int warncount, int errcount)
        {
            //label_CurWarning.Visible = warncount > 0;
            //label_CurWarning.Text = warncount.ToString();

            label_CurWarning.BackImg =
                (warncount > 0) ? AdvaMACSystemRes.warning2 : AdvaMACSystemRes.warning2_gray;
            label_CurWarning.Text = (warncount > 0) ? warncount.ToString() : string.Empty;

            //Label_CurError.Visible = errcount > 0;
            //Label_CurError.Text = errcount.ToString();

            Label_CurError.BackImg =
                (errcount > 0) ? AdvaMACSystemRes.fix2 : AdvaMACSystemRes.fix2_gray;
            Label_CurError.Text = (errcount > 0) ? errcount.ToString() : string.Empty;

            if (pvMAC != null)
                pvMAC.UpdateWarningCount();
        }

        #endregion

        #region 历史数据的记录
        private HistoryOperator historyOper = null;

        private void Create_historyOper()
        {

            if (historyOper == null)
            {
                historyOper = new HistoryOperator();

                historyOper.CanDatapool = CDataPool.GetDataPoolObject();
            }
        }

        private Thread historyRecordThread;
        private int historyRecordThreadInterval = 4000;
        private bool historyRecordThreadrunning = false;
        private void historyRecordThreadStart()
        {
            if (historyRecordThreadrunning)
                return;

            historyRecordThread = new Thread(new ThreadStart(historyRecordThreadMethod));
            historyRecordThread.Start();

        }

        private void historyRecordThreadStop()
        {
            historyRecordThreadrunning = false;

        }

        private void historyRecordThreadMethod()
        {
            historyRecordThreadrunning = true;
            while (historyRecordThreadrunning)
            {
                Thread.Sleep(historyRecordThreadInterval);

                if (historyOper != null)
                    historyOper.Update();
            }
        }

        #endregion

        #region PageView vars
        private PageViewMAC pvMAC = null;
        private PageViewDiagnose pvDiagnose = null;
        private PageViewError pvError = null;
        private PageViewHistory pvHistory = null;
        private PageViewPara pvPara = null;
        private PageViewWarn pvWarn = null;
        private PageViewPara_Sensor pvPara_Sensor = null;
        private PageViewPara_Setup pvPara_Setup = null;
        private PageViewPara_Cylinder pvPara_Cylinder = null;

        //private PagePanel_WarnErr ppWarnErr = null;
        //private PagePanel_Para ppPara = null;

        public void Create_pvWarn()
        {
            if (pvWarn == null)
            {
                pvWarn = new PageViewWarn();
                pvWarn.Location = bigviewLocation;
                pvWarn.Size = bigviewsize;
                pvWarn.Optor = WarnErrOper;
                pvWarn.Enabled = false;
                this.Controls.Add(this.pvWarn);
            }
        }
        public void Create_pvPara()
        {
            if (pvPara == null)
            {
                pvPara = new PageViewPara();
                pvPara.Location = bigviewLocation;
                pvPara.Size = bigviewsize;
                pvPara.Enabled = false;
                this.Controls.Add(this.pvPara);
            }
        }
        public void Create_pvPara_Sensor()
        {
            if (pvPara_Sensor == null)
            {
                pvPara_Sensor = new PageViewPara_Sensor();
                pvPara_Sensor.Location = bigviewLocation;
                pvPara_Sensor.Size = bigviewsize;
                pvPara_Sensor.Enabled = false;
                this.Controls.Add(this.pvPara_Sensor);
            }
        }
        public void Create_pvPara_Setup()
        {
            if (pvPara_Setup == null)
            {
                pvPara_Setup = new PageViewPara_Setup();
                pvPara_Setup.Location = bigviewLocation;
                pvPara_Setup.Size = bigviewsize;
                pvPara_Setup.Enabled = false;
                this.Controls.Add(this.pvPara_Setup);
            }
        }
        public void Create_pvPara_Cylinder()
        {
            if (pvPara_Cylinder == null)
            {
                pvPara_Cylinder = new PageViewPara_Cylinder();
                pvPara_Cylinder.Location = bigviewLocation;
                pvPara_Cylinder.Size = bigviewsize;
                pvPara_Cylinder.Enabled = false;
                this.Controls.Add(this.pvPara_Cylinder);
            }
        }
        public void Create_pvHistory()
        {
            if (pvHistory == null)
            {
                pvHistory = new PageViewHistory();
                pvHistory.Location = bigviewLocation;
                pvHistory.Size = bigviewsize;
                pvHistory.Enabled = false;
                pvHistory.CanDatapool = CDataPool.GetDataPoolObject();
                pvHistory.HistoryOper = this.historyOper;
                this.Controls.Add(this.pvHistory);
            }
        }

        public void Create_pvError()
        {
            if (pvError == null)
            {
                pvError = new PageViewError();
                pvError.Location = bigviewLocation;
                pvError.Size = bigviewsize;
                pvError.Optor = WarnErrOper;
                pvError.Enabled = false;
                this.Controls.Add(this.pvError);
            }
        }

        public void Create_pvDiagnose()
        {
            if (pvDiagnose == null)
            {
                pvDiagnose = new PageViewDiagnose();
                pvDiagnose.Location = bigviewLocation;
                pvDiagnose.Size = bigviewsize;
                pvDiagnose.Enabled = false;
                this.Controls.Add(this.pvDiagnose);
            }
        }

        public void Create_pvMAC()
        {
            if (pvMAC == null)
            {
                pvMAC = new PageViewMAC();
                pvMAC.Location = smallviewLocation;
                pvMAC.Size = smallviewsize;
                pvMAC.Enabled = false;
                pvMAC.Optor = WarnErrOper;
                UIControlbase.BaseKTUIControl = pvMAC;
                this.Controls.Add(this.pvMAC);
            }
        }

        public void Create_UIControls()
        {
            Create_pvMAC();
            Create_pvHistory();
            Create_pvDiagnose();
            Create_pvError();
            Create_pvPara();
            Create_pvPara_Sensor();
            Create_pvPara_Setup();
            Create_pvWarn();
        }
        //private void Create_ppWarnErr()
        //{
        //    if (ppWarnErr == null)
        //    {
        //        ppWarnErr = new PagePanel_WarnErr();
        //        Create_ppWarnErr();
        //        ppWarnErr.AddPageView(pvWarn);

        //        Create_pvError();
        //        ppWarnErr.AddPageView(pvError);
        //    }
        //}

        //private void Create_ppPara()
        //{
        //    if (ppPara == null)
        //    {
        //        ppPara = new PagePanel_Para();
        //        Create_pvPara_Setup();
        //        ppPara.AddPageView(pvPara_Setup);
        //        Create_pvPara();
        //        ppPara.AddPageView(pvPara);
        //        Create_pvPara_Sensor();
        //        ppPara.AddPageView(pvPara_Sensor);
        //    }
        //}
        #endregion

        private void panel_Head_Click(object sender, EventArgs e)
        {


        }

        #region Tab 事件
        public void imageLabel_MAC_Click(object sender, EventArgs e)
        {
            Create_pvMAC();
            if (UIControlbase.CurKTUIControl != pvMAC)
                pvMAC.DoEnter();
        }

        private void imageLabel_RealWarn_Click(object sender, EventArgs e)
        {
            Create_pvWarn();
            if (UIControlbase.CurKTUIControl != pvWarn)
            {
                pvWarn.IsReal = true;
                pvWarn.DoEnter();
            }
        }

        private void imageLabel_RealError_Click(object sender, EventArgs e)
        {
            Create_pvError();
            if (UIControlbase.CurKTUIControl != pvError)
            {
                pvError.IsReal = true;
                EnterpvError();
                //pvError.DoEnter();
            }
        }

        private void imageLabel_History_Click(object sender, EventArgs e)
        {
            Create_pvHistory();
            pvHistory.DoEnter();
        }

        private void imageLabel_HisWarn_Click(object sender, EventArgs e)
        {
            Create_pvWarn();
            if (UIControlbase.CurKTUIControl != pvWarn)
            {
                pvWarn.IsReal = false;
                pvWarn.DoEnter();
            }
        }

        private void imageLabel_HisError_Click(object sender, EventArgs e)
        {
            Create_pvError();
            if (UIControlbase.CurKTUIControl != pvError)
            {
                pvError.IsReal = false;
                EnterpvError();
                //pvError.DoEnter();
            }
        }
        /*
                private void imageLabel_Setup_Click(object sender, EventArgs e)
                {
                   //Create_pvPara_Setup();
                    EnterpvSetup();
                }

                private void imageLabel_Para_Click(object sender, EventArgs e)
                {
                    //Create_pvPara();
                    EnterpvPara();
                }

                private void imageLabel_Senser_Click(object sender, EventArgs e)
                {
                    //Create_pvPara_Sensor();
                    EnterpvPara_Sensor();
                }
        */

        private void imageLabel_Diagnose_Click(object sender, EventArgs e)
        {
#if UNPASSWORD
            Create_pvDiagnose();
            pvDiagnose.DoEnter();
#else
            KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
            if (f.ShowDialog() == DialogResult.OK)
            {
                // 安装设定
                if (f.KeyText == password_Diagnose)
                {
                    Create_pvDiagnose();
                    pvDiagnose.DoEnter();
                }
            }
#endif
        }

        private void label_CurWarning_Click(object sender, EventArgs e)
        {
            imageLabel_RealWarn_Click(sender, e);
            imageLabel_WarnSet_Click(imageLabel_Warn_Real, new EventArgs());
        }

        private void Label_CurError_Click(object sender, EventArgs e)
        {
            imageLabel_RealError_Click(sender, e);
            imageLabel_ErrSet_Click(imageLabel_Err_Real, new EventArgs());
        }

        private void Label_History_Click(object sender, EventArgs e)
        {
            imageLabel_History_Click(sender, e);
        }

        private void Label_Setting_Click(object sender, EventArgs e)
        {
#if UNPASSWORD
            //EnterpvSetup();
            OnPageViewChanged(null);
            imageLabel_ParaSet_Click(imageLabel_Setup, new EventArgs());

#else

            KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
            if (f.ShowDialog() == DialogResult.OK)
            {
                // 安装设定
                if (f.KeyText == password_Setting)
                {
                    //EnterpvSetup();
                    OnPageViewChanged(null);
                    imageLabel_ParaSet_Click(imageLabel_Setup, new EventArgs());
                }
            }
#endif
        }

        private void OnPageViewChanged(UIControlbase PreUICtrl)
        {
            if (UIControlbase.CurKTUIControl != null)
            {
                UIControlbase c = UIControlbase.CurKTUIControl;
                panel_Warn.Visible = c == pvWarn; panel_Warn.BringToFront();
                panel_Err.Visible = c == pvError; panel_Err.BringToFront();

                panel_Para.Visible =
                    c == pvPara_Cylinder ||
                    c == pvPara ||
                    c == pvPara_Sensor ||
                    c == pvPara_Setup;
                panel_Para.BringToFront();

                //if (PreUICtrl == pvMAC && c != pvMAC &&
                //    pvMAC.ControlMode == ControlModeType.CylinderManual)
                //MessageBox.Show("当前为手动模式，请注意！");
                //MessageBox.Show("当前为手动模式，请注意！", "",
                //      MessageBoxButtons.YesNo,
                //      MessageBoxIcon.Exclamation,
                //      MessageBoxDefaultButton.Button1);         
            }
        }

        private void imageLabel_ParaSet_Click(object sender, EventArgs e)
        {
            Control btn = (Control)sender;
            foreach (Control item in panel_Para.Controls)
            {
                item.Enabled = !(item == btn);
            }

            switch (Convert.ToInt32(btn.Tag))
            {
                case 0:
                    EnterpvSetup();
                    break;
                case 1:
                    EnterpvPara();
                    break;
                case 2:
                    EnterpvPara_Sensor();
                    break;
                case 3:
                    EnterpvPara_Cylinder();
                    break;
                default:
                    break;
            }

        }

        private void imageLabel_WarnSet_Click(object sender, EventArgs e)
        {
            Control btn = (Control)sender;

            //btn enable
            foreach (Control item in panel_Warn.Controls)
            {
                item.Enabled = !(item.Text == btn.Text);
            }

            //UIControlbase.CurKTUIControl.Exit();

            switch (Convert.ToInt32(btn.Tag))
            {
                case 0:
                    pvWarn.IsReal = true;
                    break;
                case 1:
                    pvWarn.IsReal = false;
                    break;
                default:
                    break;
            }
        }

        private void imageLabel_ErrSet_Click(object sender, EventArgs e)
        {
            Control btn = (Control)sender;

            //btn enable
            foreach (Control item in panel_Err.Controls)
            {
                item.Enabled = !(item.Text == btn.Text);
            }

            //UIControlbase.CurKTUIControl.Exit();

            switch (Convert.ToInt32(btn.Tag))
            {
                case 0:
                    pvError.IsReal = true;
                    break;
                case 1:
                    pvError.IsReal = false;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region MouseEvent & password
        private Point _MP;
        DateTime MouseDownTime = DateTime.Now;
        private void panel_Head_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //else 
            if (e.Button == MouseButtons.Left)
            {
                _MP.X = e.X;
                _MP.Y = e.Y;
                MouseDownTime = DateTime.Now;
            }
        }
        private void panel_Head_MouseUp(object sender, MouseEventArgs e)
        {
            DateTime MouseUpTime = DateTime.Now;

            TimeSpan ts = (TimeSpan)(MouseUpTime - MouseDownTime);

            if (ts.TotalMilliseconds >= mouselast)
            {
                KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    //退出程序，进入wince 
                    if (f.KeyText == password_EnterWinCE)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", "");
                        //System.Diagnostics.Process.Start("\\NORFlash\\001\\COPY.bat", "");
                        Application.DoEvents();
                        Application.Exit();
                    }
                    else if (f.KeyText == password_Update) //软件升级
                    {
                        if (File.Exists("\\HardDisk\\AdvaMACSysUpdater.exe"))
                        {
                            System.Diagnostics.Process.Start("\\HardDisk\\AdvaMACSysUpdater.exe", "");

                            Application.DoEvents();
                            Application.Exit();
                        }
                        else
                            if (File.Exists("\\USB Hard Disk\\AdvaMACSysUpdater.exe"))
                            {
                                System.Diagnostics.Process.Start("\\USB Hard Disk\\AdvaMACSysUpdater.exe", "");

                                Application.DoEvents();
                                Application.Exit();
                            }

                    }
#if WindowsCE
#else
                    else if (f.KeyText == password_Test) //test
                    {
                        if (_VirtualSetForm == null)
                            _VirtualSetForm = new VirtualSetForm();

                        _VirtualSetForm.Show();
                    }
#endif
                    else if (f.KeyText == password_Reset) //系统数据复位
                    {
                        CDataPool.GetDataPoolObject().Reset();

                        if (WarnErrOper != null)
                            WarnErrOper.Reset();

                        if (historyOper != null)
                            historyOper.Reset();
                    }
                    else if (f.KeyText == password_Backup_History)//历史记录备份
                    {
                        FileCpyForm.GetFileCpyForm().StartCopy();
                    }
                }
            }
        }

        private void panel_Head_MouseMove(object sender, MouseEventArgs e)
        {
#if WindowsCE
#else
            if (e.Button == MouseButtons.Left)
            {
                Top = MousePosition.Y - _MP.Y;
                Left = MousePosition.X - _MP.X;
            }
#endif
        }
        private void EnterpvError()
        {
            if (pvError != null)
            {
#if UNPASSWORD
                pvError.DoEnter();
#else
                KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // 故障界面
                    if (f.KeyText == password_PageError)
                    {
                        pvError.DoEnter();
                    }
                }
#endif
            }
        }
        private void EnterpvSetup()
        {
            Create_pvPara_Setup();
            //if (pvPara_Setup != null)
            //{
            //    KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
            //    if (f.ShowDialog() == DialogResult.OK)
            //    {
            //        // 安装设定
            //        if (f.KeyText == password_PageSetup)
            //        {
            //            pvPara_Setup.DoEnter();
            //        }
            //    }
            //}
            pvPara_Setup.DoEnter();
        }
        private void EnterpvCylinderPara()
        { }

        private void EnterpvPara()
        {
            Create_pvPara();
            //if (pvPara != null)
            //{
            //    KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
            //    if (f.ShowDialog() == DialogResult.OK)
            //    {
            //        // 参数
            //        if (f.KeyText == password_PagePara)
            //        {
            //            pvPara.DoEnter();
            //        }
            //    }
            //}
            pvPara.DoEnter();

        }

        private void EnterpvPara_Sensor()
        {
            Create_pvPara_Sensor();
            //if (pvPara_Sensor != null)
            //{
            //    KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
            //    if (f.ShowDialog() == DialogResult.OK)
            //    {
            //        // 传感器标定
            //        if (f.KeyText == password_PagePara_Sensor)
            //        {
            //            pvPara_Sensor.DoEnter();
            //        }
            //    }
            //}

            pvPara_Sensor.DoEnter();
        }
        private void EnterpvPara_Cylinder()
        {


#if UNPASSWORD
            Create_pvPara_Cylinder();
            pvPara_Cylinder.DoEnter();
#else

            KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
            if (f.ShowDialog() == DialogResult.OK)
            {
                //设备参数
                if (f.KeyText == password_Para_Cylinder)
                {
                    MessageBox.Show("该页面参数影响设备工作性能，请确定完全后再进行更改。", "",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Hand,
                         MessageBoxDefaultButton.Button1);

                    Create_pvPara_Cylinder();
                    pvPara_Cylinder.DoEnter();
                }
            }
#endif
        }
        #endregion

    }
}





