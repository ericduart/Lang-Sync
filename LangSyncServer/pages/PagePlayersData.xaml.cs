using LangSyncServer.utils;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LangSyncServer.pages
{
    /// <summary>
    /// Lógica de interacción para PagePlayersData.xaml
    /// </summary>
    public partial class PagePlayersData : Page
    {
        public PagePlayersData()
        {
            InitializeComponent();

        }

        public void setGrammarInfo(Constants.GrammarItem item)
        {
            try
            {
                Helpers.ChangeControlTextSafe(englishGrammar, item.english);
                Helpers.ChangeControlTextSafe(spanishGrammar, item.spanish);

            } catch(Exception ex) {

                Helpers.logging("Exception " + ex.Message);

            }
        }

        public void setPlayersCount(int numPlayersanswered, int total)
        {
            try
            {
                Helpers.ChangeControlTextSafe(playersCount, $"{numPlayersanswered}/{total}");

            }
            catch (Exception ex)
            {

                Helpers.logging("Exception " + ex.Message);

            }
        }

        public void setPlayerData(Constants.PlayerData pData)
        {

            if (!dataGridPlayersData.CheckAccess())
            {
                dataGridPlayersData.Dispatcher.Invoke(new Action(() =>
                {
                    setPlayerData(pData);
                }));

                return;

            }

            dataGridPlayersData.Items.Add(new { option = pData.grammar, userInput = pData.userInput, isCorrect = pData.isCorrect, IsError = !pData.isCorrect });
        }

        public void clearDataGrid()
        {
            Helpers.cleanDataGridSafe(dataGridPlayersData);
        }

    }
}
