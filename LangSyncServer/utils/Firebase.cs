
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Api;
using Google.Cloud.Firestore;

namespace LangSyncServer.utils
{
    internal class Firebase
    {

        static FirestoreDb database;
        static string filepath = "";

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

    }
}
