using LanguageExt;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDbClient
{
    class DocDB
    {
        protected string id { get; }
        protected string contentType { get; } = "JSON";
        protected DirectoryInfo DocDbRoot { get; }
        public DocDB(DirectoryInfo root)
        {
            this.DocDbRoot = root;
        }
        protected static DirectoryInfo GetBasePath(String id)
        {
            return new DirectoryInfo(id);
        }
        protected string ValidateOrGenerateId(string input)
        {
            return String.IsNullOrWhiteSpace(input) ? Path.GetRandomFileName() : input;
        }
        public Response GetChildren(string resourcePath, string resourceType)
        {
            return DiskFiler.InvokeAction(new FilesAction (DocDbRoot, resourcePath, FileActionType.Get ));
        }
        public Response QueryChildren(string resourcePath, string resourceType)
        {
            //TODO: Implement actual Query Logic
            return DiskFiler.InvokeAction(new FilesAction(DocDbRoot, resourcePath, FileActionType.Get));
        }
        public Response GetChild(string resourcePath, string resourceType, string childName)
        {
            //TODO: Figure out way to do so that we don't need to make an empty JObject to satisfy struct ctor
            return DiskFiler.InvokeAction(new FileAction(DocDbRoot,  resourcePath, childName, FileActionType.Get, new JObject(), false));
        }
        public Response CreateChild(string resourcePath, string resourceType, string childName, string content, bool overwrite = false)
        {
            return JsonParser.Parse(content)
                .Some(parsedJsonObj =>
                    MetaGen.AddMarkup(resourcePath, resourceType, childName, parsedJsonObj)
                        .Some(markedJsonObject =>
                            DiskFiler.InvokeAction(
                                new FileAction(DocDbRoot, resourcePath, childName, FileActionType.Create, markedJsonObject, overwrite)))
                        .None(() => new Response { Code = 599, Message = "Internal Error" }))
                .None(() => new Response { Code = 400, Message = "Bad Request" });                
        }
        public Response DeleteChild(string resourcePath, string resourceType, string childName)
        {
            return DiskFiler.InvokeAction(
                new FileAction(DocDbRoot, resourcePath, childName, FileActionType.Delete, new JObject(), false));
        }

        //TODO: Impelemnt Optional<T> in all cases
        //TODO: Implement All Exception Conditions
    }
}
