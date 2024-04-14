using LangSync;
using LangSyncServer.utils;

namespace LangSyncServer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            string dotEnv = Path.Combine(Helpers.getRoot(), ".env");
            config.DotEnv.Load(dotEnv);

            Helpers.initLogs();

            Firebase.init();
            ApplicationConfiguration.Initialize();
            Application.Run(new FormIntro());
        }
    }
}