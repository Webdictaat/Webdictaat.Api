using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Core
{
    public interface IDirectory
    {
        IEnumerable<DirectorySummary> GetDirectoriesSummary(string path);
        DirectoryDetails GetDirectoryDetails(string path);
        IEnumerable<FileSummary> GetFilesSummary(string path);
        void CopyDirectory(string name, string template);
        bool Exists(string pathNew);
    }

    public class Directory : IDirectory
    {
        public void CopyDirectory(string pathNew, string pathTemplate)
        {
            this.DirectoryCopy(pathTemplate, pathNew, true);
        }

        public IEnumerable<DirectorySummary> GetDirectoriesSummary(string directoryRoot)
        {
            string[] directories = System.IO.Directory.GetDirectories(directoryRoot);

            return directories.Select(d => new DirectorySummary()
            {
                Name = d.Split('\\').Last(),
                LastChange = System.IO.Directory.GetLastWriteTime(d),
            });
        }

        public DirectoryDetails GetDirectoryDetails(string path)
        {
            return new DirectoryDetails()
            {
                Name = path.Split('\\').Last(),
                RootEntry = GetDirectoryEntry(path)
            };
        }

        public IEnumerable<FileSummary> GetFilesSummary(string path)
        {
            return System.IO.Directory.GetFiles(path).Select(f => GetFileEntry(f));
        }

        /// <summary>
        /// Returns a object of DirectoryEntry containing details of a directory and a list of sub directories and files
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private DirectoryEntry GetDirectoryEntry(string path)
        {
            return new DirectoryEntry()
            {
                Name = path.Split('\\').Last(),
                ChildDirectories = System.IO.Directory.GetDirectories(path).Select(p => GetDirectoryEntry(p)),
                ChildFiles = System.IO.Directory.GetFiles(path).Select(f => GetFileEntry(f))
            };
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new System.IO.DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            System.IO.DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!System.IO.Directory.Exists(destDirName))
            {
                System.IO.Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            System.IO.FileInfo[] files = dir.GetFiles();
            foreach (System.IO.FileInfo file in files)
            {
                string temppath = System.IO.Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (System.IO.DirectoryInfo subdir in dirs)
                {
                    string temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private FileSummary GetFileEntry(string path)
        {
            return new FileSummary()
            {
                Name = path.Split('\\').Last().Split('.').FirstOrDefault(),
                LastChanged = System.IO.Directory.GetLastWriteTime(path),
            };
        }

        public bool Exists(string pathNew)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(pathNew);
            return dir.Exists;
        }
    }
}
