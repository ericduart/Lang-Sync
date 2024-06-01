using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LangSyncServer.utils
{
    internal class Helpers
    {
        private static string logRoute;

        public static string getRoot()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string getRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            Random random = new Random();

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

        }

        public static void ChangeLabelTextSafe(Label label, string newText)
        {

            try {
                if (label.Dispatcher.CheckAccess())
                {
                    label.Content = newText;
                }
                else
                {
                    label.Dispatcher.Invoke(new Action(() => { label.Content = newText; }));
                }

            } catch(Exception e)
            {
                logging("Exception -> " + e.Message);
            }

        }

        public static void AddLabelToWrapPanelSafe(WrapPanel panel, Label label)
        {
            if (panel.Dispatcher.CheckAccess())
            {
                panel.Children.Add(label);
            }
            else
            {
                panel.Dispatcher.Invoke(new Action(() => { panel.Children.Add(label); }));
            }
        }

        public static void CleanWrapPanelContentSafe(WrapPanel panel)
        {
            if (panel.Dispatcher.CheckAccess())
            {
                panel.Children.Clear();
            }
            else
            {
                panel.Dispatcher.Invoke(new Action(() => { panel.Children.Clear(); }));
            }
        }

        public static void cleanDataGridSafe(DataGrid data)
        {
            if (data.Dispatcher.CheckAccess())
            {
                data.Items.Clear();
            } else
            {
                data.Dispatcher.Invoke(new Action(data.Items.Clear));
            }
        }

        public static void initLogs()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string appDirectory = Path.Join(appData, "LangSync");

            if (!Directory.Exists(appDirectory)) Directory.CreateDirectory(appDirectory);

            logRoute = Path.Combine(appDirectory, "log.txt");

            if (!File.Exists(logRoute)) File.Create(logRoute);

        }

        public static void logging(string log)
        {
            File.AppendAllText(logRoute, log + Environment.NewLine);
        }

        public static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }


    }
}
