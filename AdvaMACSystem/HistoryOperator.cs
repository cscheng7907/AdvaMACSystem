using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DataPool;
using System.Windows.Forms;

namespace AdvaMACSystem
{
    public class HistoryOperator
    {
        public HistoryOperator()
        {
        }
#if WindowsCE
        public string PressureRecFileName = @"\HardDisk\History\Pressure\{0}\{1}\{2}.Rec";
        public string PositionRecFileName = @"\HardDisk\History\Position\{0}\{1}\{2}.Rec";
        public string HistoryDirectory = @"\HardDisk\History\";
#else
        public string PressureRecFileName = Application.StartupPath + @"\History\Pressure\{0}\{1}\{2}.Rec";
        public string PositionRecFileName = Application.StartupPath + @"\History\Position\{0}\{1}\{2}.Rec";
        public string HistoryDirectory = Application.StartupPath + @"\History\";
#endif

        private string pressureRecFile = string.Empty;
        private string positionRecFile = string.Empty;
        private string recPressureDirectory = string.Empty;
        private string recPositionDirectory = string.Empty;
        private const int MultiplyingFactor = 10;
        private const int FILE_HEAD_SIZE = 100;
        public long CONST_FILE_HEAD_SIZE
        {
            get { return (long)FILE_HEAD_SIZE; }
        }
        private byte[] headData = new byte[FILE_HEAD_SIZE];

        private List<FileStream> fsPressureList = null;
        private List<BinaryWriter> bwPressureList = null;
        private List<FileStream> fsPositionList = null;
        private List<BinaryWriter> bwPositionList = null;

        private CDataPool _candatapool = null;
        public CDataPool CanDatapool
        {
            set
            {
                if (_candatapool != value)
                {
                    _candatapool = value;
                }
            }
        }

        private int timerInterval = 1000;
        public int TimerInterVal
        {
            set
            {
                if (timerInterval != value)
                    timerInterval = value;
            }
        }

        private int reserveDays = 60;
        public int ReserveDays
        {
            get { return reserveDays; }
            set
            {
                if (reserveDays != value && reserveDays > 0)
                    reserveDays = value;
            }
        }

        public void Update()
        {
            if (firstRecord)
            {
                DeleteExpiredFiles();
                CreateNewFiles();
            }

            if (DateTime.Now.Date != dateCreateFile.Date)
            {
                CloseFiles();
                DeleteExpiredFiles();
                CreateNewFiles();
            }

            if (DateTime.Now.Ticks >= nextRecordTimeTicks)
            {
                RecordData();
                nextRecordTimeTicks += timerInterval * 10000;
            }
        }

        public void DeleteExpiredFiles()
        {
            TimeSpan t = new TimeSpan(reserveDays, 0, 0, 0);
            DateTime oldestTime = DateTime.Now.Date.Subtract(t);

            List<string> recFiles = new List<string>();
            FindFiles(HistoryDirectory, recFiles);
            foreach (string s in recFiles)
            {
                string nameToParse = Path.GetFileNameWithoutExtension(s);
                try
                {
                    if (nameToParse.IndexOf(' ') >= 0)
                        nameToParse = nameToParse.Substring(0, nameToParse.IndexOf(' '));
                    else
                        continue;

                    DateTime recTime = Convert.ToDateTime(nameToParse);
                    if (recTime.Ticks < oldestTime.Ticks)
                    {
                        File.Delete(s);
                    }
                }
                catch
                { }
            }
        }

        private bool firstRecord = true;
        private DateTime dateCreateFile;
        private long nextRecordTimeTicks;

        private int pumpNumber = 4;
        private int cylinderNumber = 8;

