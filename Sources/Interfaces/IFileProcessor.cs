using System.Collections.Generic;

namespace FileIndex
{
    public interface IFileProcessor
    {
        void GenerateSummary(string path);
    }
}
