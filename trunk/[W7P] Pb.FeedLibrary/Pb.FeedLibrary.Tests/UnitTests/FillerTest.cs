using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Linq;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary;

using Pb.FeedLibrary.Tests.Mocks;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class FillerTest :  SilverlightTest
    {
        [TestMethod]
        public void Filler_ConstructorTest()
        {
            Filler filler = new FillerMock();
            Assert.IsNotNull(filler);
        }

        [TestMethod]
        public void Filler_Fill()
        {
            Filler filler = new FillerMock();
            filler.Fill(new ParserMock());
        }
    }
}
