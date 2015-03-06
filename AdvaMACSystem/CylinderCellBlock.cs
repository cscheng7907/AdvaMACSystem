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

namespace AdvaMACSystem
{
    public partial class CylinderCellBlock : UserControl
    {
        private const string numberFormat = "F1";
        public CylinderCellBlock()
        {
            InitializeComponent();
            pictureBox1.Image = AdvaMACSystemRes.border;
            foreach (Control c in panel1.Controls)
            {
                c.Click += new EventHandler(CylinderCellBlock_Click);
            }
        }
        private CProgressBarImagesContainer imgContainer = null;
        public CProgressBarImagesContainer IMGContainer
        {
            get
            {
                return imgContainer;
            }
            set
            {
                if (imgContainer != value)
                {
                    imgContainer = value;
                    if (progressBar != null)
                        progressBar.IMGContainer = imgContainer;
                }
            }
        }

        private double minPressureValue;
        public double MinPressureValue
        {
            set
            {
                minPressureValue = value;
                this.lbMinPre.Text = minPressureValue.ToString(numberFormat);
            }
        }

        private double maxPressureValue;
        public double MaxPressureValue
        {
            set
            {
                maxPressureValue = value;
                this.lbMaxPre.Text = maxPressureValue.ToString(numberFormat) + PressureUnit;
            }
        }

        private double lowerWarningPressureValue;
        public double LowerWarningPressureValue
        {
            set
            {
                lowerWarningPressureValue = value;
                this.lbWarningPre.Text = lowerWarningPressureValue.ToString(numberFormat);
                if (maxPressureValue - minPressureValue != 0)
                    this.progressBar.LowerWarningPercentage = (lowerWarningPressureValue - minPressureValue) / (maxPressureValue - minPressureValue);
            }
        }

        private double upperWarningPressureValue;
        public double UpperWarningPressureValue
        {
            set
            {
                upperWarningPressureValue = value;
                if (maxPressureValue - minPressureValue != 0)
                    this.progressBar.UpperWarningPercentage = (upperWarningPressureValue - minPressureValue) / (maxPressureValue - minPressureValue);
            }
        }

        private double settingPressureValue;
        public double SettingPressureValue
        {
            set 
            { 
                settingPressureValue = value;
                this.lbSettingPre.Text = settingPressureValue.ToString(numberFormat);
                if (maxPressureValue - minPressureValue != 0)
                    this.progressBar.SettingPercentage = (settingPressureValue - minPressureValue) / (maxPressureValue - minPressureValue);
            }
        }
        private double currentPressureValue;
        public double CurrentPressureValue
        {
            set
            {
                currentPressureValue = value;
                if (maxPressureValue - minPressureValue != 0)
                    this.progressBar.ValuePercentage = (currentPressureValue - minPressureValue) / (maxPressureValue - minPressureValue);
                this.lbValuePre.Text = currentPressureValue.ToString(numberFormat) + PressureUnit;
            }
        }

        public string PressureUnit = string.Empty;

        private double minPositionValue;
        public double MinPositionValue
        {
            set
            {
                minPositionValue = value;
                this.lbMinPos.Text = minPositionValue.ToString(numberFormat);
            }
        }

        private double maxPositionValue;
        public double MaxPositionValue
        {
            set
            {
                maxPositionValue = value;
                this.lbMaxPos.Text = maxPositionValue.ToString(numberFormat) + PositionUnit;
            }
        }

        private double warningPositionValue;
        public double WarningPositionValue
        {
            set
            {
                warningPositionValue = value;
                this.lbWarningPos.Text = warningPositionValue.ToString(numberFormat);
            }
        }

        private double settingPositionValue;
        public double SettingPositionValue
        {
            set
            {
                settingPositionValue = value;
                this.lbSettingPos.Text = settingPositionValue.ToString(numberFormat);
            }
        }
        private double currentPositionValue;
        public double CurrentPositionValue
        {
            set
            {
                currentPositionValue = value;
                this.lbValuePos.Text = currentPositionValue.ToString(numberFormat) + PositionUnit;
            }
        }

        public string PositionUnit = string.Empty;

        private int pumpIndex;//泵的编号
        private int cylinderIndex;
        public int CylinderIndex
        {
            set
            {
                cylinderIndex = value;
                this.lbIndex.Text = (cylinderIndex + 1).ToString() + "#";
            }
            get { return cylinderIndex; }
        }

        public void InitialInstance(int PumpIndex, int CylinderIndex)
        {
            this.pumpIndex = PumpIndex;
            this.CylinderIndex = CylinderIndex;
        }

        private bool selected = false;
        public bool Selected
        {
            set
            {
                if (selected != value)
                {
                    selected = value;
                    if (selected)
                        this.panel1.BackColor = Color.Orange;
                    else
                        this.panel1.BackColor = Color.Silver;
                }
            }
            get
            {
                return selected;
            }
        }

        private bool inUse = true;
        public bool InUse
        {
            set
            {
                inUse = value;
                if (inUse)
                {
                    pictureBox1.Image = AdvaMACSystemRes.border;
                    foreach (Control l in panel1.Controls)
                    {
                        if (l is Label)
                            l.ForeColor = Color.Brown;
                    }
                    lbWarningPos.ForeColor = lbWarningPre.ForeColor = Color.Red;
                    lbSettingPos.ForeColor = lbSettingPre.ForeColor = Color.Blue;
                }
                else
                {
                    pictureBox1.Image = AdvaMACSystemRes.border_Disable;
                    foreach (Control l in panel1.Controls)
                    {
                        if (l is Label)
                            l.ForeColor = Color.Wheat;
                    }
                }
            }
            get
            {
                return inUse;
            }
        }

        public event OnCylinderClickHandler OnCylinderClicked;
        private void CylinderCellBlock_Click(object sender, EventArgs e)
        {
            if (OnCylinderClicked != null)
                OnCylinderClicked(cylinderIndex);
        }

    }
    public delegate void OnCylinderClickHandler(int cylinderIndex);
}
