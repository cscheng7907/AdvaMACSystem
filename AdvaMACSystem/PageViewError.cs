using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataPool;
using System.IO;

namespace AdvaMACSystem
{
    public partial class PageViewError : UIControlbase
    {
        public PageViewError()
        {
            InitializeComponent();
        }

        public void DoEnter()
        {
            base.DoEnter();

            listView1.Items.Clear();
            if (optor == null)
                return;

            int count = 0;
            int id, subid, cmdtype;

            //更新数据
            if (IsReal)
            {
                foreach (KeyValuePair<int, DateTime> item in optor.CurErrorList)
                {
                    ListViewItem lv = new ListViewItem();
                    DateTime dt = item.Value;
                    lv.Text = (++count).ToString();
                    lv.SubItems.Add(string.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second));
                    lv.SubItems.Add("发生");

                    id = item.Key / 10000;
                    subid = item.Key % 10000 / 100;
                    cmdtype = item.Key % 10000 % 100;

                    lv.SubItems.Add(string.Format("#{0}-#{1} {2}", id, subid, strlst[cmdtype - firstcmdtype]));
                    listView1.Items.Add(lv);
                }
            }
            else
            {
                if (File.Exists(WarnErrOperator.ErrorRecFileName))
                {
                    int fixitem = 0;
                    bool val = false;
                    DateTime dt;

                    FileStream fs = new FileStream(WarnErrOperator.ErrorRecFileName, FileMode.Open);
                    long linecount = fs.Length / (sizeof(int) + sizeof(int) + sizeof(long));
                    BinaryReader br = new BinaryReader(fs);
                    br.BaseStream.Seek(0, SeekOrigin.Begin); //将文件指针设置到文件开始
                    try
                    {
                        while (br.BaseStream.Position < br.BaseStream.Length) // 当未到达文件结尾时
                        {
                            ListViewItem lv = new ListViewItem();
                            lv.Text = (linecount--).ToString();

                            fixitem = br.ReadInt32();//type
                            id = fixitem / 10000;
                            subid = fixitem % 10000 / 100;
                            cmdtype = fixitem % 10000 % 100;

                            val = br.ReadInt32() != 0;//val

                            dt = new DateTime(br.ReadInt64());//time tick

                            lv.SubItems.Add(string.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second));
                            lv.SubItems.Add(string.Format("{0}", (val) ? "发生" : "消失"));
                            lv.SubItems.Add(string.Format("#{0}-#{1} {2}", id, subid, strlst[cmdtype - firstcmdtype]));

                            listView1.Items.Insert(0, lv);
                        }
                    }
                    finally
                    {
                        br.Close();
                        fs.Close();
                    }

                }

            }
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
                    label1.Text = "系统故障信息-" +
                       ((_isreal) ? "实时数据" : "历史数据");
                }
            }
        }

        private WarnErrOperator optor = null;
        public WarnErrOperator Optor { set { if (optor != value)optor = value; } }

        private string[] strlst = new string[]{
            /*cdtError_Pump_3501_3504,//*/"泵站及控制器 故障",// 4*16
            /*cdtError_PressureSenser_3501_3504,//*/"油缸压力传感器故障",// 4*8
            /*cdtError_PositionSenser_3501_3504,//*/"油缸长度传感器故障",// 4*8

            /*cdtError_cylinder_extend_3511_3514,//*/"油缸伸出电磁阀线路短路",// 4*8
            /*cdtError_cylinder_retract_3511_3514,//*/"油缸缩回电磁阀线路短路",// 4*8
            /*cdtError_MachLock_extend_3511_3514,//*/"油缸机械锁伸出电磁阀线路短路",// 4*8
            /*cdtError_MachLock_retract_3511_3514,//*/"油缸机械锁缩回电磁阀线路短路"// 4*8           
        };
        private int firstcmdtype = (int)CmdDataType.cdtError_Pump_3501_3504;


    }
}
