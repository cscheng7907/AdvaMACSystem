using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataPool;
using ComCtrls;
using System.IO;

namespace AdvaMACSystem
{
    public partial class PageViewHistory : UIControlbase
    {
        public PageViewHistory()
        {
            InitializeComponent();
            this.ForeColor = Color.Black;
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            chartFont = new Font("微软雅黑", 9F, FontStyle.Regular);
            currentFont = new Font("微软雅黑", 14F, FontStyle.Regular);
            buttonImage = new ImagesContaner();
            buttonImage.DNImg = AdvaMACSystemRes.Dgn_down;
            buttonImage.UPImg = AdvaMACSystemRes.Dgn_up;
            buttonImage.UPImgDisaable = buttonImage.DNImgDisable = AdvaMACSystemRes.Dgn_disable;

            cylinderList = new List<ImageButton>();
            for (int i = 0; i < 8; i++)
            {
                ImageButton cylinder = new ImageButton();
                cylinderList.Add(cylinder);
                cylinder.Size = new Size(CylinderWidth, CylinderHeight);
                cylinder.Location = new Point(CylinderMarginLeft, CylinderMarginTop + i * (CylinderHeight + CylinderSpacingY));
                cylinder.IMGContainer = buttonImage;
                cylinder.Font = currentFont;
                cylinder.ForeColor = textColor;
                cylinder.Tag = i;
                cylinder.Text = (i +1).ToString() + "#油缸";
                cylinder.Toggle = true;
                cylinder.Click += new EventHandler(cylinder_Click);
                this.Controls.Add(cylinder);
            }

            pBox = new PictureBox();
            pBox.Size = new Size(BmpWidth, BmpHeight);
            pBox.Location = new Point(BmpLeft, BmpTop);
            pBox.Visible = true;
            this.Controls.Add(pBox);
        }

        private void cylinder_Click(object sender, EventArgs e)
        {
            ImageButton cylinder = (ImageButton)sender;
            if (cylinderIndex >= 0 && cylinderIndex < cylinderList.Count)
                cylinderList[cylinderIndex].Checked = false;
            if (cylinderList[(int)cylinder.Tag].Checked)
                cylinderIndex = (int)cylinder.Tag;
            else
                cylinderIndex = -1;
        }

        //界面
        private ImagesContaner buttonImage = null;//按钮背景图
        private List<ImageButton> cylinderList = null;
        private PictureBox pBox = null;
       
        //绘图
        private Image chart = null;
        private Graphics g = null;
        private Font chartFont = null;
        private StringFormat sf = null;
        private const string numberFormat = "F1";

        //表格
        private int dataPerPage = 10;
        private int pageIndex = 0;
        private int totalDatas = 0;
        private int totalPages = 0;


        //数据
        private int reserveDays = 60;
        private DateTime startTime;
        private DateTime endTime;
        private TimeSpan ts;

        private int pumpIndex;
        private int cylinderIndex = -1;

        private double minValue;
        private double maxValue;
        private double settingValue;
        private string XUnit = string.Empty;
        private string YUnit = "kN";
        private int MinValueInImg;
        private int ValueRangeInImg;
        private List<string> filesToDraw;

        private List<long> startTimeInFileList;
        private List<int> intervalTimeInFileList;
        private List<List<int>> dataList;
        private int multiplyingFactor = 1;

#if WindowsCE
        private string tempFolder = @"\HardDisk\History\Temp\";
#else
        private string tempFolder = Application.StartupPath + @"\History\Temp\";
#endif

        #region 布局
        private Color textColor = Color.Black;//字体颜色
        private Font currentFont = null;//Cylinder标签字体
        private int CylinderMarginTop = 80;//第一行Cylinder标签与顶端方向间距
        private int CylinderMarginLeft = 10;//第一列Cylinder标签与左端方向间距
        private int CylinderWidth = 210;//Cylinder标签宽度
        private int CylinderHeight = 40;//Cylinder标签高度
        private int CylinderSpacingX = 40;//Cylinder标签之间X方向间距
        private int CylinderSpacingY = 5;//Cylinder标签之间Y方向间距

        private int TextMarginLeft = 15;
        private int TextMarginTop = 5;

        private int ButtonWidth = 210;
        private int ButtonHeight = 40;
        private int ButtonMarginTop = 600;
        private int ButtonSpacingX = 40;

        private const int BmpWidth = 790;
        private const int BmpHeight = 330;
        private int BmpTop = 100;
        private int BmpLeft = 230;
        #endregion

        #region 属性
        private CDataPool _candatapool = null;
        public CDataPool CanDatapool
        {
            get { return _candatapool; }
            set
            {
                if (_candatapool != value)
                    _candatapool = value;
            }
        }

        private HistoryOperator historyOper = null;
        public HistoryOperator HistoryOper
        {
            set
            {
                if (historyOper != value)
                    historyOper = value;
            }
        }
        #endregion

