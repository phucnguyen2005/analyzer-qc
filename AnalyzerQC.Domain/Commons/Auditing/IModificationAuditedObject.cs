namespace AnalyzerQC.Commons.Auditing;

public interface IModificationAuditedObject : IHasModificationTime
{
    public string? LastModifierId { get; }
}