using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class DocDbDocument : DocDbBase
    {
        public DocDbDocument(string id) : base(GetBasePath(id))
        {
        }
        //public IEnumerable<string> ListAttachments()
        //{
        //    return ListChildren("attachments");
        //}
        //public IEnumerable<DocDbAttachment> GetAttachments()
        //{
        //    return GetChildren<DocDbAttachment>("attachments");
        //}
    }
}
