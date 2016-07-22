using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class DocDbAttachment
    {
        public string id { get; }
        public string contentType { get; } 
        public DocDbCollection collection { get; }
        public DocDbDatabase database { get; } 

        public DocDbAttachment(string id, string contentType, DocDbCollection collection, DocDbDatabase database )
        {
            this.id = String.IsNullOrWhiteSpace(id) ? String.Empty : id;
            this.contentType = id ?? "JSON";
            this.collection = collection;
            this.database = database;
        }
        
    }
}
