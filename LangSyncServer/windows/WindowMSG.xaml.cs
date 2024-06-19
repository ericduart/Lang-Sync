﻿using System;
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

namespace LangSyncServer.windows
{
    /// <summary>
    /// Lógica de interacción para WindowMSG.xaml
    /// </summary>
    public partial class WindowMSG : Window
    {
        public WindowMSG(string message)
        {
            InitializeComponent();

            Title = message;
            lblMessage.Content = message;            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
