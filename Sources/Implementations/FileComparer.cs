using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileIndex
{
    public class FileComparer : IFileComparer
    {
        public bool FilesAreIdentical(FileInfo file1, FileInfo file2)
        {
            // Lock the files always in the same order to prevent deadlock
            List<FileInfo> sortedLockObject = new List<FileInfo>();

            sortedLockObject.Add(file1);
            sortedLockObject.Add(file2);
            sortedLockObject = sortedLockObject.OrderBy(x => x.ID).ToList();

            lock (sortedLockObject[0])
            {
                lock (sortedLockObject[1])
                {
                    if (file1.Size != file2.Size || file1.Size == 0 || file2.Size == 0)
                        return false;


                    const int MAX_BUFFER = 33554432; //32MB
                    byte[] buffer = new byte[MAX_BUFFER];
                    byte[] buffer1 = new byte[MAX_BUFFER];

                    using (FileStream fs = File.Open(file1.FilePath, FileMode.Open, FileAccess.Read))
                    using (FileStream fs1 = File.Open(file2.FilePath, FileMode.Open, FileAccess.Read))
                    using (BufferedStream bs = new BufferedStream(fs))
                    using (BufferedStream bs1 = new BufferedStream(fs1))
                    {
                        var memoryStream = new MemoryStream(buffer);
                        var stream = new StreamReader(memoryStream);
                        while ((bs.Read(buffer, 0, MAX_BUFFER)) != 0
                            && (bs1.Read(buffer1, 0, MAX_BUFFER)) != 0)
                        {
                            for (int i = 0; i <= bs.Length - 1; i++)
                            {
                                memoryStream.Seek(0, SeekOrigin.Begin);
                                int temp = buffer[i] ^ buffer1[i];
                                if (temp != 0)
                                {
                                    return false;
                                }
                            }
                        }

                        if (!file1.IdenticalFiles.Any(x => x.ID == file2.ID))
                            file1.IdenticalFiles.Add(file2);
                        if (!file2.IdenticalFiles.Any(x => x.ID == file1.ID))
                            file2.IdenticalFiles.Add(file1);
                        return true;
                    }
                }
            }
        }
    }
}
