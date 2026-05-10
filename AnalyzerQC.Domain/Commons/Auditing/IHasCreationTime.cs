namespace AnalyzerQC.Commons.Auditing;

public interface IHasCreationTime
{
    /// <summary>
    /// Creation time.
    /// </summary>
    public DateTime CreationTime { get; }
}