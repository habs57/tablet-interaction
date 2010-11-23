using System;
using System.IO;
using System.Windows;
using System.Windows.Resources;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pb.FeedLibrary.Tests.UnitTests
{
    [TestClass]
    public class ParserTest : SilverlightTest
    {
        [TestMethod]
        public void Parser_ConstructorTest()
        {
            var parser = new Parser(new FeedHeader(), new FeedItem());
            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void Parser_ParseTest()
        {
            StreamResourceInfo info = Application.GetResourceStream(new Uri("UnitTests/feed.xml", UriKind.Relative));            
            using (var stream = info.Stream)
            {
                StreamReader reader = new StreamReader(stream);

                FeedHeader header = new FeedHeader();
                header.SetEntity("title");
                FeedItem item = new FeedItem();                

                var parser = new Parser(header, item);                                
                parser.Parse(reader);

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
