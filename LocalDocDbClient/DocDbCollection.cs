using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDbClient
{
    class DocDbCollection : DocDbBase
    {
        public DocDbCollection(string id)
        {
        }
        //public IEnumerable<string> ListDocuments()
        //{
        //    return ListChildren("documents");
        //}
        //public IEnumerable<string> ListSprocs()
        //{
        //    return ListChildren("sprocs");
        //}
        //public IEnumerable<string> ListTriggers()
        //{
        //    return ListChildren("triggers");
        //}
        //public IEnumerable<string> ListFunctions()
        //{
        //    return ListChildren("functions");
        //}
        //public IEnumerable<DocDbDocument> GetDocuments()
        //{
        //    return GetChildren<DocDbDocument>("documents");
        //}
        //public IEnumerable<DocDbSproc> GetSprocs()
        //{
        //    return GetChildren<DocDbSproc>("sprocs");
        //}
        //public IEnumerable<DocDbTrigger> GetTriggers()
        //{
        //    return GetChildren<DocDbTrigger>("triggers");
        //}
        //public IEnumerable<DocDbFunction> GetFunctions()
        //{
        //    return GetChildren<DocDbFunction>("functions");
        //}
    }
}
