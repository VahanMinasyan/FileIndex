using FileIndex.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileIndex.Implementations
{
    class FileLoader : IFileLoader
    {
        public FileInfoContainer LoadFileInfos(string path)
        {
            // load all files and directories
            var dirInfo = new DirectoryInfo(path);
            var directories = GetDirectories(dirInfo)?.Select(x => new DirectoryInfo(x.FullName)).ToList();

            if (directories == null)
                directories = new List<DirectoryInfo>();

            directories.Add(dirInfo);

            var files = GetFiles(directories);

            // Initialize fileInfo container
            FileInfoContainer fileInfoContainer = new FileInfoContainer() { QtyDirectories = directories.Count, QtyFiles = files.Count };
            fileInfoContainer.sourcePath = path;
            long totalFileSizeBytes=0;
            
            foreach (System.IO.FileInfo fileInfo in files)
            {
                totalFileSizeBytes = totalFileSizeBytes + fileInfo.Length;
                fileInfoContainer.FileInfos.Add(
                   new FileInfo()
                   {
                       ID = Guid.NewGuid(),
                       FilePath = fileInfo.FullName,
                       FileName = fileInfo.Name,
                       Size = fileInfo.Length
                   });
            }

            fileInfoContainer.Size = $"{totalFileSizeBytes}";
            return fileInfoContainer;
        }

        private List<System.IO.FileInfo> GetFiles(List<DirectoryInfo> directories)
        {
           var fileInfos = new List<System.IO.FileInfo>();

            foreach(var directory in directories)
            {
                try
                {
                    fileInfos.AddRange(directory.GetFiles("*", SearchOption.TopDirectoryOnly));
                }
                // TODO add proper loggins
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }

            return fileInfos;
        }

        private List<FileSystemInfo> GetDirectories(DirectoryInfo dirInfo)
        {
            List<FileSystemInfo> directories = new List<FileSystemInfo>();

            try
            {
                directories.AddRange(dirInfo.GetFileSystemInfos().Where(x => x.Attributes.ToString() == "Directory"));
            }
            // TODO add proper loggins
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            if (directories.Count == 0)
                return null;

            var tempdirectories = new List<FileSystemInfo>();

            foreach (var direcory in directories)
            {
                var nextDirInfo = new DirectoryInfo(direcory.FullName);
                var directoriesInfos = GetDirectories(nextDirInfo);
                if (directoriesInfos != null)
                    tempdirectories.AddRange(directoriesInfos);
            }
            directories.AddRange(tempdirectories);
            return directories;
        }
    }
}
