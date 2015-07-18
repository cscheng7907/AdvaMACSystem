/*AdvaMACSystem 监控软件
 * 作者： 程慎
 *  
 * 修改记录：
 *       时间                内容                人员
 * 2015-2-15             创建                by cs 
 * 
 * copyright
 */

using System;
using System.Linq;
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
    public partial class PageViewPara_Setup : UIControlbase
    {
        private List<ImageButton> btnlst_id = new List<ImageButton>();
        private List<ImageButton> btnlst_subid = new List<ImageButton>();
        private List<ImageButton> btnlst_ok = new List<ImageButton>();

        private List<ImageLabel> btnlst_subid_Row = new List<ImageLabel>();
        private List<ImageLabel> btnlst_subid_Col = new List<ImageLabel>();

        private ImageLabel btnlst_subid_Row_Count = null;
        private ImageLabel btnlst_subid_Col_Count = null;

        private int pumpindex = -1;

        public PageViewPara_Setup()
        {
            InitializeComponent();

            ComCtrls.ImageButton btn1;
            ComCtrls.ImageButton btn2;
            Label lb1;
            this.SuspendLayout();


            for (int i = 0; i < DataPool.CDataPool.GetDataPoolObject().PumpCount; i++)
            {
                btn1 = new ImageButton(this.components);
                btn1.Checked = false;
                btn1.DownColor = System.Drawing.SystemColors.Control;
                btn1.Layout = ComCtrls.KTLayout.GlyphTop;

                btn1.Location = new System.Drawing.Point(0, AdvaMACSystemRes.pumpborder.Height * i);

                btn1.ShortcutKeys = System.Windows.Forms.Keys.None;
                btn1.Size = new System.Drawing.Size(AdvaMACSystemRes.pumpborder.Width, AdvaMACSystemRes.pumpborder.Height);
                btn1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
                btn1.TabIndex = 0;
                btn1.TabStop = false;
                btn1.Text = string.Format("{0:0}# 泵站", i + 1);
                btn1.ForeColor = System.Drawing.Color.Black;
                btn1.TransParent = true;
                btn1.UPImg = AdvaMACSystemRes.pumpborder;
                btn1.DNImg = AdvaMACSystemRes.pumpborder_checked;
                btn1.Toggle = true;
                btn1.UpColor = System.Drawing.SystemColors.Control;
                btn1.Tag = i;
                btn1.Click += new EventHandler(imageButton_pump_Click);

                btnlst_id.Add(btn1);

                this.Controls.Add(btn1);

                this.panel_body.Controls.Add(btn1);

            }

            CreateInputBox(-1, new Point(400, 0), true, true);
            CreateInputBox(-1, new Point(700, 0), true, false);

            for (int i = 0; i < DataPool.CDataPool.GetDataPoolObject().CylinderCount; i++)
            {

                // 
                // btn1
                // 
                btn1 = new ImageButton(this.components);
                btn1.Checked = false;
                btn1.DownColor = System.Drawing.SystemColors.Control;
                btn1.Layout = ComCtrls.KTLayout.GlyphTop;

                btn1.Location = new System.Drawing.Point(
                    (i % 2 == 0) ? 300 : 661,
                    45 + (i / 2) * (93 + 30));

                btn1.ShortcutKeys = System.Windows.Forms.Keys.None;
                btn1.Size = new System.Drawing.Size(100, 93);
                btn1.TabIndex = 0;
                btn1.TabStop = false;
                btn1.Text = "";
                btn1.TransParent = true;
                btn1.UPImg = AdvaMACSystemRes.LightOn100;
                btn1.DNImg = AdvaMACSystemRes.LightOff100;
                btn1.Toggle = true;
                btn1.UpColor = System.Drawing.SystemColors.Control;
                btn1.Tag = i;

                btnlst_subid.Add(btn1);

                // 
                // label1
                // 
                lb1 = new System.Windows.Forms.Label();
                lb1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);

                lb1.Location = new System.Drawing.Point(btn1.Location.X + btn1.Width + 20,
                    btn1.Location.Y);
                //btn1.Location.Y + btn1.Height / 2 - 20);//new System.Drawing.Point(93, 29);
                lb1.Size = new System.Drawing.Size(122, 23);
                lb1.Text = string.Format("{0:0}# 油缸", i + 1);
                lb1.ForeColor = System.Drawing.Color.Black;


                // 
                // btn2  确定按钮
                // 
                btn2 = new ImageButton(this.components);
                btn2.Checked = false;
                btn2.DownColor = System.Drawing.SystemColors.Control;
                btn2.Layout = ComCtrls.KTLayout.GlyphTop;

                btn2.Location = new System.Drawing.Point(lb1.Location.X + lb1.Width + 20, lb1.Location.Y);

                btn2.ShortcutKeys = System.Windows.Forms.Keys.None;
                btn2.Size = new System.Drawing.Size(60, 23);
                btn2.TabIndex = 0;
                btn2.TabStop = false;
                //btn2.Text = "确认";
                btn2.TransParent = true;
                btn2.UPImg = AdvaMACSystemRes.Set_up;
                btn2.DNImg = AdvaMACSystemRes.Set_down;
                btn2.Toggle = true;
                btn2.UpColor = System.Drawing.SystemColors.Control;
                btn2.Tag = i;
                btn2.MouseUp += new MouseEventHandler(cylinder_SetOK_MouseUp);

                btnlst_ok.Add(btn2);


                this.Controls.Add(btn1);
                this.Controls.Add(lb1);
                this.Controls.Add(btn2);

                this.panel_body.Controls.Add(btn1);
                this.panel_body.Controls.Add(lb1);
                this.panel_body.Controls.Add(btn2);

                CreateInputBox(i, new Point(lb1.Left, lb1.Top + lb1.Height + 5), false, true);
                CreateInputBox(i, new Point(lb1.Left, lb1.Top + lb1.Height + 35), false, false);
            }
            this.ResumeLayout(false);


        }

        private void cylinder_SetOK_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender == null || !(sender is ImageButton))
                return;

            ImageButton ib = (ImageButton)sender;
            if (ib.Checked)
            {
                DataPool.CDataPool.GetDataPoolObject().SetboolValue(pumpindex, (int)ib.Tag, CmdDataType.cdtSetupFinish_Confirm_seperate, true);
                ib.Text = "已确认";
            }
            else
            {
                DataPool.CDataPool.GetDataPoolObject().SetboolValue(pumpindex, (int)ib.Tag, CmdDataType.cdtSetupFinish_Confirm_seperate, false);
                ib.Text = "确认";
            }
        }

        private void CreateInputBox(int subid, Point topleft, bool istotal, bool isRow)
        {
            int in_Space = 5;

            ComCtrls.ImageLabel btn_input;
            Label lb_first, lb1_tail;

            // 
            // label 层 第 
            // 
            lb_first = new System.Windows.Forms.Label();
            lb_first.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            lb_first.Location = topleft;
            lb_first.Size = new System.Drawing.Size(30, 23);
            lb_first.Text = (istotal) ? "共" : "第";
            lb_first.ForeColor = System.Drawing.Color.Black;

            // 
            // btn_input
            // 
            btn_input = new ComCtrls.ImageLabel();
            btn_input.BackColor = System.Drawing.Color.White;
            btn_input.Checked = false;
            btn_input.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular);
            btn_input.Layout = ComCtrls.KTLayout.GlyphTop;
            btn_input.Location = new System.Drawing.Point(topleft.X + lb_first.Width + in_Space, topleft.Y);
            btn_input.Name = "btn_input";
            btn_input.Size = new System.Drawing.Size(103, 29);
            btn_input.TabIndex = 3;
            btn_input.Tag = subid;
            btn_input.Text = "0";
            btn_input.TextX = -1F;
            btn_input.TextY = -1F;
            btn_input.TransParent = false;

            if (istotal)
            {
                if (isRow)
                {
                    btn_input.Click += new System.EventHandler(imageLabel_Input_total_Row_Click);
                    btnlst_subid_Row_Count = btn_input;
                }
                else
                {
                    btn_input.Click += new System.EventHandler(imageLabel_Input_total_Col_Click);
                    btnlst_subid_Col_Count = btn_input;
                }
            }
            else
            {
                if (isRow)
                    btn_input.Click += new System.EventHandler(imageLabel_Input_Row_Click);
                else
                    btn_input.Click += new System.EventHandler(imageLabel_Input_Col_Click);
            }

            if (!istotal)
            {
                if (isRow)
                    btnlst_subid_Row.Add(btn_input);
                else
                    btnlst_subid_Col.Add(btn_input);
            }

            // 
            // label 层 尾
            // 
            lb1_tail = new System.Windows.Forms.Label();
            lb1_tail.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            lb1_tail.Location = new Point(topleft.X + lb_first.Width + btn_input.Width + in_Space, topleft.Y);
            lb1_tail.Size = new System.Drawing.Size(30, 23);
            lb1_tail.Text = (isRow) ? "层" : "支";
            lb1_tail.ForeColor = System.Drawing.Color.Black;

            this.Controls.Add(btn_input);
            this.Controls.Add(lb_first);
            this.Controls.Add(lb1_tail);

            this.panel_body.Controls.Add(btn_input);
            this.panel_body.Controls.Add(lb_first);
            this.panel_body.Controls.Add(lb1_tail);
        }

        private void imageLabel_Input_Row_Click(object Sender, EventArgs e)
        {
            if (Sender == null || !(Sender is ImageLabel))
                return;

            ImageLabel lb = (ImageLabel)Sender;

            KeypadForm f = KeypadForm.GetKeypadForm(lb.Text, KeypadMode.Normal);
            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int input = Convert.ToInt32(f.KeyText);

                    if (input >= 0 && input < DataPool.CDataPool.GetDataPoolObject().View_SetupPosition_RowCount)
                        lb.Text = input.ToString();
                    else
                        MessageBox.Show("输入数值越界！");
                }
                catch (Exception)
                {
                    MessageBox.Show("非法输入！");
                }
            }
        }

        private void imageLabel_Input_Col_Click(object Sender, EventArgs e)
        {
            if (Sender == null || !(Sender is ImageLabel))
                return;

            ImageLabel lb = (ImageLabel)Sender;

            KeypadForm f = KeypadForm.GetKeypadForm(lb.Text, KeypadMode.Normal);
            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int input = Convert.ToInt32(f.KeyText);

                    if (input >= 0 && input < DataPool.CDataPool.GetDataPoolObject().View_SetupPosition_ColCount)
                        lb.Text = input.ToString();
                    else
                        MessageBox.Show("输入数值越界！");
                }
                catch (Exception)
                {
                    MessageBox.Show("非法输入！");
                }
            }
        }

        private void imageLabel_Input_total_Row_Click(object Sender, EventArgs e)
        {
            if (Sender == null || !(Sender is ImageLabel))
                return;
            ImageLabel lb = (ImageLabel)Sender;

            KeypadForm f = KeypadForm.GetKeypadForm(lb.Text, KeypadMode.Normal);
            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int input = Convert.ToInt32(f.KeyText);

                    if (input >= 0)
                    {
                        lb.Text = input.ToString();
                        DataPool.CDataPool.GetDataPoolObject().View_SetupPosition_RowCount = input;
                        DataPool.CDataPool.GetDataPoolObject().SavetoFile();
                    }
                    else
                        MessageBox.Show("输入数值越界！");
                }
                catch (Exception)
                {
                    MessageBox.Show("非法输入！");
                }
            }

        }

        private void imageLabel_Input_total_Col_Click(object Sender, EventArgs e)
        {
            if (Sender == null || !(Sender is ImageLabel))
                return;
            ImageLabel lb = (ImageLabel)Sender;

            KeypadForm f = KeypadForm.GetKeypadForm(lb.Text, KeypadMode.Normal);
            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int input = Convert.ToInt32(f.KeyText);

                    if (input >= 0)
                    {
                        lb.Text = input.ToString();
                        DataPool.CDataPool.GetDataPoolObject().View_SetupPosition_ColCount = input;
                        DataPool.CDataPool.GetDataPoolObject().SavetoFile();
                    }
                    else
                        MessageBox.Show("输入数值越界！");
                }
                catch (Exception)
                {
                    MessageBox.Show("非法输入！");
                }
            }
        }


        public override void DoEnter()
        {
            base.DoEnter();

            if (btnlst_subid != null && btnlst_subid.Count > 0)
                imageButton_pump_Click(btnlst_id[0], new EventArgs());

            DataPool.CDataPool.GetDataPoolObject().sign_View_Setup = true;
        }

        private void imageButton_pump_Click(object sender, EventArgs e)
        {
            foreach (ImageButton item in btnlst_id)
            {
                item.Checked = (item == (ImageButton)sender);
            }

            pumpindex = Convert.ToInt32(((ImageButton)sender).Tag);
            DataPool.CDataPool.GetDataPoolObject().CurId = pumpindex;
            DataPool.CDataPool.GetDataPoolObject().CurSubId = 0;
            UpdateViewData();
        }

        private void imageButton_back_Click(object sender, EventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_Setup = false;
            this.DoExit();
        }

        private void imageButton_OK_Click(object sender, EventArgs e)
        {
            SaveViewData();
        }
        private void imageButton_OK_MouseUp(object sender, MouseEventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_Setup_Confirm = false;
        }
        private void imageButton_OK_MouseDown(object sender, MouseEventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_Setup_Confirm = true;
        }

        private void imageButton_Finish_OK_Click(object sender, EventArgs e)
        { }
        private void imageButton_Finish_OK_MouseUp(object sender, EventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_SetupFinish_Confirm = false;
        }
        private void imageButton_Finish_OK_MouseDown(object sender, EventArgs e)
        {
            DataPool.CDataPool.GetDataPoolObject().sign_View_SetupFinish_Confirm = true;
        }


        private void SaveViewData()
        {
            for (int i = 0; i < btnlst_subid.Count; i++)
            {
                DataPool.CDataPool.GetDataPoolObject().SetboolValue(
                    pumpindex,
                    i,
                    CmdDataType.cdtInstalled,
                    !btnlst_subid[i].Checked);

                DataPool.CDataPool.GetDataPoolObject().SetintValue(
                    pumpindex,
                    i,
                    CmdDataType.cdtView_SetupPosition_Row,
                    Convert.ToInt32(btnlst_subid_Row[i].Text));

                DataPool.CDataPool.GetDataPoolObject().SetintValue(
                    pumpindex,
                    i,
                    CmdDataType.cdtView_SetupPosition_Col,
                    Convert.ToInt32(btnlst_subid_Col[i].Text));
            }
            DataPool.CDataPool.GetDataPoolObject().SavetoFile();
            MessageBox.Show(string.Format("#{0:00}泵站，参数已经保存！", pumpindex + 1));
        }

        private void UpdateViewData()
        {
            if (btnlst_subid_Row_Count != null)
                btnlst_subid_Row_Count.Text = DataPool.CDataPool.GetDataPoolObject().View_SetupPosition_RowCount.ToString();

            if (btnlst_subid_Col_Count != null)
                btnlst_subid_Col_Count.Text = DataPool.CDataPool.GetDataPoolObject().View_SetupPosition_ColCount.ToString();

            for (int i = 0; i < btnlst_subid.Count; i++)
            {
                btnlst_subid[i].Checked =
                    !DataPool.CDataPool.GetDataPoolObject().GetBoolValue(
                        pumpindex,
                         i,
                         CmdDataType.cdtInstalled);

                btnlst_subid_Row[i].Text = DataPool.CDataPool.GetDataPoolObject().GetintValue(
                      pumpindex,
                         i,
                         CmdDataType.cdtView_SetupPosition_Row).ToString();

                btnlst_subid_Col[i].Text = DataPool.CDataPool.GetDataPoolObject().GetintValue(
                      pumpindex,
                         i,
                         CmdDataType.cdtView_SetupPosition_Col).ToString();

                btnlst_ok[i].Checked = DataPool.CDataPool.GetDataPoolObject().GetBoolValue(pumpindex, i, CmdDataType.cdtSetupFinish_Confirm_seperate);
                if (btnlst_ok[i].Checked)
                {
                    btnlst_ok[i].Text = "已确认";
                }
                else
                {
                    btnlst_ok[i].Text = "确认";
                }

            }
        }
    }
}
