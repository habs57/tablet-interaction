
using System.Collections.Generic;
using System.Windows.Threading;
using System.Xml.Linq;
using Pb.FeedLibrary;
using System.Linq;

using System.Text.RegularExpressions;

namespace TED7
{
    public class TEDBlogFiller : Filler<ItemViewModel>
    {
        private Dispatcher _Dispatcher = null;        

        public TEDBlogFiller(ICollection<ItemViewModel> collection, Dispatcher dispatcher)
            : base(collection)
        {
            this._Dispatcher = dispatcher;
        }
        
        protected override void OnFill(Parser parser, ICollection<ItemViewModel> collection)
        {
            AtomFeedParser rssParser = parser as AtomFeedParser;
            if (rssParser == null)
            {
                return;
            }

            if (collection == null)
            {
                return;
            }

            //TODO : fill out blog data
            var items = rssParser.Items;
            if (items != null)
            {
                this._Dispatcher.BeginInvoke(() =>
                {
                    collection.Clear();

                    int index = 0;
                    foreach (var item in items)
                    {
                        string title = item.Element(XName.Get("title", "http://www.w3.org/2005/Atom")).Value;
                        var author = item.Element(XName.Get("author", "http://www.w3.org/2005/Atom"));
                        string name = author.Element(XName.Get("name", "http://www.w3.org/2005/Atom")).Value;
                        
                        var itemVM = new ItemViewModel()
                        {
                            LineOne = title,
                            LineTwo = name
                        };

                        collection.Add(itemVM);

                        ++index;
                    }
                });


            }
        }
    }
}
