using Google.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void changeLabelTextSafe(Label label, string newText)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() => { label.Text = newText; }));
            } else
            {
                label.Text = newText;
            }
        }

        public static void AddLabelToFlowLayoutSafe(FlowLayoutPanel flowLayoutPanel, Label label)
        {
            if (flowLayoutPanel.InvokeRequired)
            {
                flowLayoutPanel.Invoke(new Action(() => { flowLayoutPanel.Controls.Add(label); }));
            } else
            {
                flowLayoutPanel.Controls.Add(label);
            }
        }

        public static void CleanFlowLayoutContentSafe(FlowLayoutPanel flowLayoutPanel)
        {
            if (flowLayoutPanel.InvokeRequired)
            {
                flowLayoutPanel.Invoke(new Action(() => { flowLayoutPanel.Controls.Clear(); }));
            } else
            {

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
