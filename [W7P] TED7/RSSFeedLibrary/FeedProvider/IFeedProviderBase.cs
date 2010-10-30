using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSSFeedLibrary
{
    public interface IFeedProvider
    {
        Action<IFeedProvider> RequestFeedsDelegate { get; set; }
        void RequestFeeds();
        void NotifyRecieved();

        ICollection<TaggedProperty> GeneralInfo { get; set; }
        ICollection<Episode> Episodes { get; set; }

        Uri FeedUri { get; }
    }
}
