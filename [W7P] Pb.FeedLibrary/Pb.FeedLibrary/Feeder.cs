using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Manage async feeds 
    /// </summary>
    public sealed class Feeder : IDisposable
    {
        private HttpWebRequest _RequestObject = null;
        public HttpWebRequest RequestObject 
        {
            get
            {
                if (_RequestObject == null)
                {
                    _RequestObject = HttpWebRequest.CreateHttp(this.Uri);
                }
                return _RequestObject;
            }
        }

        public bool Request(Uri uri)
        {
            if (uri == null)
            {
                return false;
            }

            this.Uri = uri;

            return true;
        }

        void IDisposable.Dispose()
        {
            
        }

        public Uri Uri { get; set; }
    }
}
