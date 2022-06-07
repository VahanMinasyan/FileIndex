namespace FileIndex
{
    public interface IFileComparer
    {
        /// <summary>
        /// Compares and marks files if they are identical
        /// </summary>
        /// <returns>Returns false if files are not identical</returns>
        bool MarkFilesIfIdentical(FileInfo sourceFile, FileInfo DestinationFile);
    }
}
