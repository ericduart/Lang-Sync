using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSyncServer.utils
{
    internal class Firebase
    {

        static FirestoreDb? database = null;
        static string filepath = "";
        private static string PARTIES_COLLECTION_NAME = "parties";

        public static async void init(string fireConfigContent)
        {

            filepath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName())) + ".json";
            File.WriteAllText(filepath, fireConfigContent);
            File.SetAttributes(filepath, FileAttributes.Hidden);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);

            string value = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");

            database = FirestoreDb.Create("lang-sync");

            File.Delete(filepath);

        }

        public static async Task<bool> createPartyCode(string partyCode, List<Constants.GrammarItem> grammarItems)
        {

            try
            {
                DocumentReference doc = database.Collection(PARTIES_COLLECTION_NAME).Document(partyCode);

                DocumentSnapshot snapshot = await doc.GetSnapshotAsync();

                if (!snapshot.Exists)
                {

                    var objects = grammarItems.Select(item => new { english = item.english, spanish = item.spanish }).ToArray();

                    await doc.CreateAsync(new { players = new List<object>(), grammar = objects, canJoin = true, gameEnded = false, currentGrammar = new { english = "", spanish = "" } });

                    return true;
                }
                else return false;


            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static DocumentReference GetDocumentReference(string partyCode)
        {
            return database.Collection(PARTIES_COLLECTION_NAME).Document(partyCode);
        }

        public static async Task<Constants.PartyData> CloseGame(string PartyCode)
        {
            var doc = database.Collection(PARTIES_COLLECTION_NAME).Document(PartyCode);

            var snapshot = await doc.GetSnapshotAsync();

            if (snapshot.Exists) await doc.UpdateAsync(new Dictionary<string, object> { { "gameEnded", true } });

            Constants.PartyData partyData = new Constants.PartyData();

            // Dictionary<string, object> playersData = new Dictionary<string, object>();

            var playersData = await GetDataFromCollection(doc.Collection("playersData"));

            var playersDataFormated = playersData.ToDictionary(x => x.Key, x => {
            
                var list = new List<Constants.PlayerData>();

                if (x.Value is Dictionary<string, object> pData)
                {
                    foreach (var item in pData)
                    {

                        if (item.Value is Dictionary<string, object> grammarData)
                        {

                            list.Add(new Constants.PlayerData { grammar = item.Key, isCorrect = (bool)(grammarData["isCorrect"] ?? false), userInput = grammarData["input"].ToString() ?? string.Empty });

                        }

                    }
                }

                return list;
            
            });


            partyData.PartyCode = PartyCode;
            partyData.playersRanking = snapshot.GetValue<List<string>>("players");
            partyData.dataPlayers = playersDataFormated;

            return partyData;

        }

        public static async Task<Dictionary<string, object>> GetDataFromCollection(CollectionReference collection)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            foreach (var snapshot in await collection.GetSnapshotAsync())
            {

                var docId = snapshot.Id;
                var docData = new Dictionary<string, object>();

                foreach (var item in snapshot.ToDictionary())
                {
                    docData.Add(item.Key, item.Value);
                }

                data.Add(docId, docData);
            }

            return data;
        }

    }
}
