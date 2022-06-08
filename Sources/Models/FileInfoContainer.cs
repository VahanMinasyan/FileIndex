using FileIndex.Implementations;
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
                return SizeFormatHelper.GetFormattedSize(FileInfos.Where(x => x.State == FileState.MarkedAsPrimary).Sum(x=>x.Size));
            }
        }

        public string Size { 
            get 
            {
                return SizeFormatHelper.GetFormattedSize(_totalSizeinBytes);
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
