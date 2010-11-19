using System;
using System.Net;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class FeederTest : SilverlightTest
    {
        [TestInitialize]
        public void SetUp()
        {
        }

        [TestMethod]
        public void Feeder_ContsructorTest()
        {
            var feeder = new Feeder(new Uri("http://feeds.feedburner.com/TEDTalks_video"));
            Assert.IsNotNull(feeder);

            Uri uri = feeder.Uri;
            Assert.AreEqual<string>("http://feeds.feedburner.com/TEDTalks_video", feeder.Uri.AbsoluteUri);
        }

        [Asynchronous]
        [TestMethod]        
        public void Feeder_RequestMethodTest()
        {
            var feeder = new Feeder(new Uri("http://feeds.feedburner.com/TEDTalks_video"));
            IAsyncResult result = feeder.Request();
            Assert.AreNotEqual<IAsyncResult>(null, result);
            
            feeder.OnRead = new Action<string>(str => 
            { 
                Assert.IsTrue(str.Length > 0);
                this.EnqueueTestComplete();
            });          
        }
       
    }
}
