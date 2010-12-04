
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
            FeedParser rssParser = parser as FeedParser;
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
                        string thumbnail = item.Element(XName.Get("thumbnail", "http://search.yahoo.com/mrss/")).FirstAttribute.Value;
                        string title = item.Element(XName.Get("subtitle", "http://www.itunes.com/dtds/podcast-1.0.dtd")).Value;
                        string description = item.Element(XName.Get("author", "http://www.itunes.com/dtds/podcast-1.0.dtd")).Value;

                        var itemVM = new ItemViewModel()
                        {
                            Thumbnail = thumbnail,
                            LineOne = title,
                            LineTwo = string.Format("by {0}", description)
                        };

                        collection.Add(itemVM);

                        ++index;
                    }
                });


            }
        }
    }
}
