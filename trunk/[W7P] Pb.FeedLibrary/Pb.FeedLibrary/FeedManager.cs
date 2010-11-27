using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Manage async feeds 
    /// </summary>
    public sealed class FeedManager : IDisposable
    {
        private Dictionary<Provider, Feeder> _Items = null;
        private Dictionary<Provider, Feeder> Items
        {
            get
            {
                if (_Items == null)
                {
                    _Items = new Dictionary<Provider, Feeder>();
                }
                return _Items;
            }
        }

        /// <summary>
        /// Register provider for feed 
        /// </summary>
        /// <param name="provider">provider</param>
        /// <returns>true : sucess, false : error</returns>
        public bool Register(Provider provider)
        {
            if (provider == null)
            {
                throw new ArgumentException("provider");
            }

            bool isExist = this.Items.ContainsKey(provider);
            if (isExist == true)
            {
                return false;
            }

            Feeder feeder = new Feeder(provider.Uri);

            feeder.OnRead = new Action<System.IO.TextReader>(r => 
            { 
                provider.Parser.Load(r);
                provider.Filler.Fill(provider.Parser);
            });
           
            this.Items.Add(provider, feeder);

            provider.RequestDelegate = new Action<Provider>(p => { feeder.Request(); });

            return true;
        }

        /// <summary>
        /// Deregister provider in regsitered feeds 
        /// </summary>
        /// <param name="provider">provider</param>
        /// <returns>true : sucess, false : error</returns>
        public bool DeRegister(Provider provider)
        {
            if (provider == null)
            {
                return false;
            }

            bool isExist = this.Items.ContainsKey(provider);
            if (isExist == false)
            {
                return false;
            }

            this.Items.Remove(provider);

            return true;
        }

        /// <summary>
        /// Dispose all providers and stop feeding
        /// </summary>
        void IDisposable.Dispose()
        {
            this.Items.Clear();
        }

        /// <summary>
        /// Checks provider is already registered 
        /// </summary>
        /// <param name="provider">provider</param>
        /// <returns>true : registered, false : not registered</returns>
        public bool Contains(Provider provider)
        {
            return this.Items.ContainsKey(provider);
        }
    }
}
