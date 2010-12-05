using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Base Parser
    /// </summary>
    public abstract class Parser
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rootName"></param>
        protected Parser(XName rootName)
        {
            _RootName = rootName;
        }

        private XName _RootName = null;        
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

        /// <summary>
        /// Get root element where start search
        /// </summary>
        /// <returns>root element</returns>
        private IEnumerable<XElement> GetRoot()
        {
            if (_Document == null)
            {
                return null;
            }

            IEnumerable<XElement> top = null;

            if (this._RootName == null)
            {
                top = _Document.Descendants();
            }
            else
            {
                top = _Document.Descendants(this._RootName);                
            }

            var value = from item in top
                        select item;

            return value;
        }

        /// <summary>
        /// Get element has specified name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>XmlElement has "name"</returns>
        public IEnumerable<XElement> Element(string name)
        {
            var root = this.GetRoot();

            if (root.FirstOrDefault() == null)
            {
                return null;
            }

            var value = from item in root.Descendants(XName.Get(name, _RootName.NamespaceName))
                        select item;

            return value;
        }
    }
}
