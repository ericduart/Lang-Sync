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
using System.Windows.Shapes;


namespace LangSyncServer.windows
{
    /// <summary>
    /// Lógica de interacción para WindowPostGame.xaml
    /// </summary>
    public partial class WindowPostGame : Window
    {

        private utils.Constants.PartyData partyData;
        

        public WindowPostGame(utils.Constants.PartyData p)
        {
            InitializeComponent();
            partyData = p;

            init();
        }

        private void init()
        {

            dataGridPlayerData.Items.Clear();
            stackPanelPlayers.Children.Clear();

            var labels = partyData.playersRanking.Select((p, i) => new Label { Content = $"{i+1}# {p}", Height = 50, HorizontalAlignment = HorizontalAlignment.Center, Tag=p });

            foreach ( Label label in labels)
            {
                label.MouseLeftButtonUp += player_clicked;
                stackPanelPlayers.Children.Add(label);
            }

        }

        private void player_clicked(object sender, MouseButtonEventArgs e)
        {
            Label? lClicked = sender as Label;

            if (lClicked != null)
            {
                setPlayerData(lClicked.Tag as string ?? string.Empty);
            }


        }

        private void setPlayerData(string player)
        {
            var playerData = partyData.dataPlayers.FirstOrDefault(x => x.Key == player);

            dataGridPlayerData.Items.Clear();

            if (playerData.Value != null && playerData.Value is List<Constants.PlayerData> playerList)
            {
                
                foreach (Constants.PlayerData p in playerList)
                {
                    //dataGridPlayersData.Items.Add(new { option = pData.grammar, userInput = pData.userInput, isCorrect = pData.isCorrect });
                    dataGridPlayerData.Items.Add(new { option = p.grammar, userInput = p.userInput, isCorrect = p.isCorrect });
                }
            }

        }
    }
}
