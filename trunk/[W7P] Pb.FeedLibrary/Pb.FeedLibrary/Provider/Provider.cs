using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Provider that comm
    /// </summary>
    public abstract class Provider
    {
        /// <summary>
        /// delegate for recieve request call and pass to Feeder
        /// </summary>
        public Action<Provider> RequestDelegate { get; set; }

        /// <summary>
        /// Provider Contstrutor
        /// </summary>
        /// <param name="uri">Uri to get Feeds</param>
        /// <param name="filler">filler that fill feed data to other object</param>
        protected Provider(Uri uri, IFiller filler)
        {
            if (filler == null)
            {
                throw new ArgumentNullException("filler");
            }

            // TODO: Complete member initialization
            this.Uri = uri;
            this.Filler = filler;
        }

        /// <summary>
        /// Uri of feed
        /// </summary>
        public Uri Uri { get; private set; }

        /// <summary>
        /// Request to get new feeds 
        /// </summary>
        /// <returns>true : can request, false : cannot request</returns>
        public bool Request()
        {
            if (this.Uri == null)
            {
                return false;
            }

            if (this.RequestDelegate != null)
            {
                this.RequestDelegate(this);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Called when parser has to be created         
        /// </summary>
        /// <returns>Parser object</returns>
        protected abstract Parser OnCreateParser();

        private Parser _Parser = null;
        /// <summary>
        /// Parser that parse feed data
        /// </summary>
        public Parser Parser 
        {
            get
            {
                if (_Parser == null)
                {
                    _Parser = this.OnCreateParser();
                }
                return _Parser;
            }
        }
        
        /// <summary>
        /// Filler that filles collection
        /// </summary>
        public IFiller Filler
        {
            get;
            private set;
        }
    }
}
