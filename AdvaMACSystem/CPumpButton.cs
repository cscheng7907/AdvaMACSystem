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
        }

        public CPumpButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                    e.Graphics.DrawString(c.Text, c.Font, new SolidBrush(c.ForeColor), c.Left +c.Width / 2, c.Top + c.Height / 2, sf);
            }
            if (icon != null)
                e.Graphics.DrawImage(icon, new Rectangle(33 - icon.Width /2, 33 - icon.Height /2, icon.Width, icon.Height), new Rectangle(0, 0, icon.Width, icon.Height), GraphicsUnit.Pixel);
        }

        private int pumpIndex;
        public int PumpIndex
        {
            get { return pumpIndex; }
            set
            {
                pumpIndex = value;
                this.lbName.Text = (pumpIndex + 1).ToString() + "#";
            }
        }

        private string currentStatus = string.Empty;
        public string CurrentStatus
        {
            set
            {
                currentStatus = value;
                this.lbStatus.Text = currentStatus;
            }
        }

        private string unit = string.Empty;
        public string Unit
        {
            set 
            {
                unit = value;
                this.lbPara.Text = currentPara.ToString(numberFormat) + unit;
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
