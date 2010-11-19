using System;
using System.Net;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pb.FeedLibrary;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class ParserTest : SilverlightTest
    {
        [TestMethod]
        public void Parser_ConstructorTest()
        {
            var parser = new Parser();
            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void Parser_ParseTest()
        {            
            using(Stream stream = File.OpenRead("feed.xml"))
            {
                StreamReader reader = new StreamReader(stream);
                var parser = new Parser();
                parser.Parse(reader.ReadToEnd());
                                
                parser.OnParseHeader = new Action<FeedHeader>(p => 
                {
                    //Assert.AreEqual<string>("TEDTalks(video)", p.GetEntity("title").Value);
                });
                parser.OnParseItem = new Action<int, FeedItem>((i, p) => 
                { 

                });
            }
        }
    }
}
