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
        }

        [TestMethod]
        public void Feeder_RequestMethodTest()
        {
            var feeder = new Feeder();
            bool result = feeder.Request(new Uri("http://feeds.feedburner.com/TEDTalks_video"));
            Assert.AreEqual<bool>(true, result);

            Assert.IsNotNull(feeder.RequestObject);

            Uri uri = feeder.Uri;
            Assert.AreEqual<string>("http://feeds.feedburner.com/TEDTalks_video", feeder.Uri.AbsoluteUri);

            bool resultIfNull = feeder.Request(null);
            Assert.IsFalse(resultIfNull);
        }
                
    }
}
