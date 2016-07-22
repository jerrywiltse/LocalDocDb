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
    public class StoreFile
    {
        public string DbName
        { get; set; }
        public string DbTable
        { get; set; }

        public string DocName
        { get; set; }

        public dynamic AddDoc(dynamic document)
        {
            dynamic result = new JObject(new JProperty(DocName, new JObject()));

            string path = String.IsNullOrEmpty(DbTable) ?
                $"{DbName}\\{DocName}.json" :
                    $"{DbName}\\{DbTable}\\{DocName}.json";
            try
            {
                Directory.CreateDirectory(DbName + "\\" + DbTable);
                File.WriteAllText(path, document.ToString());
                result[DocName]["status"] = "Ok";
                result[DocName]["path"] = path;
            }
            catch (Exception e)
            {
            }
            return result;
        }
        public IEnumerable<dynamic> AddDocs(IEnumerable<dynamic> documents, string namefield)
        {
            //List<dynamic> resultlist = new List<dynamic>();
            JArray responses = new JArray();
            foreach (var item in documents)
            {
                DocName = item[namefield];
                dynamic result = AddDoc(item);
                responses.Add(result);
            }

            //string status = "Success";
            // IEnumerable<dynamic> output = OperationResponses();
            return responses;
        }

        public dynamic DeleteDoc(string documentId)
        {
            throw new NotImplementedException();
        }

        public dynamic GetDoc(string documentId)
        {
            throw new NotImplementedException();
        }

        public dynamic UpdateDoc(string documentId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<dynamic> GetDocs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<dynamic> GetDocs(IEnumerable<dynamic> documents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<dynamic> UpdateDocs(IEnumerable<dynamic> documents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<dynamic> DeleteDocs(IEnumerable<dynamic> documents)
        {
            throw new NotImplementedException();
        }
    }
}