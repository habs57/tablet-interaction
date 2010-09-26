using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;

namespace TablectionClientLibrary
{
    internal class ClientStateObject
    {
        internal Action<ClientStateObject> StateChangedHandler = null;

        public IPAddress ServerIP { get; private set; }
        public int Port { get; private set; }
        public bool IsListening { get; private set; }

        public void Start(string hostName, int port)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(hostName);
            IPAddress ipAddress = ipHostInfo.AddressList.FirstOrDefault(p => { return p.AddressFamily == AddressFamily.InterNetwork; });

            this.Start(ipAddress, port);
        }

        public void Start(IPAddress ip, int port)
        {
            this.ServerIP = ip;
            this.Port = port;
            this.IsListening = true;

            this.NotifyStateChanged();
        }

        public void Stop()
        {
            this.IsListening = false;

            this.NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            if (this.StateChangedHandler != null)
            {
                this.StateChangedHandler(this);
            }
        }
    }
}
