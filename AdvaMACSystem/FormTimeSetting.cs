using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComCtrls;

namespace AdvaMACSystem
{
    public partial class FormTimeSetting : Form
    {
        public FormTimeSetting()
        {
            InitializeComponent();
            currentFont = new Font("微软雅黑", 14F, FontStyle.Regular);
            dateImage = new SimpleImagesContaner();
            dateImage.BackImg = dateImage.CheckedBackImg
                = dateImage.ImgDisable = AdvaMACSystemRes.Dgn_disable;

            inputImage = new SimpleImagesContaner();
            inputImage.BackImg = inputImage.CheckedBackImg
                = inputImage.ImgDisable = AdvaMACSystemRes.Input40x40;

            buttonImage = new ImagesContaner();
            buttonImage.DNImg = AdvaMACSystemRes.Dgn_down;
            buttonImage.UPImg = AdvaMACSystemRes.Dgn_up;
            buttonImage.UPImgDisaable = buttonImage.DNImgDisable = AdvaMACSystemRes.Dgn_disable;

            buttonList = new List<ImageButton>();
            ibPressure = new ImageButton();
            buttonList.Add(ibPressure);
            ibPosition = new ImageButton();
            buttonList.Add(ibPosition);
            ibBack = new ImageButton();
            buttonList.Add(ibBack);
            ibEnter = new ImageButton();
            buttonList.Add(ibEnter);

            lbTimeSetting = new Label();
            inputLabelList = new List<ImageLabel>();
            ilStartHour = new ImageLabel();
            ilStartHour.Click += new EventHandler(ilStartHour_Click);
            inputLabelList.Add(ilStartHour);
            ilStartMinute = new ImageLabel();
            ilStartMinute.Click += new EventHandler(ilStartMinute_Click);
            inputLabelList.Add(ilStartMinute);
            ilHourSpan = new ImageLabel();
            inputLabelList.Add(ilHourSpan);
            ilHourSpan.Click += new EventHandler(ilHourSpan_Click);
            ilMinuteSpan = new ImageLabel();
            inputLabelList.Add(ilMinuteSpan);
            ilMinuteSpan.Click += new EventHandler(ilMinuteSpan_Click);

            for (int i = 0; i < inputLabelList.Count; i++)
            {
                inputLabelList[i].Size = new Size(40, 40);
                inputLabelList[i].Location = new Point(LBTimeMarginLeft + i * (40 + LBTimeSpacingX), LBTimeMarginTop);
                inputLabelList[i].Font = currentFont;
                inputLabelList[i].IMGContainer = inputImage;
                this.Controls.Add(inputLabelList[i]);
            }

            for (int k = 0; k < buttonList.Count; k++)
            {
                ImageButton button = buttonList[k];
                button.Size = new Size(ButtonWidth, ButtonHeight);
                button.Location = new Point(ButtonMarginLeft + (k % 2) * (ButtonWidth + ButtonSpacingX), ButtonMarginTop + (k / 2) * (ButtonHeight + ButtonSpacingY));
                button.Font = currentFont;
                button.IMGContainer = buttonImage;
                this.Controls.Add(button);
            }
            ibPressure.Text = "压力记录";
            ibPressure.Name = "Pressure";
            ibPressure.Toggle = true;
            ibPressure.CheckedChanged += new EventHandler(dataTypeChanged);

            ibPosition.Text = "位置记录";
            ibPosition.Name = "Position";
            ibPosition.Toggle = true;
            ibPosition.CheckedChanged += new EventHandler(dataTypeChanged);

            ibBack.Text = "返回";
            ibBack.Click += new EventHandler(ibBack_Click);
            ibEnter.Text = "确认";
            ibEnter.Click += new EventHandler(ibEnter_Click);

            foreach (Control c in this.Controls)
            {
                if (!(c is MonthCalendar))
                    c.Font = currentFont;
            }
            //foreach (Control c in this.Controls)
            //{
            //    if (c is MonthCalendar)
            //        c.Font = new Font("微软雅黑", 10F, FontStyle.Regular);
            //    else
            //        c.Font = currentFont;
            //}
            //monthCalendar1.Location = new Point((this.Width - monthCalendar1.Width) / 2, monthCalendar1.Top);
        }

        private void ilMinuteSpan_Click(object sender, EventArgs e)
        {
            KeypadForm f = KeypadForm.GetKeypadForm(ilMinuteSpan.Text);
            if (f.ShowDialog() == DialogResult.OK)
            {
                int minute = 0;
                try
                {
                    minute = Convert.ToInt32(f.KeyText);
                    if (minute < 0 || minute >= 60)
                        minute = 0;
                }
                catch
                {
                    minute = 0;
                }
                ilMinuteSpan.Text = minute.ToString();
            }
        }

