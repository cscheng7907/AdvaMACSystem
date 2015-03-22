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
        private KeypadMode mode = KeypadMode.Normal;

        private static KeypadForm KeypadFormObject = null;

        public static KeypadForm GetKeypadForm(string text)
        {
            if (KeypadFormObject == null)
                KeypadFormObject = new KeypadForm();

            KeypadFormObject.ClientSize = new System.Drawing.Size(209, 254);
            KeypadFormObject.isFirstenter = true;
            KeypadFormObject.KeyText = text;
            KeypadFormObject.mode = KeypadMode.Normal;

            KeypadFormObject.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - KeypadFormObject.Width) / 2,
               (Screen.PrimaryScreen.WorkingArea.Height - KeypadFormObject.Height) / 2
                );

            return KeypadFormObject;
        }

        public static KeypadForm GetKeypadForm(string text, KeypadMode Mode)
        {
            if (KeypadFormObject == null)
                KeypadFormObject = new KeypadForm();

            KeypadFormObject.ClientSize = new System.Drawing.Size(209, 254);
            KeypadFormObject.isFirstenter = true;
            KeypadFormObject.KeyText = text;
            KeypadFormObject.mode = Mode;

            KeypadFormObject.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - KeypadFormObject.Width) / 2,
               (Screen.PrimaryScreen.WorkingArea.Height - KeypadFormObject.Height) / 2
                );

            return KeypadFormObject;
        }

        public KeypadForm()
        {
            InitializeComponent();
        }

        private bool isFirstenter = true;
        public bool IsFirstEnter
        {
            set
            {
                isFirstenter = value;
                if (isFirstenter)
                {
                    keytext = string.Empty;
                }
            }
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

                        if (keytext.Length > 1 && keytext[0] == '0' && keytext[1] != '0' && keytext[1] != '.')
                            keytext = keytext.Substring(1);

                        if (mode == KeypadMode.password)
                        {
                            label_input.Text = string.Empty;
                            for (int i = 0; i < keytext.Length; i++)
                            {
                                label_input.Text += '*'.ToString();
                            }
                        }
                        else
                            label_input.Text = keytext;
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

                if (isFirstenter)
                {
                    KeyText = c.ToString();
                    isFirstenter = false;
                }
                else
                    KeyText += c;
            }

        }

        private void imageButton_dot_Click(object sender, EventArgs e)
        {
            if (isFirstenter)
            {
                KeyText = '.'.ToString();
                isFirstenter = false;
            }
            else
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
            if (isFirstenter)
            {
                KeyText = '-'.ToString();
                isFirstenter = false;
            }
            else
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

    public enum KeypadMode
    {
        Normal = 0,
        password = 1

    }

}