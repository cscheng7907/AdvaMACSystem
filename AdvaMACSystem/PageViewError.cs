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

        public override void DoEnter()
        {
            base.DoEnter();

            if (optor == null)
                return;
            Update();
        }

        private void Update()
        {
            int count = 0;
            int id, subid, cmdtype;

            listView1.Items.Clear();

            if (!Visible)
                return;

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

                    if (cmdtype > (int)CmdDataType.cdtError_controller_warnlight_opencircuit_3501_3504)
                        lv.SubItems.Add(string.Format("{0}#泵站 {1}# {2}", id + 1, subid + 1, strlst[cmdtype - firstcmdtype]));
                    else
                        lv.SubItems.Add(string.Format("{0}#{1}", id + 1, strlst[cmdtype - firstcmdtype]));

                    listView1.Items.Add(lv);
                }
            }
            else
            {
                if (File.Exists(optor.ErrorRecFileName))
                {
                    int fixitem = 0;
                    bool val = false;
                    DateTime dt;

                    FileStream fs = new FileStream(optor.ErrorRecFileName, FileMode.Open);
                    long linecount = fs.Length / (sizeof(int) + sizeof(int) + sizeof(long));
                    BinaryReader br = new BinaryReader(fs);
                    br.BaseStream.Seek(0, SeekOrigin.Begin); //将文件指针设置到文件开始

                    logbuff.Clear();

                    try
                    {
                        while (br.BaseStream.Position < br.BaseStream.Length) // 当未到达文件结尾时
                        {
                            logType l = new logType();
                            fixitem = br.ReadInt32();//type

                            l.id = fixitem / 10000;
                            l.subid = fixitem % 10000 / 100;
                            l.cmdtype = fixitem % 10000 % 100;

                            l.val = br.ReadInt32() != 0;//val

                            l.time = br.ReadInt64();
                            logbuff.Insert(0, l);
                        }
                    }
                    finally
                    {
                        br.Close();
                        fs.Close();
                    }

                    listView1.BeginUpdate();
                    for (int i = 0; i < logbuff.Count; i++)
                    {
                        ListViewItem lv = new ListViewItem();
                        lv.Text = (i + 1).ToString();
                        dt = new DateTime(logbuff[i].time);//time tick

                        lv.SubItems.Add(string.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second));
                        lv.SubItems.Add(string.Format("{0}", (logbuff[i].val) ? "发生" : "消失"));
                        if (logbuff[i].cmdtype > (int)CmdDataType.cdtError_controller_warnlight_opencircuit_3501_3504)
                            lv.SubItems.Add(string.Format("{0}#泵站 {1}# {2}", logbuff[i].id + 1, logbuff[i].subid + 1, strlst[logbuff[i].cmdtype - firstcmdtype]));
                        else
                            lv.SubItems.Add(string.Format("{0}#{1}", logbuff[i].id + 1, strlst[logbuff[i].cmdtype - firstcmdtype]));

                        listView1.Items.Add(lv);
                    }
                    listView1.EndUpdate();



                    //listView1.BeginUpdate();
                    //try
                    //{
                    //    while (br.BaseStream.Position < br.BaseStream.Length) // 当未到达文件结尾时
                    //    {
                    //        ListViewItem lv = new ListViewItem();
                    //        lv.Text = (linecount--).ToString();

                    //        fixitem = br.ReadInt32();//type
                    //        id = fixitem / 10000;
                    //        subid = fixitem % 10000 / 100;
                    //        cmdtype = fixitem % 10000 % 100;

                    //        val = br.ReadInt32() != 0;//val

                    //        dt = new DateTime(br.ReadInt64());//time tick

                    //        lv.SubItems.Add(string.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second));
                    //        lv.SubItems.Add(string.Format("{0}", (val) ? "发生" : "消失"));
                    //        if (cmdtype > (int)CmdDataType.cdtError_controller_warnlight_opencircuit_3501_3504)
                    //            lv.SubItems.Add(string.Format("{0}#泵站 {1}# {2}", id + 1, subid + 1, strlst[cmdtype - firstcmdtype]));
                    //        else
                    //            lv.SubItems.Add(string.Format("{0}#{1}", id + 1, strlst[cmdtype - firstcmdtype]));

                    //        listView1.Items.Insert(0, lv);
                    //    }
                    //}
                    //finally
                    //{
                    //    listView1.EndUpdate();
                    //    br.Close();
                    //    fs.Close();
                    //}

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

                    Update();
                }
            }
        }

        private WarnErrOperator optor = null;
        public WarnErrOperator Optor { set { if (optor != value)optor = value; } }

        private string[] strlst = new string[]{
            /*cdtError_Pump_3501_3504,//*/"泵站及控制器 故障",// 4*16


            /*cdtError_pump_motor_shortcircuit_3501_3504,                       //*/"泵站电动机启动线路短路",  // 4
            /*cdtError_pump_motor_opencircuit_3501_3504,                        //*/"泵站电动机启动线路断路",  // 4

            /*cdtError_pump_electromagneticvalve_shortcircuit_3501_3504,        //*/"泵站冗余电磁阀线路短路",  // 4
            /*cdtError_pump_electromagneticvalve_opencircuit_3501_3504,         //*/"泵站冗余电磁阀线路断路",  // 4

            /*cdtError_pump_proportionalvalve_shortcircuit_3501_3504,           //*/"泵站比例阀线路短路",      // 4   
            /*cdtError_pump_proportionalvalve_opencircuit_3501_3504,            //*/"泵站比例阀线路断路",      // 4

            /*cdtError_pump_MachLock_proportionalvalve_shortcircuit_3501_3504, //*/"泵站机械锁马达电磁阀线路短路",  // 4
            /*cdtError_pump_MachLock_proportionalvalve_opencircuit_3501_3504,  //*/"泵站机械锁马达电磁阀线路断路",  // 4


            /*cdtError_controller_dynamo_Start_shortcircuit_3501_3504,      //*/"控制器发电机启动线路短路",  // 4
            /*cdtError_controller_dynamo_Start_opencircuit_3501_3504,       //*/"控制器发电机启动线路断路",  // 4

            /*cdtError_controller_dynamo_Stop_shortcircuit_3501_3504,        //*/"控制器发电机停止线路短路",  // 4
            /*cdtError_controller_dynamo_Stop_opencircuit_3501_3504,         //*/"控制器发电机停止线路断路",  // 4

            /*cdtError_controller_warnlight_shortcircuit_3501_3504,         //*/"控制器声光报警灯线路短路",  // 4
            /*cdtError_controller_warnlight_opencircuit_3501_3504,          //*/"控制器声光报警灯线路断路",  // 4


            /*cdtError_PressureSenser_3501_3504,//*/"油缸压力传感器故障",// 4*8
            /*cdtError_PositionSenser_3501_3504,//*/"油缸长度传感器故障",// 4*8

            /*cdtError_cylinder_extend_shortcircuit_3511_3514,//*/"油缸伸出电磁阀线路短路",// 4*8
            /*cdtError_cylinder_retract_shortcircuit_3511_3514,//*/"油缸缩回电磁阀线路短路",// 4*8
            /*cdtError_MachLock_extend_shortcircuit_3511_3514,//*/"油缸机械锁伸出电磁阀线路短路",// 4*8
            /*cdtError_MachLock_retract_shortcircuit_3511_3514,//*/"油缸机械锁缩回电磁阀线路短路",// 4*8 
          
            /*cdtError_cylinder_extend_opencircuit_3511_3514,//*/"油缸伸出电磁阀线路断路",// 4*8
            /*cdtError_cylinder_retract_opencircuit_3511_3514,//*/"油缸缩回电磁阀线路断路",// 4*8
            /*cdtError_MachLock_extend_opencircuit_3511_3514,//*/"油缸机械锁伸出电磁阀线路断路",// 4*8
            /*cdtError_MachLock_retract_opencircuit_3511_3514//*/"油缸机械锁缩回电磁阀线路断路"// 4*8
        };

















        private int firstcmdtype = (int)CmdDataType.cdtError_Pump_3501_3504;

        private void imageButton_back_Click(object sender, EventArgs e)
        {
            this.DoExit();
        }


        public struct logType
        {
            public int id;
            public int subid;
            public int cmdtype;
            public bool val;
            public long time;
        };

        private List<logType> logbuff = new List<logType>();

    }
}
