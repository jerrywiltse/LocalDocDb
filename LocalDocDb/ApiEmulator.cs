using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace LocalDocDbClient
{
    public class ApiEmulator
    {
        public const string EmptyString = "";
        public DirectoryInfo AccountFullPath;
        public ApiEmulator(string accountName)
        {
            AccountFullPath = GetDefaultPath(accountName);
        }
        public ApiEmulator (string accountName, string docDbRootPath )
        {
            AccountFullPath = String.IsNullOrEmpty(docDbRootPath) ?
                GetDefaultPath(accountName) :
                GetCustomPath(accountName, docDbRootPath);
        }
        public DirectoryInfo GetCustomPath(string accountName, string docDbRootPath)
        {
            return new DirectoryInfo(
                    Path.Combine(docDbRootPath, "accounts", accountName));
        }
        public DirectoryInfo GetDefaultPath(string accountName)
        {
            return new DirectoryInfo(
                    Path.Combine(
                        Path.GetPathRoot(Environment.SystemDirectory), "DocumentDBRoot", "accounts", accountName));
        }
        public void CreateAccountDir()
        {
            Directory.CreateDirectory(Path.Combine(AccountFullPath.FullName,"dbs"));
        }
        public void ReycleAccountDir()
        {
            FileSystem.DeleteDirectory(AccountFullPath.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin); 
        }
        // Multiple Items Operations
        public Response Submit(HttpOperation operation, string resourcePath)
        {
            string resourceType = resourcePath.Split('/').Last();

            switch (operation)
            {
                case HttpOperation.GET:
                    return new DocDB(AccountFullPath).GetChildren(resourcePath, resourceType);
                case HttpOperation.POST:
                    switch (resourceType)
                    {
                        case "offers":
                        case "docs":
                            return new DocDB(AccountFullPath).QueryChildren(resourcePath, resourceType);
                        default:
                            return new Response { Code = 489, Message = "Invalid Resource" };
                    }
                default:
                    return new Response { Code = 499, Message = "Invalid Operation" };
            }
        }
        // Single Item Operations
        public Response Submit(HttpOperation operation, string resourcePath, string targetId, string content = EmptyString)
        {
            string resourceType = resourcePath.Split('/').Last();

            switch (operation)
            {
                case HttpOperation.GET:
                    switch (resourceType)
                    {
                        case "dbs":
                        case "colls":
                        case "docs":
                        case "users":
                        case "permissions":
                        case "attachments":
                        case "offers":
                        case "sprocs":
                        case "udfs":
                        case "triggers":
                            return new DocDB(AccountFullPath).GetChild(resourcePath, resourceType, targetId);
                        default:
                            return new Response { Code = 499, Message = "Invalid Operation" };
                    }
                case HttpOperation.POST:
                    switch (resourceType)
                    {
                        case "dbs":
                        case "colls":
                        case "docs":
                        case "users":
                        case "permissions":
                        case "attachments":
                        case "offers":
                        case "sprocs":
                        case "udfs":
                        case "triggers":
                            return new DocDB(AccountFullPath).CreateChild(resourcePath, resourceType, targetId, content, false);
                        default:
                            return new Response { Code = 499, Message = "Invalid Operation" };
                    }
                case HttpOperation.PUT:
                    switch (resourceType)
                    {
                        case "colls":
                        case "docs":
                        case "users":
                        case "permissions":
                        case "attachments":
                        case "offers":
                        case "sprocs":
                        case "udfs":
                        case "triggers":
                            return new DocDB(AccountFullPath).CreateChild(resourcePath, resourceType, targetId, content, true);
                        default:
                            return new Response { Code = 499, Message = "Invalid Operation" };
                    }
                case HttpOperation.DELETE:
                    switch (resourceType)
                    {
                        case "dbs":
                        case "colls":
                        case "docs":
                        case "users":
                        case "permissions":
                        case "attachments":
                        case "offers":
                        case "sprocs":
                        case "udfs":
                        case "triggers":
                            return new DocDB(AccountFullPath).DeleteChild(resourcePath, resourceType, targetId);
                        default:
                            return new Response { Code = 499, Message = "Invalid Operation" };
                    }
                default:
                    return new Response { Code = 499, Message = "Invalid Operation" };
            }
        }
    }
}
