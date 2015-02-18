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

        private double warningPressureValue;
        public double WarningPressureValue
        {
            set
            { 
                warningPressureValue = value;
                this.lbWarningPre.Text = warningPressureValue.ToString(numberFormat);
                this.progressBar.WarningPercentage = (warningPressureValue - minPressureValue) / (maxPressureValue - minPressureValue);
            }
        }

        private double settingPressureValue;
        public double SettingPressureValue
        {
            set 
            { 
                settingPressureValue = value;
                this.lbSettingPre.Text = settingPressureValue.ToString(numberFormat);
                this.progressBar.SettingPercentage = (settingPressureValue - minPressureValue) / (maxPressureValue - minPressureValue);
            }
        }
        private double currentPressureValue;
        public double CurrentPressureValue
        {
            set
            {
                currentPressureValue = value;
                this.progressBar.ValuePercentage = (currentPressureValue - minPressureValue) / (maxPressureValue - minPressureValue);
                this.lbValuePre.Text = currentPressureValue.ToString(numberFormat);
            }
        }

        public string PressureUnit = string.Empty;

        private double minDistanceValue;
        public double MinDistanceValue
        {
            set
            {
                minDistanceValue = value;
                this.lbMinDis.Text = minDistanceValue.ToString(numberFormat);
            }
        }

        private double maxDistanceValue;
        public double MaxDistanceValue
        {
            set
            {
                maxDistanceValue = value;
                this.lbMaxDis.Text = maxDistanceValue.ToString(numberFormat) + DistanceUnit;
            }
        }

        private double warningDistanceValue;
        public double WarningDistanceValue
        {
            set
            {
                warningDistanceValue = value;
                this.lbWarningDis.Text = warningDistanceValue.ToString(numberFormat);
            }
        }

        private double settingDistanceValue;
        public double SettingDistanceValue
        {
            set
            {
                settingDistanceValue = value;
                this.lbSettingDis.Text = settingDistanceValue.ToString(numberFormat);
            }
        }
        private double currentDistanceValue;
        public double CurrentDistanceValue
        {
            set
            {
                currentDistanceValue = value;
                this.lbValueDis.Text = currentDistanceValue.ToString(numberFormat);
            }
        }

        public string DistanceUnit = string.Empty;

        private int pumpIndex;//泵的编号
        private int cylinderIndex;
        private int CylinderIndex
        {
            set
            {
                cylinderIndex = value;
                this.lbIndex.Text = (cylinderIndex + 1).ToString() + "#";
            }
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
                selected = value;
                if (selected)
                    this.panel1.BackColor = Color.Orange;
                else
                    this.panel1.BackColor = Color.Silver;
            }
        }

        private bool inUse = true;
        public bool InUse
        {
            set
            {
                inUse = value;
                if (inUse)
                    this.BackColor = Color.DarkOrange;
                else
                    this.BackColor = Color.LightGray;
            }
            get
            {
                return inUse;
            }
        }

        private void CylinderCellBlock_Click(object sender, EventArgs e)
        {
            Selected = !selected;
        }

    }
}
