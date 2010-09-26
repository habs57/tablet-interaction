using System;

namespace TablectionServer.Network
{
    /// <summary>
    /// Server 동작 및 상태를 결정하는 오브젝트
    /// </summary>
    internal sealed class ServerStateObject
    {
        internal Action<ServerStateObject> StateChangedHandler = null;

        public int Port { get; private set; }
        public bool IsListening { get; private set; }

        public void Start(int port)
        {
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
