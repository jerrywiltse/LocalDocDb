using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class DocDbFunction : DocDbBase
    {
        public DocDbFunction(string id) : base(GetBasePath(id))
        {
        }
    }
}
