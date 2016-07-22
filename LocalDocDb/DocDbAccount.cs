using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class DocDbAccount : DocDbBase
    {
        public DocDbAccount(string id, DirectoryInfo path) : base(GetAccountPath(id, path))
        {
        }
        protected static DirectoryInfo GetAccountPath(string name, DirectoryInfo path)
        {
            return new DirectoryInfo(
                    Path.Combine(
                        path.FullName,
                        "LocalDocDb",
                        name
                    )
                );
        }
        //public IEnumerable<string> ListDatabases()
        //{
        //    return ListChildren("dbs");
        //}
        //public IEnumerable<DocDbDatabase> GetDatabases()
        //{
        //    return GetChildren<DocDbDatabase>("dbs");
        //}
        //public DocDbDatabase GetDatabase(string id)
        //{
        //    return GetChild<DocDbDatabase>(id, "dbs");
        //}
        //public DocDbDatabase CreateDatabase(string id)
        //{
        //    return CreateChild<DocDbDatabase>(id, "dbs");
        //}
    }
}