        private void CreateNewFiles()
        {
            pumpNumber = (int)_candatapool.PumpCount;
            cylinderNumber = (int)_candatapool.CylinderCount;

            DateTime t = DateTime.Now;
            firstRecord = false;
            dateCreateFile = t;

            fsPressureList = new List<FileStream>();
            bwPressureList = new List<BinaryWriter>();
            fsPositionList = new List<FileStream>();
            bwPositionList = new List<BinaryWriter>();

            for (int i = 0; i < pumpNumber; i++)
            {
                for (int j = 0; j < cylinderNumber; j++)
                {
                    Array.Clear(headData, 0, FILE_HEAD_SIZE);
                    Array.Copy(BitConverter.GetBytes(t.Ticks), 0, headData, 0, 8);
                    Array.Copy(BitConverter.GetBytes(i), 0, headData, 8, 4);
                    Array.Copy(BitConverter.GetBytes(j), 0, headData, 12, 4);
                    Array.Copy(BitConverter.GetBytes(timerInterval), 0, headData, 16, 4);
                    Array.Copy(BitConverter.GetBytes(MultiplyingFactor), 0, headData, 20, 4);

                    #region 压力记录
                    pressureRecFile = string.Format(PressureRecFileName, i, j, t.ToString("yyyy-MM-dd HH-mm-ss"));
                    recPressureDirectory = Path.GetDirectoryName(pressureRecFile);
                    
                    if (!Directory.Exists(recPressureDirectory))
                        Directory.CreateDirectory(recPressureDirectory);
                    
                    if (File.Exists(pressureRecFile))
                        File.Delete(pressureRecFile);
                    FileStream fspressure = new FileStream(pressureRecFile, FileMode.OpenOrCreate);
                    BinaryWriter bwpressure = new BinaryWriter(fspressure);
                    try
                    {
                        bwpressure.Write(headData, 0, FILE_HEAD_SIZE);
                    }
                    finally
                    {
                        bwpressure.Flush();
                        bwpressure.Close();
                        fspressure.Close();
                    }
                    FileStream fs_pressure = new FileStream(pressureRecFile, FileMode.Append);
                    fsPressureList.Add(fs_pressure);
                    BinaryWriter bw_pressure = new BinaryWriter(fs_pressure);
                    bwPressureList.Add(bw_pressure);
                    #endregion

                    #region 位置记录
                    positionRecFile = string.Format(PositionRecFileName, i, j, t.ToString("yyyy-MM-dd HH-mm-ss"));
                    recPositionDirectory = Path.GetDirectoryName(positionRecFile);
                    if (!Directory.Exists(recPositionDirectory))
                        Directory.CreateDirectory(recPositionDirectory);
                    
                    if (File.Exists(positionRecFile))
                        File.Delete(positionRecFile);
                    FileStream fsposition = new FileStream(positionRecFile, FileMode.OpenOrCreate);
                    BinaryWriter bwposition = new BinaryWriter(fsposition);
                    try
                    {
                        bwposition.Write(headData, 0, FILE_HEAD_SIZE);
                    }
                    finally
                    {
                        bwposition.Flush();
                        bwposition.Close();
                        fsposition.Close();
                    }
                    FileStream fs_position = new FileStream(positionRecFile, FileMode.Append);
                    fsPositionList.Add(fs_position);
                    BinaryWriter bw_position = new BinaryWriter(fs_position);
                    bwPositionList.Add(bw_position);
                    #endregion
                }//end of [for (int j = 0; j < cylinderNumber; j++)]
            }//end of [for (int i = 0; i < pumpNumber; i++)]

            nextRecordTimeTicks = (dateCreateFile.Ticks / 10000000) * 10000000;
        }

        private void RecordData()
        {
            for (int i = 0; i < pumpNumber; i++)
            {
                for (int j = 0; j < cylinderNumber; j++)
                {
                    #region 压力记录
                    try
                    {
                        bwPressureList[i * pumpNumber + j].Write((int)(_candatapool.GetRealValue(i, j, CmdDataType.cdtPressure_Real_3001_3008) * MultiplyingFactor));
                    }
                    finally
                    {
                        bwPressureList[i * pumpNumber + j].Flush();
                    }
                    #endregion

                    #region 位置记录
                    try
                    {
                        bwPositionList[i * pumpNumber + j].Write((int)(_candatapool.GetRealValue(i, j, CmdDataType.cdtPosition_Real_3101_3108) * MultiplyingFactor));
                    }
                    finally
                    {
                        bwPositionList[i * pumpNumber + j].Flush();
                    }
                    #endregion
                }//end of [for (int j = 0; j < cylinderNumber; j++)]
            }//end of [for (int i = 0; i < pumpNumber; i++)]
        }

        private void CloseFiles()
        {
            for (int i = 0; i < pumpNumber; i++)
            {
                for (int j = 0; j < cylinderNumber; j++)
                {
                    #region 压力记录
                    try
                    {
                        bwPressureList[i * pumpNumber + j].Close();
                        fsPressureList[i * pumpNumber + j].Close();
                    }
                    finally
                    {   }
                    #endregion

                    #region 位置记录
                    try
                    {
                        bwPositionList[i * pumpNumber + j].Close();
                        fsPositionList[i * pumpNumber + j].Close();
                    }
                    finally
                    {   }
                    #endregion
                }//end of [for (int j = 0; j < cylinderNumber; j++)]
            }//end of [for (int i = 0; i < pumpNumber; i++)]
        }

        private void FindFiles(string dirPath, List<string> fileList)
        {
            DirectoryInfo Dir = new DirectoryInfo(dirPath);
            try
            {
                foreach (DirectoryInfo d in Dir.GetDirectories())//查找子目录
                {
                    FindFiles(Dir + d.ToString() + "\\", fileList);
                }
                if (fileList != null)
                {
                    foreach (FileInfo f in Dir.GetFiles()) //查找文件
                    {
                        fileList.Add(Dir + f.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
