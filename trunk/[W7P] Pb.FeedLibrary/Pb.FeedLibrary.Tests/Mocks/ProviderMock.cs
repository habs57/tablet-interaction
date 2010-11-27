using System;

namespace Pb.FeedLibrary.Tests.Mocks
{
    public class ProviderMock : Provider
    {
        public ProviderMock(Uri uri)
            : base(uri)
        {
        }

        public override Parser Parser 
        {
            get
            {
                return null;
            }
        }

        public override Filler Filler
        {
            get
            {
                return null;
            }
        }
    }
}
