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
        private const string password_EnterWinCE = "111111";
        private const string password_Test = "444444";

        //password
        private const string password_Update = "222222";
        private const string password_PageError = "333333";
        private const string password_Setting = "555555";


        private const string password_PageSetup = "555555";
        private const string password_PagePara = "666666";
        private const string password_PagePara_Sensor = "777777";

        public mainForm()
        {
            InitializeComponent();

#if WindowsCE

#else
            panel_Head.MouseMove += new MouseEventHandler(panel_Head_MouseMove);

#endif
        }

        private void mainForm_Load(object sender, EventArgs e)
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


            if (isFontExists())
                LoadFont();

#if WindowsCE
            AdvaCanBusObj = AdvaCanBus.GetAdvaCanBus();
            CDataPool.GetDataPoolObject().LoadFromFile();
            AdvaCanBusObj.CanDatapool = CDataPool.GetDataPoolObject();
            if (!AdvaCanBusObj.Open())
            {
                MessageBox.Show(AdvaCanBusObj.canErrStrArray[AdvaCanBusObj.CanErrCode]);
                return;
            }
#else
            //this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.ControlBox = true;
            this.MinimizeBox = true;
            this.MaximizeBox = false;
            CDataPool.GetDataPoolObject().LoadFromFile();
#endif

            Create_WarnErrOper();
            Create_historyOper();

            bigviewLocation = new System.Drawing.Point(0, panel_Head.Height);
            smallviewLocation = new System.Drawing.Point(0, panel_Head.Height);
            smallviewsize = new System.Drawing.Size(this.Width, this.Height - panel_Head.Height);
            bigviewsize = new System.Drawing.Size(this.Width, this.Height - panel_Head.Height);

            timer1.Enabled = true;
            WarnErrThreadStart();
            historyRecordThreadStart();

            imageLabel_MAC_Click(null, new EventArgs());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            // lb_date.Text = string.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            lb_date.Text = string.Format("日期：{0:0000}-{1:00}-{2:00}", dt.Year, dt.Month, dt.Day);//"日期：2015-02-12";

            lb_time.Text = string.Format("时间：{0:00} : {1:00} : {2:00}", dt.Hour, dt.Minute, dt.Second);//"时间：12 : 11 : 18";
        }

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
        private int WarnErrThreadInterval = 500;
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
            label_CurWarning.Visible = warncount > 0;
            label_CurWarning.Text = warncount.ToString();

            Label_CurError.Visible = errcount > 0;
            Label_CurError.Text = errcount.ToString();
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
        private int historyRecordThreadInterval = 500;
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

        private void Create_pvWarn()
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
        private void Create_pvPara()
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
        private void Create_pvPara_Sensor()
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
        private void Create_pvPara_Setup()
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

        private void Create_pvHistory()
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

        private void Create_pvError()
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

        private void Create_pvDiagnose()
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

        private void Create_pvMAC()
        {
            if (pvMAC == null)
            {
                pvMAC = new PageViewMAC();
                pvMAC.Location = smallviewLocation;
                pvMAC.Size = smallviewsize;
                pvMAC.Enabled = false;
                this.Controls.Add(this.pvMAC);
            }
        }

        #endregion

        private void panel_Head_Click(object sender, EventArgs e)
        {


        }

        #region Tab 事件
        private void imageLabel_MAC_Click(object sender, EventArgs e)
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

        private void imageLabel_Setup_Click(object sender, EventArgs e)
        {
            Create_pvPara_Setup();
            EnterpvSetup();
        }

        private void imageLabel_Para_Click(object sender, EventArgs e)
        {
            Create_pvPara();
            EnterpvPara();
        }

        private void imageLabel_Senser_Click(object sender, EventArgs e)
        {
            Create_pvPara_Sensor();
            EnterpvPara_Sensor();
        }
        private void imageLabel_Diagnose_Click(object sender, EventArgs e)
        {
            Create_pvDiagnose();
            pvDiagnose.DoEnter();
        }
        private void label_CurWarning_Click(object sender, EventArgs e)
        {
            imageLabel_RealWarn_Click(sender, e);
        }

        private void Label_CurError_Click(object sender, EventArgs e)
        {
            imageLabel_RealError_Click(sender, e);
        }

        private void Label_History_Click(object sender, EventArgs e)
        {
            imageLabel_History_Click(sender, e);
        }

        private void Label_Setting_Click(object sender, EventArgs e)
        {
        }

        private void Label_debug_Click(object sender, EventArgs e)
        {
            imageLabel_Diagnose_Click(sender, e);
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
                KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // 故障界面
                    if (f.KeyText == password_PageError)
                    {
                        pvError.DoEnter();
                    }
                }
            }
        }
        private void EnterpvSetup()
        {
            if (pvPara_Setup != null)
            {
                KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // 安装设定
                    if (f.KeyText == password_PageSetup)
                    {
                        pvPara_Setup.DoEnter();
                    }
                }
            }

        }
        private void EnterpvPara()
        {
            if (pvPara != null)
            {
                KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // 参数
                    if (f.KeyText == password_PagePara)
                    {
                        pvPara.DoEnter();
                    }
                }
            }

        }
        private void EnterpvPara_Sensor()
        {
            if (pvPara_Sensor != null)
            {
                KeypadForm f = KeypadForm.GetKeypadForm("", KeypadMode.password);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // 传感器标定
                    if (f.KeyText == password_PagePara_Sensor)
                    {
                        pvPara_Sensor.DoEnter();
                    }
                }
            }

        }
        #endregion

    }
}