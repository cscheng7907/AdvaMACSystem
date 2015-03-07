using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace AdvaMACSysUpdater
{
    public partial class Form1 : Form
    {
        private int Sum = 0, Num = 0;


        private const string Path_Local = "\\Harddisk";

        private const string Path_USBDisk1 = "\\USB Hard Disk\\Update";
        private const string Path_USBDisk2 = "\\USB\\Update";

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
                // timer1.Enabled = false;

                lb_State.Text = "USB设备已经就绪，请按“升级”继续......";

                button1.Enabled = true;

            }
            else
            {
                this.lb_State.Text = "USB设备未就绪";
                button1.Enabled = false;
            }

        }

        private bool CheckUSB()
        {
            Path_USBDisk = string.Empty;

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

            //DirectoryInfo di = new DirectoryInfo(Path_USBDisk);
            //Sum = di.GetFiles().Length;

            Sum = GetFileCount(Path_USBDisk);
            //timer2.Enabled = true;

            //Application.DoEvents();

            CopyDirectory(Path_USBDisk, Path_Local);

            //timer2.Enabled = false;
        }

        private int GetFileCount(string dir)
        {
            int cnt = 0;

            string[] filenames = Directory.GetFileSystemEntries(dir);

            foreach (string file in filenames)// 遍历所有的文件和目录
            {
                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归
                {
                    cnt += GetFileCount(file);
                }
                else // 否则文件
                {
                    cnt++;
                }
            }
            return cnt;
        }

        private void CopyDirectory(string srcdir, string desdir)
        {
            //string folderName = srcdir.Substring(srcdir.LastIndexOf("\\") + 1);

            //string desfolderdir = desdir + "\\" + folderName;

            //if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
            //{
            //    desfolderdir = desdir + folderName;
            //}

            string desfolderdir = desdir;

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

                    CopyDirectory(file, currentdir);
                }

                else // 否则直接copy文件
                {
                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

                    srcfileName = desfolderdir + "\\" + srcfileName;

                    if (!Directory.Exists(desfolderdir))
                    {
                        Directory.CreateDirectory(desfolderdir);
                    }

                    File.Copy(file, srcfileName, true);

                    Num++;
                    progressBar1.Invoke(SetProcess, Num);
                }
            }//foreach
        }

        //private void DoProcess()
        //{

        //    if (Directory.Exists(Path_Local))
        //    {
        //        DirectoryInfo di = new DirectoryInfo(Path_Local);
        //        Num = di.GetFiles().Length;
        //    }
        //    else
        //        Num = 0;

        //    if (Num > 0)
        //        progressBar1.Value = Num / Sum;
        //    else
        //        progressBar1.Value = 100;
        //}

        private void timer2_Tick(object sender, EventArgs e)
        {
            //progressBar1.Visible = true;

            //DoProcess();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            // DoUpdate();
            button1.Enabled = false;

            lb_State.Text = "升级中，请稍候......";

            ThreadStart();
        }

        public delegate void UpdateProcessDelegate(int FileCopycount);
        //Controls can be used in thread by delegate
        private UpdateProcessDelegate SetProcess;

        private void ThreadStart()
        {
            Thread MyThread;

            SetProcess = new UpdateProcessDelegate(UpdateProcess);
            MyThread = new Thread(new ThreadStart(WarnErrThreadMethod));
            MyThread.Start();
        }
        private void WarnErrThreadMethod()
        { DoUpdate(); }
        public void UpdateProcess(int FileCopycount)
        {
            progressBar1.Visible = true;
            lb_Process.Visible = true;
            if (FileCopycount <= Sum && Sum >= 0)
                progressBar1.Value = (int)((FileCopycount / (double)Sum) * 100);
            else
                progressBar1.Value = 100;

            lb_Process.Text = progressBar1.Value.ToString() + "%";

            Application.DoEvents();

            if (progressBar1.Value >= 99)
                lb_State.Text = "升级完成，请断电重启系统";
        }

    }
}