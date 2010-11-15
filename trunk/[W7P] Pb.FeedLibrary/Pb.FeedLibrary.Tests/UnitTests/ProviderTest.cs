using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary.Tests.Mocks;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class ProviderTest
    {
        [TestMethod]
        public void Provider_ConstructorTest()
        {
            var provider = new ProviderMock(new Uri("rss://"));            
            Assert.IsNotNull(provider);
            Assert.AreEqual<string>(new Uri("rss://").AbsolutePath, provider.Uri.AbsolutePath); 
        }        

        [TestMethod]
        public void Provider_RequestMethodTest()
        {
            var provider = new ProviderMock(new Uri("rss://"));
            provider.RequestDelegate = new Action<Provider>(p => { Assert.AreEqual<Provider>(provider, p); });
            Assert.IsTrue(provider.Request());

            var false_provider = new ProviderMock(null);
            Assert.IsFalse(false_provider.Request());
        }        
    }
}
