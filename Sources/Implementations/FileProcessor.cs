using FileIndex.Implementations;
using FileIndex.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FileIndex
{
    public class FileProcessor : IFileProcessor
    {
        private IFileComparer _fileComparer;
        private IFileLoader _fileLoader;
        public FileProcessor(IFileComparer fileComparer, IFileLoader fileLoader)
        {
            _fileComparer = fileComparer;
            _fileLoader = fileLoader;
        }

        public void GenerateSummary(string path)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            // Load file metadata
            var fileInfoContainer = _fileLoader.LoadFileInfos(path);


            // Identify identical files
            Parallel.ForEach(fileInfoContainer.FileInfos, new ParallelOptions { MaxDegreeOfParallelism = Configs.MaxNumberOfThreads }, fileInfo =>
            {
                GetIdenticalFiles(fileInfo, fileInfoContainer.FileInfos);
            });

            // Generate summary
            Console.WriteLine($"File-index of {fileInfoContainer.sourcePath} ({fileInfoContainer.QtyFiles} files in {fileInfoContainer.QtyDirectories} directories, {fileInfoContainer.Size} total)");

            if (fileInfoContainer.FileInfos.Any(x => x.IdenticalFiles.Count > 0))
            {
                Console.WriteLine("Identical files found:");

                foreach (var fileInfo in fileInfoContainer.FileInfos)
                {
                    if (fileInfo.State == FileState.IsProcessed)
                    {
                        if (!fileInfo.IdenticalFiles.Any())
                            continue;

                        // Update IncludedInSummary of identicals as well not to include them in summary any more 
                        fileInfo.IdenticalFiles.Select(x => x.State = FileState.IncludedInSummary).ToList();
                        fileInfo.State = FileState.MarkedAsPrimary;

                        Console.WriteLine($"{SizeFormatHelper.GetFormattedSize(fileInfo.Size)} - {fileInfo.FilePath}");

                        foreach (var identicalFileInfo in fileInfo.IdenticalFiles)
                            Console.WriteLine($"    {identicalFileInfo.FilePath}");
                    }
                }

                Console.WriteLine($"Total {fileInfoContainer.QtyIdenticalFiles} identical files ({fileInfoContainer.SizeOfIdenticalFiles}) found ({watch.Elapsed.TotalSeconds}s elapsed).");

            }
            else
                Console.WriteLine($"No identical files found ({watch.Elapsed.TotalSeconds}s elapsed).");

            watch.Stop();
        }

        private List<FileInfo> GetIdenticalFiles(FileInfo fileInfo, List<FileInfo> fileInfos)
        {
            // Alow only one thread at a time to treat this fileInfo as source, i.e. other threads could use it as destination  

            // This duplication is to avoid unnececcarry lock
            if (fileInfo.State != FileState.ReadyForProcessing)
                return null;

            lock (fileInfo)
            {
                if (fileInfo.State != FileState.ReadyForProcessing)
                    return null;

                fileInfo.State = FileState.InProcess;

            }
            // Identify identical files
            var IdenticalFileInfos = new List<FileInfo>();

            foreach (var fileInfoDestination in fileInfos)
            {
                if (fileInfoDestination.State != FileState.IsProcessed && fileInfoDestination.ID != fileInfo.ID)
                {
                    _fileComparer.MarkFilesIfIdentical(fileInfo, fileInfoDestination);
                }
            }

            // Exclude file and all identical files from further processing as they are compared with the rest
            fileInfo.State = FileState.IsProcessed;
            if (IdenticalFileInfos.Count > 0)
            {
                fileInfo.IdenticalFiles = IdenticalFileInfos;
                fileInfo.IdenticalFiles.Select(x => x.State = FileState.IsProcessed).ToList();
            }


            return IdenticalFileInfos;

        }
    }
}
