using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ComCtrls
{
    //添加了ImageButton 的自绘背景及图标的功能，并通过聚合ImagesContaner以优化资源分配 by cs at 2009-1-20  {295CEBAC-5099-403c-90BF-DD86BC58264D} 
    public partial class ImagesContaner : Component
    {
        public ImagesContaner()
        {
            InitializeComponent();
        }

        public ImagesContaner(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

       // private List<Control> ownerlst = new List<Control>();

        public void AddOwner(Control Owner)
        {
            //if (Owner != null)
            //    ownerlst.Add(Owner);
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

        private Bitmap upimg = null;
        public Bitmap UPImg
        {
            get
            {
                return upimg;
            }
            set
            {
                if (upimg != value)
                {
                    upimg = value;
                    this.DoRefresh();
                }
            }
        }

        private Bitmap upimgdisaable = null;
        public Bitmap UPImgDisaable
        {
            get
            {
                return upimgdisaable;
            }
            set
            {
                if (upimgdisaable != value)
                {
                    upimgdisaable = value;
                    this.DoRefresh();
                }
            }
        }

        private Bitmap dnimg = null;
        public Bitmap DNImg
        {
            get
            {
                return dnimg;
            }
            set
            {
                if (dnimg != value)
                {
                    dnimg = value;
                    this.DoRefresh();
                }
            }
        }
        private Bitmap dnimgdisable = null;
        public Bitmap DNImgDisable
        {
            get
            {
                return dnimgdisable;
            }
            set
            {
                if (dnimgdisable != value)
                {
                    dnimgdisable = value;
                    this.DoRefresh();
                }
            }
        }

        private Bitmap icon = null;
        public Bitmap Icon
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
