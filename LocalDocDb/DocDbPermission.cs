using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class DocDbPermission : DocDbBase
    {
        public DocDbPermission(string id) : base(GetBasePath(id))
        {
        }
    }
}
