using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComCtrls;


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
    }
}
