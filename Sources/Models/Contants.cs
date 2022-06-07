namespace FileIndex.Models
{
    // Havn't used ConfigurationManager and app.config to avoid additional dependency
    public static class Configs
    {
        /// <summary>
        /// Maximum number of threads that will search identical files
        /// </summary>
        public const int MaxNumberOfThreads = 10;

        /// <summary>
        /// Maximum amount of data to be loaded from the file by one chunk
        /// </summary>
        public const int MAX_BUFFER = 33554432; //32MB


    }

    public static class DataSizeSufix
    {
        public const string Bytes = "Bytes";
        public const string KB = "KB";
        public const string MB = "MB";
        public const string GB = "GB";
    }
}
