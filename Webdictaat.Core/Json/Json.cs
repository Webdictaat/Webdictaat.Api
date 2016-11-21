using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Core
{
    public interface IJson{

    bool TryReadFile(string path, out string content);


    bool TryEditFile(string path, string source);
}

    public class Json : IJson
    {
        public bool TryEditFile(string path, string source)
        {
            throw new NotImplementedException();
        }

        public bool TryReadFile(string path, out string content)
        {
            throw new NotImplementedException();

        }
    }
}
