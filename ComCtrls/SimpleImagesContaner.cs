using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ComCtrls
{
    public partial class SimpleImagesContaner : Component
    {
        public SimpleImagesContaner()
        {
            InitializeComponent();
        }

        public SimpleImagesContaner(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        //private List<Control> ownerlst = new List<Control>();

        public void AddOwner(Control Owner)
        {
            //ownerlst.Add(Owner);
        }

        public void RemoveOwner(Control Owner)
        {
            //ownerlst.Remove(Owner);
        }

        public void DoRefresh()
        {
            //foreach (Control c in ownerlst)
            //{
            //    if (c != null)
            //        c.Invalidate();
            //}
        }

        private Image backimg = null;
        public Image BackImg
        {
            get
            {
                return backimg;
            }
            set
            {
                if (backimg != value)
                {
                    backimg = value;
                    this.DoRefresh();
                }
            }
        }

        private Image checkedBackimg = null;
        public Image CheckedBackImg
        {
            get
            {
                return checkedBackimg;
            }
            set
            {
                if (checkedBackimg != value)
                {
                    checkedBackimg = value;
                    this.DoRefresh();
                }
            }
        }

        private Image imgdisable = null;
        public Image ImgDisable
        {
            get
            {
                return imgdisable;
            }
            set
            {
                if (imgdisable != value)
                {
                    imgdisable = value;
                    this.DoRefresh();
                }
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
                if (icon != value)
                {
                    icon = value;
                    this.DoRefresh();
                }
            }
        }

    }
}
