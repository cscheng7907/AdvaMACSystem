using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComCtrls
{
    public partial class KeypadForm : Form
    {
        private static KeypadForm KeypadFormObject = null;

        public static KeypadForm GetKeypadForm(string text)
        {
            if (KeypadFormObject == null)
                KeypadFormObject = new KeypadForm();

            KeypadFormObject.KeyText = text;
            return KeypadFormObject;
        }


        public KeypadForm()
        {
            InitializeComponent();
        }

        private string keytext = string.Empty;
        public string KeyText
        {
            get { return keytext; }
            set
            {
                if (keytext != value)
                {
                    if (value.Length <= 12)
                    {
                        keytext = value;
                        label_input.Text = value;
                    }
                }
            }
        }


        private void imageButton_Click(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {
                ImageButton btn = (ImageButton)sender;


                char c = Convert.ToChar(btn.Tag);

                KeyText += c;
            }

        }

        private void imageButton_dot_Click(object sender, EventArgs e)
        {
            if (KeyText.IndexOf('.') < 0)
            {
                KeyText += '.';
            }
            else
            {
                //do nothing
            }
        }

        private void imageButton_Minus_Click(object sender, EventArgs e)
        {
            if (KeyText.IndexOf('-') < 0)
            {
                KeyText = '-' + KeyText;
            }
            else
            {
                KeyText = KeyText.Substring(1);
            }
        }

        private void imageButton_cr_Click(object sender, EventArgs e)
        {
            KeyText = string.Empty;
        }

        private void imageButton_es_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void imageButton_ent_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}