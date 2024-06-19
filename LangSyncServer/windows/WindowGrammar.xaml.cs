using Google.Cloud.Firestore;
using LangSyncServer.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static LangSyncServer.utils.Constants;

namespace LangSyncServer.windows
{
    /// <summary>
    /// Lógica de interacción para WindowGrammar.xaml
    /// </summary>
    public partial class WindowGrammar : Window
    {

        private List<GrammarItem> grammarItems;


        public WindowGrammar()
        {
            InitializeComponent();
            grammarItems = new List<Constants.GrammarItem>();
        }

        private void btnAddGrammar_Click(object sender, RoutedEventArgs e)
        {

            if (tbEnglish.Text.Length == 0 || tbSpanish.Text.Length == 0) return;

            Constants.GrammarItem data = new Constants.GrammarItem();

            data.english = tbEnglish.Text;
            data.spanish = tbSpanish.Text;


            dataGridGrammar.Items.Add(data);

            tbEnglish.Clear();
            tbSpanish.Clear();

            grammarItems.Add(data);

            tbEnglish.Focus();

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (grammarItems.Count == 0) {

                MessageBox.Show("Add at least 1 vocabulary");
                return;

            }

            WindowWaitingPlayers windowWaitingPlayers = new WindowWaitingPlayers(grammarItems);

            windowWaitingPlayers.Show();
            Close();


        }

    }
}
