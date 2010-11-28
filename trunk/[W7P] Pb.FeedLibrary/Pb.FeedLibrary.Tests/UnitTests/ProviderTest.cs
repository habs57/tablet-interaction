using System;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary.Tests.Mocks;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class ProviderTest : SilverlightTest
    {
        [TestMethod]
        public void Provider_ConstructorTest()
        {
            var provider = new ProviderMock(new Uri("rss://"), new FillerMock(null));            
            Assert.IsNotNull(provider);
            Assert.AreEqual<string>(new Uri("rss://").AbsolutePath, provider.Uri.AbsolutePath); 
        }        

        [TestMethod]
        public void Provider_RequestMethodTest()
        {
            var filler = new FillerMock(null);
            var provider = new ProviderMock(new Uri("rss://"), filler);
            provider.RequestDelegate = new Action<Provider>(p => { Assert.AreEqual<Provider>(provider, p); });
            Assert.IsTrue(provider.Request());

            var false_provider = new ProviderMock(null, new FillerMock(null));
            Assert.IsFalse(false_provider.Request());
        }        
    }
}
