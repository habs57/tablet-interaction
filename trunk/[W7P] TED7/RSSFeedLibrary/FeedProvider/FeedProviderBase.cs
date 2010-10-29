using System;

using System.Collections.Generic;

namespace RSSFeedLibrary
{
    public class FeedRecievedEventHandler : EventArgs
    {
        public FeedRecievedEventHandler(IFeedProvider provider)
        {
            this.Provider = provider;
        }

        public IFeedProvider Provider { get; private set; }
    }

    public abstract class FeedProviderBase : IFeedProvider
    {
        #region NotifyRecieved

        public event EventHandler<FeedRecievedEventHandler> FeedRecieved;

        void IFeedProvider.NotifyRecieved()
        {
            if (this.FeedRecieved != null)
            {
                this.FeedRecieved(this, new FeedRecievedEventHandler(this));
            }
        }

        #endregion NotifyRecieved

        #region Data

        public ICollection<TaggedProperty> GeneralInfo
        {
            get;
            set;
        }

        public ICollection<Episode> Episodes
        {
            get;
            set;
        }

        #endregion Data

        #region Refresh

        public void RefreshFeeds()
        {
            IFeedProvider reciever = this as IFeedProvider;
            if (reciever.RefreshFeedsDelegate != null)
            {
                reciever.RefreshFeedsDelegate();
            }
        }

        Action IFeedProvider.RefreshFeedsDelegate
        {
            get;           
            set;           
        }

        #endregion Refresh
        
        public Uri FeedUri
        {
            get;
            set;
        }
    }
}
