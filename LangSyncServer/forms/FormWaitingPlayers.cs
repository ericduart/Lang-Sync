using Google.Api;
using Google.Cloud.Firestore;
using LangSyncServer.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LangSyncServer.forms
{
    public partial class FormWaitingPlayers : Form
    {

        private List<Constants.GrammarItem> grammarItems;
        private FirestoreChangeListener playersListener;
        private List<string> players;

        public FormWaitingPlayers(List<Constants.GrammarItem> items)
        {
            InitializeComponent();

            grammarItems = items;

            players = new List<string>();

            Thread t = new Thread(getPartyCode);

            t.Name = "GettingPartyCode";

            t.Start();
            
            

        }

        private async void getPartyCode()
        {
            int length = 5;
            string randomPartyCode = string.Empty;
            while (true)
            {
                randomPartyCode = Helpers.getRandomString(length);

                bool res = await Firebase.createPartyCode(randomPartyCode, grammarItems);

                if (res) {

                    Helpers.changeLabelTextSafe(labelPartyCode, randomPartyCode);
                    break;

                } else
                {
                    length++;
                }

            }

            DocumentReference reference = Firebase.GetDocumentReference(randomPartyCode);

            listenForPlayers(reference);
        }

        private void listenForPlayers(DocumentReference playersCollReference)
        {
            playersListener = playersCollReference.Listen(async (snapshot, _) =>
            {

                try
                {
                    string[] firebasePlayers = snapshot.GetValue<string[]>("players");

                    Helpers.CleanFlowLayoutContentSafe(flowLayoutPanel1);
                    players.Clear();

                    foreach (string player in firebasePlayers)
                    {
                        Label playerLabel = new Label();
                        playerLabel.AutoSize = true;
                        playerLabel.Text = player;

                        players.Add(player);

                        Helpers.AddLabelToFlowLayoutSafe(flowLayoutPanel1, playerLabel);
                    }

                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            });
        }
    }
}
