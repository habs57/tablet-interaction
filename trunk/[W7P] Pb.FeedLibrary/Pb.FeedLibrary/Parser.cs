using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pb.FeedLibrary
{
    public class Parser
    {
        public void Parse(string p)
        {
            throw new NotImplementedException();
        }

        public Action<FeedHeader> OnParseHeader { get; set; }

        public Action<int, FeedItem> OnParseItem { get; set; }
    }
}
