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
                return FileInfos.Where(x => x.State == FileState.MarkedAsPrimary).Count();
            }
        }
        public string SizeOfIdenticalFiles
        {
            get
            {
                return GetFormattedSize(FileInfos.Where(x => x.State == FileState.MarkedAsPrimary).Sum(x=>x.Size));
            }
        }

        public string Size { 
            get 
            {
                return GetFormattedSize(_totalSizeinBytes);
            } 
            set 
            {
                _totalSizeinBytes = Convert.ToDecimal(value);
            } 
        }
        public string sourcePath;

        private decimal _totalSizeinBytes;

        private string GetFormattedSize(decimal sizeInBytes)
        {
            string sufix = DataSizeSufix.Bytes;
            decimal factor = 1;

            if (sizeInBytes >= 1024 * 1024 * 1024)
            {
                sufix = DataSizeSufix.GB;
                factor = 1024 * 1024 * 1024;
            }
            else if (sizeInBytes >= 1024 * 1024)
            {
                sufix = DataSizeSufix.MB;
                factor = 1024 * 1024;
            }
            else if (sizeInBytes >= 1024)
            {
                sufix = DataSizeSufix.KB;
                factor = 1024;
            }

            return $"{Math.Round(sizeInBytes / factor, 1)} {sufix}";
        }
    }
}
