using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using LangSyncServer.utils;
using LangSyncServer.windows;
using System.Security.Cryptography;

namespace LangSyncServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowGrammar window = new WindowGrammar();

            window.Show();
            Close();

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            Task.Run(() =>
            {

                Helpers.ChangeLabelTextSafe(lblInfo, "Loading Modules");
                Thread.Sleep(1000);

                string dotEnv = Path.Combine(Helpers.getRoot(), ".env");
                config.DotEnv.Load(dotEnv);

                string key = Environment.GetEnvironmentVariable("key") ?? string.Empty;
                string iv = Environment.GetEnvironmentVariable("iv") ?? string.Empty;

                if (key == string.Empty || iv == string.Empty)
                {
                    MessageBox.Show("The app will close because either a key or an IV were not found");
                    Application.Current.Dispatcher.Invoke(() => { Application.Current.Shutdown(); });

                }

                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                string encodedFirebaseConfig = File.ReadAllText(Path.Join(basePath, "config.dat"));

                string decodedFirebaseConfig = Helpers.Decrypt(Convert.FromBase64String(encodedFirebaseConfig), Convert.FromBase64String(key), Convert.FromBase64String(iv));

                Helpers.initLogs();

                Helpers.ChangeLabelTextSafe(lblInfo, "Loading Firebase");

                Thread.Sleep(500);
                Firebase.init(decodedFirebaseConfig);

                pbLoader.Dispatcher.Invoke(new Action(() => { pbLoader.Visibility = Visibility.Hidden; }));
                lblInfo.Dispatcher.Invoke(new Action(() => { lblInfo.Visibility = Visibility.Hidden; }));
                btnStart.Dispatcher.Invoke(new Action(() => { btnStart.Visibility = Visibility.Visible; }));

            });

        }
    }
}