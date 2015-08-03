/*AdvaMACSystem 监控软件
 * 作者： 程慎
 *  
 * 修改记录：
 *       时间                内容                人员
 * 2015-2-15             创建                by cs 
 * 
 * copyright
 */

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

            timeSetting = new FormTimeSetting();

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

            ibBack = new ImageButton();
            ibBack.Size = new Size(ButtonWidth, ButtonHeight);
            ibBack.Location = new Point(800, 600);
            ibBack.IMGContainer = buttonImage;
            ibBack.Font = currentFont;
            ibBack.Text = "返回";
            ibBack.Click += new EventHandler(ibBack_Click);
            this.Controls.Add(ibBack);

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
                cylinder.Text = (i + 1).ToString() + "#油缸";
                cylinder.Toggle = true;
                cylinder.Click += new EventHandler(cylinder_Click);
                this.Controls.Add(cylinder);
            }

            dateImage = new SimpleImagesContaner();
            dateImage.BackImg = dateImage.CheckedBackImg
                = dateImage.ImgDisable = AdvaMACSystemRes.Dgn_disable;

            inputImage = new SimpleImagesContaner();
            inputImage.BackImg = inputImage.CheckedBackImg
                = inputImage.ImgDisable = AdvaMACSystemRes.Input40x40;

            imageLabel_title = new ComCtrls.ImageLabel();
            pBox = new PictureBox();
            pbDataTable = new PictureBox();
            lbPumpName = new Label();
            ilDate = new ImageLabel();
            ibPrevDay = new ImageButton();
            ibNextDay = new ImageButton();
            ilPumpIndex = new ImageLabel();
            ilCurrentPage = new ImageLabel();

            lbPage = new Label();
            prevPage = new ImageButton();
            nextPage = new ImageButton();

            this.SuspendLayout();

            prevPage.Size = new Size(40, 40);
            prevPage.Location = new Point(BmpLeft, 562);
            prevPage.Font = currentFont;
            prevPage.UPImg = prevPage.UPImgDisable = AdvaMACSystemRes.leftup;
            prevPage.DNImg = prevPage.DNImgDisable = AdvaMACSystemRes.leftdown;
            prevPage.Click += new EventHandler(prevPage_Click);
            this.Controls.Add(prevPage);

            nextPage.Size = new Size(40, 40);
            nextPage.Location = new Point(BmpLeft + 350, 562);
            nextPage.Font = currentFont;
            nextPage.UPImg = nextPage.UPImgDisable = AdvaMACSystemRes.rightup;
            nextPage.DNImg = nextPage.DNImgDisable = AdvaMACSystemRes.rightdown;
            nextPage.Click += new EventHandler(nextPage_Click);
            this.Controls.Add(nextPage);


            ibPrevDay.Size = new Size(100, 40);
            ibPrevDay.Location = new Point(CylinderMarginLeft, CylinderMarginTop + 9 * (CylinderHeight + CylinderSpacingY));
            ibPrevDay.Font = currentFont;
            ibPrevDay.Text = "前一天";
            ibPrevDay.UPImg = ibPrevDay.UPImgDisable = AdvaMACSystemRes.half_disable;
            ibPrevDay.DNImg = ibPrevDay.DNImgDisable = AdvaMACSystemRes.half_disable;
            ibPrevDay.Click += new EventHandler(ibPrevDay_Click);
            this.Controls.Add(ibPrevDay);

            ibNextDay.Size = new Size(100, 40);
            ibNextDay.Location = new Point(CylinderMarginLeft + 110, CylinderMarginTop + 9 * (CylinderHeight + CylinderSpacingY));
            ibNextDay.Font = currentFont;
            ibNextDay.Text = "后一天";
            ibNextDay.UPImg = ibNextDay.UPImgDisable = AdvaMACSystemRes.half_disable;
            ibNextDay.DNImg = ibNextDay.DNImgDisable = AdvaMACSystemRes.half_disable;
            ibNextDay.Click += new EventHandler(ibNextDay_Click);
            this.Controls.Add(ibNextDay);

            this.imageLabel_title.BackColor = System.Drawing.Color.Silver;
            this.imageLabel_title.Checked = false;
            this.imageLabel_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.imageLabel_title.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.imageLabel_title.Location = new System.Drawing.Point(0, 0);
            this.imageLabel_title.Name = "imageLabel_title";
            this.imageLabel_title.Size = new System.Drawing.Size(1024, 57);
            this.imageLabel_title.TabIndex = 0;
            this.imageLabel_title.Text = "历史记录";
            this.imageLabel_title.TransParent = false;
            this.Controls.Add(this.imageLabel_title);

            pBox.Size = new Size(BmpWidth, BmpHeight);
            pBox.Location = new Point(BmpLeft, BmpTop);
            pBox.Visible = true;
            this.Controls.Add(pBox);

            pbDataTable.Size = new Size(CellWidth * Columns + 1, CellHeight * Lines + 1);
            pbDataTable.Location = new Point(BmpLeft, TableMarginTop);
            pbDataTable.Visible = true;
            this.Controls.Add(pbDataTable);

            lbPumpName.Size = new Size(100, 40);
            lbPumpName.Location = new Point(CylinderMarginLeft + 100, CylinderMarginTop - CylinderHeight - CylinderSpacingY + 10);
            lbPumpName.Font = currentFont;
            lbPumpName.Text = "号泵站";
            this.Controls.Add(lbPumpName);

            ilDate.Size = new Size(CylinderWidth, CylinderHeight);
            ilDate.Location = new Point(CylinderMarginLeft, CylinderMarginTop + 8 * (CylinderHeight + CylinderSpacingY));
            ilDate.Font = currentFont;
            ilDate.IMGContainer = dateImage;
            ilDate.Click += new EventHandler(ilDate_Click);
            this.Controls.Add(ilDate);

            ilCurrentPage.Size = new Size(80, 40);
            ilCurrentPage.Location = new Point(BmpLeft + 70, 562);
            ilCurrentPage.Font = currentFont;
            ilCurrentPage.BackImg = AdvaMACSystemRes.Input80x40;
            ilCurrentPage.Click += new EventHandler(ilCurrentPage_Click);
            this.Controls.Add(ilCurrentPage);

            ilPumpIndex.Size = new Size(40, 40);
            ilPumpIndex.Font = currentFont;
            ilPumpIndex.IMGContainer = inputImage;
            ilPumpIndex.Location = new Point(CylinderMarginLeft + 50, CylinderMarginTop - CylinderHeight - CylinderSpacingY);
            ilPumpIndex.Click += new EventHandler(ilPumpIndex_Click);
            this.Controls.Add(ilPumpIndex);

            lbPage.Size = new Size(300, 40);
            lbPage.Location = new Point(BmpLeft + 45, 572);
            lbPage.Font = currentFont;
            this.Controls.Add(lbPage);

            this.ResumeLayout(false);
        }

        private void ibNextDay_Click(object sender, EventArgs e)
        {
            if (startTime.Add(new TimeSpan(1, 0, 0, 0)).Date <= DateTime.Now.Date)
            {
                startTime = startTime.Add(new TimeSpan(1, 0, 0, 0));
                ilDate.Text = startTime.ToString("yyyy-MM-dd HH:mm");
                ts = new TimeSpan(24, 0, 0);
                if (ReadyToDrawChart() == 0)
                {
                    DrawChart();
                    DrawTable();
                }
            }
        }

        private void ibPrevDay_Click(object sender, EventArgs e)
        {
            if (startTime.Subtract(new TimeSpan(1, 0, 0, 0)).Date >= DateTime.Now.Date.Subtract(new TimeSpan(reserveDays, 0, 0, 0)).Date)
            {
                startTime = startTime.Subtract(new TimeSpan(1, 0, 0, 0));
                ilDate.Text = startTime.ToString("yyyy-MM-dd HH:mm");
                ts = new TimeSpan(24, 0, 0);
                if (ReadyToDrawChart() == 0)
                {
                    DrawChart();
                    DrawTable();
                }
            }
        }

        private void prevPage_Click(object sender, EventArgs e)
        {
            if (pageIndex - 1 >= 0)
                pageIndex = pageIndex - 1;
            int index = totalPages > 0 ? pageIndex + 1 : 0;
            ilCurrentPage.Text = index.ToString();
            DrawDefaultTable();
            DrawTableDatas(pageIndex);
            pbDataTable.Image = table;
        }

        private void nextPage_Click(object sender, EventArgs e)
        {
            if (pageIndex + 1 < totalPages)
                pageIndex = pageIndex + 1;
            int index = totalPages > 0 ? pageIndex + 1 : 0;
            ilCurrentPage.Text = index.ToString();
            DrawDefaultTable();
            DrawTableDatas(pageIndex);
            pbDataTable.Image = table;
        }

        private void ilCurrentPage_Click(object sender, EventArgs e)
        {
            KeypadForm f = KeypadForm.GetKeypadForm(ilCurrentPage.Text);
            if (f.ShowDialog() == DialogResult.OK)
            {
                int index = 1;
                try
                {
                    index = Convert.ToInt32(f.KeyText);
                    if (index <= 0 || index > totalPages)
                    {
                        index = totalPages > 0 ? 1 : 0;
                        pageIndex = 0;
                    }
                    else
                    {
                        pageIndex = index - 1;
                    }
                }
                catch
                {
                    index = totalPages > 0 ? 1 : 0;
                    pageIndex = 0;
                }
                ilCurrentPage.Text = index.ToString();
            }
            DrawDefaultTable();
            DrawTableDatas(pageIndex);
            pbDataTable.Image = table;
        }

        private void ibBack_Click(object sender, EventArgs e)
        {
            base.DoExit();
        }

        private void ilPumpIndex_Click(object sender, EventArgs e)
        {
            KeypadForm f = KeypadForm.GetKeypadForm(ilPumpIndex.Text);
            if (f.ShowDialog() == DialogResult.OK)
            {
                int index = 1;
                try
                {
                    index = Convert.ToInt32(f.KeyText);
                    if (index <= 0 || index > pumpNumber)
                        index = 1;
                }
                catch
                {
                    index = 1;
                }
                pumpIndex = index - 1;
                ilPumpIndex.Text = index.ToString();
                cylinder_Click(cylinderList[0], null);
            }
        }

        private void cylinder_Click(object sender, EventArgs e)
        {
            ImageButton ibSender = (ImageButton)sender;
            foreach (ImageButton cylinder in cylinderList)
            {
                cylinder.Checked = cylinder == ibSender;
            }
            cylinderIndex = (int)ibSender.Tag;

            if (ReadyToDrawChart() == 0)
            {
                DrawChart();
                DrawTable();
            }

        }

        //界面
        private ComCtrls.ImageLabel imageLabel_title;//标题

        private ImagesContaner buttonImage = null;//按钮背景图
        private List<ImageButton> cylinderList = null;
        private PictureBox pBox = null;

        private Label lbPumpName;
        private SimpleImagesContaner inputImage = null;
        private SimpleImagesContaner dateImage = null;
        private ImageLabel ilPumpIndex;//泵站选择
        private ImageLabel ilDate;//起始日期
        private ImageButton ibPrevDay;//前一天
        private ImageButton ibNextDay;//后一天

        private ImageButton ibBack;

        private FormTimeSetting timeSetting = null;

        private Label lbPage;
        private ImageLabel ilCurrentPage;//当前页编号
        private PictureBox pbDataTable;//数据表格
        private ImageButton prevPage;//上一页
        private ImageButton nextPage;//下一页


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

        private int Lines = 3;
        private int Columns = 11;
        private int CellWidth = 70;
        private int CellHeight = 40;
        private int TableMarginTop = 440;
        private Image table = null;
        private Graphics gt = null;

        //数据
        private int reserveDays = 60;
        private DateTime startTime = DateTime.Now.Date;
        private DateTime endTime;
        private TimeSpan ts = new TimeSpan(24, 0, 0);

        private bool drawPressure = true;
        private int pumpNumber = 4;
        private int pumpIndex = 0;
        private int cylinderIndex = 0;

        private double minValue;
        private double maxValue;
        private double settingValue;
        private string XUnit = string.Empty;
        private string YUnit = "KN";
        private int MinValueInImg;
        private int ValueRangeInImg;
        private List<string> filesToDraw;

        private List<long> startTimeInFileList;
        private List<int> intervalTimeInFileList;
        private List<List<int>> dataList;
        private int multiplyingFactor = 1;
        private double Scale = 10.17;//轴力比例 1.8*1.8*3.14, 位移比例 1

