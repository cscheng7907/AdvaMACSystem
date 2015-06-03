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
using ComCtrls;
using DataPool;

namespace AdvaMACSystem
{
    public partial class PageViewMAC : UIControlbase
    {
        public PageViewMAC()
        {
            InitializeComponent();

            this.ForeColor = Color.Black;
            currentFont = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular);

            _candatapool = CDataPool.GetDataPoolObject();

            //pump美化
            pumpIcon = AdvaMACSystemRes.pump;

            pumpImages = new ImagesContaner();
            pumpImages.DNImg = AdvaMACSystemRes.pumpborder_checked;
            pumpImages.UPImg = AdvaMACSystemRes.pumpborder;
            pumpImages.DNImgDisable = AdvaMACSystemRes.pumpborder_disable;
            pumpImages.UPImgDisaable = AdvaMACSystemRes.pumpborder_disable;

            //button美化
            buttonImages = new ImagesContaner();
            buttonImages.DNImg = AdvaMACSystemRes.half_down;
            buttonImages.UPImg = AdvaMACSystemRes.half_up;
            buttonImages.DNImgDisable = AdvaMACSystemRes.half_disable;
            buttonImages.UPImgDisaable = AdvaMACSystemRes.half_disable;

            //cylinder美化
            progressBarImages = new CProgressBarImagesContainer();
            progressBarImages.BgImage = AdvaMACSystemRes.graybar;
            progressBarImages.FrontImage = AdvaMACSystemRes.greenbar;
            progressBarImages.FrontImage_Unpass = AdvaMACSystemRes.redbar;
            progressBarImages.WarningImage = AdvaMACSystemRes.yellowbar;
            progressBarImages.SettingImage = AdvaMACSystemRes.bluebar;

            //new pumps
            pumpList = new List<CPumpButton>();
            for (int j = 0; j < pumpNumber; j++)
            {
                CPumpButton pumpBlock = new CPumpButton();
                pumpList.Add(pumpBlock);
            }

            //new cylinders
            cylinderList = new List<CylinderCellBlock>();
            for (int i = 0; i < cylinderNumber; i++)
            {
                CylinderCellBlock cylinder = new CylinderCellBlock();
                cylinderList.Add(cylinder);
            }

            //new buttons
            controlModeButton = new ImageButton();

            //controlButtonList = new List<ImageButton>();
            //autoModeButton = new ImageButton();
            //controlButtonList.Add(autoModeButton);
            //manualModeButton = new ImageButton();
            //controlButtonList.Add(manualModeButton);

            stateButtonList = new List<ImageButton>();

            PumpInstallButton = new ImageButton();
            stateButtonList.Add(PumpInstallButton);

            PumpSettingButton = new ImageButton();
            stateButtonList.Add(PumpSettingButton);

            cylinderExtendButton = new ImageButton();
            stateButtonList.Add(cylinderExtendButton);
            //cylinderStopButton = new ImageButton();
            //stateButtonList.Add(cylinderStopButton);
            cylinderRetractButton = new ImageButton();
            stateButtonList.Add(cylinderRetractButton);


            this.SuspendLayout();

            //pumps
            for (int j = 0; j < pumpNumber; j++)
            {
                CPumpButton pumpBlock = pumpList[j];
                pumpBlock.Size = new Size(PumpWidth, PumpHeight);
                pumpBlock.Location = new Point(PumpMarginLeft, PumpMarginTop + j * (PumpHeight + PumpSpacingY));
                pumpBlock.Toggle = true;
                pumpBlock.PumpIndex = j;
                pumpBlock.IMGContainer = pumpImages;
                pumpBlock.TransParent = true;
                pumpBlock.Icon = pumpIcon;
                pumpBlock.Click += new EventHandler(pumpBlock_Click);
                this.Controls.Add(pumpBlock);
            }

            //cylinders
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

