using FileIndex.Models;
using System;

namespace FileIndex.Implementations
{
    public static class SizeFormatHelper
    {
        public static string GetFormattedSize(decimal sizeInBytes)
        {
            string sufix = DataSizeSufix.Bytes;
            decimal factor = 1;

            if (sizeInBytes >= 1024 * 1024 * 1024)
            {
                sufix = DataSizeSufix.GB;
                factor = 1024 * 1024 * 1024;
            }
            else if (sizeInBytes >= 1024 * 1024)
            {
                sufix = DataSizeSufix.MB;
                factor = 1024 * 1024;
            }
            else if (sizeInBytes >= 1024)
            {
                sufix = DataSizeSufix.KB;
                factor = 1024;
            }

            return $"{Math.Round(sizeInBytes / factor, 1)} {sufix}";
        }
    }
}
