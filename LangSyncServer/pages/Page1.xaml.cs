using LangSyncServer.utils;
using LangSyncServer.windows;
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
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1(List<Constants.GrammarItem> items)
        {
            InitializeComponent();

            Task.Run(() => {

                getPartyCode(items);

            });

        }

        private async void getPartyCode(List<Constants.GrammarItem> items)
        {
            int length = 5;
            string randomPartyCode = string.Empty;

            while (true)
            {
                randomPartyCode = Helpers.getRandomString(length);

                bool res = await Firebase.createPartyCode(randomPartyCode, items);

                if (res)
                {
                    Helpers.ChangeLabelTextSafe(lblPartyCode, randomPartyCode);
                    break;
                } else
                {
                    length++;
                }

            }

            WindowWaitingPlayers.partyCodeFound.Invoke(null, randomPartyCode);

        }

        public void clearWrapPanel()
        {
            Helpers.CleanWrapPanelContentSafe(WrapPlayers);
        }

        public void addPlayer(Label player)
        {
            Helpers.AddLabelToWrapPanelSafe(WrapPlayers, player);
            utils.Helpers.logging("added player");
        }
    }
}
