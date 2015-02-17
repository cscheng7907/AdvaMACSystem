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
        public CylinderCellBlock()
        {
            InitializeComponent();
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

        private double minValue;
        public double MinValue
        {
            set
            {
                minValue = value;
                this.lbMinDis.Text = "Min:" + minValue.ToString();
            }
        }

        private double maxValue;
        public double MaxValue
        {
            set
            {
                maxValue = value; this.lbMaxDis.Text = "Max:" + maxValue.ToString();
            }
        }

        private double warningValue;
        public double WarningValue
        {
            set { warningValue = value; this.lbWarningDis.Text = "warn:" + warningValue.ToString();
            this.progressBar.WarningPercentage = (warningValue - minValue) / (maxValue - minValue);
            }
        }

        private double settingValue;
        public double SettingValue
        {
            set { settingValue = value;
            this.lbSettingDis.Text = "set:" + settingValue.ToString();
            this.progressBar.SettingPercentage = (settingValue - minValue) / (maxValue - minValue);
            }
        }
        private double currentValue;
        public double CurrentValue
        {
            set
            {
                currentValue = value;
                this.progressBar.ValuePercentage = (currentValue - minValue) / (maxValue - minValue);
            }
        }

        private int pumpIndex;//泵的编号
        private int cylinderIndex;
        public void InitialInstance(int PumpIndex, int CylinderIndex)
        {
            this.pumpIndex = PumpIndex;
            this.cylinderIndex = CylinderIndex;
        }

        public void DoRefresh()
        {
            //CurrentValue = 采样数据;
        }
    }
}
