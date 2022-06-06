using System.Collections.Generic;

namespace FileIndex
{
    public interface IFileProcessor
    {
        void GenerateSummary(string path);
        List<FileInfo> GetIdenticalFiles(FileInfo fileInfo, List<FileInfo> fileInfos);
    }
}
