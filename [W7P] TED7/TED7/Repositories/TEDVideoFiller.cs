
using System.Collections.Generic;
using System.Windows.Threading;
using System.Xml.Linq;
using Pb.FeedLibrary;
using System.Linq;

using System.Text.RegularExpressions;

namespace TED7
{
    public class TEDVideoFiller : Filler<ItemViewModel>
    {
        private Dispatcher _Dispatcher = null;
        private int? _VisibleCount = null;

        public TEDVideoFiller(ICollection<ItemViewModel> collection, Dispatcher dispatcher)
            : base(collection)
        {
            this._Dispatcher = dispatcher;
        }

        public TEDVideoFiller(ICollection<ItemViewModel> collection, Dispatcher dispatcher, int count)
            : this(collection, dispatcher)
        {
            this._VisibleCount = count;
        }

        private string GetTitle(XElement element)
        {
            if (element == null)
	        {
                return null;
	        }

            string title = element.Value;
            if (title == null)
	        {
                return null;	 
	        }

            string[] titles = title.Split(':');
            if (titles != null)
            {
                string trimmed = titles.LastOrDefault();
                if (trimmed != null)
                {
                    return trimmed.Trim();
                }
            }

            return null;
        }

        //private string GetDescription(XElement element)
        //{
        //    if (element == null)
        //    {
        //        return null;
        //    }

        //    string title = element.Value;
        //    if (title == null)
        //    {
        //        return null;
        //    }
        //}

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

                    int index = 0;
                    foreach (var item in items)
                    {
                        if (this._VisibleCount < index)
                        {
                            break;
                        }

                        string thumbnail = item.Element(XName.Get("thumbnail", "http://search.yahoo.com/mrss/")).FirstAttribute.Value;
                        string title = this.GetTitle(item.Element(XName.Get("subtitle", "http://www.itunes.com/dtds/podcast-1.0.dtd")));
                        string description = item.Element(XName.Get("author", "http://www.itunes.com/dtds/podcast-1.0.dtd")).Value;

                        var itemVM = new ItemViewModel()
                        {
                            Thumbnail = thumbnail,
                            LineOne = title,
                            LineTwo = string.Format("by {0}",description)
                        };

                        collection.Add(itemVM);

                        ++index;
                    }                
                });

            }
        }
    }
}
