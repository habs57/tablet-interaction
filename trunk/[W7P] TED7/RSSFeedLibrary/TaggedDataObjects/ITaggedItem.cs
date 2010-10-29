using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSSFeedLibrary
{
    public interface ITaggedItem<T>
    {
        string Tag { get; set; }
        T Value { get; set; }
    }
}
