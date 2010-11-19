using System;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary;
using Pb.FeedLibrary.Tests.Mocks;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class FeedManagerTest : SilverlightTest
    {
        [TestMethod]
        public void Feeder_ContsructorTest()
        {
            var feeder = new FeedManager();
            Assert.IsNotNull(feeder);
        }

        [TestMethod]
        public void Feeder_RegisterMethodTest()
        {
            var feeder = new FeedManager();
            var provider = new ProviderMock(new Uri("http://feeds.feedburner.com/TEDTalks_video"));

            bool canRegister = feeder.Register(provider);
            Assert.IsTrue(canRegister);

            bool canRegisterAgain = feeder.Register(provider);
            Assert.IsFalse(canRegisterAgain);

            bool isContains = feeder.Contains(provider);
            Assert.IsTrue(isContains);
        }

        [TestMethod]
        public void Feeder_DeRegisterMethodTest()
        {
            var feeder = new FeedManager();
            var provider = new ProviderMock(new Uri("http://feeds.feedburner.com/TEDTalks_video"));

            feeder.Register(provider);
            bool canDeRegister = feeder.DeRegister(provider);
            Assert.IsTrue(canDeRegister);

            bool isContains = feeder.Contains(provider);
            Assert.IsFalse(isContains);
        }

        [TestMethod]
        public void Feeder_ContainsMethodTest()
        {
            var feeder = new FeedManager();
            var provider = new ProviderMock(new Uri("http://feeds.feedburner.com/TEDTalks_video"));

            feeder.Register(provider);
            bool isContains = feeder.Contains(provider);
            Assert.IsTrue(isContains);
        }

        [TestMethod]
        public void Feeder_DisposeMethodTest()
        {
            var feeder = new FeedManager();
            var provider = new ProviderMock(new Uri("http://feeds.feedburner.com/TEDTalks_video"));

            feeder.Register(provider);
            (feeder as IDisposable).Dispose();

            bool isContains = feeder.Contains(provider);
            Assert.IsFalse(isContains);
        }
        
    }
}
