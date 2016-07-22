using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDbClient
{
    public struct FilesAction
    {
        public string Resource { get; }
        public DirectoryInfo Path { get; }
        public FileActionType Type { get; }
        public FilesAction(DirectoryInfo path, string resource,  FileActionType type)
        {
            Resource = resource;
            Path = path;
            Type = type;
        }
    }
}
