using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Provider that comm
    /// </summary>
    public class Provider
    {
        /// <summary>
        /// Provider Contstrutor
        /// </summary>
        /// <param name="uri">Uri to get Feeds</param>
        public Provider(Uri uri)
        {
            // TODO: Complete member initialization
            this.Uri = uri;
        }

        public Uri Uri { get; private set; }

        public bool Request()
        {
            throw new NotImplementedException();
        }
    }
}