        public override void DoEnter()
        {
            DateTime dt = DateTime.Now.Date;
            this.monthCalendar1.MaxDate = new DateTime(dt.Ticks);
            TimeSpan cts = new TimeSpan(reserveDays, 0, 0, 0);//可查询历史记录的日期跨度
            this.monthCalendar1.MinDate = new DateTime(dt.Ticks - cts.Ticks);
            button2.Text = this.monthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
            textBox1.Text = "0:00";
            textBox2.Text = "24";
            DrawDefaultChart();
            base.DoEnter();
        }

        private int ReadyToDrawChart()
        {
            //debug by zxj
            maxValue = 100;
            minValue = 0;
            if (maxValue - minValue <= 0)
            {
                MessageBox.Show("数据范围不正确！");
                return 1;
            }
            try
            {
                //开始时间
                startTime = Convert.ToDateTime(button2.Text + " " + textBox1.Text);
                //开始时间
                ts = new TimeSpan((long)(36000000000 * Convert.ToDouble(textBox2.Text)));
                //ts = new TimeSpan(Convert.ToInt32(textBox2.Text), 0, 0);
                //结束时间
                endTime = startTime.Add(ts);
            }
            catch
            {
                startTime = DateTime.Now.Date;
                ts = new TimeSpan(24, 0, 0);
                endTime = startTime.Add(ts);
                MessageBox.Show("日期格式不正确！");
                return 2;
            }
            if (cylinderIndex < 0)
            {
                MessageBox.Show("请选择油缸！");
                return 3;
            }

            //load files
            string tempFile;
            bool drawPressure = !checkBox1.Checked;
            if (drawPressure)
            {
                tempFile = string.Format(historyOper.PressureRecFileName, pumpIndex, cylinderIndex, startTime.ToString("yyyy-MM-dd HH-mm-ss"));
                settingValue = _candatapool.GetRealValue(pumpIndex, cylinderIndex, CmdDataType.cdtPressure_Value);
            }
            else
            {
                tempFile = string.Format(historyOper.PositionRecFileName, pumpIndex, cylinderIndex, startTime.ToString("yyyy-MM-dd HH-mm-ss"));
                settingValue = _candatapool.GetRealValue(pumpIndex, cylinderIndex, CmdDataType.cdtPosition_Value);
            }
            string recDirectory = Path.GetDirectoryName(tempFile);
            if (!Directory.Exists(recDirectory))
            {
                MessageBox.Show("无相应记录！");
                return 4;
            }
            filesToDraw = new List<string>();
            string[] filesArray = Directory.GetFiles(recDirectory);
            foreach (string s in filesArray)
            {
                string nameToParse = Path.GetFileNameWithoutExtension(s);
                try
                {
                    if (nameToParse.IndexOf(' ') >= 0)
                        nameToParse = nameToParse.Substring(0, nameToParse.IndexOf(' '));
                    else
                        continue;

                    DateTime recTime = Convert.ToDateTime(nameToParse);
                    if (recTime.Ticks >= startTime.Date.Ticks && recTime.Ticks < endTime.Ticks)
                    {
                        filesToDraw.Add(s);
                    }
                }
                catch
                { }
            }
            if (!Directory.Exists(tempFolder))
                Directory.CreateDirectory(tempFolder);

            return 0;
        }

        private void DrawDefaultChart()
        {
            chart = new Bitmap(BmpWidth, BmpHeight);
            g = Graphics.FromImage(chart);
            Brush backBrush = new SolidBrush(Color.Black);
            Brush whiteBrush = new SolidBrush(Color.White);
            g.FillRectangle(backBrush, new Rectangle(0, 0, BmpWidth, BmpHeight));
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString("请在左侧设置参数", chartFont, whiteBrush, BmpWidth / 2, BmpHeight / 2, sf);

            //show chart
            pBox.Image = chart;
        }

