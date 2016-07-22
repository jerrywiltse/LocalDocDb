using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class DocDbTrigger : DocDbBase
    {
        public DocDbTrigger(string id) : base(GetBasePath(id))
        {
        }
    }
}
