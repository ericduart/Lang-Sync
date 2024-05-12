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
                Helpers.ChangeLabelTextSafe(englishGrammar, item.english);
                Helpers.ChangeLabelTextSafe(spanishGrammar, item.spanish);

            } catch(Exception ex) {

                Helpers.logging("Exception " + ex.Message);

            }
        }

        public void setPlayersCount(int numPlayersanswered, int total)
        {
            try
            {
                Helpers.ChangeLabelTextSafe(playersCount, $"{numPlayersanswered}/{total}");

            }
            catch (Exception ex)
            {

                Helpers.logging("Exception " + ex.Message);

            }
        }

        public void setPlayerData(Constants.PlayerData pData)
        {
            if (dataGridPlayersData.CheckAccess()) {
                dataGridPlayersData.Items.Add(new { option = pData.grammar, userInput = pData.userInput, isCorrect = pData.isCorrect });

            } else
            {
                dataGridPlayersData.Dispatcher.Invoke(new Action(() => {
                    dataGridPlayersData.Items.Add(new { option = pData.grammar, userInput = pData.userInput, isCorrect = pData.isCorrect });
                }));
            }
        }

        public void clearDataGrid()
        {
            Helpers.cleanDataGridSafe(dataGridPlayersData);
        }

    }
}
