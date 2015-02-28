using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComCtrls;
using System.IO;


namespace KeyPadDemo_Win32
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show("MouseClick");


            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("textBox1_Click");
            KeypadForm f = KeypadForm.GetKeypadForm(textBox1.Text);
            if (f.ShowDialog(this) == DialogResult.OK)
                textBox1.Text = f.KeyText;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            imageButton1.Text = "123";
        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int listkey = Convert.ToInt32(numericUpDown1.Value * 10000 +
                numericUpDown2.Value * 100 + 10 +
                comboBox3.SelectedIndex);

            listBox1.Items.Add(listkey.ToString() + "+" +
                comboBox1.SelectedIndex.ToString() + "+" +
                DateTime.Now.Ticks.ToString()
                );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int listkey = Convert.ToInt32(numericUpDown1.Value * 10000 +
                    numericUpDown2.Value * 100 + 14 +
                    comboBox4.SelectedIndex);

            listBox2.Items.Add(listkey.ToString() + "+" +
                comboBox2.SelectedIndex.ToString() + "+" +
                DateTime.Now.Ticks.ToString()
                );
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileStream RecFile;
            BinaryWriter bw;
            string WarningRecFileName = @"I:\AdvaMACSystem\AdvaMACSystem_Win32\bin\Debug\Record\Warning.Rec";
            string ErrorRecFileName = @"I:\AdvaMACSystem\AdvaMACSystem_Win32\bin\Debug\Record\Error.Rec";

            //WARNING
            RecFile = new FileStream(WarningRecFileName, FileMode.Append);
             bw = new BinaryWriter(RecFile);


            try
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    string[] its = (listBox1.Items[i] as string).Split('+');

                    if (its.Length == 3)
                    {
                        bw.Write(Convert.ToInt32(its[0]));//4
                        bw.Write(Convert.ToInt32(its[1]));//4
                        bw.Write(Convert.ToInt64(its[2]));//8
                    }
                }
            }
            finally
            {
                bw.Flush();
                bw.Close();
                RecFile.Close();
            }


            //ERR
            RecFile = new FileStream(ErrorRecFileName, FileMode.Append);
             bw = new BinaryWriter(RecFile);


            try
            {
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    string[] its = (listBox2.Items[i] as string).Split('+');

                    if (its.Length == 3)
                    {
                        bw.Write(Convert.ToInt32(its[0]));//4
                        bw.Write(Convert.ToInt32(its[1]));//4
                        bw.Write(Convert.ToInt64(its[2]));//8
                    }
                }
            }
            finally
            {
                bw.Flush();
                bw.Close();
                RecFile.Close();
            }


        }
    }
}
