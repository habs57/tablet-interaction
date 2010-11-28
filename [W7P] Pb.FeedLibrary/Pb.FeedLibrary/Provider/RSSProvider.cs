using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Provider provides RSS Feeds 
    /// </summary>
    public class RSSProvider : Provider
    {        
        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="uri">uri of RSS feed</param>
        /// <param name="filler">filler fills collection</param>
        public RSSProvider(Uri uri, IFiller filler)
            : base(uri, filler)
        {
            
        }

        /// <summary>
        /// Called when parser for RSS Created 
        /// </summary>
        /// <returns></returns>
        protected override Parser OnCreateParser()
        {
            return new RSSParser();
        }
    }
}
