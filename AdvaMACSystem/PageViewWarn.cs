using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AdvaMACSystem
{
    public partial class PageViewWarn : UIControlbase
    {
        public PageViewWarn()
        {
            InitializeComponent();
        }

        private bool _isreal = false;
        public bool IsReal
        {
            get { return _isreal; }
            set
            {
                if (_isreal != value)
                {
                    _isreal = value;
                    label1.Text = "系统报警信息-" +
                       ((_isreal) ? "实时数据" : "历史数据");
                }
            }
        }
    }
}
