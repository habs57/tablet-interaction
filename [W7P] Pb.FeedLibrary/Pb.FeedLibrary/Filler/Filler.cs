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
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">Collection to fill</param>
        protected Filler(ICollection<T> collection)
        {
            this.Collection = collection;
        }

        /// <summary>
        /// Collection that control
        /// </summary>
        protected ICollection<T> Collection { get; private set; }

        /// <summary>
        /// Fill data
        /// </summary>
        /// <param name="parser">parser</param>
        public void Fill(Parser parser)
        {
            if (this.Collection != null)
            {
                this.OnFill(parser, this.Collection);
            }
        }        
                
        /// <summary>
        /// Called when fills 
        /// </summary>
        /// <param name="parser">Parser to parse</param>
        /// <param name="collection">Collection to fillups</param>
        #if UNIT_TESTS
        public
        #else
        protected 
        #endif
        abstract void OnFill(Parser parser, ICollection<T> collection);            
    }
}
