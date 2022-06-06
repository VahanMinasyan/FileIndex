using FileIndex.Implementations;
using System;
using System.IO;

namespace FileIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (InputIsValid(args))
                {
                    IFileComparer fileComparer = new FileComparer();
                    IFileLoader fileLoader = new FileLoader();
                    IFileProcessor fileProcessor = new FileProcessor(fileComparer, fileLoader);

                    string folderPath = args[0];
                    fileProcessor.GenerateSummary(folderPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static bool InputIsValid(string[] args)
        {
            if (args.Length != 1)
                throw new Exception("FileIndex requires 1 argument, which should be a valid path.");
            if (!Directory.Exists(args[0]))
                throw new Exception("Directory with provided path doesn't exist, please provide valid path.");

            return true;
        }
    }
}
