using LanguageExt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class JsonParser
    {
        public static Option<JObject> Parse(string jsonBody)
        {
            try
            {
                return JObject.Parse(jsonBody);
            }
            catch (JsonReaderException jex)
            {
                return Option<JObject>.None;
            }
        }
    }
}
