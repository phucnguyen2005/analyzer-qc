namespace AnalyzerQC.Commons.Auditing;

public interface IHasDeletionTime
{
    public DateTime? DeletionTime { get; set; }
}