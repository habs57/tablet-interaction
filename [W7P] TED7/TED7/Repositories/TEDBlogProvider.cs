using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Pb.FeedLibrary;

namespace TED7
{
    public class TEDBlogProvider : FeedProvider
    {
        public TEDBlogProvider(IFiller filler)
            : base(new Uri("http://feeds.feedburner.com/tedblog"), filler) 
        {

        }
    }
}