#if WindowsCE
        private string tempFolder = @"\HardDisk\History\Temp\";
#else
        private string tempFolder = Application.StartupPath + @"\History\Temp\";
#endif

        #region 布局
        private Color textColor = Color.Black;//字体颜色
        private Font currentFont = null;//Cylinder标签字体
        private int CylinderMarginTop = 150;//第一行Cylinder标签与顶端方向间距
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
            ilPumpIndex.Text = (pumpIndex + 1).ToString();
            pumpNumber = (int)_candatapool.PumpCount;
            ilDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm");
            cylinderList[cylinderIndex].Checked = true;
            pageIndex = 0;
            ilCurrentPage.Text = "0";
            totalPages = 0;
            lbPage.Text = string.Format("第                    页，共{0}页", totalPages);
            DrawDefaultChart();
            DrawDefaultTable();
            ibBack.Checked = false;
            base.DoEnter();
        }

        private int ReadyToDrawChart()
        {
            ////debug by zxj
            //maxValue = 100;
            //minValue = 0;
            //if (maxValue - minValue <= 0)
            //{
            //    MessageBox.Show("数据范围不正确！");
            //    return 1;
            //}
            try
            {
                //结束时间
                endTime = startTime.Add(ts);
            }
            catch
            {
                startTime = DateTime.Now.Date;
                ts = new TimeSpan(24, 0, 0);
                endTime = startTime.Add(ts);
                //MessageBox.Show("日期格式不正确！");
                MessageBox.Show("日期格式不正确！", "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Hand,
                    MessageBoxDefaultButton.Button1);
                return 2;
            }
            if (cylinderIndex < 0)
            {
                //MessageBox.Show("请选择油缸！");
                MessageBox.Show("请选择油缸！", "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);

                return 3;
            }

            //load files
            string tempFile;
            if (drawPressure)
            {
                Scale = 10.17;
                maxValue = 4000;
                minValue = 0;

                tempFile = string.Format(historyOper.PressureRecFileName, pumpIndex, cylinderIndex, startTime.ToString("yyyy-MM-dd HH-mm-ss"));
                settingValue = _candatapool.GetRealValue(pumpIndex, cylinderIndex, CmdDataType.cdtPressure_Value);
            }
            else
            {
                Scale = 1;
                maxValue = 1000;
                minValue = 0;

                tempFile = string.Format(historyOper.PositionRecFileName, pumpIndex, cylinderIndex, startTime.ToString("yyyy-MM-dd HH-mm-ss"));
                settingValue = _candatapool.GetRealValue(pumpIndex, cylinderIndex, CmdDataType.cdtPosition_Value);
            }
            string recDirectory = Path.GetDirectoryName(tempFile);
            if (!Directory.Exists(recDirectory))
            {
                Directory.CreateDirectory(recDirectory);
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
            Brush yellowBrush = new SolidBrush(Color.Yellow);
            Pen settingValuePen = new Pen(Color.FromArgb(0, 255, 255));
            Brush blueBrush = new SolidBrush(Color.FromArgb(0, 255, 255));
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
                g.DrawString(ymin.ToString(numberFormat), chartFont, whiteBrush, 40, 270 - i * 24, sf);
                ymin += (maxValue - minValue) / 10;
            }
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            if (drawPressure)
            {
                YUnit = "KN";
            }
            else
            {
                YUnit = "mm";
            }
            g.DrawString(YUnit, chartFont, whiteBrush, 50, 3);


            DateTime xdate = startTime;
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            for (int i = 0; i <= 12; i++)
            {
                g.DrawLine(backPen, i * 60 + 40, 270, i * 60 + 40, 265);
                g.DrawString(xdate.ToString("HH:mm"), chartFont, whiteBrush, i * 60 + 40, 285, sf);
                xdate = xdate.Add(new TimeSpan(ts.Ticks / 12));
            }

            //draw setting line
            int ysetting = (int)(270 - (settingValue - minValue) / (maxValue - minValue) * 240);
            g.DrawLine(settingValuePen, 40, ysetting, 40 + 720, ysetting);

            //draw legend
            g.DrawRectangle(backPen, new Rectangle(320, 300, 150, 20));
            g.FillRectangle(blueBrush, new Rectangle(325, 305, 10, 10));
            g.FillRectangle(yellowBrush, new Rectangle(395, 305, 10, 10));
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString("设定值", chartFont, blueBrush, 340, 301 + g.MeasureString("设定值", chartFont).Height / 2, sf);
            g.DrawString("实际值", chartFont, yellowBrush, 410, 301 + g.MeasureString("设定值", chartFont).Height / 2, sf);
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

                    long firstValidDataIndex = 0;
                    //调整记录起始点
                    if (startTimeInRec < startTime.Ticks)
                    {
                        //需要调整记录起始点
                        long dataCountInRec = (fs.Length - historyOper.CONST_FILE_HEAD_SIZE) / sizeof(int);

                        if (dataCountInRec <= 0)
                            continue;

                        long endTimeInRec = startTimeInRec + (dataCountInRec - 1) * 10000 * interval;
                        if (endTimeInRec < startTime.Ticks)
                            continue;
                        else//记录中包含查询数据
                        {
                            firstValidDataIndex = (long)Math.Ceiling((startTime.Ticks - startTimeInRec) / 10000.0 / interval);
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
                    File.Delete(tempFolder + Path.GetFileName(filesToDraw[key]));
                }
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

        private void DrawDefaultTable()
        {
            int tableWidth = CellWidth * Columns;
            int tableHeight = CellHeight * Lines;
            table = new Bitmap(tableWidth + 1, tableHeight + 1);
            gt = Graphics.FromImage(table);
            Brush backgroundBrush = new SolidBrush(this.BackColor);
            Brush blackBrush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(Color.Black);
            gt.FillRectangle(backgroundBrush, new Rectangle(0, 0, CellWidth * Columns, CellHeight * Lines));
            for (int i = 0; i <= Lines; i++)
            {
                gt.DrawLine(blackPen, 0, i * CellHeight, tableWidth, i * CellHeight);
            }
            for (int i = 0; i <= Columns; i++)
            {
                gt.DrawLine(blackPen, i * CellWidth, 0, i * CellWidth, tableHeight);
            }
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            gt.DrawString("序号", currentFont, blackBrush, CellWidth / 2, CellHeight / 2, sf);
            gt.DrawString("时间", currentFont, blackBrush, CellWidth / 2, 3 * CellHeight / 2, sf);
            gt.DrawString("记录", currentFont, blackBrush, CellWidth / 2, 5 * CellHeight / 2, sf);

            pbDataTable.Image = table;

        }

        private void DrawTable()
        {
            DrawDefaultTable();

            totalDatas = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                totalDatas += dataList[i].Count;
            }
            totalPages = totalDatas / dataPerPage + (totalDatas % dataPerPage != 0 ? 1 : 0);
            lbPage.Text = string.Format("第                    页，共{0}页", totalPages);
            pageIndex = 0;
            ilCurrentPage.Text = totalPages > 0 ? "1" : "0";
            DrawTableDatas(pageIndex);
            pbDataTable.Image = table;
        }

        private List<DataPair> GetData(int startID)
        {
            List<DataPair> result = new List<DataPair>();
            int sum = 0;
            int i = 0;
            if (dataList == null)
                return result;

            for (; i < dataList.Count; i++)
            {
                if (sum + dataList[i].Count > startID)
                    break;

                sum = sum + dataList[i].Count;
            }
            int count = 0;
            for (int k = startID - sum; i < dataList.Count && k < dataList[i].Count; k++)
            {
                DataPair dp;
                dp.id = startID;
                startID++;
                dp.data = dataList[i][k];
                dp.recTime = startTimeInFileList[i] + (long)k * intervalTimeInFileList[i] * 10000;
                result.Add(dp);
                count++;
                if (count >= dataPerPage)
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
                    if (count >= dataPerPage)
                        return result;
                }
            }

            return result;
        }

        private void ilDate_Click(object sender, EventArgs e)
        {
            timeSetting.ReserveDays = this.reserveDays;
            timeSetting.Initialize();
            timeSetting.SetStartTime(startTime, ts);
            if (timeSetting.ShowDialog() == DialogResult.OK)
            {
                startTime = timeSetting.GetStartTime();
                ts = timeSetting.GetTimeSpan();
                drawPressure = timeSetting.DataType == 0;
                ilDate.Text = startTime.ToString("yyyy-MM-dd HH:mm");

                if (ReadyToDrawChart() == 0)
                {
                    DrawChart();
                    DrawTable();
                }
            }
        }

        private void DrawTableDatas(int pageIndex)
        {
            gt = Graphics.FromImage(table);
            Brush backgroundBrush = new SolidBrush(this.BackColor);
            Brush blackBrush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(Color.Black);

            List<DataPair> lvl = GetData(pageIndex * dataPerPage);
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            for (int i = 0; i < lvl.Count; i++)
            {
                gt.DrawString((lvl[i].id + 1).ToString(), currentFont, blackBrush, 3 * CellWidth / 2 + i * CellWidth, CellHeight / 2, sf);
                gt.DrawString((new DateTime(lvl[i].recTime)).ToString("HH:mm"), currentFont, blackBrush, 3 * CellWidth / 2 + i * CellWidth, 3 * CellHeight / 2, sf);
                gt.DrawString(((double)lvl[i].data / multiplyingFactor * Scale).ToString(numberFormat), currentFont, blackBrush, 3 * CellWidth / 2 + i * CellWidth, 5 * CellHeight / 2, sf);
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
