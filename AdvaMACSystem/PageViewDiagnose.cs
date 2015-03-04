using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ComCtrls;
using DataPool;

namespace AdvaMACSystem
{
    public partial class PageViewDiagnose : UIControlbase
    {
        public PageViewDiagnose()
        {
            InitializeComponent();

            _candatapool = DataPool.CDataPool.GetDataPoolObject();

            this.ForeColor = Color.Black;

            pumpImgLabelList = new List<CParaLabel>();
            pumpTitleList = new List<ImageLabel>();
            cylinderIOList = new List<ImageLabel>();
            cylinderParaList = new List<CParaLabel>();
            buttonList = new List<ImageButton>();
            pumpNameImage = new SimpleImagesContaner();
            pumpNameImage.BackImg = pumpNameImage.CheckedBackImg
                = pumpNameImage.ImgDisable = AdvaMACSystemRes.IO;
            
            pumpParaImage = new SimpleImagesContaner();
            pumpParaImage.BackImg = pumpParaImage.CheckedBackImg
                = pumpParaImage.ImgDisable = AdvaMACSystemRes.IOlabel;

            buttonImage = new ImagesContaner();
            buttonImage.DNImg = AdvaMACSystemRes.Dgn_down;
            buttonImage.UPImg = AdvaMACSystemRes.Dgn_up;
            buttonImage.UPImgDisaable = buttonImage.DNImgDisable = AdvaMACSystemRes.Dgn_disable;
            cylinderImage = new SimpleImagesContaner();
            cylinderImage.BackImg = AdvaMACSystemRes.IOred;
            cylinderImage.ImgDisable = AdvaMACSystemRes.IOred;
            cylinderImage.CheckedBackImg = AdvaMACSystemRes.IOblack;

            currentFont = new Font("微软雅黑", 14F, FontStyle.Regular);
            //new pump names
            for (int i = 0; i < 4; i++)
            {
                ImageLabel pumpName = new ImageLabel();
                pumpTitleList.Add(pumpName);
            }

            //new pump paras
            for (int i = 0; i < 12; i++)
            {
                CParaLabel pumpPara = new CParaLabel();
                pumpImgLabelList.Add(pumpPara);
            }

            //new cylinders
            for (int i = 0; i < 32; i++)
            {
                ImageLabel cylinder = new ImageLabel();
                cylinderIOList.Add(cylinder);
            }
            for (int i = 0; i < 32; i++)
            {
                CParaLabel cylinderPara = new CParaLabel();
                cylinderParaList.Add(cylinderPara);
            }

            for (int i = 0; i < 4; i++)
            {
                ImageButton button = new ImageButton();
                buttonList.Add(button);
            }
            exitButton = new ImageButton();
            panelIO = new Panel();
            panelPara = new Panel();
            this.imageLabel_title = new ComCtrls.ImageLabel();
            this.SuspendLayout();
            this.panelIO.SuspendLayout();
            this.panelPara.SuspendLayout();

            this.imageLabel_title.BackColor = System.Drawing.Color.Silver;
            this.imageLabel_title.Checked = false;
            this.imageLabel_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.imageLabel_title.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.imageLabel_title.Layout = ComCtrls.KTLayout.GlyphTop;
            this.imageLabel_title.Location = new System.Drawing.Point(0, 0);
            this.imageLabel_title.Name = "imageLabel_title";
            this.imageLabel_title.Size = new System.Drawing.Size(1024, 85);
            this.imageLabel_title.TabIndex = 0;
            this.imageLabel_title.Text = "I/O诊断";
            this.imageLabel_title.TextX = -1F;
            this.imageLabel_title.TextY = -1F;
            this.imageLabel_title.TransParent = false;
            this.Controls.Add(this.imageLabel_title);

            panelIO.Size = new Size(1024, 8 * (IOHeight + IOSpacingY));
            panelIO.Location = new Point(IOMarginLeft, 4 * (IOHeight + IOSpacingY) + IOMarginTop);
            panelIO.Visible = false;
            this.Controls.Add(panelIO);

            panelPara.Size = new Size(1024, 8 * (IOHeight + IOSpacingY));
            panelPara.Location = new Point(IOMarginLeft, 4 * (IOHeight + IOSpacingY) + IOMarginTop);
            panelPara.Visible = false;
            this.Controls.Add(panelPara);

            for (int i = 0; i < 4; i++)
            {
                ImageLabel pumpName = pumpTitleList[i];
                pumpName.Size = new Size(IOWidth, IOHeight);
                pumpName.Location = new Point(IOMarginLeft + i * (IOWidth + IOSpacingX), IOMarginTop);
                pumpName.Font = currentFont;
                pumpName.ForeColor = textColor;
                pumpName.IMGContainer = pumpNameImage;
                this.Controls.Add(pumpName);
            }

            for (int i = 0; i < 12; i++)
            {
                CParaLabel pumpPara = pumpImgLabelList[i];
                pumpPara.Size = new Size(IOWidth, IOHeight);
                pumpPara.Location = new Point(IOMarginLeft + (i / 3) * (IOWidth + IOSpacingX), IOMarginTop + (i % 3 + 1) * (IOHeight + IOSpacingY));
                pumpPara.Font = currentFont;
                pumpPara.ForeColor = textColor;
                pumpPara.TextX = TextMarginLeft;
                pumpPara.TextY = TextMarginTop;
                pumpPara.IMGContainer = pumpParaImage;
                this.Controls.Add(pumpPara);
            }
            for (int i = 0; i < 32; i++)
            {
                ImageLabel cylinder = cylinderIOList[i];
                cylinder.Size = new Size(IOWidth, IOHeight);
                cylinder.Location = new Point((i / 8) * (IOWidth + IOSpacingX), (i % 8) * (IOHeight + IOSpacingY));
                cylinder.IMGContainer = cylinderImage;
                cylinder.Font = currentFont;
                cylinder.ForeColor = textColor;
                cylinder.TextX = TextMarginLeft;
                cylinder.TextY = TextMarginTop;
                this.panelIO.Controls.Add(cylinder);
            }
            for (int i = 0; i < 32; i++)
            {
                CParaLabel cylinderPara = cylinderParaList[i];
                cylinderPara.Size = new Size(IOWidth, IOHeight);
                cylinderPara.Location = new Point((i / 8) * (IOWidth + IOSpacingX), (i % 8) * (IOHeight + IOSpacingY));
                cylinderPara.IMGContainer = pumpParaImage;
                cylinderPara.Font = currentFont;
                cylinderPara.ForeColor = textColor;
                cylinderPara.TextX = TextMarginLeft;
                cylinderPara.TextY = TextMarginTop;
                this.panelPara.Controls.Add(cylinderPara);
            }
            for (int i = 0; i < 4; i++)
            {
                ImageButton button = buttonList[i];
                button.Size = new Size(ButtonWidth, ButtonHeight);
                button.Location = new Point(IOMarginLeft + i * (ButtonWidth + ButtonSpacingX), ButtonMarginTop);
                button.IMGContainer = buttonImage;
                button.Click += new EventHandler(diagnoseItemButton_Click);
                button.Toggle = true;
                button.Font = currentFont;
                button.ForeColor = textColor;
                button.Tag = i;//diagnoseItem_Tag
                this.Controls.Add(button);
            }
            buttonList[0].Text = "5mm接近开关限位";
            buttonList[1].Text = "10mm接近开关限位";
            buttonList[2].Text = "油缸运行状态";
            buttonList[3].Text = "油缸机械锁状态";

            exitButton.Size = new Size(ButtonWidth, ButtonHeight);
            exitButton.Location = new Point(IOMarginLeft + 3 * (ButtonWidth + ButtonSpacingX), ButtonMarginTop + (ButtonHeight + ButtonSpacingY));
            exitButton.IMGContainer = buttonImage;
            exitButton.Font = currentFont;
            exitButton.ForeColor = System.Drawing.Color.Black;
            exitButton.Text = "返回";
            exitButton.CheckedChanged += new EventHandler(exitButton_CheckedChanged);
            this.Controls.Add(exitButton);
            this.panelPara.ResumeLayout(false);
            this.panelIO.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void diagnoseItemButton_Click(object sender, EventArgs e)
        {
            ImageButton bSender = (ImageButton)sender;
            foreach (ImageButton button in buttonList)
            {
                button.Checked = button == bSender;
            }
            DiagnoseItem = (int)bSender.Tag;
        }

        private void exitButton_CheckedChanged(object sender, EventArgs e)
        {
            this.DoExit();
        }

        private void diagnoseItemButton_CheckedChanged(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            if (button.Checked == true)
            {
                if ((int)button.Tag != diagnoseItem)
                {
                    int temp = diagnoseItem;
                    DiagnoseItem = (int)button.Tag;
                    buttonList[temp].Checked = false;
                }
            }
            else
            {
                if ((int)button.Tag == diagnoseItem)
                    button.Checked = true;
            }
        }

        private const string numberFormat = "F1";
        private const string PressureUnit = "kN";
        private const string VoltageUnit = "V";

        private int diagnoseItem = 0;//0: 5mm接近开关限位; 1: 10mm接近开关限位; 2:油缸运行状态; 3:油缸机械锁状态  
        private int DiagnoseItem
        {
            set
            {
                if (diagnoseItem != value)
                {
                    diagnoseItem = value;
                    RefreshList();
                }
            }
        }

        private int pumpNumber = 4;
        private int cylinderNumber = 8;
        private List<ImageLabel> pumpTitleList = null;//四泵名称列表
        private ComCtrls.SimpleImagesContaner pumpNameImage = null;//四泵名称背景图
        private List<CParaLabel> pumpImgLabelList = null;//四泵参数列表
        private ComCtrls.SimpleImagesContaner pumpParaImage = null;//四泵参数背景图
        private Panel panelIO = null;//三十二缸IO标签容器
        private List<ImageLabel> cylinderIOList = null;//四泵三十二缸IO标签列表
        private Panel panelPara = null;//三十二缸状态标签容器
        private List<CParaLabel> cylinderParaList = null;//四泵三十二缸状态标签列表
        private List<ImageButton> buttonList = null;//切换按钮列表
        private ImagesContaner buttonImage = null;//按钮背景图
        private ComCtrls.SimpleImagesContaner cylinderImage = null;//IO标签背景图
        private ImageButton exitButton = null;
        private ComCtrls.ImageLabel imageLabel_title;

        #region 布局
        private Color textColor = Color.Black;//字体颜色
        private Font currentFont = null;//IO标签字体
        private int IOMarginTop = 90;//第一行IO标签与顶端方向间距
        private int IOMarginLeft = 20;//第一列IO标签与左端方向间距
        private int IOWidth = 210;//IO标签宽度
        private int IOHeight = 35;//IO标签高度
        private int IOSpacingX = 40;//IO标签之间X方向间距
        private int IOSpacingY = 2;//IO标签之间Y方向间距

        private int TextMarginLeft = 15;
        private int TextMarginTop = 5;

        private int ButtonWidth = 210;
        private int ButtonHeight = 40;
        private int ButtonMarginTop = 570;
        private int ButtonSpacingX = 40;
        private int ButtonSpacingY = 5;
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
        #endregion

        #region 方法
        public void DoRefresh()
        {
            if (_candatapool != null)
            {
                this.SuspendLayout();
                int startPumpIndex = 0;
                for (int i = 0; i < 4; i++)
                {
                    pumpImgLabelList[i * 3 + 0].Para
                        = _candatapool.GetRealValue(startPumpIndex + i, 0, CmdDataType.cdtPressure_Pump_Real_3301_3304).ToString(numberFormat) + PressureUnit;
                    pumpImgLabelList[i * 3 + 1].Para
                        = _candatapool.GetRealValue(startPumpIndex + i, 0, CmdDataType.cdtVoltage_Real_3301_3304).ToString(numberFormat) + VoltageUnit;
                    pumpImgLabelList[i * 3 + 2].Para = _candatapool.GetBoolValue(startPumpIndex + i, 0, CmdDataType.cdtPowerSupply_3301_3304) ? "发电机" : "市电";
                }
                if (diagnoseItem == 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            cylinderIOList[i * 8 + j].Checked = _candatapool.GetBoolValue(startPumpIndex + i, j, CmdDataType.cdtLimit_5_3301_3304);
                        }
                    }
                }
                else if (diagnoseItem == 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            cylinderIOList[i * 8 + j].Checked = _candatapool.GetBoolValue(startPumpIndex + i, j, CmdDataType.cdtLimit_10_3301_3304);
                        }
                    }
                }
                else if (diagnoseItem == 2)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            cylinderParaList[i * 8 + j].Para = ConvertIntToEnum(_candatapool.GetintValue(startPumpIndex + i, j, CmdDataType.cdtcylinderState_Real_3201_3208));
                        }
                    }
                }
                else if (diagnoseItem == 3)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            cylinderParaList[i * 8 + j].Para = ConvertIntToEnum(_candatapool.GetintValue(startPumpIndex + i, j, CmdDataType.cdtMachLockState_Real_3201_3208));
                        }
                    }
                }
                this.ResumeLayout(false);
            }
        }

        public override void DoEnter()
        {
            diagnoseItemButton_Click(buttonList[0], null);
            RefreshList();
            timer_Refresh.Enabled = true;
            base.DoEnter();
        }
        #endregion

        protected override void DoExit()
        {
            timer_Refresh.Enabled = false;
            base.DoExit();
        }

        private string ConvertIntToEnum(int value)
        {
            switch (value)
            {
                case 1:
                    return "伸";
                    break;
                case 2:
                    return "缩";
                    break;
                case 0:
                    return "停";
                    break;
                default:
                    return "错";
                    break;
            }
        }

        private void RefreshList()
        {
            int startPumpIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                pumpTitleList[i].Text = (startPumpIndex + i + 1).ToString() + "#泵站";
                pumpImgLabelList[i * 3 + 0].Text = "泵站压力";
                pumpImgLabelList[i * 3 + 1].Text = "控制器电压";
                pumpImgLabelList[i * 3 + 2].Text = "供电方式";
                if (diagnoseItem == 0 || diagnoseItem == 1)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        cylinderIOList[i * 8 + j].Text = String.Format("{0}#油缸限位", j + 1);
                    }
                    this.panelIO.Visible = true;
                    this.panelPara.Visible = false;
                }
                else if (diagnoseItem == 2 || diagnoseItem == 3)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        cylinderParaList[i * 8 + j].Text = String.Format("{0}#油缸", j + 1);
                    }
                    this.panelIO.Visible = false;
                    this.panelPara.Visible = true;
                }
            }
        }
        
        private void timer_Refresh_Tick(object sender, EventArgs e)
        {
            DoRefresh();
        }
    }
}
