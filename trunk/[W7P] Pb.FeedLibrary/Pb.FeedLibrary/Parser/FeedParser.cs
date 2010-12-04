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
    public class FeedParser : Parser
    {
        /// <summary>
        /// Constructor of RSS Parser
        /// </summary>
        public FeedParser()   
            : base(XName.Get("feed"))
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
