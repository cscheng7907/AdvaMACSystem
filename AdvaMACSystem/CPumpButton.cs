using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ComCtrls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace AdvaMACSystem
{
    public partial class CPumpButton : ImageButton
    {
        private const string numberFormat = "F1";

        public CPumpButton()
        {
            InitializeComponent();
            this.lbPara.Text = currentPara.ToString(numberFormat) + unit;
            this.lbName.Text = (pumpIndex + 1).ToString() + "#";
        }

        public CPumpButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.lbPara.Text = currentPara.ToString(numberFormat) + unit;
            this.lbName.Text = (pumpIndex + 1).ToString() + "#";
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Graphics gxOff; //Offscreen graphics
            base.OnPaintBasicImage(e);
            gxOff = Graphics.FromImage(m_bmpOffscreen);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                    gxOff.DrawString(c.Text, c.Font, new SolidBrush(c.ForeColor), c.Left + c.Width / 2, c.Top + c.Height / 2, sf);
            }
            if (icon != null)
                gxOff.DrawImage(icon, new Rectangle(40 - icon.Width / 2, 40 - icon.Height / 2, icon.Width, icon.Height), new Rectangle(0, 0, icon.Width, icon.Height), GraphicsUnit.Pixel);

            e.Graphics.DrawImage(m_bmpOffscreen, 0, 0);

        }

        private int pumpIndex;
        public int PumpIndex
        {
            get { return pumpIndex; }
            set
            {
                if (pumpIndex != value)
                {
                    pumpIndex = value;
                    this.lbName.Text = (pumpIndex + 1).ToString() + "#";
                    this.Invalidate();
                }
            }
        }

        private string currentStatus = string.Empty;
        public string CurrentStatus
        {
            set
            {
                if (currentStatus != value)
                {
                    currentStatus = value;
                    this.lbStatus.Text = currentStatus;
                    this.Invalidate();
                }
            }
        }

        private string unit = string.Empty;
        public string Unit
        {
            set 
            {
                if (unit != value)
                {
                    unit = value;
                    this.lbPara.Text = currentPara.ToString(numberFormat) + unit;
                    this.Invalidate();
                }
            }
        }

        private double currentPara;
        public double CurrentPara
        {
            set
            {
                if (currentPara != value)
                {
                    currentPara = value;
                    this.lbPara.Text = currentPara.ToString(numberFormat) + unit;
                    this.Invalidate();
                }
            }
        }

        private Bitmap icon = null;
        public Bitmap Icon
        {
            set
            {
                if (icon != value)
                {
                    icon = value;
                    this.Refresh();
                }
            }
        }
    }
}
