using FileIndex.Models;

namespace FileIndex
{
    public interface IFileLoader
    {
        /// <summary>
        /// Will load all files metadata recursivly from a given directory
        /// </summary>
        /// <param name="path">Path of the directory from which files would be loaded recursively</param>
        /// <returns>Return object which contains list of file metadata</returns>
        FileInfoContainer LoadFileInfos(string path);
    }
}