        private void ilHourSpan_Click(object sender, EventArgs e)
        {
            KeypadForm f = KeypadForm.GetKeypadForm(ilHourSpan.Text);
            if (f.ShowDialog() == DialogResult.OK)
            {
                int hour = 0;
                try
                {
                    hour = Convert.ToInt32(f.KeyText);
                    if (hour < 0 || hour > 99)
                        hour = 0;
                }
                catch
                {
                    hour = 0;
                }
                ilHourSpan.Text = hour.ToString();
            }
        }

        private void ilStartMinute_Click(object sender, EventArgs e)
        {
            KeypadForm f = KeypadForm.GetKeypadForm(ilStartMinute.Text);
            if (f.ShowDialog() == DialogResult.OK)
            {
                int minute = 0;
                try
                {
                    minute = Convert.ToInt32(f.KeyText);
                    if (minute < 0 || minute >= 60)
                        minute = 0;
                }
                catch
                {
                    minute = 0;
                }
                ilStartMinute.Text = minute.ToString();
            }
        }

        private void ilStartHour_Click(object sender, EventArgs e)
        {
            KeypadForm f = KeypadForm.GetKeypadForm(ilStartHour.Text);
            if (f.ShowDialog() == DialogResult.OK)
            {
                int hour = 0;
                try
                {
                    hour = Convert.ToInt32(f.KeyText);
                    if (hour < 0 || hour >= 24)
                        hour = 0;
                }
                catch
                {
                    hour = 0;
                }
                ilStartHour.Text = hour.ToString();
            }
        }

        private void dataTypeChanged(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            if (button.Checked == true)
            {
                if (button.Name == "Pressure")
                {
                    dataType = 0;
                    ibPosition.Checked = false;
                }
                else
                {
                    dataType = 1;
                    ibPressure.Checked = false;
                }
            }
            else
            {
                if ((button.Name == "Pressure" && dataType == 0) ||
                    (button.Name == "Position" && dataType == 1))
                    button.Checked = true;
            }
        }

        private void ibEnter_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void ibBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public void Initialize()
        {
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
               (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            DateTime dt = DateTime.Now.Date;
            this.monthCalendar1.MaxDate = new DateTime(dt.Ticks);
            TimeSpan cts = new TimeSpan(reserveDays, 0, 0, 0);//可查询历史记录的日期跨度
            this.monthCalendar1.MinDate = new DateTime(dt.Ticks - cts.Ticks);
            ilStartHour.Text = "0";
            ilStartMinute.Text = "00";
            ilHourSpan.Text = "24";
            ilMinuteSpan.Text = "0";
            monthCalendar1.Location = new Point(100, 100);
            ibPressure.Checked = true;
            dataType = 0;
        }

        public DateTime GetStartTime()
        {
            DateTime dt = this.monthCalendar1.SelectionStart;
            try
            {
                TimeSpan ts = new TimeSpan(Convert.ToInt32(ilStartHour.Text), Convert.ToInt32(ilStartMinute.Text), 0);
                dt = dt.Add(ts);
            }
            catch
            { }
            return dt;
        }

        public TimeSpan GetTimeSpan()
        {
            TimeSpan ts = new TimeSpan(24, 0, 0);
            try
            {
                ts = new TimeSpan(Convert.ToInt32(ilHourSpan.Text), Convert.ToInt32(ilMinuteSpan.Text), 0);
                if (ts.Ticks <= 0)
                    ts = new TimeSpan(24, 0, 0);
            }
            catch
            { }
            return ts;
        }
        private Font currentFont = null;

        private List<ImageLabel> inputLabelList = null;
        private Label lbTimeSetting;
        private SimpleImagesContaner inputImage = null;
        private SimpleImagesContaner dateImage = null;
        private ImageLabel ilStartHour;//起始小时
        private ImageLabel ilStartMinute;//起始分钟
        private ImageLabel ilHourSpan;//小时跨度
        private ImageLabel ilMinuteSpan;//分钟跨度

        private ImagesContaner buttonImage;
        private List<ImageButton> buttonList;
        private ImageButton ibPressure;
        private ImageButton ibPosition;
        private ImageButton ibBack;
        private ImageButton ibEnter;

        private int LBTimeMarginLeft = 40;
        private int LBTimeMarginTop = 278;
        private int LBTimeSpacingX = 52;

        private int ButtonWidth = 210;
        private int ButtonHeight = 40;
        private int ButtonMarginTop = 350;
        private int ButtonMarginLeft = 10;
        private int ButtonSpacingX = 10;
        private int ButtonSpacingY = 10;

        private int reserveDays = 60;
        public int ReserveDays
        {
            set
            {
                if (value > 0 && reserveDays != value)
                    reserveDays = value;
            }
        }

        private int dataType = 0;//0:压力; 1:位置
        public int DataType
        {
            get { return dataType; }
        }

    }
}