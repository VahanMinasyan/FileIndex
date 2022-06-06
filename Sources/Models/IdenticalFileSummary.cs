using System.Collections.Generic;

namespace FileIndex.Models
{
    public class IdenticalFileSummary
    {
        public IdenticalFileSummary()
        {
            IdenticalFiles = new List<IdenticalFile>();
        }
        public string Size;
        public List<IdenticalFile> IdenticalFiles; 
    }

    public class IdenticalFile
    {
        public IdenticalFile()
        {
            FilePaths = new List<string>();
        }
        public string Size;
        public List<string> FilePaths;
    }
}
