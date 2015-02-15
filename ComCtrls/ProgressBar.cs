using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComCtrls
{
    public partial class CustomProgressBar : Control
    {
        public CustomProgressBar()
        {
            this.warningImage = Resource1.Warning;
            this.settingImage = Resource1.Setting;
            this.bgImage = Resource1.backGround;
            this.valueImage = Resource1.front;
            this.unpassValueImage = Resource1.unpass;
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
            Brush backBrush; //brush for filling a backcolor
            Pen framepen;//pen for draw frame
            TextureBrush backImageBrush = new TextureBrush(this.bgImage);
            TextureBrush frontImageBrush = null;
            if (valuePercentage >= warningPercentage)
                frontImageBrush = new TextureBrush(valueImage);
            else
                frontImageBrush = new TextureBrush(unpassValueImage);

            if (m_bmpOffscreen == null) //Bitmap for doublebuffering
            {
                m_bmpOffscreen = new Bitmap(ClientSize.Width, ClientSize.Height);
            }

            gxOff = Graphics.FromImage(m_bmpOffscreen);

            backBrush = new SolidBrush(Color.White);

            gxOff.Clear(this.BackColor);

            //画背景
            gxOff.FillRectangle(backImageBrush, this.ClientRectangle);

            //画前景
            int valueWidth = (int)(this.ClientRectangle.Width * valuePercentage);
            gxOff.FillRectangle(frontImageBrush, new Rectangle(0, 0, valueWidth, this.ClientRectangle.Height));

            //标注报警值位置
            if (warningImage != null)
            {
                int warningPos = (int)(this.ClientRectangle.Width * warningPercentage);
                imageLeft = warningPos - warningImage.Width / 2;
                imageTop = 0;
                imgRect = new Rectangle(imageLeft, imageTop, warningImage.Width, warningImage.Height);
                gxOff.DrawImage(warningImage, imgRect, new Rectangle(0, 0, warningImage.Width, warningImage.Height), GraphicsUnit.Pixel);
            }

            //标注设定值位置
            if (settingImage != null)
            {
                int settingPos = (int)(this.ClientRectangle.Width * settingPercentage);
                imageLeft = settingPos - settingImage.Width / 2;
                imageTop = 0;
                imgRect = new Rectangle(imageLeft, imageTop, settingImage.Width, settingImage.Height);
                gxOff.DrawImage(settingImage, imgRect, new Rectangle(0, 0, settingImage.Width, settingImage.Height), GraphicsUnit.Pixel);
            }
            
            //Draw from the memory bitmap
            e.Graphics.DrawImage(m_bmpOffscreen, 0, 0);
        }

        private float warningPercentage;
        public float WarningPercentage
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

        private float settingPercentage;
        public float SettingPercentage
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

        private float valuePercentage;
        public float ValuePercentage
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

        private Image bgImage;
        public Image BgImage
        {
            set
            {
                bgImage = value;
            }
        }

        private Image valueImage = null;
        public Image ValueImage
        {
            set
            {
                valueImage = value;
            }
        }

        private Image unpassValueImage = null;
        public Image UnpassValueImage
        {
            set
            {
                unpassValueImage = value;
            }
        }

        private Image warningImage = null;
        public Image WarningImage
        {
            set
            {
                warningImage = value;
            }
        }

        private Image settingImage = null;
        public Image SettingImage
        {
            set
            {
                settingImage = value;
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
