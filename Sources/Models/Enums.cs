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
        IncludedInSummary
    }
}
