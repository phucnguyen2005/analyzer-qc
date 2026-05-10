namespace AnalyzerQC.Commons.Auditing;

public interface IDeletionAuditedObject : IHasDeletionTime
{
    public string? DeleterId { get; set; }
}