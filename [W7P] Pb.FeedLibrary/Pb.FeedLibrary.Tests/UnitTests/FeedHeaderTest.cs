using System;
using System.Net;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary;
using System.Collections.Generic;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class FeedHeaderTest : SilverlightTest
    {
        [TestMethod]
        public void FeedHeader_ConstructorTest()
        {
            var header = new FeedHeader();
            Assert.IsNotNull(header);
        }

        [TestMethod]
        public void FeedHeader_SetEntityMethodTest()
        {
            var header = new FeedHeader();                        
            Assert.IsTrue(header.SetEntity("title") == true);    
            Assert.IsTrue(header.Contains("title") == true);
        }        
    }
}