        private void DrawChart()
        {
            chart = new Bitmap(BmpWidth, BmpHeight);
            g = Graphics.FromImage(chart);
            Brush backBrush = new SolidBrush(Color.Black);
            Brush whiteBrush = new SolidBrush(Color.White);
            Pen backPen = new Pen(Color.White);
            Pen realValuePen = new Pen(Color.Yellow);
            Pen settingValuePen = new Pen(Color.Blue);

            #region 画背景
            //draw background
            g.FillRectangle(backBrush, new Rectangle(0, 0, BmpWidth, BmpHeight));
            //draw coordinate system
            g.DrawLine(backPen, 40, 3, 40, 270);
            g.DrawLine(backPen, 40, 270, 787, 270);
            g.DrawLine(backPen, 40, 3, 37, 13);
            g.DrawLine(backPen, 40, 3, 43, 13);
            g.DrawLine(backPen, 787, 270, 777, 267);
            g.DrawLine(backPen, 787, 270, 777, 273);

            double ymin = minValue;
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Center;
            for (int i = 0; i <= 10; i++)
            {
                g.DrawLine(backPen, 40, 270 - i * 24, 45, 270 - i * 24);
                g.DrawString(ymin.ToString(numberFormat), chartFont, whiteBrush, 35, 270 - i * 24, sf);
                ymin += (maxValue - minValue) / 10;
            }
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            g.DrawString(YUnit, chartFont, whiteBrush, 50, 3);


            DateTime xdate = startTime;
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            
            for (int i = 0; i <= 12; i++)
            {
                g.DrawLine(backPen, i * 60 + 40, 270, i * 60 + 40, 265);
                if (xdate.Date == startTime.Date)
                    g.DrawString(xdate.ToString("HH:mm"), chartFont, whiteBrush, i * 60 + 40, 285, sf);
                xdate = xdate.Add(new TimeSpan(ts.Ticks / 12));
            }

            //draw setting line
            int ysetting = (int)(270 - (settingValue - minValue) / (maxValue - minValue) * 2409);
            g.DrawLine(settingValuePen, 40, ysetting, 40 + 720, ysetting);
            #endregion

            startTimeInFileList = new List<long>();
            intervalTimeInFileList = new List<int>();
            dataList = new List<List<int>>();

            long ticksPerPixel = ts.Ticks / 720;
            for (int key = 0; key < filesToDraw.Count; key++)
            {
                List<int> currentSegment = null;
                long startTimeInRec = 0;
                int interval = 1000;
                currentSegment = new List<int>();

                #region 获取数据
                //try
                //{
                    File.Copy(filesToDraw[key], tempFolder + Path.GetFileName(filesToDraw[key]), true);
                    FileStream fs = new FileStream(tempFolder + Path.GetFileName(filesToDraw[key]), FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    try
                    {
                        br.BaseStream.Seek(0, SeekOrigin.Begin); //将文件指针设置到文件开始
                        startTimeInRec = br.ReadInt64();
                        int pumpInFile = br.ReadInt32();
                        int cylinderInFile = br.ReadInt32();
                        interval = br.ReadInt32();
                        multiplyingFactor = br.ReadInt32();

                        //验证数据
                        if (pumpInFile != pumpIndex || cylinderInFile != cylinderIndex || multiplyingFactor <= 0)
                            continue;

                        int firstValidDataIndex = 0;
                        //调整记录起始点
                        if (startTimeInRec < startTime.Ticks)
                        {
                            //需要调整记录起始点
                            int dataCountInRec = (int)((fs.Length - historyOper.CONST_FILE_HEAD_SIZE) / sizeof(int));
                            
                            if (dataCountInRec <= 0)
                                continue;

                            long endTimeInRec = startTimeInRec + (dataCountInRec - 1) * 10000 * interval;
                            if (endTimeInRec < startTime.Ticks)
                                continue;
                            else//记录中包含查询数据
                            {
                                firstValidDataIndex = (int)Math.Ceiling((startTime.Ticks - startTimeInRec) / 10000.0 / interval);
                                startTimeInRec = startTimeInRec + firstValidDataIndex * 10000 * interval;//调整记录起始点
                            }
                        }

                        //计算有效数据总数
                        int maxValidDataCount = (int)Math.Ceiling((endTime.Ticks - startTimeInRec) / 10000.0 / interval);

                        int pos = 0;
                        for (; pos < startTimeInFileList.Count; pos++)
                        {
                            if (startTimeInRec <= startTimeInFileList[pos])
                            {
                                startTimeInFileList.Insert(pos, startTimeInRec);
                                intervalTimeInFileList.Insert(pos, interval);
                                dataList.Insert(pos, currentSegment);
                                break;
                            }
                        }
                        if (pos == startTimeInFileList.Count)
                        {
                            startTimeInFileList.Add(startTimeInRec);
                            intervalTimeInFileList.Add(interval);
                            dataList.Add(currentSegment);
                        }

                        br.BaseStream.Seek(historyOper.CONST_FILE_HEAD_SIZE + sizeof(int) * firstValidDataIndex, SeekOrigin.Begin);

                        //读文件体
                        MinValueInImg = (int)(minValue * multiplyingFactor);
                        ValueRangeInImg = (int)((maxValue - minValue) * multiplyingFactor);
                        int value;
                        int count = 0;
                        for (count = 0; count < maxValidDataCount && br.BaseStream.Position < br.BaseStream.Length; count++) // 当未到达文件结尾时
                        {
                            value = br.ReadInt32();
                            currentSegment.Add(value);
                        }
                    }
                    finally
                    {
                        br.Close();
                        fs.Close();
                    }
                    File.Delete(tempFolder + Path.GetFileName(filesToDraw[key]));
                //}
                //catch
                //{ }
                #endregion

                int pointX = 0, pointY = 0, followX = 0, followY = 0;
                double k = (double)(interval * 10000.0 / ts.Ticks * 720);//系数
                double b = (double)(Convert.ToDouble(startTimeInRec - startTime.Ticks) / ts.Ticks * 720);//截距
                //绘制起点
                if (currentSegment.Count > 0)
                {
                    pointX = (int)(b);
                    pointY = (int)(240 * (currentSegment[0] - MinValueInImg) / ValueRangeInImg);
                    followX = pointX;
                    followY = pointY;

                    if (ticksPerPixel / interval / 10000 <= 1)
                    {
                        for (int i = 1; i < currentSegment.Count; i++)
                        {
                            pointX = (int)(k * i + b);
                            pointY = (int)(240 * (currentSegment[i] - MinValueInImg) / ValueRangeInImg);
                            if (pointX >= 0 && pointX <= 720)
                                g.DrawLine(realValuePen, 40 + followX, 270 - followY, 40 + pointX, 270 - pointY);
                            followX = pointX;
                            followY = pointY;
                        }
                    }//end of [if (ticksPerPixel / interval / 10000 <= 1)]
                    else
                    {
                        int delta = (int)(ticksPerPixel / interval / 10000);
                        for (int i = delta; i < currentSegment.Count; )
                        {
                            if (k * i + b > followX)
                            {
                                pointX = (int)(k * i + b);
                                pointY = (int)(240 * (currentSegment[i] - MinValueInImg) / ValueRangeInImg);
                                if (pointX >= 0 && pointX <= 720)
                                    g.DrawLine(realValuePen, 40 + followX, 270 - followY, 40 + pointX, 270 - pointY);
                                followX = pointX;
                                followY = pointY;
                                i += delta;
                            }
                            else
                            {
                                i += 1;
                            }
                        }
                        pointX = (int)(k * (currentSegment.Count - 1) + b);
                        pointY = (int)(240 * (currentSegment[currentSegment.Count - 1] - MinValueInImg) / ValueRangeInImg);
                        if (pointX >= 0 && pointX <= 720)
                            g.DrawLine(realValuePen, 40 + followX, 270 - followY, 40 + pointX, 270 - pointY);
                    }//end of [!(if (ticksPerPixel / interval / 10000 <= 1))]
                }//end of [if (currentSegment.Count > 0)]
            }//end of [for (int key = 0; key < filesToDraw.Count; key++)]

            //show chart
            pBox.Image = chart;
        }

        private void DrawTable()
        {
            totalDatas = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                totalDatas += dataList[i].Count;
            }
            totalPages = totalDatas / dataPerPage + (totalDatas % dataPerPage != 0 ? 1 : 0);
            label1.Text = totalPages.ToString();

        }

