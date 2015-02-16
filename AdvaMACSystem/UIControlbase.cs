using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AdvaMACSystem
{
    public partial class UIControlbase : UserControl
    {
        public UIControlbase()
        {
            InitializeComponent();
        }
        public virtual void DoEnter()
        {
            UIControlbase uictrl = CurKTUIControl;

            if (CurKTUIControl != null)
            {
                CurKTUIControl.Enabled = false;
                if (CurKTUIControl._prehideonenter)
                    CurKTUIControl.Visible = false;
            }

            if (KTUIControlList != null)
            {
                KTUIControlList.Add(this);

                this.Enabled = true;
                this.Visible = true;
                this.Show();
                this.BringToFront();
                this.Focus();

                if (OnKTUIControlChanged != null)
                    OnKTUIControlChanged(uictrl);
            }
        }

        protected virtual void DoExit()
        {
            if (KTUIControlList != null && KTUIControlList.Remove(this))
            {
                if (_disableonexit)
                    this.Enabled = false;
                if (this._hideonexit)
                    this.Visible = false;

                if (CurKTUIControl != null)
                {
                    CurKTUIControl.Enabled = true;
                    CurKTUIControl.Visible = true;
                    CurKTUIControl.Show();
                    if (this._hideonexit)
                        CurKTUIControl.BringToFront();
                    CurKTUIControl.Focus();
                }

                if (OnKTUIControlChanged != null)
                    OnKTUIControlChanged(this);
            }

        }

        protected bool _hideonexit = true;
        protected bool _disableonexit = true;

        protected bool _prehideonenter = true;

        //KTUIControlList
        private static List<UIControlbase> KTUIControlList = new List<UIControlbase>();
        public static UIControlbase CurKTUIControl
        {
            get
            {
                if ((KTUIControlList == null) ||
                ((KTUIControlList != null) &&
                (KTUIControlList.Count == 0))
                   )
                    return null;
                else
                {
                    return KTUIControlList[KTUIControlList.Count - 1];
                }
            }
        }

        public static event KTUIControlChangedEventHandler OnKTUIControlChanged;
    }


    public delegate void KTUIControlChangedEventHandler(UIControlbase PreUICtrl);
}