            //buttons
            controlModeButton.Size = new Size(ButtonWidth, ButtonHeight);
            controlModeButton.Location = new Point(ButtonMarginLeft, ButtonMarginTop);
            controlModeButton.Font = currentFont;
            controlModeButton.IMGContainer = buttonImages;
            controlModeButton.Checked = false;
            controlModeButton.Toggle = true;
            //controlModeButton.CheckedChanged += new EventHandler(controlModeButton_CheckedChanged);
            controlModeButton.MouseUp += new MouseEventHandler(controlModeButton_MouseUp);
            //controlModeButton.Click += new EventHandler(controlModeButton_Click);
            controlModeButton.Text = "自动模式";
            this.Controls.Add(controlModeButton);

            //for (int k = 0; k < controlButtonList.Count; k++)
            //{
            //    ImageButton button = controlButtonList[k];
            //    button.Size = new Size(ButtonWidth, ButtonHeight);
            //    button.Location = new Point(ButtonMarginLeft, ButtonMarginTop + k * (ButtonHeight + ButtonSpacingY));
            //    button.Font = currentFont;
            //    button.IMGContainer = buttonImages;
            //    button.Toggle = true;
            //    button.Click += new EventHandler(controlModeButton_Click);
            //    this.Controls.Add(button);
            //}
            //autoModeButton.Name = "auto";
            //manualModeButton.Name = "manual";
            //autoModeButton.Text = "自动模式";
            //manualModeButton.Text = "手动模式";

            for (int k = 0; k < stateButtonList.Count; k++)
            {
                ImageButton button = stateButtonList[k];
                button.Size = new Size(ButtonWidth, ButtonHeight);
                button.Location = new Point(ButtonMarginLeft + (k + 1) * (ButtonWidth + ButtonSpacingX), ButtonMarginTop);
                button.Font = currentFont;
                button.IMGContainer = buttonImages;
                button.Toggle = false;
                this.Controls.Add(button);
            }
            cylinderExtendButton.Text = "油缸伸出";
            //cylinderExtendButton.Click += new EventHandler(cylinderExtendButton_Click);
            cylinderExtendButton.MouseDown += new MouseEventHandler(cylinderExtendButton_MouseDown);
            cylinderExtendButton.MouseUp += new MouseEventHandler(cylinderExtendButton_MouseUp);

            //cylinderStopButton.Text = "油缸停止";
            //cylinderStopButton.Click += new EventHandler(cylinderStopButton_Click);
            cylinderRetractButton.Text = "油缸缩回";
            //cylinderRetractButton.Click += new EventHandler(cylinderRetractButton_Click);
            cylinderRetractButton.MouseDown += new MouseEventHandler(cylinderRetractButton_MouseDown);
            cylinderRetractButton.MouseUp += new MouseEventHandler(cylinderRetractButton_MouseUp);

            cylinderExtendButton.Enabled = false;
            //cylinderStopButton.Enabled = false;
            cylinderRetractButton.Enabled = false;


            PumpInstallButton.Text = "启动泵站";
            PumpInstallButton.Click += new EventHandler(PumpInstallButton_Click);
            //PumpInstallButton.MouseDown += new MouseEventHandler();
            //PumpInstallButton.MouseUp += new MouseEventHandler();
            PumpInstallButton.Enabled = false;

            PumpSettingButton.Text = "泵站设置";
            PumpSettingButton.Click += new EventHandler(PumpSettingButton_Click);
            //PumpSettingButton.MouseDown += new MouseEventHandler();
            //PumpSettingButton.MouseUp += new MouseEventHandler();
            PumpSettingButton.Enabled = false;

            this.ResumeLayout(false);
        }

