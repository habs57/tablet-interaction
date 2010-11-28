
using System.Windows.Threading;
using System.Collections.Generic;
using Pb.FeedLibrary;
using System.Xml.Linq;

namespace TED7
{
    public class TEDVideoFiller : Filler<ItemViewModel>
    {
        private Dispatcher _Dispatcher = null;

        public TEDVideoFiller(ICollection<ItemViewModel> collection, Dispatcher dispatcher)
            : base(collection)
        {
            this._Dispatcher = dispatcher;
        }

        protected override void OnFill(Parser parser, ICollection<ItemViewModel> collection)
        {
            RSSParser rssParser = parser as RSSParser;
            if (rssParser == null)
            {
                return;
            }

            if (collection == null)
            {
                return;
            }

            //TODO: fill out data
            var items = rssParser.Items;
            if (items != null)
            {
                this._Dispatcher.BeginInvoke(() => 
                {
                    collection.Clear();

                    foreach (var item in items)
                    {
                        string title = item.Element(XName.Get("title")).Value;
                        string description = item.Element(XName.Get("description")).Value;

                        var itemVM = new ItemViewModel()
                        {
                            LineOne = title,
                            LineTwo = description
                        };

                        collection.Add(itemVM);
                    }                
                });

            }
        }
    }
}
