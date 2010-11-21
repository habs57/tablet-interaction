using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pb.FeedLibrary
{
    public class FeedHeader
    {
        private IDictionary<string, Entity> _Entities = null;
        internal IDictionary<string, Entity> Entities
        {
            get
            {
                if (_Entities == null)
                {
                    _Entities = new Dictionary<string, Entity>();
                }
                return _Entities;
            }
        }

        public bool SetEntity(string p)
        {
            bool canAdd = this.Entities.ContainsKey(p);
            if (canAdd == false)
            {
                Entity entity = new Entity(p);
                this.Entities.Add(p, entity);
                return true;
            }
            return false;
        }

        public bool Contains(string p)
        {
            return this.Entities.ContainsKey(p);
        }
    }
}
