using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (label.Dispatcher.CheckAccess())
            {
                label.Content = newText;
            }
            else
            {
                label.Dispatcher.Invoke(new Action(() => { label.Content = newText; }));
            }
        }

        public static void AddLabelToWrapPanelSafe(WrapPanel panel, Label label)
        {
            if (panel.Dispatcher.CheckAccess())
            {
                panel.Dispatcher.Invoke(new Action(() => { panel.Children.Add(label); }));
            }
            else
            {
                panel.Children.Add(label);
            }
        }

        public static void CleanWrapPanelContentSafe(WrapPanel panel)
        {
            if (panel.Dispatcher.CheckAccess())
            {
                panel.Dispatcher.Invoke(new Action(() => { panel.Children.Clear(); }));
            }
            else
            {
                panel.Children.Clear();
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

    }
}
