using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void Feeder_RegisterMethodTest()
        {
            var feeder = new Feeder();
            var provider = new Provider();

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
            var feeder = new Feeder();
            var provider = new Provider();

            feeder.Register(provider);
            bool canDeRegister = feeder.DeRegister(provider);
            Assert.IsTrue(canDeRegister);

            bool isContains = feeder.Contains(provider);
            Assert.IsFalse(isContains);
        }

        [TestMethod]
        public void Feeder_ContainsMethodTest()
        {
            var feeder = new Feeder();
            var provider = new Provider();

            feeder.Register(provider);
            bool isContains = feeder.Contains(provider);
            Assert.IsTrue(isContains);
        }

        [TestMethod]
        public void Feeder_DisposeMethodTest()
        {
            var feeder = new Feeder();
            var provider = new Provider();

            feeder.Register(provider);
            (feeder as IDisposable).Dispose();

            bool isContains = feeder.Contains(provider);
            Assert.IsFalse(isContains);
        }
        
    }
}
