using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Linq;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class FillerTest :  SilverlightTest
    {
        [TestMethod]
        public void Filler_ConstructorTest()
        {
            var filler = new Filler();
            Assert.IsNotNull(filler);
        }

        [TestMethod]
        public void Filler_Fill()
        {
            var filler = new Filler();
             
        }
    }
}
