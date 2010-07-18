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

using System.Net;
using System.Net.Sockets;
using System.Threading;


using System.Diagnostics;

namespace WpfApplication3
{

    public class UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
    }

    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9985);
            UdpClient newsock = new UdpClient(ipep);


            UdpState s = new UdpState();
            s.e = ipep;
            s.u = newsock;


      
            newsock.BeginReceive(new AsyncCallback(OnReceive), s);
            Debug.WriteLine("Waiting for a client...");

            
        }

        public static void OnReceive(IAsyncResult ar)
        {
            UdpState s = new UdpState();
            
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

            s.u = u;
            s.e = e;

            Byte[] receiveBytes = u.EndReceive(ar, ref e);
            Debug.WriteLine(Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length));
            u.BeginReceive(new AsyncCallback(OnReceive), s);
           
        }

    }
}
