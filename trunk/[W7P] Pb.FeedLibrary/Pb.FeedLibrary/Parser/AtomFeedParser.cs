using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Parser for feed blog
    /// </summary>
    public class AtomFeedParser : Parser
    {
        /// <summary>
        /// Constructor of Atom Feed Parser
        /// </summary>
        public AtomFeedParser()   
            : base(XName.Get("feed", "http://www.w3.org/2005/Atom"))
        {

        }

        /// <summary>
        /// Items list
        /// </summary>
        public IEnumerable<XElement> Items
        {
            get
            {
                return this.Element("entry");
            }
        }
    }
}
