using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsSpicker
{
    public partial class Form1 : Form
    {
        private static CommonOpenFileDialog dialog = new CommonOpenFileDialog();
        string pfad = dialog.InitialDirectory = @"C:\Users";

        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Clear();

            if (pfad != String.Empty)
            {
                DirectoryInfo dir = new DirectoryInfo(pfad);
                FileInfo[] liste = dir.GetFiles();

                //Dateien in liste laden
                foreach (FileInfo file in liste)
                {
                    listBox1.Items.Add(file.Name);
                }
            }

            listBox1.MouseDoubleClick += new MouseEventHandler(listBox1_MouseDoubleClick);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private async void NewButton_Click(object sender, EventArgs e)
        {

            //forms 2 für neue notiz öffnen
            await Task.Run(() => CreateNode());
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            //notiz bearbeiten
            int index = this.listBox1.SelectedIndex;
            if (index != ListBox.NoMatches)
            {
                FileInfo file = new FileInfo(listBox1.SelectedItem.ToString());

                StreamReader reader = new StreamReader(pfad + file.Name);

                string text = reader.ReadToEnd();

                reader.Close();

                Form5 lesen = new Form5(text, pfad);

                lesen.ShowDialog();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //notiz löschen
            if (pfad != string.Empty)
            {
                File.Delete(pfad + listBox1.SelectedItem.ToString());
                listBox1.Items.Remove(listBox1.SelectedItem.ToString());
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            //menü beenden
            Application.Exit();
        }
        private async Task CreateNode()
        {
            //notiz erzeugen und in form2 schicken
            var createwindow = new Form2(pfad);
            await Task.Factory.StartNew(createwindow.ShowDialog);
        }
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //datei mit doppel click in form4 schicken und öffnen
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                FileInfo file = new FileInfo(listBox1.SelectedItem.ToString());

                using (StreamReader reader = new StreamReader(pfad + file.Name))
                {
                    string text = reader.ReadToEnd();
                    Form4 lesen = new Form4(text);
                    lesen.ShowDialog();
                };
            }

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (pfad != string.Empty)
            {
                listBox1.Items.Clear();

                DirectoryInfo dir = new DirectoryInfo(pfad);
                FileInfo[] liste = dir.GetFiles();

                //Dateien in liste laden
                foreach (FileInfo file in liste)
                {
                    listBox1.Items.Add(file.Name);
                }
            }
        }

        private void path_Click(object sender, EventArgs e)
        {
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                pfad = dialog.FileName + @"\";
            }
            return;
        }
    }
}
