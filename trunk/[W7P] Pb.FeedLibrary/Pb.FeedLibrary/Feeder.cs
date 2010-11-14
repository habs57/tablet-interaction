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
        public HttpWebRequest RequestObject { get; set; }

        public bool Request(Uri uri)
        {
            return true;
        }

        void IDisposable.Dispose()
        {
            
        }
    }
}
