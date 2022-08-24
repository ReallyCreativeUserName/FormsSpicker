using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsSpicker
{
    public partial class Form5 : Form
    {
        private string text { get; set; }
        private string title { get; set; }
        private string inhalt { get; set; }
        private string pfad { get; set; }
        private bool save { get; set; }
        public Form5(string text, string pfad)
        {
            InitializeComponent();
            this.pfad = pfad;
            this.text = text;
            string[] teil = text.Split(";");
            title = teil[0];
            inhalt = teil[1];
            textBox1.Text = title;
            textBox2.Text = inhalt;
            save = false;
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            Form3 fehlerfenster = new Form3("");

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                if (textBox1.Text == "" && textBox2.Text == "")
                {
                    fehlerfenster = new Form3("Titel und Notiz ohne Inhalt");
                }
                if (textBox1.Text == "")
                {
                    fehlerfenster = new Form3("Titel ohne Inhalt");
                }
                if (textBox2.Text == "")
                {
                    fehlerfenster = new Form3("Notiz ohne Inhalt");
                }

                await Task.Run(() => fehlerfenster.ShowDialog());
                return;
            }
            else
            {
                string savefile = textBox1.Text + ";" + textBox2.Text;

                using (StreamWriter writer = new StreamWriter(pfad + textBox1.Text + ".txt"))
                {
                    await writer.WriteLineAsync(savefile);
                }

                save = true;
            }

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            File.Delete(pfad + title);
        }
    }
}
