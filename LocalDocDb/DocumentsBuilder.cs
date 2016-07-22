using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class DocumentsBuilder
    {
        string AccountId { get; set; }
        string DatabaseId { get; set; }
        string CollectionId { get; set; }
        DirectoryInfo AccountPath { get; set; }

        public DocumentsBuilder Account(string accountId)
        {
            this.AccountId = accountId;
            return this;
        }
        public DocumentsBuilder Database(string databaseId)
        {
            this.DatabaseId = databaseId;
            return this;
        }
        public DocumentsBuilder Collection(string collectionId)
        {
            this.CollectionId = collectionId;
            return this;
        }
        //public DocumentsBuilder Build()
        //{
        //    DocDbAccount account = new DocDbAccount(
        //                                AccountId, 
        //                                AccountPath ?? 
        //                                new DirectoryInfo(Path.GetPathRoot(Environment.SystemDirectory)));

        //    DocDbDatabase db = new DocDbDatabase(DatabaseId);
        //    DocDbCollection coll = new DocDbCollection(CollectionId);


        //    return new Docs { Account = account, Database = db, Collection = coll);
        //}
    }
}
