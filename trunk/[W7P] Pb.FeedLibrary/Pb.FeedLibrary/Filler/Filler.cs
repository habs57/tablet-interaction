using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Filler fills data
    /// </summary>
    public abstract class Filler
    {
        /// <summary>
        /// Fill
        /// </summary>
        /// <param name="parser">parser</param>
        public void Fill(Parser parser)
        {
            throw new NotImplementedException();
        }
    }
}
