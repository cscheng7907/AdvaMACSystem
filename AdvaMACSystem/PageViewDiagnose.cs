using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ComCtrls;
using DataPool;

namespace AdvaMACSystem
{
    public partial class PageViewDiagnose : UIControlbase
    {
        public PageViewDiagnose()
        {
            InitializeComponent();

            cylinderList = new List<ImageLabel>();
            buttonList = new List<ImageButton>();
            buttonImage = new ImagesContaner();
            cylinderImage = new ComCtrls.SimpleImagesContaner();
            cylinderImage.BackImg = AdvaMACSystemRes.gray_off;
            cylinderImage.ImgDisable = AdvaMACSystemRes.gray_off;
            cylinderImage.CheckedBackImg = AdvaMACSystemRes.green_on;

            currentFont = new Font("微软雅黑", 14F, FontStyle.Regular);
            //new cylinders
            for (int i = 0; i < 32; i++)
            {
                ImageLabel cylinder = new ImageLabel();
                cylinderList.Add(cylinder);
            }
            for (int i = 0; i < 2; i++)
            {
                ImageButton button = new ImageButton();
                buttonList.Add(button);
            }
            this.SuspendLayout();
            for (int i = 0; i < 32; i++)
            {
                ImageLabel cylinder = cylinderList[i];
                cylinder.Size = new Size(IOWidth, IOHeight);
                cylinder.Location = new Point(IOMarginLeft + (i / 8) * (IOWidth + IOSpacingX), IOMarginTop + (i % 8 +3) * (IOHeight + IOSpacingY));
                cylinder.IMGContainer = cylinderImage;
                cylinder.Font = currentFont;
                cylinder.TextX = 50;
                cylinder.TextY = 5;
                cylinder.Text = String.Format("{0}#油缸", i % 8 + 1);
                //cylinder.Checked =( i % 2 )== 0;
                this.Controls.Add(cylinder);
            }
            for (int i = 0; i < 2; i++)
            {
                ImageButton button = buttonList[i];
                button.Size = new Size(ButtonWidth, ButtonHeight);
                button.Location = new Point(IOMarginLeft + i * (ButtonWidth + BUttonSpacingX), this.Height - 60);
                button.IMGContainer = buttonImage;
                this.Controls.Add(button);
            }
            this.ResumeLayout(false);
        }

        private int pumpNumber = 4;
        private int cylinderNumber = 8;
        private List<ImageLabel> cylinderList = null;//四泵三十二缸列表
        private List<ImageButton> buttonList = null;//切换按钮列表
        private ImagesContaner buttonImage = null;//按钮背景图
        private ComCtrls.SimpleImagesContaner cylinderImage = null;//IO标签背景图

        #region 布局
        private Font currentFont = null;//字体
        private int IOMarginTop = 80;//第一行IO标签与顶端方向间距
        private int IOMarginLeft = 20;//第一列IO标签与左端方向间距
        private int IOWidth = 235;//IO标签宽度
        private int IOHeight = 35;//IO标签高度
        private int IOSpacingX = 15;//IO标签之间X方向间距
        private int IOSpacingY = 5;//IO标签之间Y方向间距

        private int ButtonWidth = 235;
        private int ButtonHeight = 50;
        private int BUttonSpacingX = 15;
        #endregion

        #region 属性
        private CDataPool _candatapool = null;
        public CDataPool CanDatapool
        {
            get { return _candatapool; }
            set
            {
                if (_candatapool != value)
                    _candatapool = value;
            }
        }
        #endregion

        #region 方法
        public void DoRefresh()
        {
            if (_candatapool != null)
            {
                //取数据
                //赋值ImageLabel
            }
        }
        #endregion

        private void timer_Refresh_Tick(object sender, EventArgs e)
        {
            DoRefresh();
        }
    }
}
