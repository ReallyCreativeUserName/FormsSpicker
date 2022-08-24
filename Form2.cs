using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsSpicker
{
    public partial class Form2 : Form
    {
        private string title { get; set; }
        private string inhalt { get; set; }
        string pfad { get; set; }
        public Form2(string pfad)
        {
            InitializeComponent();
            this.pfad = pfad;
        }
        public Form2(string titel, string text)
        {
            InitializeComponent();
            this.title = titel;
            this.inhalt = text;
            textBox1.Text = this.title.ToString();
            textBox2.Text = this.inhalt.ToString();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            Form3 fehlerfenster = new Form3("");

            if (textBox1.Text == "" && textBox2.Text == "" || textBox1.Text == "" || textBox2.Text == "")
            {
                if (textBox1.Text == "" && textBox2.Text == "")
                {
                    fehlerfenster = new Form3("Titel und Notiz ohne Inhalt");
                }
                else if (textBox1.Text == "")
                {
                    fehlerfenster = new Form3("Titel ohne Inhalt");
                }
                else if (textBox2.Text == "")
                {
                    fehlerfenster = new Form3("Notiz ohne Inhalt");
                }

                await Task.Run(() => fehlerfenster.ShowDialog());
                return;
            }
            else
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(pfad);
                    FileInfo[] fileliste = dir.GetFiles("*.txt");
                    foreach (FileInfo file in fileliste)
                    {
                        if (file.Name == textBox1.Text + ".txt")
                        {
                            Form3 fehler = new Form3("Datei existiert schon");
                            await Task.Run(() => fehler.ShowDialog());
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                string savefile = textBox1.Text + ";" + textBox2.Text;

                try
                {
                    using (StreamWriter writer = new StreamWriter(pfad + textBox1.Text + ".txt"))
                    {
                        if (pfad == string.Empty)
                        {
                            Form3 fehler = new Form3("Es wurde kein Pfad angegeben");
                            await Task.Run(() => fehler.ShowDialog());
                        }
                        else
                        {
                            await writer.WriteLineAsync(savefile);
                        }
                    }
                }
                catch(System.UnauthorizedAccessException uae) 
                {
                    Form3 fehler = new Form3("Der Zugriff wurde verweigert.");
                    await Task.Run(() => fehler.ShowDialog());
                }
            }
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
