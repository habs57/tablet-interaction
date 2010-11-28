using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Filler fills data
    /// </summary>
    public abstract class Filler<T> : IFiller
    {
        protected Filler(ICollection<T> collection)
        {
            this.Collection = collection;
        }

        protected ICollection<T> Collection { get; private set; }

        /// <summary>
        /// Fill data
        /// </summary>
        /// <param name="parser">parser</param>
        public void Fill(Parser parser)
        {
            
        }        

        public abstract void OnFill(Parser parser, ICollection<T> collection);            
    }
}
