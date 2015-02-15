using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ComCtrls;

namespace CylinderCell
{
    public partial class CylinderCellBlock : UserControl
    {
        public CylinderCellBlock()
        {
            InitializeComponent();
        }
        private float minValue;
        public float MinValue
        {
            set
            {
                minValue = value;
                this.labelMin.Text = "Min:" + minValue.ToString();
            }
        }

        private float maxValue;
        public float MaxValue
        {
            set
            {
                maxValue = value; this.labelMax.Text = "Max:" + maxValue.ToString();
            }
        }

        private float warningValue;
        public float WarningValue
        {
            set { warningValue = value; this.label3.Text = "warn:" + warningValue.ToString();
            this.customStatusBar1.WarningPercentage = (warningValue - minValue) / (maxValue - minValue);
            }
        }

        private float settingValue;
        public float SettingValue
        {
            set { settingValue = value;
            this.label4.Text = "set:" + settingValue.ToString();
            this.customStatusBar1.SettingPercentage = (settingValue - minValue) / (maxValue - minValue);
            }
        }
        private float currentValue;
        public float CurrentValue
        {
            set
            {
                currentValue = value;
                this.customStatusBar1.ValuePercentage = (currentValue - minValue) / (maxValue - minValue);
            }
        }
    }
}
