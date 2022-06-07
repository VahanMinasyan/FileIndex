namespace FileIndex
{
    public enum FileState
    {
        ReadyForProcessing = 1, 
        InProcess,
        /// <summary>
        /// Has been compared to all the files and no need for further processing
        /// </summary>
        IsProcessed,
        /// <summary>
        /// Used to flag identical files to avoid duplications in final report
        /// </summary>
        IncludedInSummary,
        /// <summary>
        /// FileInfo marked as primary will serve as primary source of information which will be included in summary report, 
        /// e.g. size of the file, qty of the files
        /// </summary>
        MarkedAsPrimary
    }
}
