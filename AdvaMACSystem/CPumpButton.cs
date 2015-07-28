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
            {
                Image bgimage = icon;

                //if (!Enabled)
                //    if (!Checked)
                //        bgimage = IMGContainer.UPImgDisaable;
                //    else
                //        bgimage = IMGContainer.DNImgDisable;
                //else
                //    if (!Checked)
                //        bgimage = IMGContainer.UPImg;
                //    else
                //        bgimage = IMGContainer.DNImg;

                if (this.TransParent)
                {
                    //Set transparent key
                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetColorKey(BackgroundImageColor(bgimage), BackgroundImageColor(bgimage));

                    //Draw image
                    gxOff.DrawImage(icon,
                        new Rectangle(40 - icon.Width / 2, 40 - icon.Height / 2, icon.Width, icon.Height),
                        0, 0, icon.Width, icon.Height, GraphicsUnit.Pixel, imageAttr);
                }
                else
                {
                    gxOff.DrawImage(icon,
                        new Rectangle(40 - icon.Width / 2, 40 - icon.Height / 2, icon.Width, icon.Height),
                        new Rectangle(0, 0, icon.Width, icon.Height), GraphicsUnit.Pixel);

                }
                //gxOff.DrawImage(icon, new Rectangle(40 - icon.Width / 2, 40 - icon.Height / 2, icon.Width, icon.Height), new Rectangle(0, 0, icon.Width, icon.Height), GraphicsUnit.Pixel);
            }
            if (warningCount > 0)
            {
                gxOff.FillRectangle(new SolidBrush(Color.Red), 80, 34, 80, 30);
                gxOff.DrawString(warningCount.ToString("D2"), new System.Drawing.Font("Microsoft YaHei", 20F, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Yellow), 120, 49, sf);
            }

            switch (type)
            {
                case PumpType.ptEmpty:
                    break;
                case PumpType.ptControlled:
                    gxOff.DrawString("被控站", new System.Drawing.Font("Microsoft YaHei", 20F, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), 120, 81, sf);
                    break;
                case PumpType.ptRedundancy:
                    gxOff.DrawString("冗余站", new System.Drawing.Font("Microsoft YaHei", 20F, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), 120, 81, sf);
                    break;
            }


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
            get { return currentPara; }
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

        //报警数
        private int warningCount = 0;
        public int WarningCount
        {
            set
            {
                if (warningCount != value)
                {
                    warningCount = value;
                    this.Refresh();
                }
            }

        }

        //被控站和冗余站信息
        private PumpType type = PumpType.ptEmpty;
        public PumpType Type
        {
            set
            {
                if (type != value)
                {
                    type = value;
                    this.Refresh();
                }
            }
        }
    }

    public enum PumpType
    {
        ptEmpty = 0,
        ptControlled = 1,
        ptRedundancy = 2
    }
}
