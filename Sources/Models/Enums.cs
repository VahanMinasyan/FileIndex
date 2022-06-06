namespace FileIndex
{
    public enum FileState
    {
        ReadyForProcessing = 1, 
        InProcess, 
        IsProcessed, //Has been compared to all the files and no need for further processing
        IncludedInSummary
    }
}
