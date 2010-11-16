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
    public class Feeder : IDisposable
#else
    internal class Feeder : IDisposable
#endif
    {
        /// <summary>
        /// Save request state for asynchronous operation
        /// </summary>
#if UNIT_TESTS
        public sealed class RequestState
#else
        internal sealed class RequestState
#endif 
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

#if UNIT_TESTS
        public RequestState Test_RequestState { get; set; }

        public Action<IAsyncResult> Test_RespCallback { get; set; }

        public Action<IAsyncResult> Test_ReadCallBack { get; set; }

        public string Test_FeedRawStringContent { get; set; }
#endif

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
            }

#if UNIT_TESTS

            Test_RespCallback(asynchronousResult);
#endif
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

#if UNIT_TESTS
                        this.Test_FeedRawStringContent = stringContent;
#endif
                    }

                    responseStream.Close();
                }

            }
            catch (WebException e)
            {
                // Need to handle the exception
            }

#if UNIT_TESTS

            Test_ReadCallBack(asyncResult);
#endif
        }

        public IAsyncResult Request(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
            else
            {
                this.Uri = uri;
            }            

            try
            {
                // Create a HttpWebrequest object to the desired URL.
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

                RequestState myRequestState = new RequestState();
                myRequestState.request = myHttpWebRequest;

                // Start the asynchronous request.
                IAsyncResult result = (IAsyncResult)myHttpWebRequest.BeginGetResponse(new AsyncCallback(RespCallback), myRequestState);

#if UNIT_TESTS
                this.Test_RequestState = myRequestState;
#endif

                return result;
            }
            catch (WebException e)
            {
                return null;
            }            
        }

        void IDisposable.Dispose()
        {
            
        }

        public Uri Uri { get; set; }
    }
}
