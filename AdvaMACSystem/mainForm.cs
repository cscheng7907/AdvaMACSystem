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

namespace AdvaMACSystem
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            lb_Now.Text = string.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
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

    }
}