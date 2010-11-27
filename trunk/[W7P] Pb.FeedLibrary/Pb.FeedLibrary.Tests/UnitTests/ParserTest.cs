using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Linq;
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
            var parser = new RSSParser();
            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void Parser_ParseTest()
        {
            StreamResourceInfo info = Application.GetResourceStream(new Uri("UnitTests/feed.xml", UriKind.Relative));            
            using (var stream = info.Stream)
            {
                TextReader reader = new StreamReader(stream);
                var parser = new RSSParser(); 

                //넘어온 데이터로 XDocument를 만드는지 확인
                bool canLoad = parser.Load(reader);
                Assert.IsTrue(canLoad);

                //Header 정보를 정상적으로 가져오는지 확인 
                var title = parser.Element("title");
                Assert.AreEqual<string>("TEDTalks (video)", title.First().Value);
               
                //Items 정보를 정상적으로 가TEDTalks져오는지 확인 
                var item = parser.Items.First();
                Assert.AreEqual<string>("TEDTalks : John Hardy: My green school dream - John Hardy (2010)", item.Descendants(XName.Get("title")).First().Value);                                
            }            
        }
    }
}
