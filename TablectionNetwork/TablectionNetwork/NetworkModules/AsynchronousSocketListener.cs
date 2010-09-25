using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace TablectionServer.Network
{
    public enum ErrorType
    {
        Warning, //로그엔 나오고 무시해도 됨 
        Error    //로그에 당연히 나오고 서버도 재 시작 해야 됨 
    }

    /// <summary>
    /// 비동기 소켓 서버 예제 from MSDN
    /// modified by yoonjs2@naver.com
    /// </summary>
    internal class AsynchronousSocketListener
    {
        // Thread signal.
        private ManualResetEvent allDone = new ManualResetEvent(false);

        public AsynchronousSocketListener()
        {
        }

        protected void StartListening(ServerStateObject context)
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, context.Port);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                this.OnStartListening(listener);

                while (context.IsListening)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

                this.OnStopListening(listener);

            }           
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                this.OnError(ErrorType.Error, e);
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        private void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);

            this.OnAcceptClient(handler);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.Unicode.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the 
                    // client. Display it on the console.
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);

                    this.OnReceiveData(handler, content);

                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

                this.OnSendData(handler);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                this.OnError(ErrorType.Warning, e);
            }
        }

        #region Overrides

        protected virtual void OnStartListening(Socket listener)
        {

        }

        protected virtual void OnStopListening(Socket listener)
        {

        }

        protected virtual void OnError(ErrorType type, Exception exc)
        {

        }

        protected virtual void OnAcceptClient(Socket handler)
        {

        }

        protected virtual void OnReceiveData(Socket handler, string content)
        {
            // Echo the data back to the client.
            Send(handler, content);
        }

        protected virtual void OnSendData(Socket handler)
        {

        }

        #endregion
    }

}
