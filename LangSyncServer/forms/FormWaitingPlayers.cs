using Google.Api;
using Google.Cloud.Firestore;
using LangSyncServer.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LangSyncServer.utils.Constants;

namespace LangSyncServer.forms
{
    public partial class FormWaitingPlayers : Form
    {

        private List<GrammarItem> grammarItems;
        private List<PlayerData> playersData;
        private FirestoreChangeListener docRef;
        private FirestoreChangeListener playersDataListener;


        GrammarItem currentGrammar;
        private DocumentReference partyRef;
        private List<string> players;
        private bool isGameStarted = false;

        public FormWaitingPlayers(List<Constants.GrammarItem> items)
        {
            InitializeComponent();

            grammarItems = items;

            players = new List<string>();
            playersData = new List<PlayerData>();
            currentGrammar = null;

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

                if (res)
                {

                    Helpers.changeLabelTextSafe(labelPartyCode, randomPartyCode);
                    break;

                }
                else
                {
                    length++;
                }

            }

            partyRef = Firebase.GetDocumentReference(randomPartyCode);

            listenForPlayers(partyRef);
        }

        private void listenForPlayers(DocumentReference playersCollReference)
        {
            docRef = playersCollReference.Listen(async (snapshot, _) =>
            {

                
                try
                {

                    // PLAYERS

                    if (!isGameStarted)
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
                    }

                    // CurrentGrammar updated

                    var currentGrammarDic = snapshot.GetValue<Dictionary<string, object>>("currentGrammar");

                    var currentGrammar = new Constants.GrammarItem
                    {
                        english = currentGrammarDic.GetValueOrDefault("english","").ToString(),
                        spanish = currentGrammarDic.GetValueOrDefault("spanish", "").ToString()
                    };



                    if (!string.IsNullOrEmpty(currentGrammar.english))
                    {
                        tabControl1.Invoke(new Action(() => { tabControl1.SelectedIndex = 1; }));

                    }

                    Helpers.changeLabelTextSafe(lblCurrentGrammarEnglish, currentGrammar.english);
                    Helpers.changeLabelTextSafe(lblCurrentGrammarSpanish, currentGrammar.spanish);


                    // Players answered count

                    Helpers.changeLabelTextSafe(lblPlayersAnswered, $"0/{players.Count}");


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            });

            playersDataListener = playersCollReference.Collection("playersData").Listen(async (snapshot, _) =>
            {

                if (currentGrammar == null) return;


                playersData.Clear();
                dataGridView1.Invoke(new Action(() => { dataGridView1.Rows.Clear(); }));

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {

                    var data = doc.ToDictionary();

                    foreach (var item in data)
                    {
                        if (item.Key.Equals(currentGrammar.english))
                        {
                            var values = (IDictionary<string, object>)item.Value;

                            PlayerData p = new PlayerData
                            {
                                name = doc.Id,
                                grammar = item.Key,
                                isCorrect = (bool)values["isCorrect"],
                                userInput = (string)values["input"]
                            };

                            playersData.Add(p);


                            dataGridView1.Invoke(new Action(() => {
                                dataGridView1.Rows.Add(new object[] { p.name, p.isCorrect.ToString(), p.userInput });
                            }));

                        }

                    }

                }

                if (playersData.Count > 0) Helpers.changeLabelTextSafe(lblPlayersAnswered, $"{playersData.Count}/{players.Count}");

            });

        }

        private GrammarItem? getNextGrammar()
        {
            if (grammarItems.Count == 0) return null;

            GrammarItem current = grammarItems[0];

            grammarItems.RemoveAt(0);

            return current;

        }

        private async void buttonStartGame_Click(object sender, EventArgs e)
        {
            if (players.Count == 0)
            {
                MessageBox.Show("Wait for at least 2 players");
                return;
            }

            currentGrammar = getNextGrammar();

            if (currentGrammar == null)
            {
                // Post partida
            } else
            {

                await partyRef.UpdateAsync(new Dictionary<string, object>() { { "currentGrammar", new { english = currentGrammar.english, spanish = currentGrammar.spanish } } });
                isGameStarted = true;

            }

        }
    }
}
