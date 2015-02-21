using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ComCtrls;
using System.Drawing;

namespace AdvaMACSystem
{
    public partial class CParaLabel : ImageLabel
    {
        public CParaLabel()
        {
            InitializeComponent();
            initialize();
        }

        public CParaLabel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            initialize();
        }

        private int paraOffsetX = 210;
        private int paraOffsetY = 5;

        private void initialize()
        {
        }

        private string para = string.Empty;
        public string Para
        {
            set
            {
                if (para != value)
                {
                    para = value;
                    this.Invalidate();
                }
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawString(para, this.Font, new SolidBrush(this.ForeColor), paraOffsetX - e.Graphics.MeasureString(para, this.Font).Width, paraOffsetY);
        }
    }
}
