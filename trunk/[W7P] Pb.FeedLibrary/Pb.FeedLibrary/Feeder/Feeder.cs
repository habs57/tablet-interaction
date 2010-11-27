using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Pb.FeedLibrary
{

    /// <summary>
    /// Manage async feeds 
    /// </summary>
#if UNIT_TESTS
    public
#else
    internal 
#endif
    sealed class Feeder
    {
        /// <summary>
        /// Init feeder
        /// </summary>
        /// <param name="uri">uri</param>
        public Feeder(System.Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            this.Uri = uri;
        }
        
        /// <summary>
        /// Save request state for asynchronous operation
        /// </summary>
        internal sealed class RequestState
        {
            // This class stores the State of the request.
            public const int BUFFER_SIZE = 1024;
            public StringBuilder requestData;
            public byte[] BufferRead;
            public HttpWebRequest request;
            public HttpWebResponse response;
            public Stream streamResponse;

            public RequestState()
            {
                BufferRead = new byte[BUFFER_SIZE];
                requestData = new StringBuilder(string.Empty);
                request = null;
                streamResponse = null;
            }
        }

        private void RespCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                // State of request is asynchronous.
                RequestState myRequestState = (RequestState)asynchronousResult.AsyncState;
                HttpWebRequest myHttpWebRequest2 = myRequestState.request;
                myRequestState.response = (HttpWebResponse)myHttpWebRequest2.EndGetResponse(asynchronousResult);

                // Read the response into a Stream object.
                Stream responseStream = myRequestState.response.GetResponseStream();
                myRequestState.streamResponse = responseStream;

                // Begin the Reading of the contents of the HTML page and print it to the console.
                IAsyncResult asynchronousInputRead = responseStream.BeginRead(myRequestState.BufferRead, 0, RequestState.BUFFER_SIZE, new AsyncCallback(ReadCallBack), myRequestState);
            }
            catch (WebException e)
            {
                // Need to handle the exception
                Logger.Log(e);
            }
        }

        private void ReadCallBack(IAsyncResult asyncResult)
        {
            try
            {
                RequestState myRequestState = (RequestState)asyncResult.AsyncState;
                Stream responseStream = myRequestState.streamResponse;
                int read = responseStream.EndRead(asyncResult);
                // Read the HTML page and then do something with it
                if (read > 0)
                {
                    myRequestState.requestData.Append(Encoding.UTF8.GetString(myRequestState.BufferRead, 0, read));
                    IAsyncResult asynchronousResult = responseStream.BeginRead(myRequestState.BufferRead, 0, RequestState.BUFFER_SIZE, new AsyncCallback(ReadCallBack), myRequestState);
                }
                else
                {
                    if (myRequestState.requestData.Length > 1)
                    {
                        string stringContent;
                        stringContent = myRequestState.requestData.ToString();
                        // do something with the response stream here
                        if (this.OnRead != null)
                        {
                            StringReader reader = new StringReader(stringContent);
                            this.OnRead(reader);
                        }
                    }

                    responseStream.Close();
                }
            }
            catch (WebException e)
            {
                // Need to handle the exception
                Logger.Log(e);
            }
        }

        /// <summary>
        /// Begin request
        /// </summary>        
        /// <returns>null : exception</returns>
        public IAsyncResult Request()
        {
            try
            {
                // Create a HttpWebrequest object to the desired URL.
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(this.Uri);
                
                RequestState myRequestState = new RequestState();
                myRequestState.request = myHttpWebRequest;

                // Start the asynchronous request.
                IAsyncResult result = (IAsyncResult)myHttpWebRequest.BeginGetResponse(new AsyncCallback(RespCallback), myRequestState);

                return result;
            }
            catch (WebException e)
            {
                Logger.Log(e);
            }

            return null;
        }

        /// <summary>
        /// Feed Uri
        /// </summary>
        public Uri Uri { get; set; }

        public Action<TextReader> OnRead { get; set; }
        
    }
}
