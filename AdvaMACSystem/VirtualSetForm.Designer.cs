using DataPool;
namespace AdvaMACSystem
{
    partial class VirtualSetForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.il_Presure = new ComCtrls.ImageLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.il_Position = new ComCtrls.ImageLabel();
            this.comboBoxcylinderState = new System.Windows.Forms.ComboBox();
            this.comboBoxMachLockState = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.il_Pressure_Pump = new ComCtrls.ImageLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.il_Voltage = new ComCtrls.ImageLabel();
            this.rb__PowerSupply = new ComCtrls.ImageLabel();
            this.rb_Limit_5 = new System.Windows.Forms.CheckBox();
            this.rb_Limit_10 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.rb_Warn_LowPosition = new System.Windows.Forms.CheckBox();
            this.rb_Warn_HighPosition = new System.Windows.Forms.CheckBox();
            this.rb_Warn_LowPressure = new System.Windows.Forms.CheckBox();
            this.rb_Warn_HighPressure = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox_PumpErr = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rb_Error_cylinder_extend = new System.Windows.Forms.CheckBox();
            this.rb_Error_MachLock_retract = new System.Windows.Forms.CheckBox();
            this.rb_Error_Pump = new System.Windows.Forms.CheckBox();
            this.rb_Error_cylinder_retract = new System.Windows.Forms.CheckBox();
            this.rb_Error_MachLock_extend = new System.Windows.Forms.CheckBox();
            this.rb_Error_PressureSenser = new System.Windows.Forms.CheckBox();
            this.rb_Error_PositionSenser = new System.Windows.Forms.CheckBox();
            this.rb_CompAct_Pump = new System.Windows.Forms.CheckBox();
            this.rb_StartFailed_Pump = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.il_idControl = new ComCtrls.ImageLabel();
            this.il_idrongyu = new ComCtrls.ImageLabel();
            this.rb_Estop1 = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Items.AddRange(new object[] {
            "#1 泵站",
            "#2 泵站",
            "#3 泵站",
            "#4 泵站"});
            this.comboBox1.Location = new System.Drawing.Point(55, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.Items.AddRange(new object[] {
            "#1 油缸",
            "#2 油缸",
            "#3 油缸",
            "#4 油缸",
            "#5 油缸",
            "#6 油缸",
            "#7 油缸",
            "#8 油缸"});
            this.comboBox2.Location = new System.Drawing.Point(217, 22);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(100, 20);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(166, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 34;
            this.label2.Text = "subID";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 20);
            this.label3.TabIndex = 33;
            this.label3.Text = "油缸压力当前值";
            // 
            // il_Presure
            // 
            this.il_Presure.BackColor = System.Drawing.Color.White;
            this.il_Presure.BackImg = null;
            this.il_Presure.IMGContainer = null;
            this.il_Presure.ImgDisable = null;
            this.il_Presure.Location = new System.Drawing.Point(143, 71);
            this.il_Presure.Name = "il_Presure";
            this.il_Presure.Size = new System.Drawing.Size(104, 20);
            this.il_Presure.TabIndex = 6;
            this.il_Presure.Tag = DataPool.CmdDataType.cdtPressure_Real_3001_3008;
            this.il_Presure.Text = "0";
            this.il_Presure.TextX = -1F;
            this.il_Presure.TextY = -1F;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(253, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 20);
            this.label4.TabIndex = 28;
            this.label4.Text = "油缸当前长度值";
            // 
            // il_Position
            // 
            this.il_Position.BackColor = System.Drawing.Color.White;
            this.il_Position.BackImg = null;
            this.il_Position.IMGContainer = null;
            this.il_Position.ImgDisable = null;
            this.il_Position.Location = new System.Drawing.Point(377, 71);
            this.il_Position.Name = "il_Position";
            this.il_Position.Size = new System.Drawing.Size(104, 20);
            this.il_Position.TabIndex = 9;
            this.il_Position.Tag = DataPool.CmdDataType.cdtPosition_Real_3101_3108;
            this.il_Position.Text = "0";
            this.il_Position.TextX = -1F;
            this.il_Position.TextY = -1F;
            // 
            // comboBoxcylinderState
            // 
            this.comboBoxcylinderState.Items.AddRange(new object[] {
            "0：停",
            "1：伸",
            "2：缩"});
            this.comboBoxcylinderState.Location = new System.Drawing.Point(143, 106);
            this.comboBoxcylinderState.Name = "comboBoxcylinderState";
            this.comboBoxcylinderState.Size = new System.Drawing.Size(100, 20);
            this.comboBoxcylinderState.TabIndex = 10;
            this.comboBoxcylinderState.Tag = DataPool.CmdDataType.cdtcylinderState_Real_3201_3208;
            // 
            // comboBoxMachLockState
            // 
            this.comboBoxMachLockState.Items.AddRange(new object[] {
            "0：停",
            "1：伸",
            "2：缩"});
            this.comboBoxMachLockState.Location = new System.Drawing.Point(381, 106);
            this.comboBoxMachLockState.Name = "comboBoxMachLockState";
            this.comboBoxMachLockState.Size = new System.Drawing.Size(100, 20);
            this.comboBoxMachLockState.TabIndex = 11;
            this.comboBoxMachLockState.Tag = DataPool.CmdDataType.cdtMachLockState_Real_3201_3208;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(19, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 20);
            this.label5.TabIndex = 32;
            this.label5.Text = "油缸运行状态";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(257, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 20);
            this.label6.TabIndex = 29;
            this.label6.Text = "机械锁运行状态";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(19, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 17);
            this.label7.TabIndex = 31;
            this.label7.Text = "泵站压力";
            // 
            // il_Pressure_Pump
            // 
            this.il_Pressure_Pump.BackColor = System.Drawing.Color.White;
            this.il_Pressure_Pump.BackImg = null;
            this.il_Pressure_Pump.IMGContainer = null;
            this.il_Pressure_Pump.ImgDisable = null;
            this.il_Pressure_Pump.Location = new System.Drawing.Point(143, 137);
            this.il_Pressure_Pump.Name = "il_Pressure_Pump";
            this.il_Pressure_Pump.Size = new System.Drawing.Size(104, 20);
            this.il_Pressure_Pump.TabIndex = 6;
            this.il_Pressure_Pump.Tag = DataPool.CmdDataType.cdtPressure_Pump_Real_3301_3304;
            this.il_Pressure_Pump.Text = "0";
            this.il_Pressure_Pump.TextX = -1F;
            this.il_Pressure_Pump.TextY = -1F;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(257, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 17);
            this.label8.TabIndex = 30;
            this.label8.Text = "控制器电压";
            // 
            // il_Voltage
            // 
            this.il_Voltage.BackColor = System.Drawing.Color.White;
            this.il_Voltage.BackImg = null;
            this.il_Voltage.IMGContainer = null;
            this.il_Voltage.ImgDisable = null;
            this.il_Voltage.Location = new System.Drawing.Point(381, 139);
            this.il_Voltage.Name = "il_Voltage";
            this.il_Voltage.Size = new System.Drawing.Size(104, 20);
            this.il_Voltage.TabIndex = 6;
            this.il_Voltage.Tag = DataPool.CmdDataType.cdtVoltage_Real_3301_3304;
            this.il_Voltage.Text = "0";
            this.il_Voltage.TextX = -1F;
            this.il_Voltage.TextY = -1F;
            // 
            // rb__PowerSupply
            // 
            this.rb__PowerSupply.BackColor = System.Drawing.Color.White;
            this.rb__PowerSupply.BackImg = null;
            this.rb__PowerSupply.IMGContainer = null;
            this.rb__PowerSupply.ImgDisable = null;
            this.rb__PowerSupply.Location = new System.Drawing.Point(19, 177);
            this.rb__PowerSupply.Name = "rb__PowerSupply";
            this.rb__PowerSupply.Size = new System.Drawing.Size(104, 20);
            this.rb__PowerSupply.TabIndex = 6;
            this.rb__PowerSupply.Tag = DataPool.CmdDataType.cdtPowerSupply_3301_3304;
            this.rb__PowerSupply.Text = "0";
            this.rb__PowerSupply.TextX = -1F;
            this.rb__PowerSupply.TextY = -1F;
            // 
            // rb_Limit_5
            // 
            this.rb_Limit_5.Location = new System.Drawing.Point(19, 203);
            this.rb_Limit_5.Name = "rb_Limit_5";
            this.rb_Limit_5.Size = new System.Drawing.Size(176, 20);
            this.rb_Limit_5.TabIndex = 16;
            this.rb_Limit_5.Tag = DataPool.CmdDataType.cdtLimit_5_3301_3304;
            this.rb_Limit_5.Text = "油缸5mm接近开关限位";
            // 
            // rb_Limit_10
            // 
            this.rb_Limit_10.Location = new System.Drawing.Point(273, 203);
            this.rb_Limit_10.Name = "rb_Limit_10";
            this.rb_Limit_10.Size = new System.Drawing.Size(188, 20);
            this.rb_Limit_10.TabIndex = 16;
            this.rb_Limit_10.Tag = DataPool.CmdDataType.cdtLimit_10_3301_3304;
            this.rb_Limit_10.Text = "油缸10mm接近开关限位";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.rb_Warn_LowPosition);
            this.panel1.Controls.Add(this.rb_Warn_HighPosition);
            this.panel1.Controls.Add(this.rb_Warn_LowPressure);
            this.panel1.Controls.Add(this.rb_Warn_HighPressure);
            this.panel1.Location = new System.Drawing.Point(19, 240);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 130);
            this.panel1.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(53, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "报警";
            // 
            // rb_Warn_LowPosition
            // 
            this.rb_Warn_LowPosition.Location = new System.Drawing.Point(3, 104);
            this.rb_Warn_LowPosition.Name = "rb_Warn_LowPosition";
            this.rb_Warn_LowPosition.Size = new System.Drawing.Size(132, 20);
            this.rb_Warn_LowPosition.TabIndex = 0;
            this.rb_Warn_LowPosition.Tag = DataPool.CmdDataType.cdtWarn_LowPosition_3401_3404;
            this.rb_Warn_LowPosition.Text = "油缸长度过低";
            // 
            // rb_Warn_HighPosition
            // 
            this.rb_Warn_HighPosition.Location = new System.Drawing.Point(3, 78);
            this.rb_Warn_HighPosition.Name = "rb_Warn_HighPosition";
            this.rb_Warn_HighPosition.Size = new System.Drawing.Size(132, 20);
            this.rb_Warn_HighPosition.TabIndex = 0;
            this.rb_Warn_HighPosition.Tag = DataPool.CmdDataType.cdtWarn_HighPosition_3401_3404;
            this.rb_Warn_HighPosition.Text = "油缸长度过高";
            // 
            // rb_Warn_LowPressure
            // 
            this.rb_Warn_LowPressure.Location = new System.Drawing.Point(3, 52);
            this.rb_Warn_LowPressure.Name = "rb_Warn_LowPressure";
            this.rb_Warn_LowPressure.Size = new System.Drawing.Size(132, 20);
            this.rb_Warn_LowPressure.TabIndex = 0;
            this.rb_Warn_LowPressure.Tag = DataPool.CmdDataType.cdtWarn_LowPressure_3401_3404;
            this.rb_Warn_LowPressure.Text = "油缸压力过低";
            // 
            // rb_Warn_HighPressure
            // 
            this.rb_Warn_HighPressure.Location = new System.Drawing.Point(3, 26);
            this.rb_Warn_HighPressure.Name = "rb_Warn_HighPressure";
            this.rb_Warn_HighPressure.Size = new System.Drawing.Size(132, 20);
            this.rb_Warn_HighPressure.TabIndex = 0;
            this.rb_Warn_HighPressure.Tag = DataPool.CmdDataType.cdtWarn_HighPressure_3401_3404;
            this.rb_Warn_HighPressure.Text = "油缸压力过高";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comboBox_PumpErr);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.rb_Error_cylinder_extend);
            this.panel2.Controls.Add(this.rb_Error_MachLock_retract);
            this.panel2.Controls.Add(this.rb_Error_Pump);
            this.panel2.Controls.Add(this.rb_Error_cylinder_retract);
            this.panel2.Controls.Add(this.rb_Error_MachLock_extend);
            this.panel2.Controls.Add(this.rb_Error_PressureSenser);
            this.panel2.Controls.Add(this.rb_Error_PositionSenser);
            this.panel2.Location = new System.Drawing.Point(257, 240);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(348, 130);
            this.panel2.TabIndex = 26;
            // 
            // comboBox_PumpErr
            // 
            this.comboBox_PumpErr.Items.AddRange(new object[] {
            "泵站电动机启动线路短路\t\t        ",
            "泵站电动机启动线路断路\t\t\t    ",
            "泵站冗余电磁阀线路短路\t\t\t    ",
            "泵站冗余电磁阀线路断路\t\t\t    ",
            "泵站机械锁马达电磁阀线路短路\t\t",
            "泵站机械锁马达电磁阀线路断路\t\t",
            "控制器发电机启动线路短路\t\t\t",
            "控制器发电机启动线路断路\t\t\t",
            "泵站比例阀线路短路\t\t                ",
            "泵站比例阀线路断路\t            "});
            this.comboBox_PumpErr.Location = new System.Drawing.Point(39, 22);
            this.comboBox_PumpErr.Name = "comboBox_PumpErr";
            this.comboBox_PumpErr.Size = new System.Drawing.Size(122, 20);
            this.comboBox_PumpErr.TabIndex = 2;
            this.comboBox_PumpErr.SelectedIndexChanged += new System.EventHandler(this.comboBox_PumpErr_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(120, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "故障";
            // 
            // rb_Error_cylinder_extend
            // 
            this.rb_Error_cylinder_extend.Location = new System.Drawing.Point(173, 26);
            this.rb_Error_cylinder_extend.Name = "rb_Error_cylinder_extend";
            this.rb_Error_cylinder_extend.Size = new System.Drawing.Size(165, 20);
            this.rb_Error_cylinder_extend.TabIndex = 0;
            this.rb_Error_cylinder_extend.Tag = DataPool.CmdDataType.cdtError_cylinder_extend_shortcircuit_3511_3514;
            this.rb_Error_cylinder_extend.Text = "油缸伸出电磁阀线路短路";
            // 
            // rb_Error_MachLock_retract
            // 
            this.rb_Error_MachLock_retract.Location = new System.Drawing.Point(173, 104);
            this.rb_Error_MachLock_retract.Name = "rb_Error_MachLock_retract";
            this.rb_Error_MachLock_retract.Size = new System.Drawing.Size(165, 20);
            this.rb_Error_MachLock_retract.TabIndex = 0;
            this.rb_Error_MachLock_retract.Tag = DataPool.CmdDataType.cdtError_MachLock_retract_shortcircuit_3511_3514;
            this.rb_Error_MachLock_retract.Text = "油缸机械锁缩回电磁阀线路短路";
            // 
            // rb_Error_Pump
            // 
            this.rb_Error_Pump.Location = new System.Drawing.Point(16, 53);
            this.rb_Error_Pump.Name = "rb_Error_Pump";
            this.rb_Error_Pump.Size = new System.Drawing.Size(150, 20);
            this.rb_Error_Pump.TabIndex = 0;
            this.rb_Error_Pump.Tag = DataPool.CmdDataType.cdtError_Pump_3501_3504;
            this.rb_Error_Pump.Text = "泵站及控制器 故障";
            // 
            // rb_Error_cylinder_retract
            // 
            this.rb_Error_cylinder_retract.Location = new System.Drawing.Point(173, 52);
            this.rb_Error_cylinder_retract.Name = "rb_Error_cylinder_retract";
            this.rb_Error_cylinder_retract.Size = new System.Drawing.Size(165, 20);
            this.rb_Error_cylinder_retract.TabIndex = 0;
            this.rb_Error_cylinder_retract.Tag = DataPool.CmdDataType.cdtError_cylinder_retract_shortcircuit_3511_3514;
            this.rb_Error_cylinder_retract.Text = "油缸缩回电磁阀线路短路";
            // 
            // rb_Error_MachLock_extend
            // 
            this.rb_Error_MachLock_extend.Location = new System.Drawing.Point(173, 78);
            this.rb_Error_MachLock_extend.Name = "rb_Error_MachLock_extend";
            this.rb_Error_MachLock_extend.Size = new System.Drawing.Size(165, 20);
            this.rb_Error_MachLock_extend.TabIndex = 0;
            this.rb_Error_MachLock_extend.Tag = DataPool.CmdDataType.cdtError_MachLock_extend_shortcircuit_3511_3514;
            this.rb_Error_MachLock_extend.Text = "油缸机械锁伸出电磁阀线路短路";
            // 
            // rb_Error_PressureSenser
            // 
            this.rb_Error_PressureSenser.Location = new System.Drawing.Point(16, 79);
            this.rb_Error_PressureSenser.Name = "rb_Error_PressureSenser";
            this.rb_Error_PressureSenser.Size = new System.Drawing.Size(150, 20);
            this.rb_Error_PressureSenser.TabIndex = 0;
            this.rb_Error_PressureSenser.Tag = DataPool.CmdDataType.cdtError_PressureSenser_3501_3504;
            this.rb_Error_PressureSenser.Text = "油缸压力传感器故障";
            // 
            // rb_Error_PositionSenser
            // 
            this.rb_Error_PositionSenser.Location = new System.Drawing.Point(16, 105);
            this.rb_Error_PositionSenser.Name = "rb_Error_PositionSenser";
            this.rb_Error_PositionSenser.Size = new System.Drawing.Size(150, 20);
            this.rb_Error_PositionSenser.TabIndex = 0;
            this.rb_Error_PositionSenser.Tag = DataPool.CmdDataType.cdtError_PositionSenser_3501_3504;
            this.rb_Error_PositionSenser.Text = "油缸长度传感器故障";
            // 
            // rb_CompAct_Pump
            // 
            this.rb_CompAct_Pump.Location = new System.Drawing.Point(190, 177);
            this.rb_CompAct_Pump.Name = "rb_CompAct_Pump";
            this.rb_CompAct_Pump.Size = new System.Drawing.Size(100, 20);
            this.rb_CompAct_Pump.TabIndex = 25;
            this.rb_CompAct_Pump.Text = "补偿动作";
            this.rb_CompAct_Pump.Tag = DataPool.CmdDataType.cdtCompAct_Pump_1010_1013;
            // 
            // rb_StartFailed_Pump
            // 
            this.rb_StartFailed_Pump.Location = new System.Drawing.Point(347, 177);
            this.rb_StartFailed_Pump.Name = "rb_StartFailed_Pump";
            this.rb_StartFailed_Pump.Size = new System.Drawing.Size(100, 20);
            this.rb_StartFailed_Pump.TabIndex = 36;
            this.rb_StartFailed_Pump.Text = "建压失败";
            this.rb_StartFailed_Pump.Tag = DataPool.CmdDataType.cdtStartFailed_Pump_1010_1013;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(377, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 37;
            this.label11.Text = "label11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(486, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 38;
            this.label12.Text = "label12";
            // 
            // il_idControl
            // 
            this.il_idControl.BackColor = System.Drawing.Color.White;
            this.il_idControl.BackImg = null;
            this.il_idControl.IMGContainer = null;
            this.il_idControl.ImgDisable = null;
            this.il_idControl.Location = new System.Drawing.Point(379, 37);
            this.il_idControl.Name = "il_idControl";
            this.il_idControl.Size = new System.Drawing.Size(68, 20);
            this.il_idControl.TabIndex = 39;
            this.il_idControl.Tag = DataPool.CmdDataType.cdtPosition_Real_3101_3108;
            this.il_idControl.Text = "0";
            this.il_idControl.TextX = -1F;
            this.il_idControl.TextY = -1F;
            // 
            // il_idrongyu
            // 
            this.il_idrongyu.BackColor = System.Drawing.Color.White;
            this.il_idrongyu.BackImg = null;
            this.il_idrongyu.IMGContainer = null;
            this.il_idrongyu.ImgDisable = null;
            this.il_idrongyu.Location = new System.Drawing.Point(488, 37);
            this.il_idrongyu.Name = "il_idrongyu";
            this.il_idrongyu.Size = new System.Drawing.Size(68, 20);
            this.il_idrongyu.TabIndex = 40;
            this.il_idrongyu.Tag = DataPool.CmdDataType.cdtPosition_Real_3101_3108;
            this.il_idrongyu.Text = "0";
            this.il_idrongyu.TextX = -1F;
            this.il_idrongyu.TextY = -1F;
            // 
            // rb_Estop1
            // 
            this.rb_Estop1.Location = new System.Drawing.Point(495, 71);
            this.rb_Estop1.Name = "rb_Estop1";
            this.rb_Estop1.Size = new System.Drawing.Size(100, 20);
            this.rb_Estop1.TabIndex = 41;
            this.rb_Estop1.Text = "急停1";
            this.rb_Estop1.Tag = DataPool.CmdDataType.cdtEStop_1010_1013;
            // 
            // VirtualSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(633, 409);
            this.Controls.Add(this.rb_Estop1);
            this.Controls.Add(this.il_idrongyu);
            this.Controls.Add(this.il_idControl);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.rb_StartFailed_Pump);
            this.Controls.Add(this.rb_CompAct_Pump);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rb_Limit_10);
            this.Controls.Add(this.rb_Limit_5);
            this.Controls.Add(this.rb__PowerSupply);
            this.Controls.Add(this.comboBoxMachLockState);
            this.Controls.Add(this.comboBoxcylinderState);
            this.Controls.Add(this.il_Position);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.il_Voltage);
            this.Controls.Add(this.il_Pressure_Pump);
            this.Controls.Add(this.il_Presure);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Name = "VirtualSetForm";
            this.Text = "VirtualSetForm";
            this.Click += new System.EventHandler(this.VirtualSetForm_Click);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ComCtrls.ImageLabel il_Presure;
        private System.Windows.Forms.Label label4;
        private ComCtrls.ImageLabel il_Position;
        private System.Windows.Forms.ComboBox comboBoxcylinderState;
        private System.Windows.Forms.ComboBox comboBoxMachLockState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private ComCtrls.ImageLabel il_Pressure_Pump;
        private System.Windows.Forms.Label label8;
        private ComCtrls.ImageLabel il_Voltage;
        private ComCtrls.ImageLabel rb__PowerSupply;
        private System.Windows.Forms.CheckBox rb_Limit_5;
        private System.Windows.Forms.CheckBox rb_Limit_10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox rb_Warn_HighPressure;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox rb_Warn_LowPosition;
        private System.Windows.Forms.CheckBox rb_Warn_HighPosition;
        private System.Windows.Forms.CheckBox rb_Warn_LowPressure;
        private System.Windows.Forms.CheckBox rb_Error_cylinder_extend;
        private System.Windows.Forms.CheckBox rb_Error_MachLock_retract;
        private System.Windows.Forms.CheckBox rb_Error_Pump;
        private System.Windows.Forms.CheckBox rb_Error_cylinder_retract;
        private System.Windows.Forms.CheckBox rb_Error_MachLock_extend;
        private System.Windows.Forms.CheckBox rb_Error_PressureSenser;
        private System.Windows.Forms.CheckBox rb_Error_PositionSenser;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_PumpErr;
        private System.Windows.Forms.CheckBox rb_CompAct_Pump;
        private System.Windows.Forms.CheckBox rb_StartFailed_Pump;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private ComCtrls.ImageLabel il_idControl;
        private ComCtrls.ImageLabel il_idrongyu;
        private System.Windows.Forms.CheckBox rb_Estop1;
    }
}