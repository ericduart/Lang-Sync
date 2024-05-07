﻿using System.Diagnostics;
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
            //this.Hide();
            WindowGrammar window = new WindowGrammar();

            window.Show();
            Close();

            
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            Task.Run(() =>
            {
                
                Helpers.ChangeLabelTextSafe(lblInfo, "Loading Firebase");

                string dotEnv = Path.Combine(Helpers.getRoot(), ".env");
                config.DotEnv.Load(dotEnv);

                Helpers.initLogs();

                Thread.Sleep(500);
                Firebase.init();

                pbLoader.Dispatcher.Invoke(new Action(() => { pbLoader.Visibility = Visibility.Hidden; }));
                lblInfo.Dispatcher.Invoke(new Action(() => { lblInfo.Visibility = Visibility.Hidden; }));
                btnStart.Dispatcher.Invoke(new Action(() => { btnStart.Visibility = Visibility.Visible; }));




            });

        }
    }
}