using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Core
{
    public interface IJson{

    dynamic ReadFile(string path);


    bool EditFile(string path, dynamic source);
}

    public class Json : IJson
    {
        public bool EditFile(string path, dynamic source)
        {
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(source, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(path, output);
            return true;
        }

        public dynamic ReadFile(string path)
        {
            string json = System.IO.File.ReadAllText(path);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json);      
        }
    }
}
