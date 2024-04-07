using Newtonsoft.Json;
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

namespace LangSync.forms
{
    public partial class FormGrammar : Form
    {
        public FormGrammar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxEnglish.Text.Length == 0 || textBoxSpanish.Text.Length == 0)
            {
                MessageBox.Show("Error. Missed english or spanish grammar.");
                return;
            }

            Label grammar = new Label();
            grammar.AutoSize = true;
            grammar.Text = $"English: {textBoxEnglish.Text}     Spanish: {textBoxSpanish.Text}";

            flowLayoutPanel1.Controls.Add(grammar);
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            string firstJsonFile = Array.Find(files, file => Path.GetExtension(file).Equals(".json", StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(firstJsonFile))
            {
                string content = File.ReadAllText(firstJsonFile);

                dynamic obj = JsonConvert.DeserializeObject<object>(content);

                string names = string.Empty;

                foreach (dynamic item in obj)
                {
                    string english = item.english;
                    string spanish = item.spanish;

                    addGrammar(english, spanish);

                }

            }

        }

        private void addGrammar(string english, string spanish)
        {
            if (string.IsNullOrEmpty(english) || string.IsNullOrEmpty(spanish)) return;

            Label grammar = new Label();
            grammar.AutoSize = true;
            grammar.Text = $"English: {english}     Spanish: {spanish}";

            flowLayoutPanel1.Controls.Add(grammar);


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
