using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class DocDbUser : DocDbBase
    {
        public DocDbUser(string id) : base(GetBasePath(id))
        {
        }
    }
}
