using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pb.FeedLibrary
{
    public class FeedHeader
    {
        private IDictionary<string, Tag> _Entities = null;
        internal IDictionary<string, Tag> Entities
        {
            get
            {
                if (_Entities == null)
                {
                    _Entities = new Dictionary<string, Tag>();
                }
                return _Entities;
            }
        }

        public bool SetEntity(string p)
        {
            bool canAdd = this.Entities.ContainsKey(p);
            if (canAdd == false)
            {
                Tag entity = new Tag(p);
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
