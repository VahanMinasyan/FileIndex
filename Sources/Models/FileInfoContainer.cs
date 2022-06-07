using System;
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
        public string Size { 
            get 
            {
                string sufix = "Bytes";
                decimal factor = 1;

                if (_totalSizeinBytes >= 1024 * 1024 * 1024)
                {
                    sufix = "GB";
                    factor = 1024 * 1024 * 1024;
                }
                if (_totalSizeinBytes >= 1024 * 1024)
                {
                    factor = 1024 * 1024;
                    sufix = "MB";
                }
                if (_totalSizeinBytes >= 1024)
                {
                    factor = 1024;
                    sufix = "KB";
                }

                return $"{Math.Round(_totalSizeinBytes / factor,1)} {sufix}"; 
            } 
            set 
            {
                _totalSizeinBytes = Convert.ToDecimal(value);
            } 
        }
        public string sourcePath;

        private decimal _totalSizeinBytes;
    }
}
