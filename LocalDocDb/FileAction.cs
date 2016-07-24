using LanguageExt;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    public struct FileAction
    {
        public string Filename { get;  }
        public string Resource { get; }
        public JObject Content { get; }
        public DirectoryInfo Path { get; }
        public FileActionType Type { get; }
        public bool Overwrite { get; }
        public FileAction (DirectoryInfo path, string resource, string filename, FileActionType type, JObject content, bool overwrite)
        {
            Filename = filename;
            Resource = resource;
            Path = path;
            Type = type;
            Content = content;
            Overwrite = overwrite;
        }
    }
}
