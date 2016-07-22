using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class DocDbDatabase : DocDbBase
    {
        public DocDbDatabase(string id) : base(GetBasePath(id))
        {
        }
    }
}
