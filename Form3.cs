using System;
using System.Windows.Forms;

namespace FormsSpicker
{
    public partial class Form3 : Form
    {
        public Form3(string fehler)
        {
            InitializeComponent();
            textBox1.Text = fehler;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
