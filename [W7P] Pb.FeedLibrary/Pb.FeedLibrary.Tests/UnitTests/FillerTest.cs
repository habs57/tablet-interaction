using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Linq;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Pb.FeedLibrary.Tests.Mocks;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class FillerTest :  SilverlightTest
    {
        [TestMethod]
        public void Filler_ConstructorTest()
        {
            ICollection<int> t1 = new ObservableCollection<int>();

            Filler<int> filler = new FillerMock(t1);
            Assert.IsNotNull(filler);
        }

        [TestMethod]
        public void Filler_Fill()
        {
            ICollection<int> t1 = new ObservableCollection<int>() { 1, 2, 3, 4, 5 };
            Filler<int> filler = new FillerMock(t1);

            Parser parser = new ParserMock();
            filler.Fill(parser);
            
            //템플릿 
            ICollection<int> t2 = new ObservableCollection<int>() { 1, 2, 3, 4, 5 };
            filler.OnFill(parser, t2);                       
            
            //Check t1 equals to t2 
            Assert.IsTrue(t1.SequenceEqual(t2) == true);
        }
    }
}
