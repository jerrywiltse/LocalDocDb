using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class DocDbSproc : DocDbBase
    {
        public DocDbSproc(string id) : base(GetBasePath(id))
        {
        }
    }
}
