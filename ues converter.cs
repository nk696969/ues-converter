using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UES_Converter
{
    public partial class Form1 : Form
    {
        private string selectedFilePath = "";
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //blank
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select";
                dialog.Filter = "(*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = dialog.FileName;
                    textBox1.Text = "Selected: " + selectedFilePath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Textbox is blank");
                return;
            }

            try
            {
                string text = textBox1.Text;
                StringBuilder unicode = new StringBuilder();

                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];

                    if (c == '\r' || c == '\n')
                    {
                        unicode.Append(c);
                    }
                    else if (c > 0x7F || c == '"' || c == '\\' || c < 0x20)
                    {
                        unicode.Append("\\u");
                        unicode.Append(((int)c).ToString("X4"));
                    }
                    else
                    {
                        unicode.Append(c);
                    }
                }

                textBox1.Text = unicode.ToString();
            }
            catch
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Textbox is blank");
                    return;
                }

                try
                {
                    Clipboard.SetText(textBox1.Text);
                    MessageBox.Show("Coppied");
                }
                catch
                {
                    return;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Textbox is blank");
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export";
                dialog.Filter = "(*.txt)|*.txt";
                dialog.DefaultExt = "txt";
                dialog.AddExtension = true;
                dialog.FileName = "ues_output.txt";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(
                            dialog.FileName,
                            textBox1.Text,
                            new UTF8Encoding(false)
                        );

                        MessageBox.Show("Saved as \n" + dialog.FileName);
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }

    }
}
