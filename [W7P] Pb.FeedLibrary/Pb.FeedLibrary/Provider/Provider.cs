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
        public Provider(Uri uri)
        {
            // TODO: Complete member initialization
            this.Uri = uri;
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
        /// Parser that parse feed data
        /// </summary>
        public abstract Parser Parser { get; }

        /// <summary>
        /// Filler that filles collection
        /// </summary>
        public abstract Filler Filler { get; }
    }
}
