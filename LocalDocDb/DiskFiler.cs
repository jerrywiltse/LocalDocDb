using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class DiskFiler
    {
        //TODO: Replace with return Optional<Response>
        public static Response InvokeAction(FileAction action)
        {
            switch (action.Type)
            {
                case FileActionType.Get:
                    return TryGetFile(action);
                case FileActionType.Create:
                    return TryCreateFile(action, false);
                case FileActionType.Replace:
                    return TryCreateFile(action, true);
                case FileActionType.Delete:
                    return TryDeleteFile(action);
                default:
                    throw new InvalidOperationException();
            }            
        }
        public static Response InvokeAction(FilesAction action)
        {
            switch (action.Type)
            {
                case FileActionType.Get:
                    return TryListFiles(action);
                case FileActionType.Query:
                    return TryQueryFiles(action);
                default:
                    throw new InvalidOperationException();
            }
        }
        public static Response TryListFiles(FilesAction action)
        {
            DirectoryInfo resourceDir = GetFullPathDir(action);
            JArray objArr = new JArray(resourceDir.GetDirectories().Select(
                resourceItemDir => ReadJson(GetFullPathFile(resourceItemDir))
            ));
            return new Response { Code = 200, Message = "OK", Body = objArr.ToString() };
        }
        public static Response TryQueryFiles(FilesAction action)
        {
            return new Response { Code = 200, Message = "OK", Body = @"{QueryNotImplemeneted : []}" };
        }
        public static Response TryCreateFile(FileAction action, bool overwrite)
        {
            FileInfo resourceItemFile = GetFullPathFile(action);
            Response response;
            if (File.Exists(resourceItemFile.FullName))
            {
                if (overwrite)
                {
                    try
                    {
                        WriteJson(resourceItemFile, action.Content);
                        response = new Response { Code = 200, Message = "Ok", Body = action.Content.ToString() };
                    }
                    catch (IOException e)
                    {
                        response = new Response { Code = 500, Message = $"Write Error {e.Message}" };
                    }
                }else
                {
                    response = new Response { Code = 409, Message = "Conflict" };
                }
            }
            else
            {
                response = new Response { Code = 404, Message = "Not Found" };
            }
            return response;
        }
        public static Response TryGetFile(FileAction action)
        {
            FileInfo resourceItemFile = GetFullPathFile(action);
            return File.Exists(resourceItemFile.FullName) ?
                new Response { Code = 200, Message = "Ok", Body = ReadJson(resourceItemFile).ToString() } : 
                new Response { Code = 404, Message = "Not Found" };
        }

        public static Response TryDeleteFile(FileAction action)
        {
            FileInfo resourceItemFile = GetFullPathFile(action);
            return (File.Exists(resourceItemFile.FullName)) ?            
                new Response { Code = 200, Message = "Ok", Body = "" } :
                new Response { Code = 404, Message = "Not Found" };
        }


        private static FileInfo GetFullPathFile(FileAction action)
        {
            return new FileInfo(Path.Combine(action.Path.FullName, action.Resource, action.Filename, ".json"));
        }
        private static FileInfo GetFullPathFile(DirectoryInfo dirPath)
        {
            return new FileInfo(Path.Combine(dirPath.FullName, dirPath.Name, ".json"));
        }
        private static DirectoryInfo GetFullPathDir(FilesAction action)
        {
            return new DirectoryInfo(Path.Combine(action.Path.FullName, action.Resource));
        }
        private static JObject ReadJson(FileInfo fileInfo)
        {
            using (StreamReader file = File.OpenText(fileInfo.FullName))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                return (JObject)JToken.ReadFrom(reader);
            }
        }
        private static void WriteJson(FileInfo fileInfo, JObject content)
        {
            try
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
                using (StreamWriter file = File.CreateText(fileInfo.FullName))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    content.WriteTo(writer);
                }
            }catch (Exception e)
            {
                throw e;
            }
        }
        private static void DeleteJson(FileInfo fileInfo)
        {
            try
            {
                Directory.Delete(fileInfo.DirectoryName, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
