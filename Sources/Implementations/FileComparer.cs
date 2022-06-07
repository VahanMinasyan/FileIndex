using FileIndex.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileIndex
{
    public class FileComparer : IFileComparer
    {
        public bool MarkFilesIfIdentical(FileInfo sourceFile, FileInfo destinationFile)
        {
            // Lock the files always in the same order to prevent deadlock
            List<FileInfo> sortedLockObject = new List<FileInfo>();

            sortedLockObject.Add(sourceFile);
            sortedLockObject.Add(destinationFile);
            sortedLockObject = sortedLockObject.OrderBy(x => x.ID).ToList();

            lock (sortedLockObject[0])
            {
                lock (sortedLockObject[1])
                {
                    if (sourceFile.Size != destinationFile.Size || sourceFile.Size == 0 || destinationFile.Size == 0)
                        return false;

                    using (FileStream fs = File.Open(sourceFile.FilePath, FileMode.Open, FileAccess.Read))
                    using (FileStream fs1 = File.Open(destinationFile.FilePath, FileMode.Open, FileAccess.Read))
                    using (BufferedStream bs = new BufferedStream(fs))
                    using (BufferedStream bs1 = new BufferedStream(fs1))
                    {
                        int maxBuffer = Configs.MAX_BUFFER< bs.Length ? Configs.MAX_BUFFER : Convert.ToInt32(bs.Length);
                        byte[] sourceFilebuffer = new byte[maxBuffer];
                        byte[] destinationFileBuffer = new byte[maxBuffer];

                        while ((bs.Read(sourceFilebuffer, 0, maxBuffer)) != 0
                            && (bs1.Read(destinationFileBuffer, 0, maxBuffer)) != 0)
                        {
                            if (!((ReadOnlySpan<byte>)sourceFilebuffer).SequenceEqual(destinationFileBuffer)) return false;
                        }

                        if (!sourceFile.IdenticalFiles.Any(x => x.ID == destinationFile.ID))
                            sourceFile.IdenticalFiles.Add(destinationFile);
                        if (!destinationFile.IdenticalFiles.Any(x => x.ID == sourceFile.ID))
                            destinationFile.IdenticalFiles.Add(sourceFile);
                        return true;
                    }
                }
            }
        }
    }
}
