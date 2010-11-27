using System;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary.Tests.Mocks;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class RSSProviderTest : SilverlightTest
    {
        [TestMethod]
        public void RSSProvider_ConstructorTest()
        {
            var provider = new ProviderMock(new Uri("rss://"));            
            Assert.IsNotNull(provider);
            Assert.AreEqual<string>(new Uri("rss://").AbsolutePath, provider.Uri.AbsolutePath);

            Parser parser = provider.Parser;
            Assert.IsNotNull(parser);

            Filler filler = provider.Filler;
            Assert.IsNotNull(filler);
        }        

        [TestMethod]
        public void RSSProvider_RequestMethodTest()
        {
            var provider = new ProviderMock(new Uri("rss://"));
            provider.RequestDelegate = new Action<Provider>(p => { Assert.AreEqual<Provider>(provider, p); });
            Assert.IsTrue(provider.Request());

            var false_provider = new ProviderMock(null);
            Assert.IsFalse(false_provider.Request());
        }        
    }
}
