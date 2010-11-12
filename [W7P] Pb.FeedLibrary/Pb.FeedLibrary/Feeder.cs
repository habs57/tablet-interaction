using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Manage async feeds 
    /// </summary>
    public sealed class Feeder : IDisposable
    {
        private Dictionary<Provider, Worker> _Providers = null;
        private Dictionary<Provider, Worker> Providers
        {
            get
            {
                if (_Providers == null)
                {
                    _Providers = new Dictionary<Provider, Worker>();                    
                }
                return _Providers;
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

            bool isExist = this.Providers.ContainsKey(provider);
            if (isExist == true)
            {
                return false;
            }

            this.Providers.Add(provider, new Worker());
            
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

            bool isExist = this.Providers.ContainsKey(provider);
            if (isExist == false)
            {
                return false;
            }

            this.Providers.Remove(provider);

            return true;
        }
        
        /// <summary>
        /// Dispose all providers and stop feeding
        /// </summary>
        void IDisposable.Dispose()
        {
            foreach (var item in this.Providers)
            {
                (item.Value as IDisposable).Dispose();
            }

            this.Providers.Clear();
        }

        /// <summary>
        /// Checks provider is already registered 
        /// </summary>
        /// <param name="provider">provider</param>
        /// <returns>true : registered, false : not registered</returns>
        public bool Contains(Provider provider)
        {
            return this.Providers.ContainsKey(provider);
        }
    }
}
