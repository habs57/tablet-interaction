using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TablectionClientLibrary;

namespace TablectionClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TablectionClientLib _client;

        public MainWindow()
        {
            InitializeComponent();

            _client = new TablectionClientLib();
            
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            bool result = _client.Connect("111.111.111.111", "00");
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            bool result = _client.Send("aaaaaaaaaaaaaa");
        }
    }
}
