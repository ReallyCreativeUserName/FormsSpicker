using System;
using System.Windows.Forms;

namespace FormsSpicker
{
    public partial class Form4 : Form
    {
        private string text { get; set; }
        private string title { get; set; }
        private string inhalt { get; set; }
        public Form4(string text)
        {
            InitializeComponent();
            this.text = text;
            string[] teil = text.Split(";");
            title = teil[0];
            inhalt = teil[1];
            textBox1.Text = title;
            textBox2.Text = inhalt;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
