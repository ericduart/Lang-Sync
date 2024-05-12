using Google.Cloud.Firestore;
using LangSyncServer.pages;
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
using static LangSyncServer.utils.Constants;
using DocumentReference = Google.Cloud.Firestore.DocumentReference;

namespace LangSyncServer.windows
{
    /// <summary>
    /// Lógica de interacción para WindowWaitingPlayers.xaml
    /// </summary>
    public partial class WindowWaitingPlayers : Window
    {

        private List<GrammarItem> grammarItems;
        private List<PlayerData> playersData;
        private FirestoreChangeListener docRef;
        private FirestoreChangeListener playersDataListener;

        GrammarItem currentGrammar;
        private DocumentReference partyRef;
        private List<string> players;
        private bool isGameStarted = false;
        private string partyCode = string.Empty;

        public static EventHandler<string> partyCodeFound;

        public WindowWaitingPlayers(List<Constants.GrammarItem> items)
        {
            InitializeComponent();
            this.grammarItems = items;
            content.Content = new pages.PageWaitingPlayers(grammarItems);

            players = new List<string>();
            playersData = new List<PlayerData>();
            currentGrammar = null;

            partyCodeFound += newPartyCode;
        }

        private void newPartyCode(object? sender, string e)
        {
            partyCode = e;
            partyRef = Firebase.GetDocumentReference(partyCode);
            listenForPlayers(partyRef);
        }

        private void listenForPlayers(DocumentReference playersCollReference)
        {
            docRef = playersCollReference.Listen(async (snapshot, _) =>
            {

                try
                {

                    PageWaitingPlayers page1 = null;


                    Application.Current.Dispatcher.Invoke(delegate () {
                        page1 = content.Content as PageWaitingPlayers;
                    });


                    // PLAYERS

                    if (!isGameStarted)
                    {
                        string[] firebasePlayers = snapshot.GetValue<string[]>("players");

                        // Helpers.CleanFlowLayoutContentSafe(flowLayoutPanel1);
                        page1?.clearWrapPanel();

                        players.Clear();

                        foreach (string player in firebasePlayers)
                        {

                            Application.Current.Dispatcher.Invoke(() => {

                                Label playerLabel = new Label();
                                playerLabel.Content = player;

                                players.Add(player);

                                page1?.addPlayer(playerLabel);

                            });

                        }
                    }

                    // CurrentGrammar updated

                    var currentGrammarDic = snapshot.GetValue<Dictionary<string, object>>("currentGrammar");

                    var currentGrammar = new Constants.GrammarItem
                    {
                        english = currentGrammarDic.GetValueOrDefault("english", "").ToString(),
                        spanish = currentGrammarDic.GetValueOrDefault("spanish", "").ToString()
                    };



                    if (!string.IsNullOrEmpty(currentGrammar.english))
                    {
                        // tabControl1.Invoke(new Action(() => { tabControl1.SelectedIndex = 1; }));
                        // CHANGE PAGE

                        Application.Current.Dispatcher.Invoke(delegate () {
                            content.Content = new PagePlayersData();
                        });

                    }

                    PagePlayersData pagePlayersData = null;

                    Application.Current.Dispatcher.Invoke(delegate () {
                        pagePlayersData = content.Content as PagePlayersData;
                    });

                    pagePlayersData?.setGrammarInfo(currentGrammar);

                    //// Players answered count

                    //Helpers.changeLabelTextSafe(lblPlayersAnswered, $"0/{players.Count}");
                    pagePlayersData?.setPlayersCount(0, players.Count);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    utils.Helpers.logging(ex.Message);
                }


            });

            playersDataListener = playersCollReference.Collection("playersData").Listen(async (snapshot, _) =>
            {


                try
                {

                    PagePlayersData pagePlayersData = null;

                    Application.Current.Dispatcher.Invoke(delegate () {
                        pagePlayersData = content.Content as PagePlayersData;
                    });


                    if (currentGrammar == null) return;


                    playersData.Clear();
                    pagePlayersData?.clearDataGrid();

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

                                pagePlayersData?.setPlayerData(p);

                            }

                        }

                    }

                    if (playersData.Count > 0) pagePlayersData?.setPlayersCount(playersData.Count, players.Count);
                } catch (Exception ex)
                {
                    utils.Helpers.logging(ex.Message);
                }


            });
            
        }

        private GrammarItem? getNextGrammar()
        {
            if (grammarItems.Count == 0) return null;

            GrammarItem current = grammarItems[0];

            grammarItems.RemoveAt(0);

            return current;

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (players.Count == 0)
            {
                MessageBox.Show("Wait for at least 2 players");
                return;
            }

            currentGrammar = getNextGrammar();

            if (currentGrammar == null)
            {
                var partyData = await Firebase.CloseGame(partyCode);              


                WindowPostGame window = new WindowPostGame(partyData);

                window.Show();
                Close();

            } else
            {
                await partyRef.UpdateAsync(new Dictionary<string, object>() { { "currentGrammar", new { english = currentGrammar.english, spanish = currentGrammar.spanish } } });
                isGameStarted = true;
            }
        }
    }
}
