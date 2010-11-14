using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class ProviderTest
    {
        [TestMethod]
        public void Provider_ConstructorTest()
        {
            var provider = new Provider(new Uri("rss://"));            
            Assert.IsNotNull(provider);
            Assert.AreEqual<string>(new Uri("rss://").AbsolutePath, provider.Uri.AbsolutePath); 
        }        

        [TestMethod]
        public void Provider_RequestMethodTest()
        {
            var provider = new Provider(new Uri("rss://"));
            provider.RequestDelegate = new Action<Provider>(p => { Assert.AreEqual<Provider>(provider, p); });
            Assert.IsTrue(provider.Request());

            var false_provider = new Provider(null);
            Assert.IsFalse(false_provider.Request());
        }

        
    }
}
