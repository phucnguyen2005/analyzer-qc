namespace AnalyzerQC.Commons.Auditing;

public interface IHasModificationTime
{
    public DateTime? LastModificationTime { get; }
}