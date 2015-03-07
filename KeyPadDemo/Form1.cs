using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComCtrls;

namespace KeyPadDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //    MessageBox.Show("right");
            listBox1.Items.Insert(0, e.Button.ToString() + " MouseDown");
        }

        private void label1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void imageLabel1_Click(object sender, EventArgs e)
        {
            KeypadForm f = KeypadForm.GetKeypadForm(imageLabel1.Text);
            if (f.ShowDialog() == DialogResult.OK)
                imageLabel1.Text = f.KeyText;


        }

        private void Form1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Insert(0, "Click");
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            listBox1.Items.Insert(0, "DoubleClick");

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {

            listBox1.Items.Insert(0, e.Button.ToString() + " MouseUp");

        }
    }
}