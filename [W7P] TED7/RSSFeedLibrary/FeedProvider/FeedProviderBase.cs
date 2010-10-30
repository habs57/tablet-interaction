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
        protected FeedProviderBase(Uri feedUri)
        {
            if (feedUri == null)
            {
                throw new ArgumentNullException("feedUri");
            }

            this.FeedUri = feedUri;
        }

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

        public void RequestFeeds()
        {
            IFeedProvider provider = this as IFeedProvider;
            if (provider.RequestFeedsDelegate != null)
            {
                provider.RequestFeedsDelegate(this);
            }
        }      

        #endregion Refresh

        public Uri FeedUri
        {
            get;
            internal set;
        }

        Action<IFeedProvider> IFeedProvider.RequestFeedsDelegate
        {
            get;
            set;
        }
    }
}