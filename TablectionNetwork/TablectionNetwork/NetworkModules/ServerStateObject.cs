
namespace TablectionServer.Network
{
    /// <summary>
    /// Server 동작 및 상태를 결정하는 오브젝트
    /// </summary>
    internal sealed class ServerStateObject
    {
        public int Port { get; private set; }
        public bool IsListening { get; private set; }

        public void Start(int port)
        {
            this.Port = port;
            this.IsListening = true;
        }

        public void Stop()
        {
            this.IsListening = false;
        }
    }
}
