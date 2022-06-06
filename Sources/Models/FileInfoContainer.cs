using System.Collections.Generic;
using System.Linq;

namespace FileIndex.Models
{
    public class FileInfoContainer
    {
        public FileInfoContainer()
        {
            FileInfos = new List<FileInfo>();
        }
        public List<FileInfo> FileInfos;
        public int QtyFiles;
        public int QtyDirectories;
        public int QtyIdenticalFiles
        {
            get
            {
                return FileInfos.SelectMany(x=>x.IdenticalFiles).Select(x=>x.ID).Distinct().Count();
            }
        }
        public string Size;
        public string sourcePath;
    }
}
