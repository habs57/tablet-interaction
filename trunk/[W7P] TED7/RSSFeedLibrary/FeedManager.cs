using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;

namespace RSSFeedLibrary
{
    public sealed class FeedManager : IDisposable
    {
        /// <summary>
        /// This class stores the state of the request.
        /// </summary>
        private sealed class RequestState
        {            
            public const int BUFFER_SIZE = 1024;
            public StringBuilder requestData;
            public byte[] bufferRead;
            public WebRequest request;
            public WebResponse response;
            public Stream responseStream;
            public RequestState()
            {
                bufferRead = new byte[BUFFER_SIZE];
                requestData = new StringBuilder(string.Empty);
                request = null;
                responseStream = null;
            }
        }

        /// <summary>
        /// Object contains and process information of feed per provider
        /// </summary>
        private sealed class FeedRequestObject : IDisposable
        {
            public FeedRequestObject(IFeedProvider provider)
            {
                if (provider == null)
                {
                    throw new ArgumentNullException("provider");
                }

                this.Provider = provider;
            }

            #region Fields            

            /// <summary>
            /// feed provider, contains interaction logic between feed manager & user
            /// </summary>
            private IFeedProvider Provider { get; set; }

            /// <summary>
            /// Object requests to actual uri
            /// </summary>
            private WebRequest _RequestObject = null;
            private WebRequest RequestObject 
            { 
                get
                {
                    if (_RequestObject == null)
                    {
                        this.CreateNewRequest();
                    }
                    else if (_RequestObject.RequestUri == null)
                    {
                        this.CreateNewRequest();
                    }
                    else
                    {
                        bool isUriEquals = _RequestObject.RequestUri.AbsoluteUri.Equals(this.Provider.FeedUri.AbsoluteUri);
                        if (isUriEquals == false)
                        {
                            this.CreateNewRequest();
                        }
                    }
                    return _RequestObject;
                }                
            }

            /// <summary>
            /// Asynchronous information for feed request
            /// </summary>
            private IAsyncResult AsyncResult { get; set; }

            #region Request State
            
            /// <summary>
            /// 
            /// </summary>
            private RequestState _RequestStateObject = null;
            public RequestState RequestStateObject
            {
                get
                {
                    if (_RequestStateObject == null)
                    {
                        _RequestStateObject = new RequestState();
                    }
                    return _RequestStateObject;
                }
            }

            #endregion
            #endregion

            /// <summary>
            /// Create new feed request
            /// </summary>
            private void CreateNewRequest()
            {
                WebRequest webRequest = HttpWebRequest.Create(this.Provider.FeedUri);
                _RequestObject = webRequest;        
            }
            
            /// <summary>
            /// Response callback for asynchronous read
            /// </summary>
            /// <param name="result"></param>
            private void ResponseAsyncCallback(IAsyncResult result)
            {
                try
                {
                    // Set the State of request to asynchronous.
                    RequestState reqState = (RequestState)result.AsyncState;
                    WebRequest webRequest = reqState.request;
                    // End the Asynchronous response.
                    reqState.response = webRequest.EndGetResponse(result);
                    // Read the response into a 'Stream' object.
                    Stream responseStream = reqState.response.GetResponseStream();
                    reqState.responseStream = responseStream;
                    // Begin the reading of the contents of the HTML page and print it to the console.
                    IAsyncResult asynchronousResultRead = responseStream.BeginRead(reqState.bufferRead, 0, RequestState.BUFFER_SIZE, new AsyncCallback(ReadResponseCallback), reqState);

                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine("WebException raised!");
                    System.Diagnostics.Debug.WriteLine(string.Format("\n{0}", ex.Message));
                    System.Diagnostics.Debug.WriteLine(string.Format("\n{0}", ex.Status));
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception raised!");
                    System.Diagnostics.Debug.WriteLine(ex.ToString());                    
                }
            }

