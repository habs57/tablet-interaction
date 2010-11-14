using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class FeederTest
    {
        [TestMethod]
        public void Feeder_ContsructorTest()
        {
            var feeder = new Feeder();
            Assert.IsNotNull(feeder);

            HttpWebRequest webReq = feeder.RequestObject;
            Assert.AreEqual<object>(webReq, null);
        }

        [TestMethod]
        public void Feeder_RequestMethodTest()
        {
            var feeder = new Feeder();
            bool result = feeder.Request(new Uri("rss://"));
            Assert.AreEqual<bool>(true, result);
        }
                
    }
}
