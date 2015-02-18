using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ComCtrls
{
    //添加了ImageButton 的自绘背景及图标的功能，并通过聚合ImagesContaner以优化资源分配 by cs at 2009-1-20  {295CEBAC-5099-403c-90BF-DD86BC58264D} 
    public class ImagesContaner : Component
    {

        public ImagesContaner()
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

        private Image upimg = null;
        public Image UPImg
        {
            get
            {
                return upimg;
            }
            set
            {
                upimg = value;
                this.DoRefresh();
            }
        }

        private Image upimgdisable = null;
        public Image UPImgDisable
        {
            get
            {
                return upimgdisable;
            }
            set
            {
                upimgdisable = value;
                this.DoRefresh();
            }
        }

        private Image dnimg = null;
        public Image DNImg
        {
            get
            {
                return dnimg;
            }
            set
            {
                dnimg = value;
                this.DoRefresh();
            }
        }
        private Image dnimgdisable = null;
        public Image DNImgDisable
        {
            get
            {
                return dnimgdisable;
            }
            set
            {
                dnimgdisable = value;
                this.DoRefresh();
            }
        }

        private Image icon = null;
        public Image Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
                this.DoRefresh();
            }
        }
    }


    public enum KTLayout
    {
        GlyphTop = 0,
        GlyphButtom = 1,
        GlyphLeft = 2,
        GlyphRight = 3

    }

    public partial class ImageButton : Control
    {
        public ImageButton()
        {
            InitializeComponent();
            InitKTImageButton();
        }

        public ImageButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            InitKTImageButton();
        }

        private void InitKTImageButton()
        {
            this.Size = new Size(21, 8);

            this.TabStop = false;

            this.GotFocus += new EventHandler(BtnGotFocus);
            this.LostFocus += new EventHandler(BtnLostFocus);
        }

        private void BtnGotFocus(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void BtnLostFocus(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private Bitmap m_bmpOffscreen;


        #region key mouse


        public void DoKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }


        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            //修正了KeyUp或KeyDown事件连续相应的问题，并消除了不同按键同时按下时产生的干扰 by cs at  2009-3-13 9:31:04 {D816688A-9ED7-4e22-B258-A6469D6C2D16}
            //if (IsKeyDown)
            //if (IsKeyDown && e.Handled)
            //if (kv == e.KeyValue)
            //{
            //try
            //{
            //MessageBox.Show(e.KeyData.ToString() + "+" + ShortcutKeys.ToString());
            //if (tempkey == e.KeyData)
            //{
            if ((this.Enabled) && (e.KeyData == ShortcutKeys) && kv == e.KeyValue)
            {
                kv = -1;

                if (!this.toggle)
                    Checked = !Checked;


                e.Handled = false;

                KeyEventArgs f = new KeyEventArgs(Keys.Space);

                f.Handled = true;

                base.OnKeyUp(f);
            }
            //}
            //}
            //finally
            //{
            //    IsKeyDown = false;
            //}
            //}

        }

        public void DoKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }

        //private bool IsKeyDown = false;
        //private Keys tempkey = Keys.None;
        //修正了KeyUp或KeyDown事件连续相应的问题，并消除了不同按键同时按下时产生的干扰 by cs at  2009-3-13 9:31:04 {D816688A-9ED7-4e22-B258-A6469D6C2D16}
        static private int kv = -1;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //修正了KeyUp或KeyDown事件连续相应的问题，并消除了不同按键同时按下时产生的干扰 by cs at  2009-3-13 9:31:04 {D816688A-9ED7-4e22-B258-A6469D6C2D16}
            //if (!IsKeyDown && e.Handled)
            //if (kv == -1)
            //{
            //IsKeyDown = true;

            //try
            //{

            if ((this.Enabled) && (e.KeyData == ShortcutKeys) && kv == -1)
            {
                Checked = !Checked;

                //e.Handled = false;
                kv = e.KeyValue;

                KeyEventArgs f = new KeyEventArgs(Keys.Space);
                f.Handled = true;

                base.OnKeyDown(f);
            }
            //}
            //finally
            //{
            //    //tempkey = e.KeyData;
            //}
            //}

        }


        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            //bPushed = true;
            if (this.Enabled)
            {
                Checked = !Checked;
                this.Focus();

                this.Invalidate();

                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            //bPushed = false;
            if (this.Enabled)
            {
                if (!this.toggle)
                    Checked = !Checked;

                this.Invalidate();

                base.OnMouseUp(e);
            }
        }

        #endregion

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
            //Do nothing
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Graphics gxOff; //Offscreen graphics
            Rectangle imgRect; //image rectangle
            Brush backBrush; //brush for filling a backcolor
            Pen framepen;//pen for draw frame

            if (m_bmpOffscreen == null) //Bitmap for doublebuffering
            {
                m_bmpOffscreen = new Bitmap(ClientSize.Width, ClientSize.Height);
            }

            gxOff = Graphics.FromImage(m_bmpOffscreen);

            /*
            if (!Checked)
                backBrush = new SolidBrush(Parent.BackColor);
            else //change the background when it's pressed
                backBrush = new SolidBrush(Color.LightGray);

            */

            if (!Checked)
                backBrush = new SolidBrush(UpColor);
            else //change the background when it's pressed
                backBrush = new SolidBrush(DownColor);

            gxOff.Clear(this.BackColor);

            //backBrush = new SolidBrush(this.BackColor);

            gxOff.FillRectangle(backBrush, this.ClientRectangle);

            //添加了ImageButton 的自绘背景及图标的功能，并通过聚合ImagesContaner以优化资源分配 by cs at 2009-1-20  {295CEBAC-5099-403c-90BF-DD86BC58264D} 
            //if (bgimage != null)
            if ((IMGContainer != null) &&
                (IMGContainer.DNImg != null) &&
                (IMGContainer.DNImgDisable != null) &&
                (IMGContainer.UPImgDisable != null) &&
                (IMGContainer.UPImg != null)
                )
            {
                Image bgimage;

                if (!Enabled)
                    if (!Checked)
                        bgimage = IMGContainer.UPImgDisable;
                    else
                        bgimage = IMGContainer.DNImgDisable;
                else
                    if (!Checked)
                        bgimage = IMGContainer.UPImg;
                    else
                        bgimage = IMGContainer.DNImg;

                //Center the image relativelly to the control
                int imageLeft = (this.Width - bgimage.Width) / 2;
                int imageTop = (this.Height - bgimage.Height) / 2;

                imgRect = new Rectangle(imageLeft, imageTop, bgimage.Width, bgimage.Height);

                //if (!Checked)
                //    imgRect = new Rectangle(imageLeft, imageTop, bgimage.Width, bgimage.Height);
                //else //The button was pressed
                //    //Shift the image by one pixel
                //    imgRect = new Rectangle(imageLeft + 1, imageTop + 1, bgimage.Width, bgimage.Height);

                if (this.TransParent)
                {
                    //Set transparent key
                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetColorKey(BackgroundImageColor(bgimage), BackgroundImageColor(bgimage));

                    //Draw image
                    gxOff.DrawImage(bgimage, imgRect, 0, 0, bgimage.Width, bgimage.Height, GraphicsUnit.Pixel, imageAttr);
                }
                else
                {
                    gxOff.DrawImage(bgimage, imgRect, new Rectangle(0, 0, bgimage.Width, bgimage.Height), GraphicsUnit.Pixel);

                }
            }
            else if ((DNImg != null) &&
                (DNImgDisable != null) &&
                (UPImgDisable != null) &&
                (UPImg != null)
                )
            {
                Image bgimage;

                if (!Enabled)
                    if (!Checked)
                        bgimage = UPImgDisable;
                    else
                        bgimage = DNImgDisable;
                else
                    if (!Checked)
                        bgimage = UPImg;
                    else
                        bgimage = DNImg;

                //Center the image relativelly to the control
                int imageLeft = (this.Width - bgimage.Width) / 2;
                int imageTop = (this.Height - bgimage.Height) / 2;

                imgRect = new Rectangle(imageLeft, imageTop, bgimage.Width, bgimage.Height);

                //if (!Checked)
                //    imgRect = new Rectangle(imageLeft, imageTop, bgimage.Width, bgimage.Height);
                //else //The button was pressed
                //    //Shift the image by one pixel
                //    imgRect = new Rectangle(imageLeft + 1, imageTop + 1, bgimage.Width, bgimage.Height);

                if (this.TransParent)
                {
                    //Set transparent key
                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetColorKey(BackgroundImageColor(bgimage), BackgroundImageColor(bgimage));

                    //Draw image
                    gxOff.DrawImage(bgimage, imgRect, 0, 0, bgimage.Width, bgimage.Height, GraphicsUnit.Pixel, imageAttr);
                }
                else
                {
                    gxOff.DrawImage(bgimage, imgRect, new Rectangle(0, 0, bgimage.Width, bgimage.Height), GraphicsUnit.Pixel);

                }
            }
            else if ((DNImg != null) &&
                //(DNImgDisable != null) &&
                //(UPImgDisable != null) &&
                (UPImg != null)
                )
            {
                Image bgimage;

                //if (!Enabled)
                //    if (!Checked)
                //        bgimage = UPImgDisable;
                //    else
                //        bgimage = DNImgDisable;
                //else
                if (!Checked)
                    bgimage = UPImg;
                else
                    bgimage = DNImg;

                //Center the image relativelly to the control
                int imageLeft = (this.Width - bgimage.Width) / 2;
                int imageTop = (this.Height - bgimage.Height) / 2;

                imgRect = new Rectangle(imageLeft, imageTop, bgimage.Width, bgimage.Height);

                //if (!Checked)
                //    imgRect = new Rectangle(imageLeft, imageTop, bgimage.Width, bgimage.Height);
                //else //The button was pressed
                //    //Shift the image by one pixel
                //    imgRect = new Rectangle(imageLeft + 1, imageTop + 1, bgimage.Width, bgimage.Height);

                if (this.TransParent)
                {
                    //Set transparent key
                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetColorKey(BackgroundImageColor(bgimage), BackgroundImageColor(bgimage));

                    //Draw image
                    gxOff.DrawImage(bgimage, imgRect, 0, 0, bgimage.Width, bgimage.Height, GraphicsUnit.Pixel, imageAttr);
                }
                else
                {
                    gxOff.DrawImage(bgimage, imgRect, new Rectangle(0, 0, bgimage.Width, bgimage.Height), GraphicsUnit.Pixel);

                }
            }
            else //draw frame
            {
                if (!Checked) //up
                {
                    //gray
                    framepen = new Pen(Color.Gray, 1);
                    gxOff.DrawRectangle(framepen, 0, 0, ClientSize.Width - 2, ClientSize.Height - 2);


                    //white
                    framepen = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(127)))), ((int)(((byte)(181))))), 1);
                    gxOff.DrawLine(framepen, 1, 1, ClientSize.Width - 3, 1);
                    gxOff.DrawLine(framepen, 1, 1, 1, ClientSize.Height - 3);

                    //black
                    framepen = new Pen(Color.Black, 1);
                    gxOff.DrawLine(framepen, ClientSize.Width - 1, ClientSize.Height - 1, 0, ClientSize.Height - 1);
                    gxOff.DrawLine(framepen, ClientSize.Width - 1, ClientSize.Height - 1, ClientSize.Width - 1, 0);

                }
                else//down
                {
                    //white
                    framepen = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(86)))), ((int)(((byte)(111))))), 1);
                    gxOff.DrawLine(framepen, ClientSize.Width - 1, ClientSize.Height - 1, 0, ClientSize.Height - 1);
                    gxOff.DrawLine(framepen, ClientSize.Width - 1, ClientSize.Height - 1, ClientSize.Width - 1, 0);

                    //black
                    framepen = new Pen(Color.Black, 1);
                    gxOff.DrawLine(framepen, 0, 0, ClientSize.Width - 1, 0);
                    gxOff.DrawLine(framepen, 0, 0, 0, ClientSize.Height - 1);

                    gxOff.DrawLine(framepen, 1, 1, ClientSize.Width - 2, 1);
                    gxOff.DrawLine(framepen, 1, 1, 1, ClientSize.Height - 2);
                }

                //draw fucus frame
                //if (this.Focused)
                //{
                //    framepen = new Pen(Color.Black, 1);

                //    framepen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                //    gxOff.DrawRectangle(framepen, 4, 4, ClientSize.Width - 10, ClientSize.Height - 10);
                //}
            }


            //todo: icon
            //添加了ImageButton 的自绘背景及图标的功能，并通过聚合ImagesContaner以优化资源分配 by cs at 2009-1-20  {295CEBAC-5099-403c-90BF-DD86BC58264D} 
            //if (Icon != null)
            if ((IMGContainer != null) &&
                (IMGContainer.Icon != null))
            {
                //draw icon and text

                //Layout

                //Center the image relativelly to the control
                int imageLeft = (this.Width - IMGContainer.Icon.Width) / 2;
                int imageTop = (this.Height - IMGContainer.Icon.Height) / 2;

                if (!Checked)
                    imgRect = new Rectangle(imageLeft, imageTop, IMGContainer.Icon.Width, IMGContainer.Icon.Height);
                else //The button was pressed
                    //Shift the image by one pixel
                    imgRect = new Rectangle(imageLeft + 1, imageTop + 1, IMGContainer.Icon.Width, IMGContainer.Icon.Height);

                if (this.TransParent)
                {
                    //Set transparent key
                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetColorKey(BackgroundImageColor(IMGContainer.Icon), BackgroundImageColor(IMGContainer.Icon));

                    //Draw image
                    gxOff.DrawImage(IMGContainer.Icon, imgRect, 0, 0, IMGContainer.Icon.Width, IMGContainer.Icon.Height, GraphicsUnit.Pixel, imageAttr);
                }
                else
                {
                    gxOff.DrawImage(IMGContainer.Icon, imgRect, new Rectangle(0, 0, IMGContainer.Icon.Width, IMGContainer.Icon.Height), GraphicsUnit.Pixel);

                }
            }
            else
                if (Text != string.Empty)//todo:text no icon
                {
                    if (this.Enabled)
                    {
                        backBrush = new SolidBrush(this.ForeColor);
                    }
                    else
                    {
                        backBrush = new SolidBrush(Color.Gray);
                    }

                    gxOff.DrawString(this.Text, this.Font, backBrush,
                        (this.ClientSize.Width - gxOff.MeasureString(this.Text, this.Font).Width) / 2,
                        (this.ClientSize.Height - gxOff.MeasureString(this.Text, this.Font).Height) / 2);
                }

            //Draw from the memory bitmap
            e.Graphics.DrawImage(m_bmpOffscreen, 0, 0);

            //base.OnPaint(e);
        }

        #region Property
        private Color upcolor = System.Drawing.SystemColors.Control;
        public Color UpColor
        {
            get
            {
                return upcolor;
            }
            set
            {
                if (upcolor != value)
                {
                    upcolor = value;
                    this.Invalidate();
                }
            }
        }

        private Color dncolor = System.Drawing.SystemColors.Control;
        public Color DownColor
        {
            get
            {
                return dncolor;
            }
            set
            {
                if (dncolor != value)
                {
                    dncolor = value;
                    this.Invalidate();
                }
            }
        }











        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
                this.Refresh();
                //this.Invalidate();
            }
        }



        private bool transparent;
        [DefaultValue(false)]
        public bool TransParent
        {
            get
            {
                return transparent;
            }
            set
            {
                transparent = value;
                this.Invalidate();
            }
        }

        //private Image bgimage = null;
        //public Image BgImage
        //{
        //    get
        //    {
        //        return bgimage;
        //    }
        //    set
        //    {
        //        bgimage = value;
        //        this.Invalidate();
        //    }
        //}

        //private Image icon = null;
        //public Image Icon
        //{
        //    get
        //    {
        //        return icon;
        //    }
        //    set
        //    {
        //        icon = value;
        //        this.Invalidate();
        //    }
        //}

        //添加了ImageButton 的自绘背景及图标的功能，并通过聚合ImagesContaner以优化资源分配 by cs at 2009-1-20  {295CEBAC-5099-403c-90BF-DD86BC58264D} 
        private ImagesContaner imglst = null;
        public ImagesContaner IMGContainer
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

        private Keys shortcutKeys;
        [DefaultValue(Keys.None)]//, UpdatableProperty, Localizable(true)]
        public Keys ShortcutKeys
        {
            get { return shortcutKeys; }
            set
            {
                if (shortcutKeys != value)
                    shortcutKeys = value;
            }
        }

        private bool toggle;
        [DefaultValue(false)]
        public bool Toggle
        {
            get
            {
                return toggle;
            }
            set
            {
                toggle = value;

            }
        }

        public event EventHandler CheckedChanged;


        private void OnCheckedChanged()
        {
            if (this.CheckedChanged != null)
            {
                EventArgs e = new EventArgs();

                CheckedChanged(this, e);
            }
        }

        private bool cChecked;
        [DefaultValue(false)]
        public bool Checked
        {
            get
            {
                return cChecked;
            }
            set
            {
                if (cChecked != value)
                {
                    cChecked = value;

                    OnCheckedChanged();

                    this.Invalidate();
                }
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;

                this.Invalidate();
            }
        }

        private KTLayout layout;
        [DefaultValue(KTLayout.GlyphTop)]
#if _WIN32
        public new KTLayout Layout
#else
        public  KTLayout Layout
#endif
        {
            get
            {
                return layout;
            }
            set
            {
                if (layout != value)
                {
                    layout = value;
                    this.Invalidate();
                }
            }
        }



        #endregion

        private Color BackgroundImageColor(Image image)
        {
            Bitmap bmp = new Bitmap(image);
            return bmp.GetPixel(0, 0);
        }

        private Image upimg = null;
        public Image UPImg
        {
            get
            {
                return upimg;
            }
            set
            {
                upimg = value;
                this.Invalidate();
            }
        }

        private Image upimgdisable = null;
        public Image UPImgDisable
        {
            get
            {
                return upimgdisable;
            }
            set
            {
                upimgdisable = value;
                this.Invalidate();
            }
        }

        private Image dnimg = null;
        public Image DNImg
        {
            get
            {
                return dnimg;
            }
            set
            {
                dnimg = value;
                this.Invalidate();
            }
        }

        private Image dnimgdisable = null;
        public Image DNImgDisable
        {
            get
            {
                return dnimgdisable;
            }
            set
            {
                dnimgdisable = value;
                this.Invalidate();
            }
        }

    }
}