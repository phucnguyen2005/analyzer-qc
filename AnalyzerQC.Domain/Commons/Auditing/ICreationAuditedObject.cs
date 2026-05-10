namespace AnalyzerQC.Commons.Auditing;

public interface ICreationAuditedObject : IHasCreationTime
{
    /// <summary>
    /// This interface can be implemented to store creation information (who and when created).
    /// </summary>
    public string? CreatorId { get; }
}