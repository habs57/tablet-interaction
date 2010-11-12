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
            var provider = new Provider();
            Assert.IsNotNull(provider);
        }


        
    }
}