        private List<DataPair> GetData(int startID)
        {
            List<DataPair> result = new List<DataPair>();
            int sum = 0;
            int i = 0;
            for (; i < dataList.Count; i++)
            {
                if (sum + dataList[i].Count > startID)
                    break;

                sum = sum + dataList[i].Count;
            }
            int count = 0;
            for (int k = startID - sum; k < dataList[i].Count; k++)
            {
                DataPair dp;
                dp.id = startID;
                startID++;
                dp.data = dataList[i][k];
                dp.recTime = startTimeInFileList[i] + k * intervalTimeInFileList[i];
                result.Add(dp);
                count++;
                if (count > dataPerPage)
                    return result;
            }
            i++;
            for (; i < dataList.Count; i++)
            {
                for (int j = 0; j < dataList[i].Count; j++)
                {
                    DataPair dp;
                    dp.id = startID;
                    startID++;
                    dp.data = dataList[i][j];
                    dp.recTime = startTimeInFileList[i] + j * intervalTimeInFileList[i];
                    result.Add(dp);
                    count++;
                    if (count > dataPerPage)
                        return result;
                }         
            }

            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.monthCalendar1.Visible = !this.monthCalendar1.Visible;
            button2.Text = this.monthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.monthCalendar1.Visible = false;
            button2.Text = this.monthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ReadyToDrawChart() == 0)
            {
                DrawChart();
                DrawTable();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            List<DataPair> lvl = GetData(Convert.ToInt32(textBox3.Text) * dataPerPage);
            for (int i = 0; i < lvl.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = lvl[i].id.ToString();
                lvi.SubItems.Add((new DateTime(lvl[i].recTime)).ToString("yyyy-MM-dd HH:mm"));
                lvi.SubItems.Add(((double)lvl[i].data / multiplyingFactor).ToString(numberFormat));
                listView1.Items.Add(lvi);
            }
        }

    }

    public struct DataPair
    {
        public int id;
        public int data;
        public long recTime;
    }
}
