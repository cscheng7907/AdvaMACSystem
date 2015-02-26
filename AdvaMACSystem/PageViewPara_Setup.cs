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

        private int pumpindex = -1;

        public PageViewPara_Setup()
        {
            InitializeComponent();

            ComCtrls.ImageButton btn1;
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
                    30 + (i / 2) * (93 + 30));

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
                    btn1.Location.Y + btn1.Height / 2 - 20);//new System.Drawing.Point(93, 29);
                lb1.Size = new System.Drawing.Size(122, 33);
                lb1.Text = string.Format("{0:0}# 油缸", i + 1);
                lb1.ForeColor = System.Drawing.Color.Black;


                this.Controls.Add(btn1);
                this.Controls.Add(lb1);

                this.panel_body.Controls.Add(btn1);
                this.panel_body.Controls.Add(lb1);
            }
            this.ResumeLayout(false);


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
        private void SaveViewData()
        {
            for (int i = 0; i < btnlst_subid.Count; i++)
            {
                DataPool.CDataPool.GetDataPoolObject().SetboolValue(
                    pumpindex,
                    i,
                    CmdDataType.cdtInstalled,
                    btnlst_subid[i].Checked);
            }
        }

        private void UpdateViewData()
        {
            for (int i = 0; i < btnlst_subid.Count; i++)
            {
                btnlst_subid[i].Checked =
                    !DataPool.CDataPool.GetDataPoolObject().GetBoolValue(
                        pumpindex,
                         i,
                         CmdDataType.cdtInstalled);
            }
        }
    }
}
