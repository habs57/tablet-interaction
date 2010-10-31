using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSSFeedLibrary;

namespace RSSFeedLibrary
{
    public class PodcastFeedProvider : FeedProviderBase
    {
        public PodcastFeedProvider(Uri feedUri)
            : base(feedUri)
        {

        }
    }
}
