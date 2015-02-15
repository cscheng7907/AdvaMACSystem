using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AdvaMACSysUpdater
{
    public partial class Form1 : Form
    {
        private int Sum = 0, Num = 0;


        private const string Path_Local = "\\Harddisk";

        private const string Path_USBDisk1 = "\\Usb\\Update";
        private const string Path_USBDisk2 = "\\Harddisk\\Update";

        private string Path_USBDisk = string.Empty;

        private const int WaitCount = 8;
        private int Waiting = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Waiting <= WaitCount)
            {
                Waiting++;
            }

            if (CheckUSB())
            {
                timer1.Enabled = false;

                lb_State.Text = "USB设备已经就绪，请按“升级”继续。。。";

                button1.Enabled = true;

                DoUpdate();
            }

        }

        private bool CheckUSB()
        {
            if (Directory.Exists(Path_USBDisk1))
                Path_USBDisk = Path_USBDisk1;

            if (Directory.Exists(Path_USBDisk2))
                Path_USBDisk = Path_USBDisk2;

            if (Path_USBDisk != string.Empty)
                return true;

            return false;
        }

        private void DoUpdate()
        {

            DirectoryInfo di = new DirectoryInfo(Path_USBDisk);
            Sum = di.GetFiles().Length;
            timer2.Enabled = true;

            button1.Enabled = false;
            CopyDirectory(Path_USBDisk, Path_Local);

            lb_State.Text = "升级完成，请断电重启系统";
            timer2.Enabled = false;
        }

        private void CopyDirectory(string srcdir, string desdir)
        {
            string folderName = srcdir.Substring(srcdir.LastIndexOf("\\") + 1);

            string desfolderdir = desdir + "\\" + folderName;

            if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
            {
                desfolderdir = desdir + folderName;
            }


            string[] filenames = Directory.GetFileSystemEntries(srcdir);

            foreach (string file in filenames)// 遍历所有的文件和目录
            {
                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {
                    string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(currentdir))
                    {
                        Directory.CreateDirectory(currentdir);
                    }

                    CopyDirectory(file, desfolderdir);
                }

                else // 否则直接copy文件
                {
                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

                    srcfileName = desfolderdir + "\\" + srcfileName;

                    if (!Directory.Exists(desfolderdir))
                    {
                        Directory.CreateDirectory(desfolderdir);
                    }

                    File.Copy(file, srcfileName);
                }
            }//foreach
        }

        private void DoProcess()
        {

            if (Directory.Exists(Path_Local))
            {
                DirectoryInfo di = new DirectoryInfo(Path_Local);
                Num = di.GetFiles().Length;
            }
            else
                Num = 0;

            if (Num > 0)
                progressBar1.Value = Num / Sum;
            else
                progressBar1.Value = 100;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            progressBar1.Visible = true;

            DoProcess();
        }

    }
}