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
    public class RSSParser
    {        
        private XDocument _Document = null;

        /// <summary>
        /// Load Xml data from TextReader
        /// </summary>
        /// <param name="reader">reader</param>
        /// <returns>true : read success, false ; read failed</returns>
        public bool Load(System.IO.TextReader reader)
        {
            if (reader == null)
            {
                return false;
            }

            _Document = XDocument.Load(reader);

            return _Document != null;
        }

        private IEnumerable<XElement> GetRoot()
        {
            if (_Document == null)
            {
                return null;
            }

            var value = from item in _Document.Descendants(XName.Get("rss"))
                        select item;

            return value;
        }

        public IEnumerable<XElement> Element(string name)
        {
            var root = this.GetRoot();

            if (root == null)
            {
                return null;
            }

            var value = from item in root.Descendants(XName.Get(name))
                        select item;

            return value;
        }

        public IEnumerable<XElement> Items
        {
            get
            {
                return this.Element("item");
            }
        }
    }
}
