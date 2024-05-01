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

        public static async void init()
        {

            string fireconfigFile = Path.Combine(Helpers.getRoot(), "firebase_config.json");

            string fireconfigContent = File.ReadAllText(fireconfigFile);

            filepath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName())) + ".json";
            File.WriteAllText(filepath, fireconfigContent);
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

        public static async Task<bool> CloseGame(string PartyCode)
        {
            var doc = database.Collection(PARTIES_COLLECTION_NAME).Document(PartyCode);

            var snapshot = await doc.GetSnapshotAsync();

            if (snapshot.Exists) await doc.UpdateAsync(new Dictionary<string, object> { { "gameEnded", true } });


            return true;

        }

    }
}
