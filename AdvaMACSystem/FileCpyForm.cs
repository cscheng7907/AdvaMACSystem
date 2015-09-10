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

namespace AdvaMACSystem
{
    public partial class FileCpyForm : Form
    {
        private static FileCpyForm cpyfrm = null;

        public static FileCpyForm GetFileCpyForm()
        {
            if (cpyfrm == null)
                cpyfrm = new FileCpyForm();
            return cpyfrm;
        }

        private int Sum = 0, Num = 0;

        private const string Path_Local = "\\Harddisk\\History";

        private const string Path_USBDisk1 = "\\USB Hard Disk";
        private const string Path_USBDisk2 = "\\USB";

        private string Path_USBDisk = string.Empty;



        private FileCpyForm()
        {
            InitializeComponent();
        }


        public void StartCopy()
        {

            progressBar1.Value = 0;
            timer1.Enabled = true;
            label_Caption.Text = "文件复制中,请稍候...";


            this.Show();
            this.BringToFront();

            if (CheckUSB())
            {

                ThreadStart();

            }
            else
            {

                this.Close();
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
            Sum = GetFileCount(Path_Local);

            string des = Path_USBDisk + "\\HistoryBackUp" + DateTime.Now.ToString("[yyyy-MM-dd-HH-mm-ss]");
            Directory.CreateDirectory(des);

            CopyDirectory(Path_Local, des);
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

        public delegate void UpdateProcessDelegate(int FileCopycount);
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
            label_PValue.Visible = true;
            if (FileCopycount <= Sum && Sum >= 0)
                progressBar1.Value = (int)((FileCopycount / (double)Sum) * 100);
            else
                progressBar1.Value = 100;

            label_PValue.Text = progressBar1.Value.ToString() + "%";

            Application.DoEvents();

            if (progressBar1.Value >= 99)
            {
                //lb_State.Text = "升级完成，请断电重启系统";
                label_Caption.Text = "文件复制完成！";

                button1.Enabled = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            cpyfrm = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}