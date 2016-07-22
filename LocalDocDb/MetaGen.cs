using LanguageExt;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDbClient
{
    class MetaGen
    {
        public static Option<JObject> AddMarkup(string resourcePath, string childName, string resourceType, JObject parsedJsonObj)
        {
            AddGenericAttrs(parsedJsonObj, resourcePath, childName);
            switch(resourceType)
            {
                case "dbs":
                    AddDatabaseAttrs(parsedJsonObj);
                    break;
                case "collections":
                    AddDatabaseAttrs(parsedJsonObj);
                    break;
                case "documents":
                    AddDocumentAttrs(parsedJsonObj);
                    break;
                case "users":
                case "permissions":
                case "attachments":
                case "offers":
                case "sprocs":
                case "udfs":
                case "triggers":
                    break;
            }
            return parsedJsonObj;
        }
        //TODO: Implement GUID uniqueness validator
        private static JObject AddGenericAttrs (JObject obj, string resourcePath, string childName)
        { 
            obj.Add("id", childName);
            obj.Add("_rid", "rid mocking not yet implemented"); //TODO: Implement _rid mock
            obj.Add("_ts", DateTime.Now);
            obj.Add("_self", $"{resourcePath}/{childName}");
            obj.Add("_etag", "etag mocking not yet implemented"); //TODO: Consider implementing _etag mock
            return obj;
        }
        public static JObject AddDatabaseAttrs(JObject obj)
        {
            obj.Add("_collections", "");
            return obj;
        }
        public static JObject AddCollectionAttrs(JObject obj)
        {
            obj.Add("_docs", "");
            obj.Add("_sprocs", "");
            obj.Add("_triggers", "");
            obj.Add("_udfs", "");
            obj.Add("_conflicts", "");
            return obj;
        }
        public static JObject AddDocumentAttrs(JObject obj)
        {
            obj.Add("_docs", "");
            obj.Add("_sprocs", "");
            obj.Add("_triggers", "");
            obj.Add("_udfs", "");
            obj.Add("_conflicts", "");
            return obj;
        }
        public static JObject AddAttachmentAttrs(JObject obj)
        {
            obj.Add("_attachments", "");
            return obj;
        }
    }
}
