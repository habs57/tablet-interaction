using System;

namespace Pb.FeedLibrary.Tests.Mocks
{
    public class ProviderMock : Provider
    {
        public ProviderMock(Uri uri, Filler<int> filler)
            : base(uri, filler)
        {
        }
        
        protected override Parser OnCreateParser()
        {
            return new ParserMock();
        }
    }
}