        private void controlModeButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show(string.Format("是否切换为 {0} 模式？", (!controlModeButton.Checked) ? "自动" : "手动"),
                 "",
                 MessageBoxButtons.OKCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button1
                 ) == DialogResult.OK)
            {
                //controlModeButton.Checked = !controlModeButton.Checked;
                if (!controlModeButton.Checked)
                {
                    ControlMode = ControlModeType.Auto;
                    //_candatapool.ControlMode = ControlModeType.Auto;

                }
                else
                {
                    ControlMode = ControlModeType.CylinderManual;
                    //_candatapool.ControlMode = ControlModeType.CylinderManual;
                }
            }
            else
                controlModeButton.Checked = !controlModeButton.Checked;
        }

        private void PumpInstallButton_Click(object sender, EventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().SetboolValue(
                    selectedPumpIndex,
                    selectedCylinderIndex,
                    CmdDataType.cdtManualStart_Pump, true);
        }

        private void PumpSettingButton_Click(object sender, EventArgs e)
        {
            short dv = (short)DataPool.CDataPool.GetDataPoolObject().GetintValue(
                            selectedPumpIndex,
                            selectedCylinderIndex,
                            CmdDataType.cdtPressure_Pump);

            KeypadForm f = KeypadForm.GetKeypadForm(dv.ToString());
            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dv = Convert.ToInt16(f.KeyText);
                    if (dv >= 0 && dv <= 4000)
                    {
                        DataPool.CDataPool.GetDataPoolObject().SetintValue(
                            selectedPumpIndex,
                            selectedCylinderIndex,
                            CmdDataType.cdtPressure_Pump,
                            dv);
                    }
                    else
                        MessageBox.Show("输入非法！");
                }
                catch (Exception)
                {
                    MessageBox.Show("输入非法！");
                }
            }
        }

        private void controlModeButton_CheckedChanged(object sender, EventArgs e)
        {
        }

        private const string PumpUnit = "bar";
        private const string PressureUnit = "kN";
        private const string PositionUnit = "mm";
        private const double MinPressureValue = 0;
        private const double MaxPressureValue = 60;
        private const double MinPositionValue = 0;
        private const double MaxPositionValue = 60;

        #region 布局
        private Font currentFont = null;//字体
        private int CBMarginTop = 10;//第一行CylinderBlock与顶端方向间距
        private int CBMarginLeft = 200;//第一列CylinderBlock与左端方向间距
        private int CBWidth = 400;//CylinderBlock宽度
        private int CBHeight = 145;//CylinderBlock高度
        private int CBSpacingX = 12;//CylinderBlock之间X方向间距
        private int CBSpacingY = 12;//CylinderBlock之间Y方向间距

        private int PumpMarginTop = 10;//第一行Pump按钮与顶端方向间距
        private int PumpMarginLeft = 10;//第一列Pump按钮与左端方向间距
        private int PumpWidth = 170;//Pump按钮宽度
        private int PumpHeight = 145;//Pump按钮高度
        private int PumpSpacingY = 12;//Pump按钮之间Y方向间距

        private int ButtonMarginTop = 633;
        private int ButtonMarginLeft = 10;//200;
        private int ButtonWidth = 184;//210;
        private int ButtonHeight = 40;
        private int ButtonSpacingX = 20;
        #endregion

        #region 油缸
        private int cylinderNumber = 8;
        private List<CylinderCellBlock> cylinderList = null;
        private int selectedCylinderIndex = 0;
        private void cylinder_OnCylinderClicked(int cylinderIndex)
        {
            foreach (CylinderCellBlock ccb in cylinderList)
            {
                ccb.Selected = (ccb.CylinderIndex == cylinderIndex);
            }
            selectedCylinderIndex = cylinderIndex;
            _candatapool.CurSubId = selectedCylinderIndex;
            CylinderControlStatus = _candatapool.GetintValue(selectedPumpIndex, selectedCylinderIndex, CmdDataType.cdtcylinderState_Real_3201_3208);

            UpdateCylinderControlButtonEnabled();
        }
        private void UpdateCylinderControlButtonEnabled()
        {
            if (ControlMode == ControlModeType.CylinderManual && cylinderList[selectedCylinderIndex].InUse)
            {
                cylinderExtendButton.Enabled = true;
                //cylinderStopButton.Enabled = true;
                cylinderRetractButton.Enabled = true;
            }
            else
            {
                cylinderExtendButton.Enabled = false;
                //cylinderStopButton.Enabled = false;
                cylinderRetractButton.Enabled = false;
            }
        }

        private void UpdatePumpButtonEnabled()
        {
            PumpInstallButton.Enabled = pumpList[selectedPumpIndex].CurrentPara < 20 && ControlMode != ControlModeType.Auto;

        }


        private CProgressBarImagesContainer progressBarImages = null;
        #endregion

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
                    _candatapool.CurId = selectedPumpIndex;
                }
            }
        }

        private ImagesContaner pumpImages = null;
        private Bitmap pumpIcon = null;
        private List<CPumpButton> pumpList = null;

        private void pumpBlock_Click(object sender, EventArgs e)
        {
            CPumpButton pBSender = (CPumpButton)sender;
            if (pBSender.Enabled)
            {
                foreach (CPumpButton pumpBlock in pumpList)
                {
                    pumpBlock.Checked = pumpBlock == pBSender;
                }
                SelectedPumpIndex = pBSender.PumpIndex;
                cylinder_OnCylinderClicked(0);
            }

        }

        #endregion

        #region 控制按钮
        //private ControlModeType controlMode = ControlModeType.cmtAuto; //0:Auto 1:Manual
        public ControlModeType ControlMode
        {
            get { return _candatapool.ControlMode; }
            set
            {
                if (_candatapool.ControlMode != value)
                {
                    _candatapool.ControlMode = value;
                    DoControlModeChanged();
                }
            }
        }

        private void DoControlModeChanged()
        {
            if (ControlMode == ControlModeType.Auto)
            {
                controlModeButton.Checked = false;
                controlModeButton.Text = "自动模式";
                //autoModeButton.Checked = true;
                //manualModeButton.Checked = false;

                cylinderExtendButton.Enabled = false;
                //cylinderStopButton.Enabled = false;
                cylinderRetractButton.Enabled = false;

                PumpInstallButton.Enabled = false;
                PumpSettingButton.Enabled = false;
            }
            else
            {
                controlModeButton.Checked = true;
                controlModeButton.Text = "手动模式";

                //autoModeButton.Checked = false;
                //manualModeButton.Checked = true;
                PumpSettingButton.Enabled = true;
                UpdateCylinderControlButtonEnabled();
            }
        }

        private void controlModeButton_Click(object sender, EventArgs e)
        {
            //ImageButton bSender = (ImageButton)sender;
            //foreach (ImageButton button in controlButtonList)
            //{
            //    button.Checked = button == bSender;
            //}
            //if (bSender.Name == "auto")
            //{
            //    ControlMode = 0;
            //    _candatapool.ControlMode = ControlModeType.Auto;

            //}
            //else if (bSender.Name == "manual")
            //{
            //    ControlMode = 1;
            //    _candatapool.ControlMode = ControlModeType.CylinderManual;
            //}
        }

        private int cylinderControlStatus = 0;
        public int CylinderControlStatus
        {
            get { return cylinderControlStatus; }
            set
            {
                if (cylinderControlStatus != value)
                {
                    cylinderControlStatus = value;
                    UpdateCylinderControlStatus();
                }
            }
        }

        private void UpdateCylinderControlStatus()
        {
            if (cylinderControlStatus == 0)
                _candatapool.out_MotionState = MotionStateType.stsStop;
            else if (cylinderControlStatus == 1)
                _candatapool.out_MotionState = MotionStateType.stsextend;
            else
                _candatapool.out_MotionState = MotionStateType.stsretract;

            //cylinderExtendButton.Checked = (cylinderControlStatus == 1);
            //cylinderStopButton.Checked = (cylinderControlStatus == 0);
            //cylinderRetractButton.Checked = (cylinderControlStatus == 2);
        }

        private ImagesContaner buttonImages = null;
        //private List<ImageButton> controlButtonList = null;
        private List<ImageButton> stateButtonList = null;
        private ImageButton controlModeButton = null;
        //private ImageButton autoModeButton = null;
        //private ImageButton manualModeButton = null;
        private ImageButton cylinderExtendButton = null;
        //private ImageButton cylinderStopButton = null;
        private ImageButton cylinderRetractButton = null;

        private ImageButton PumpInstallButton = null;
        private ImageButton PumpSettingButton = null;

        private void cylinderRetractButton_Click(object sender, EventArgs e)
        {
            //if (ControlMode == ControlModeType.CylinderManual && selectedCylinderIndex >= 0 && selectedCylinderIndex < cylinderList.Count)
            {
                CylinderControlStatus = 2;
            }
        }

        private void cylinderStopButton_Click(object sender, EventArgs e)
        {
            //if (ControlMode == ControlModeType.CylinderManual && selectedCylinderIndex >= 0 && selectedCylinderIndex < cylinderList.Count)
            {
                CylinderControlStatus = 0;
            }
        }

        private void cylinderExtendButton_Click(object sender, EventArgs e)
        {
            //if (ControlMode == ControlModeType.CylinderManual && selectedCylinderIndex >= 0 && selectedCylinderIndex < cylinderList.Count)
            {
                CylinderControlStatus = 1;
            }
        }

        private void cylinderRetractButton_MouseDown(object sender, MouseEventArgs e)
        { cylinderRetractButton_Click(sender, e); }
        private void cylinderRetractButton_MouseUp(object sender, MouseEventArgs e)
        { cylinderStopButton_Click(sender, e); }
        private void cylinderExtendButton_MouseDown(object sender, MouseEventArgs e)
        { cylinderExtendButton_Click(sender, e); }
        private void cylinderExtendButton_MouseUp(object sender, MouseEventArgs e)
        { cylinderStopButton_Click(sender, e); }

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
            DoReEnter();
            base.DoEnter();
        }

        public override void DoReEnter()
        {
            //更新控制方式
            //ControlMode = (int)_candatapool.ControlMode;
            //controlButtonList[controlMode].Checked = true;
            DoControlModeChanged();

            //更新pumpList
            if (pumpList == null)
                return;

            for (int j = 0; j < pumpList.Count; j++)
            {
                pumpList[j].Unit = PumpUnit;
            }
            pumpList[selectedPumpIndex].Checked = true;
            _candatapool.CurId = selectedPumpIndex;

            //更新cylinderList
            RefreshCylinderList();
            cylinderList[selectedCylinderIndex].Selected = true;
            _candatapool.CurSubId = selectedCylinderIndex;

            //更新cylinderControlStatus
            cylinderControlStatus = _candatapool.GetintValue(selectedPumpIndex, selectedCylinderIndex, CmdDataType.cdtcylinderState_Real_3201_3208);
            UpdateCylinderControlStatus();
            UpdateCylinderControlButtonEnabled();

            this.timer_RefreshMac.Enabled = true;

            base.DoReEnter();
        }


        protected override void DoExit()
        {
            timer_RefreshMac.Enabled = false;
            base.DoExit();
        }

        private void RefreshCylinderList()
        {
            if (cylinderList == null)
                return;

            if (_candatapool == null)
                return;

            for (int i = 0; i < cylinderList.Count; i++)
            {
                cylinderList[i].InitialInstance(selectedPumpIndex, i);
                int row = _candatapool.GetintValue(selectedPumpIndex, i, CmdDataType.cdtView_SetupPosition_Row);
                int col = _candatapool.GetintValue(selectedPumpIndex, i, CmdDataType.cdtView_SetupPosition_Col);
                cylinderList[i].SetRowAndCol(row, col);
            }

            for (int i = 0; i < cylinderList.Count; i++)
            {
                //设定压力条单位
                cylinderList[i].PressureUnit = PressureUnit;
                //设定压力条下限值
                cylinderList[i].MinPressureValue = MinPressureValue;
                //设定压力条上限值
                cylinderList[i].MaxPressureValue = MaxPressureValue;
                //设定压力条下限报警值
                cylinderList[i].LowerWarningPressureValue = _candatapool.GetRealValue(selectedPumpIndex, i, CmdDataType.cdtPressureLowerLimitAlarm_Value);
                //设定压力条上限报警值
                cylinderList[i].UpperWarningPressureValue = _candatapool.GetRealValue(selectedPumpIndex, i, CmdDataType.cdtPressureUpperLimitAlarm_Value);
                //设定压力条设定值
                cylinderList[i].SettingPressureValue = _candatapool.GetRealValue(selectedPumpIndex, i, CmdDataType.cdtPressure_Value);

                //设定长度条单位
                cylinderList[i].PositionUnit = PositionUnit;
                //设定长度条下限值
                cylinderList[i].MinPositionValue = MinPositionValue;
                //设定长度条上限值
                cylinderList[i].MaxPositionValue = MaxPositionValue;
                //设定长度条下限报警值
                cylinderList[i].LowerWarningPositionValue = _candatapool.GetRealValue(selectedPumpIndex, i, CmdDataType.cdtPositionLowerLimitAlarm_Value);
                //设定长度条上限报警值
                cylinderList[i].UpperWarningPositionValue = _candatapool.GetRealValue(selectedPumpIndex, i, CmdDataType.cdtPositionUpperLimitAlarm_Value);
                //设定长度条设定值
                cylinderList[i].SettingPositionValue = _candatapool.GetRealValue(selectedPumpIndex, i, CmdDataType.cdtPosition_Value);

                //检测油缸运行状态
                cylinderList[i].InUse = _candatapool.GetBoolValue(selectedPumpIndex, i, CmdDataType.cdtInstalled);
            }
        }

        private void timer_RefreshMac_Tick(object sender, EventArgs e)
        {
            if (!this.Visible)
                return;

            //pumps
            for (int j = 0; j < pumpList.Count; j++)
            {
                pumpList[j].CurrentStatus = "运行";
                pumpList[j].CurrentPara = _candatapool.GetRealValue(j, 0, CmdDataType.cdtPressure_Pump_Real_3301_3304);
            }
            UpdatePumpButtonEnabled();

            //cylinders
            for (int i = 0; i < cylinderList.Count; i++)
            {
                if (cylinderList[i].InUse)
                {
                    cylinderList[i].CurrentPressureValue = _candatapool.GetRealValue(selectedPumpIndex, i, CmdDataType.cdtPressure_Real_3001_3008);
                    cylinderList[i].CurrentPositionValue = _candatapool.GetRealValue(selectedPumpIndex, i, CmdDataType.cdtPosition_Real_3101_3108);
                }
                else
                {
                    cylinderList[i].CurrentPressureValue = 0;
                    cylinderList[i].CurrentPositionValue = 0;
                }
            }

            int warncount = 0;
            //cylinder warning 
            for (int j = 0; j < pumpList.Count; j++)
            {
                warncount = 0;

                for (int i = 0; i < cylinderList.Count; i++)
                {
                    //position UpperLimit
                    if (_candatapool.GetBoolValue(j, i, CmdDataType.cdtPositionUpperLimitAlarm_Enable) &&
                        _candatapool.GetRealValue(j, i, CmdDataType.cdtPosition_Real_3101_3108) >
                        _candatapool.GetRealValue(j, i, CmdDataType.cdtPositionUpperLimitAlarm_Value)
                        )
                        warncount++;

                    //position LowerLimit
                    if (_candatapool.GetBoolValue(j, i, CmdDataType.cdtPositionLowerLimitAlarm_Enable) &&
                        _candatapool.GetRealValue(j, i, CmdDataType.cdtPosition_Real_3101_3108) <
                        _candatapool.GetRealValue(j, i, CmdDataType.cdtPositionLowerLimitAlarm_Value)
                        )
                        warncount++;

                    //pressure UpperLimit
                    if (_candatapool.GetBoolValue(j, i, CmdDataType.cdtPressureUpperLimitAlarm_Enable) &&
                        _candatapool.GetRealValue(j, i, CmdDataType.cdtPressure_Real_3001_3008) >
                        _candatapool.GetRealValue(j, i, CmdDataType.cdtPressureUpperLimitAlarm_Value)
                        )
                        warncount++;

                    //pressure LowerLimit
                    if (//_candatapool.GetBoolValue(j, i, CmdDataType.cdtPressureUpperLimitAlarm_Enable) &&
                        _candatapool.GetRealValue(j, i, CmdDataType.cdtPressure_Real_3001_3008) <
                        _candatapool.GetRealValue(j, i, CmdDataType.cdtPressureLowerLimitAlarm_Value)
                        )
                        warncount++;
                }
                pumpList[j].WarningCount = warncount;
            }
        }


    }

}
