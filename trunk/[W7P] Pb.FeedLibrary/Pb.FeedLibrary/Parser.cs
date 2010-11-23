using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Parsing feed data  
    /// </summary>
    public class Parser
    {
        private FeedHeader header;
        private FeedItem item;

        /// <summary>
        /// Contsturctor for parser
        /// </summary>
        /// <param name="header">header information</param>
        /// <param name="item">item information</param>
        public Parser(FeedHeader header, FeedItem item)
        {
            // TODO: Complete member initialization
            this.header = header;
            this.item = item;
        }

        /// <summary>
        /// Parsing data from reader
        /// </summary>
        /// <param name="xmlText">text reader object that contains feed text</param>
        public void Parse(TextReader xmlText)
        {
            XmlReader reader = XmlReader.Create(xmlText);
            while (reader.EOF == false)
            {
                bool canRead = reader.Read();
                if (canRead == true)
                {
                    string nodeName = reader.Name;
                    if (nodeName.Equals("item") == false)
                    {
                        //reads header
                        
                    }
                    else
                    {
                        //reads item
                    }
                }
            }
        }

        /// <summary>
        /// Delegate called when parsing header is done.
        /// </summary>
        public Action<FeedHeader> OnParseHeader { get; set; }

        /// <summary>
        /// Delegate called when parsing item is done.
        /// </summary>
        public Action<int, FeedItem> OnParseItem { get; set; }
    }
}
