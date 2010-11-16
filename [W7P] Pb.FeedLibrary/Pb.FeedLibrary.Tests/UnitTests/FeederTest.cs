using System;
using System.Net;
using Microsoft.Silverlight.Testing.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class FeederTest
    {
        [TestInitialize]
        public void SetUp()
        {
        }

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
        public void Feeder_RespAndReadCallbackMethodTest()
        {
            //리퀘스트 요청             
            var feeder = new Feeder();
            
            feeder.Test_RespCallback = new Action<IAsyncResult>(p =>
            {                
                Feeder.RequestState state = feeder.Test_RequestState;

                Assert.IsNotNull(state.streamResponse);
                Assert.IsNotNull(state.response);
            });

            feeder.Test_ReadCallBack = new Action<IAsyncResult>(p =>
            {
                Assert.AreNotEqual<int>(feeder.Test_FeedRawStringContent.Length, 0); 
            });
            
            IAsyncResult result = feeder.Request(new Uri("http://feeds.feedburner.com/TEDTalks_video"));                                                                                
        }
                
    }
}
