using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Pb.FeedLibrary
{
    public sealed class ItemsFeedParser : Parser
    {
        private string _itemsNodeName = string.Empty;

         /// <summary>
        /// Constructor of Atom Feed Parser
        /// </summary>
        private ItemsFeedParser(XName rootName, string itemsNodeName)
            : base(rootName)
        {
            if (string.IsNullOrEmpty(itemsNodeName) == true)
            {
                throw new ArgumentException("itemsNodeName");
            }

            this._itemsNodeName = itemsNodeName;
        }
        
        /// <summary>
        /// Items list
        /// </summary>
        public IEnumerable<XElement> Items
        {
            get
            {
                return this.Element(this._itemsNodeName);
            }
        }

        /// <summary>
        /// Atom 1.0 Parser
        /// </summary>
        public static ItemsFeedParser AtomParser
        {
            get
            {
                return new ItemsFeedParser(XName.Get("feed", "http://www.w3.org/2005/Atom"), "entry");
            }
        }

        /// <summary>
        /// RSS Parser
        /// </summary>
        public static ItemsFeedParser RSSParser
        {
            get
            {
                return new ItemsFeedParser(XName.Get("rss"), "item");
            }
        }
    }
}
