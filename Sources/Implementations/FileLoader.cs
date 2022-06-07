using FileIndex.Models;
using System;
using System.IO;

namespace FileIndex.Implementations
{
    class FileLoader : IFileLoader
    {
        public FileInfoContainer LoadFileInfos(string path)
        {
            // load all files and directories
            var dirInfo = new DirectoryInfo(path);
            System.IO.FileInfo[] files = dirInfo.GetFiles("*", SearchOption.AllDirectories);
            DirectoryInfo[] directories = dirInfo.GetDirectories( "*", SearchOption.AllDirectories);

            // Initialize fileInfo container
            FileInfoContainer fileInfoContainer = new FileInfoContainer() { QtyDirectories = directories.Length, QtyFiles = files.Length };
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
    }
}
