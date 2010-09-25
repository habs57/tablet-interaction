using System;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace TablectionServer.Network
{
    public static class SocketExtension
    {
        public static string GetLocalInfo(this Socket socket)
        {
            return GetEndPointInfo((IPEndPoint)socket.LocalEndPoint);            
        }

        public static string GetRemoteInfo(this Socket socket)
        {
            return GetEndPointInfo((IPEndPoint)socket.RemoteEndPoint);
        }

        private static string GetEndPointInfo(IPEndPoint endPoint)
        {
            return string.Format("{0}:{1}", endPoint.Address.ToString(), endPoint.Port);
        }
    }

    public class TablectionServerErrorEventArgs : EventArgs
    {
        public ErrorType Type { get; internal set; }
        public Exception Exception { get; internal set; }
    }

    internal sealed class TablectionAsyncServer : AsynchronousSocketListener
    {
        private Logger _logger = null;

        public event EventHandler<TablectionServerErrorEventArgs> Error;

        public TablectionAsyncServer(Logger logger)
            : base()
        {
            _logger = logger;
        }

        private ServerStateObject _StateContextObject = null;
        private ServerStateObject StateContextObject 
        { 
            get
            {
                if (_StateContextObject == null)
                {
                    _StateContextObject = new ServerStateObject();
                }
                return _StateContextObject;            
            }
              
        }

        public bool BeginStartListening(int port)
        {
            if (this.StateContextObject.IsListening == false)
            {
                this.StateContextObject.Start(port);
                return ThreadPool.QueueUserWorkItem(new WaitCallback(BeginListeningWaitCallback), this.StateContextObject);
            }

            return false;
        }

        public void EndListening()
        {
            this.StateContextObject.Stop();
        }

        private void BeginListeningWaitCallback(object param)
        {
            this.StartListening(param as ServerStateObject);
        }
        
        protected override void OnError(ErrorType type, Exception exc)
        {
            _logger.CreateLog(LogType.Error, exc.ToString());

            if (type == ErrorType.Error)
            {
                this.EndListening();
            }

            if (this.Error != null)
            {
                this.Error(this, new TablectionServerErrorEventArgs() { Type = type, Exception = exc });
            }
        }

        protected override void OnStartListening(Socket listener)
        {
            _logger.CreateLog(LogType.Normal, "서버 시작됨... 클라이언트를 기다립니다.");
        }

        protected override void OnStopListening(Socket listener)
        {
            _logger.CreateLog(LogType.Normal, "서버를 종료합니다...");
        }

        protected override void OnReceiveData(Socket handler, string content)
        {
            _logger.CreateLog(LogType.Normal, handler.GetRemoteInfo(), string.Format("Received : {0}", content));            
        }              

        public bool IsRunning 
        { 
            get 
            {
                return this.StateContextObject.IsListening;
            }
        }
    }
}
