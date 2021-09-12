using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLifeCycle.Serivces
{
    public class GuidService 
    {
        private Guid _guid;
        public GuidService()
        {
            _guid = System.Guid.NewGuid();
        }

        public Guid GetGuid()
        {
            return _guid;
        }
    }
}
