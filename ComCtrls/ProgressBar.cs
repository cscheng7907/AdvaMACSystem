using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComCtrls
{
    public class CProgressBarImagesContainer : Component
    {

        public CProgressBarImagesContainer()
        {
            ownerlst = new List<Control>();
        }

        private List<Control> ownerlst = null;

        public void AddOwner(Control Owner)
        {
            ownerlst.Add(Owner);
        }

        public void RemoveOwner(Control Owner)
        {
            ownerlst.Remove(Owner);
        }

        public void DoRefresh()
        {
            foreach (Control c in ownerlst)
            {
                c.Invalidate();
            }
        }

        private Image bgImage = null;
        public Image BgImage
        {
            get
            {
                return bgImage;
            }
            set
            {
                bgImage = value;
                this.DoRefresh();
            }
        }

        private Image frontImage = null;
        public Image FrontImage
        {
            get
            {
                return frontImage;
            }
            set
            {
                frontImage = value;
                this.DoRefresh();
            }
        }

        private Image frontImage_Unpass = null;
        public Image FrontImage_Unpass
        {
            get
            {
                return frontImage_Unpass;
            }
            set
            {
                frontImage_Unpass = value;
                this.DoRefresh();
            }
        }

        private Image warningImage = null;
        public Image WarningImage
        {
            get
            {
                return warningImage;
            }
            set
            {
                warningImage = value;
                this.DoRefresh();
            }
        }
        private Image settingImage = null;
        public Image SettingImage
        {
            get
            {
                return settingImage;
            }
            set
            {
                settingImage = value;
                this.DoRefresh();
            }
        }

    }


    public partial class CustomProgressBar : Control
    {
        public CustomProgressBar()
        {
            InitializeComponent();
            this.Size = new Size(300, 30);
            this.TabStop = false;
        }

        private Bitmap m_bmpOffscreen;
        protected override void OnResize(EventArgs e)
        {
            if (m_bmpOffscreen == null) //Bitmap for doublebuffering
            {
                m_bmpOffscreen = new Bitmap(ClientSize.Width, ClientSize.Height);
            }
            else
            {
                m_bmpOffscreen.Dispose();

                m_bmpOffscreen = new Bitmap(ClientSize.Width, ClientSize.Height);
            }

            base.OnResize(e);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //do nothing
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics gxOff; //Offscreen graphics
            Rectangle imgRect; //image rectangle
            int imageLeft;
            int imageTop;
            Brush backImageBrush = null;
            Brush frontImageBrush = null;
            Pen cutOffRule;
            if (m_bmpOffscreen == null) //Bitmap for doublebuffering
            {
                m_bmpOffscreen = new Bitmap(ClientSize.Width, ClientSize.Height);
            }

            gxOff = Graphics.FromImage(m_bmpOffscreen);

            gxOff.Clear(this.BackColor);

            if (imglst != null)
            {
                if (this.IMGContainer.BgImage != null)
                    backImageBrush = new TextureBrush(this.IMGContainer.BgImage);
                else
                    backImageBrush = new SolidBrush(this.BackColor);

                if (valuePercentage >= warningPercentage)
                {
                    if (this.IMGContainer.FrontImage != null)
                        frontImageBrush = new TextureBrush(this.IMGContainer.FrontImage);
                    else
                        frontImageBrush = new SolidBrush(this.ForeColor);
                }
                else
                {
                    if (this.IMGContainer.FrontImage_Unpass != null)
                        frontImageBrush = new TextureBrush(this.IMGContainer.FrontImage_Unpass);
                    else if (this.IMGContainer.FrontImage != null)
                        frontImageBrush = new TextureBrush(this.IMGContainer.FrontImage);
                    else
                        frontImageBrush = new SolidBrush(this.ForeColor);
                }
            }
            else
            {
                backImageBrush = new SolidBrush(this.BackColor);
                frontImageBrush = new SolidBrush(this.ForeColor);
            }
            //画背景
            gxOff.FillRectangle(backImageBrush, this.ClientRectangle);

            //画前景
            int valueWidth = (int)(this.ClientRectangle.Width * valuePercentage);
            gxOff.FillRectangle(frontImageBrush, new Rectangle(0, 0, valueWidth, this.ClientRectangle.Height));

            if (imglst != null)
            {
                //标注报警值位置
                if (IMGContainer.WarningImage != null)
                {
                    int warningPos = (int)(this.ClientRectangle.Width * warningPercentage);
                    imageLeft = warningPos - IMGContainer.WarningImage.Width / 2;
                    imageTop = 0;
                    imgRect = new Rectangle(imageLeft, imageTop, IMGContainer.WarningImage.Width, IMGContainer.WarningImage.Height);
                    gxOff.DrawImage(IMGContainer.WarningImage, imgRect, new Rectangle(0, 0, IMGContainer.WarningImage.Width, IMGContainer.WarningImage.Height), GraphicsUnit.Pixel);
                }
                else
                {
                    int warningPos = (int)(this.ClientRectangle.Width * warningPercentage);
                    if (warningPos > 0)
                    {
                        cutOffRule = new Pen(Color.Red, 2);
                        gxOff.DrawLine(cutOffRule, warningPos, this.ClientRectangle.Top, warningPos, this.ClientRectangle.Bottom);
                    }
                }
                //标注设定值位置
                if (IMGContainer.SettingImage != null)
                {
                    int settingPos = (int)(this.ClientRectangle.Width * settingPercentage);
                    imageLeft = settingPos - IMGContainer.SettingImage.Width / 2;
                    imageTop = 0;
                    imgRect = new Rectangle(imageLeft, imageTop, IMGContainer.SettingImage.Width, IMGContainer.SettingImage.Height);
                    gxOff.DrawImage(IMGContainer.SettingImage, imgRect, new Rectangle(0, 0, IMGContainer.SettingImage.Width, IMGContainer.SettingImage.Height), GraphicsUnit.Pixel);
                }
                else
                {
                    int settingPos = (int)(this.ClientRectangle.Width * settingPercentage);
                    if (settingPos > 0)
                    {
                        cutOffRule = new Pen(Color.Blue, 2);
                        gxOff.DrawLine(cutOffRule, settingPos, this.ClientRectangle.Top, settingPos, this.ClientRectangle.Bottom);
                    }
                }
            }

            //Draw from the memory bitmap
            e.Graphics.DrawImage(m_bmpOffscreen, 0, 0);
        }

        private double warningPercentage;
        public double WarningPercentage
        {
            set
            {
                if (warningPercentage != value)
                {
                    warningPercentage = value;
                    this.Invalidate();
                }
            }

        }

        private double settingPercentage;
        public double SettingPercentage
        {
            set
            {
                if (settingPercentage != value)
                {
                    settingPercentage = value;
                    this.Invalidate();
                }
            }
        }

        private double valuePercentage;
        public double ValuePercentage
        {
            set
            {
                if (valuePercentage != value)
                {
                    valuePercentage = value;
                    this.Invalidate();
                }
            }
        }

        private CProgressBarImagesContainer imglst = null;
        public CProgressBarImagesContainer IMGContainer
        {
            get
            {
                return imglst;
            }
            set
            {
                if (imglst != value)
                {
                    if (imglst != null)
                        imglst.RemoveOwner(this);

                    imglst = value;

                    if (value != null)
                        value.AddOwner(this);

                    this.Invalidate();
                }
            }
        }

        public void SetWarningAndSettingPosition(float warningPercent, float settingPercent)
        {
            if (this.warningPercentage != warningPercent)
                this.warningPercentage = warningPercent;
            if (this.settingPercentage != settingPercent)
                this.settingPercentage = settingPercent;
            this.Invalidate();
        }
    }
}
