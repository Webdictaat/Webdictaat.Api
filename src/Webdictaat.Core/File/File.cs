using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Core
{
    public interface IFile
    {
        string TryReadFile(string path);
        bool TryCreateFile(string path);
        bool TryDeleteFile(string path);
        bool TryEditFile(string path, string content);
        bool TryCopyFile(string path, string templatePath);
    }

    public class File : IFile
    {
        public bool TryCopyFile(string path, string templatePath)
        {
            if (System.IO.File.Exists(path))
            {
                return false;
            }

            System.IO.File.Copy(templatePath, path);
            return true;
        }

        public bool TryCreateFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                return false;
            }

            //We moeten de file meteen vrij geven!
            System.IO.File.Create(path).Dispose();
            return true;
        }

        public bool TryDeleteFile(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return false;
            }

            System.IO.File.Delete(path);
            return true;

        }

        public bool TryEditFile(string path, string content)
        {
            if (!System.IO.File.Exists(path))
            {
                return false;
            }

            System.IO.File.WriteAllText(path, content);
            return true;
        }

        public string TryReadFile(string path)
        {
            string source = null;
            if (!System.IO.File.Exists(path))
            {
                return source;
            }

            source = System.IO.File.ReadAllText(path);
            return source;
        }

    }
}
