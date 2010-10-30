using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSSFeedLibrary;

namespace FeedParser.FeedProvider
{
    public class RSSFeedProvider : FeedProviderBase
    {
        public RSSFeedProvider(Uri feedUri)
            : base(feedUri)
        {

        }        
    }
}