            /// <summary>
            /// Actual callback for read response stream
            /// </summary>
            /// <param name="result"></param>
            private void ReadResponseCallback(IAsyncResult result)
            {
                try
                {
                    // Result state is set to AsyncState.
                    RequestState myRequestState = (RequestState)result.AsyncState;
                    Stream responseStream = myRequestState.responseStream;
                    int read = responseStream.EndRead(result);
                    // Read the contents of the HTML page and then print to the console.
                    if (read > 0)
                    {
                        myRequestState.requestData.Append(Encoding.Unicode.GetString(myRequestState.bufferRead, 0, read));
                        IAsyncResult asynchronousResult = responseStream.BeginRead(myRequestState.bufferRead, 0, RequestState.BUFFER_SIZE, new AsyncCallback(ReadResponseCallback), myRequestState);
                    }
                    else
                    {
                        //읽기가 끝나면 이쪽 수행
                        System.Diagnostics.Debug.WriteLine("\nThe HTML page Contents are:  ");
                        if (myRequestState.requestData.Length > 1)
                        {
                            string sringContent;
                            sringContent = myRequestState.requestData.ToString();
                            System.Diagnostics.Debug.WriteLine(sringContent);
                        }
                        System.Diagnostics.Debug.WriteLine("\nPress 'Enter' key to continue........");
                        responseStream.Close();                        
                    }
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine("WebException raised!");
                    System.Diagnostics.Debug.WriteLine(string.Format("\n{0}", ex.Message));
                    System.Diagnostics.Debug.WriteLine(string.Format("\n{0}", ex.Status));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception raised!");
                    System.Diagnostics.Debug.WriteLine(ex.ToString());                    
                }
            }

            #region Public Methods 

            /// <summary>
            /// Begin request to feed uri in feed provider 
            /// </summary>
            public void BeginRequest()
            {
                if (this.Provider.FeedUri == null)
                {
                    throw new InvalidOperationException();
                }

                this.AsyncResult = this.RequestObject.BeginGetResponse(ResponseAsyncCallback, this.RequestStateObject);                     
            }

            /// <summary>
            /// stop request
            /// </summary>
            public void StopRequest()
            {
                if (this.AsyncResult == null)
                {
                    return;
                }

                WebResponse response = this.RequestObject.EndGetResponse(this.AsyncResult);
                response.Close();
            }

            #region IDisposable Members

            /// <summary>
            /// Dispose all request
            /// </summary>
            public void Dispose()
            {
                this.StopRequest();
            }

            #endregion IDisposable Members

            #endregion Public Methods
        }

        /// <summary>
        /// Feed provider & requests 
        /// </summary>
        private Dictionary<IFeedProvider, FeedRequestObject> _FeedProviders = null;
        private Dictionary<IFeedProvider, FeedRequestObject> FeedProviders 
        {
            get
            {
                if (_FeedProviders == null)
                {
                    _FeedProviders = new Dictionary<IFeedProvider, FeedRequestObject>();
                }
                return _FeedProviders;            
            }
        }

        /// <summary>
        /// Try send request to web again with refreshed request from provider
        /// </summary>
        /// <param name="provider"></param>
        private void FeedRequestDelegateCallback(IFeedProvider provider)
        {
            FeedRequestObject reqObject = null;

            bool isReqObjectExist = this.FeedProviders.TryGetValue(provider, out reqObject);
            if (isReqObjectExist == true && reqObject != null)
            {
                reqObject.BeginRequest();
            }
        }

        /// <summary>
        /// Begin asynchronous parsing
        /// </summary>
        /// <param name="provider">provider</param>
        /// <returns>true to register success</returns>
        public bool Register(IFeedProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            if (this.FeedProviders.ContainsKey(provider) == true)
            {
                return false;
            }

            FeedRequestObject requestObject = this.GetRequestObject(provider);
            this.FeedProviders.Add(provider, requestObject);

            return true;
        }

        /// <summary>
        /// Get request object from provider
        /// </summary>
        /// <param name="provider">provider</param>
        /// <returns>request object</returns>
        private FeedRequestObject GetRequestObject(IFeedProvider provider)
        {
            provider.RequestFeedsDelegate = this.FeedRequestDelegateCallback;
            FeedRequestObject reqObject = new FeedRequestObject(provider);
            return reqObject;
        }

      
        /// <summary>
        /// Deregister feed provider for stop request feeds 
        /// </summary>
        /// <param name="provider">provider for deregister</param>
        public void DeRegister(IFeedProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            if (this.FeedProviders.ContainsKey(provider) == false)
            {
                return;
            }

            FeedRequestObject reqObject = null;
            bool result = this.FeedProviders.TryGetValue(provider, out reqObject);
            if (result == true)
            {
                reqObject.StopRequest();
            }

            //해당 파서에 대한 피딩 중단
            //프로바이더 제거
            provider.RequestFeedsDelegate = null;
            this.FeedProviders.Remove(provider);
        }

        /// <summary>
        /// Dispose all things 
        /// </summary>
        public void Dispose()
        {
            foreach (var item in this.FeedProviders)
            {
                item.Value.Dispose();
            }

            this.FeedProviders.Clear();
        }
    }
}

