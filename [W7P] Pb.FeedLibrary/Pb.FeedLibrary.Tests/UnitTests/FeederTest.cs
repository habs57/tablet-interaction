using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary;

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
            IAsyncResult result = feeder.Request(new Uri("http://feeds.feedburner.com/TEDTalks_video"));
            Assert.AreNotEqual<IAsyncResult>(null, result);

            Uri uri = feeder.Uri;
            Assert.AreEqual<string>("http://feeds.feedburner.com/TEDTalks_video", feeder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void Feeder_RespCallbackMethodTest()
        {
            //리퀘스트 요청             
            var feeder = new Feeder();
            IAsyncResult result = feeder.Request(new Uri("http://feeds.feedburner.com/TEDTalks_video"));
            
            System.Threading.ManualResetEvent allDone = new System.Threading.ManualResetEvent(false);
            allDone.WaitOne();
                        
            Feeder.RequestState state = feeder.TestRequestState;
            Assert.IsNotNull(state.streamResponse);
            Assert.IsNotNull(state.response);
                        
        }
                
    }
}
