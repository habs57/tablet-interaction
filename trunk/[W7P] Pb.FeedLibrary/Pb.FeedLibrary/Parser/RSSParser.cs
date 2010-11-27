using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Parser for RSS
    /// </summary>
    public class RSSParser : Parser
    {
        /// <summary>
        /// Constructor of RSS Parser
        /// </summary>
        public RSSParser()   
            : base(XName.Get("rss"))
        {

        }

        /// <summary>
        /// Items list
        /// </summary>
        public IEnumerable<XElement> Items
        {
            get
            {
                return this.Element("item");
            }
        }
    }
}
