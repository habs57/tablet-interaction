
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
            RSSParser rssParser = parser as RSSParser;
            if (rssParser == null)
            {
                return;
            }

            if (collection == null)
            {
                return;
            }

            //TODO : fill out blog data

        }
    }
}
