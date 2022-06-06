using System;
using System.Collections.Generic;

namespace FileIndex
{
    public class FileInfo
    {
        public FileInfo()
        {
            IdenticalFiles = new List<FileInfo>();
            State = FileState.ReadyForProcessing;
        }

        public Guid ID;
        public long Size;
        public List<FileInfo> IdenticalFiles;
        public FileState State;
        public string FileName;
        public string FilePath;
    }
}
