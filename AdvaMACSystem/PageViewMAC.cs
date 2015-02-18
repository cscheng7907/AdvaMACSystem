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
    public partial class PageViewMAC : UIControlbase
    {
        public PageViewMAC()
        {
            InitializeComponent();
            progressBarImages = new CProgressBarImagesContainer();
            progressBarImages.BgImage = AdvaMACSystemRes.graybar;
            progressBarImages.FrontImage = AdvaMACSystemRes.greenbar;
            progressBarImages.FrontImage_Unpass = AdvaMACSystemRes.redbar;
            progressBarImages.WarningImage = AdvaMACSystemRes.yellowbar;
            progressBarImages.SettingImage = AdvaMACSystemRes.bluebar;

            foreach (CylinderCellBlock ccb in this.Controls)
            {
                ccb.IMGContainer = progressBarImages;
                ccb.MinValue = 0;
                ccb.MaxValue = 60;
                ccb.WarningValue = 30;
                ccb.SettingValue = 50;
            }
            this.timer_RefreshMac.Enabled = true;
        }

        private double x = 0;

        private CProgressBarImagesContainer progressBarImages = null;
        private void timer_RefreshMac_Tick(object sender, EventArgs e)
        {
            x += 0.1;
            foreach (CylinderCellBlock ccb in this.Controls)
            {
                ccb.CurrentValue = 30 + 20 * Math.Sin(x);
            }
        }
    }
}
