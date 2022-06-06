namespace FileIndex
{
    public interface IFileComparer
    {
        bool FilesAreIdentical(FileInfo file1, FileInfo file2);
    }
}
