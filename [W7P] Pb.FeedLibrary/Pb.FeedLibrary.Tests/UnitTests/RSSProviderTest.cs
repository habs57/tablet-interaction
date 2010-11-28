using System;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary.Tests.Mocks;
using Pb.FeedLibrary;

using System.Collections.ObjectModel;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class RSSProviderTest : SilverlightTest
    {
        [TestMethod]
        public void RSSProvider_ConstructorTest()
        {
            Provider provider = new RSSProvider(new Uri("rss://"), new FillerMock(new ObservableCollection<int>()));            
            Assert.IsNotNull(provider);
            Assert.AreEqual<string>(new Uri("rss://").AbsolutePath, provider.Uri.AbsolutePath);

            Parser parser = provider.Parser;
            Assert.IsNotNull(parser);

            IFiller filler = provider.Filler;
            Assert.IsNotNull(filler);
        }        

        [TestMethod]
        public void RSSProvider_RequestMethodTest()
        {
            Provider provider = new RSSProvider(new Uri("rss://"), new FillerMock(new ObservableCollection<int>()));
            provider.RequestDelegate = new Action<Provider>(p => { Assert.AreEqual<Provider>(provider, p); });
            Assert.IsTrue(provider.Request());

            var false_provider = new ProviderMock(null, new FillerMock(new ObservableCollection<int>()));
            Assert.IsFalse(false_provider.Request());
        }        
    }
}
