using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolekcijeOV
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        List<Osoba> ListaOsoba = new List<Osoba>();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Osoba osoba = new Osoba(textBox1.Text, textBox2.Text, Convert.ToInt16(textBox3.Text), Convert.ToChar(comboBox1.Text));
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox1.Focus();
                ListaOsoba.Add(osoba);
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message, "Pogrešan unos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bntObradi_Click(object sender, EventArgs e)
        {
            foreach (Osoba os in ListaOsoba)
            {
                os.ObradiDodatak();
            }
        }

        private void bntIspisi_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox4.Text = "Podatak 1\tPodatak 2\tPodatak 3\tPodatak 4\tDodatak\t\r\n";
            foreach (Osoba os in ListaOsoba)
            {
                textBox4.Text += os.ToString() + "\r\n";
            }
        }

        private void bntSave_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                /*if ((myStream = saveFileDialog1.OpenFile()) != null)
                {*/
                    StringBuilder output = new StringBuilder();
                    String separator = ", ";
                    String[] headings = { "Podatak1", "Podatak2", "Podatak3", "Podatak4", "Dodatak, " };
                    output.AppendLine(string.Join(separator, headings));

                    foreach (Osoba os in ListaOsoba)
                    {
                        String[] newLine = { os.ToCsvString() };
                        output.AppendLine(string.Join(separator, newLine));
                    }
                    try
                    {
                        File.AppendAllText(saveFileDialog1.FileName, output.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Pogrešan u .csv file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }
        }

        private void bntLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string fileContent = reader.ReadToEnd();
                        string[] content = fileContent.Split(',');
                        int numb = 5;
                        while (content.Length >= numb+5) 
                        {
                            Osoba osoba = new Osoba(content[numb].Substring(1, content[numb].Length - 1),
                                content[numb + 1].Substring(1, content[numb+1].Length - 1),
                                Convert.ToInt16(content[numb + 2].Substring(1, content[numb + 2].Length-1)),
                                Convert.ToChar(content[numb + 3].Substring(1, 1)));
                            ListaOsoba.Add(osoba);
                            numb += 5;
                        }
                        textBox4.Text = "File Loadded!";
                    }
                }
            }
            
        }
    }
}
