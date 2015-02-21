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
    public partial class PageViewMAC : UIControlbase
    {
        public PageViewMAC()
        {
            InitializeComponent();

            currentFont = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular);

            _candatapool = CDataPool.GetDataPoolObject();

            pumpIcon = AdvaMACSystemRes.pump;

            progressBarImages = new CProgressBarImagesContainer();
            progressBarImages.BgImage = AdvaMACSystemRes.graybar;
            progressBarImages.FrontImage = AdvaMACSystemRes.greenbar;
            progressBarImages.FrontImage_Unpass = AdvaMACSystemRes.redbar;
            progressBarImages.WarningImage = AdvaMACSystemRes.yellowbar;
            progressBarImages.SettingImage = AdvaMACSystemRes.bluebar;

            pumpList = new List<CPumpButton>();
            for (int j = 0; j < pumpNumber; j++)
            {
                CPumpButton pumpBlock = new CPumpButton();
                pumpList.Add(pumpBlock);
            }

            cylinderList = new List<CylinderCellBlock>();
            //new cylinders
            for (int i = 0; i < cylinderNumber; i++)
            {
                CylinderCellBlock cylinder = new CylinderCellBlock();
                cylinderList.Add(cylinder);
            }

            buttonList = new List<ImageButton>();
            autoModeButton = new ImageButton();
            buttonList.Add(autoModeButton);
            manualModeButton = new ImageButton();
            buttonList.Add(manualModeButton);
            cylinderExtendButton = new ImageButton();
            buttonList.Add(cylinderExtendButton);
            cylinderStopButton = new ImageButton();
            buttonList.Add(cylinderStopButton);
            cylinderRetractButton = new ImageButton();
            buttonList.Add(cylinderRetractButton);

            this.SuspendLayout();
            for (int j = 0; j < pumpNumber; j++)
            {
                CPumpButton pumpBlock = pumpList[j];
                pumpBlock.Size = new Size(PumpWidth, PumpHeight);
                pumpBlock.Location = new Point(PumpMarginLeft, PumpMarginTop + j * (PumpHeight + PumpSpacingY));
                pumpBlock.Toggle = true;
                pumpBlock.PumpIndex = j;
                pumpBlock.Icon = pumpIcon;
                pumpBlock.CheckedChanged += new EventHandler(pumpBlock_CheckedChanged);
                this.Controls.Add(pumpBlock);
            }
            for (int i = 0; i < cylinderNumber; i++)
            {
                CylinderCellBlock cylinder = cylinderList[i];
                cylinder.Size = new Size(CBWidth, CBHeight);
                cylinder.Location = new Point(CBMarginLeft + (i % 2) * (CBWidth + CBSpacingX), CBMarginTop + (i / 2) * (CBHeight + CBSpacingY));
                cylinder.OnCylinderClicked += new OnCylinderClickHandler(cylinder_OnCylinderClicked);
                cylinder.IMGContainer = progressBarImages;
                //cylinder.Font = currentFont;
                this.Controls.Add(cylinder);
            }
            for (int k = 0; k < buttonList.Count; k++)
            {
                ImageButton button = buttonList[k];
                button.Size = new Size(ButtonWidth, ButtonHeight);
                button.Location = new Point(ButtonMarginLeft, ButtonMarginTop + k * (ButtonHeight + ButtonSpacingY));
                button.Font = currentFont;
                this.Controls.Add(button);
            }
            autoModeButton.Toggle = true;
            manualModeButton.Toggle = true;
            autoModeButton.CheckedChanged += new EventHandler(controlModeButton_CheckedChanged);
            manualModeButton.CheckedChanged += new EventHandler(controlModeButton_CheckedChanged);
            autoModeButton.Name = "auto";
            manualModeButton.Name = "manual";
            autoModeButton.Text = "自动模式";
            manualModeButton.Text = "手动模式";
            cylinderExtendButton.Text = "油缸伸出";
            cylinderStopButton.Text = "油缸停止";
            cylinderRetractButton.Text = "油缸缩回";

            this.ResumeLayout(false);
        }

        #region 布局
        private Font currentFont = null;//字体
        private int CBMarginTop = 3;//第一行CylinderBlock与顶端方向间距
        private int CBMarginLeft = 200;//第一列CylinderBlock与左端方向间距
        private int CBWidth = 310;//CylinderBlock宽度
        private int CBHeight = 145;//CylinderBlock高度
        private int CBSpacingX = 5;//CylinderBlock之间X方向间距
        private int CBSpacingY = 5;//CylinderBlock之间Y方向间距

        private int PumpMarginTop = 3;//第一行Pump按钮与顶端方向间距
        private int PumpMarginLeft = 3;//第一列Pump按钮与左端方向间距
        private int PumpWidth = 170;//Pump按钮宽度
        private int PumpHeight = 145;//Pump按钮高度
        private int PumpSpacingY = 5;//Pump按钮之间Y方向间距

        private int ButtonMarginTop = 3;
        private int ButtonMarginLeft = 850;
        private int ButtonWidth = 170;
        private int ButtonHeight = 70;
        private int ButtonSpacingY = 10;
        #endregion

        #region 油缸
        private int cylinderNumber = 8;
        private List<CylinderCellBlock> cylinderList = null;
        private int selectedCylinderIndex = -1;
        private void cylinder_OnCylinderClicked(int cylinderIndex)
        {
            if (selectedCylinderIndex >= 0 && selectedCylinderIndex < cylinderList.Count)
                cylinderList[selectedCylinderIndex].Selected = false;
            if (cylinderList[cylinderIndex].Selected)
                selectedCylinderIndex = cylinderIndex;
            else
                selectedCylinderIndex = -1;
        }

        private CProgressBarImagesContainer progressBarImages = null;
        #endregion

        double x = 0;
        private void timer_RefreshMac_Tick(object sender, EventArgs e)
        {
            //x += 0.1;
            for (int i = 0; i < cylinderList.Count; i++)
            {
                if (cylinderList[i].InUse)
                {
                    //cylinderList[i].CurrentPressureValue = 20 * Math.Sin(x) + 30;
                    cylinderList[i].CurrentPressureValue = _candatapool.GetRealValue(selectedPumpIndex, i, CmdDataType.cdtPressure_Real);
                    cylinderList[i].CurrentDistanceValue = _candatapool.GetRealValue(selectedPumpIndex, i, CmdDataType.cdtPosition_Real);
                }
                else
                {
                    cylinderList[i].CurrentPressureValue = 0;
                    cylinderList[i].CurrentDistanceValue = 0;
                }
            }
        }

        #region 泵
        private int pumpNumber = 4;
        private int selectedPumpIndex = 0;//当前选中的泵的编号
        private int SelectedPumpIndex
        {
            set
            {
                if (selectedPumpIndex != value)
                {
                    selectedPumpIndex = value;
                    RefreshCylinderList();
                }
            }
        }

        private Bitmap pumpIcon = null;
        private List<CPumpButton> pumpList = null;
        private void pumpBlock_CheckedChanged(object sender, EventArgs e)
        {
            CPumpButton pumpBlock = (CPumpButton)sender;
            if (pumpBlock.Checked == true)
            {
                if (pumpBlock.PumpIndex != selectedPumpIndex)
                {
                    int temp = selectedPumpIndex;
                    SelectedPumpIndex = pumpBlock.PumpIndex;
                    pumpList[temp].Checked = false;
                }
            }
            else
            {
                if (pumpBlock.PumpIndex == selectedPumpIndex)
                    pumpBlock.Checked = true;
            }
        }
        #endregion

        #region 控制按钮
        private int controlMode = 0; //0:Auto 1:Manual
        private int cylinderControlStatus = 0;
        private List<ImageButton> buttonList = null;
        private ImageButton autoModeButton = null;
        private ImageButton manualModeButton = null;
        private ImageButton cylinderExtendButton = null;
        private ImageButton cylinderStopButton = null;
        private ImageButton cylinderRetractButton = null;
        private void controlModeButton_CheckedChanged(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            if (button.Checked == true)
            {
                if (button.Name == "auto")
                {
                    controlMode = 0;
                    manualModeButton.Checked = false;
                }
                else
                {
                    controlMode = 1;
                    autoModeButton.Checked = false;
                }
            }
            else
            {
                if ((button.Name == "auto" && controlMode == 0) ||
                    (button.Name == "manual" && controlMode == 1))
                    button.Checked = true;
            }
            if (manualModeButton.Checked)
            {
                cylinderExtendButton.Enabled = true;
                cylinderStopButton.Enabled = true;
                cylinderRetractButton.Enabled = true;
            }
            else
            {
                cylinderExtendButton.Enabled = false;
                cylinderStopButton.Enabled = false;
                cylinderRetractButton.Enabled = false;
            }
        }

        #endregion
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

        public override void DoEnter()
        {
            if (pumpList == null)
                return;

            for (int j = 0; j < pumpList.Count; j++)
            {
                pumpList[j].Checked = false;
            }
            pumpList[0].Checked = true;
            selectedPumpIndex = 0;
            RefreshCylinderList();

            autoModeButton.Checked = true;
            controlMode = 0;

            this.timer_RefreshMac.Enabled = true;
            base.DoEnter();
        }

        private void RefreshCylinderList()
        {
            if (cylinderList == null)
                return;

            for (int i = 0; i < cylinderList.Count; i++)
            {
                cylinderList[i].InitialInstance(selectedPumpIndex, i);
            }

            if (_candatapool == null)
                return;

            for (int i = 0; i < cylinderList.Count; i++)
            {
                //设定压力条单位
                cylinderList[i].PressureUnit = "kN";
                //设定压力条下限值
                cylinderList[i].MinPressureValue = 0;
                //设定压力条上限值
                cylinderList[i].MaxPressureValue = 60;
                //设定压力条下限报警值
                cylinderList[i].WarningPressureValue = 30;
                //设定压力条设定值
                cylinderList[i].SettingPressureValue = 50;

                //设定长度条单位
                cylinderList[i].DistanceUnit = "mm";
                //设定长度条下限值
                cylinderList[i].MinDistanceValue = 0;
                //设定长度条上限值
                cylinderList[i].MaxDistanceValue = 600;
                //设定长度条下限报警值
                cylinderList[i].WarningDistanceValue = 300;
                //设定长度条设定值
                cylinderList[i].SettingDistanceValue = 500;

                //检测油缸运行状态
                cylinderList[i].InUse = true;
                //cylinderList[i].InUse = _candatapool.GetintValue(pumpIndex, i, CmdDataType.cdtcylinderState_Real) != 0;
            }
            cylinderList[1].InUse = false;
        }

    }
}
