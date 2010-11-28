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

using System.Collections.Generic;

namespace Pb.FeedLibrary.Tests.Mocks
{
    public class FillerMock : Filler<int>
    {
        public FillerMock(ICollection<int> t1)
            : base(t1)
        {
            // TODO: Complete member initialization            
        }

        #if UNIT_TESTS
        public
        #else
        protected 
        #endif
        override void OnFill(Parser parser, ICollection<int> collection)
        {
            
        }
    }
}
