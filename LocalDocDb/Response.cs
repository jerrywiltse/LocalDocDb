using LanguageExt;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDbClient
{
    public class Response
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Option<string> Body { get; set; }
    }
}
