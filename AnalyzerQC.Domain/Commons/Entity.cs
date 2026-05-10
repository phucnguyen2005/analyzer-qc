

using AnalyzerQC.Commons.Auditing;

namespace AnalyzerQC.Commons;

public abstract class Entity<TId> where TId : struct
{
    public TId Id { get; protected set; }
}

public abstract class CreationAuditedEntity<TId> : Entity<TId>, ICreationAuditedObject where TId : struct
{
    public DateTime CreationTime { get; set; }
    public string? CreatorId { get; protected set; }
}

public abstract class AuditedEntity<TId> : CreationAuditedEntity<TId>, IModificationAuditedObject where TId : struct
{
    public DateTime? LastModificationTime { get; set; }
    public string? LastModifierId { get; protected set; }
}

public abstract class FullAuditedEntity<TId> : AuditedEntity<TId>, IDeletionAuditedObject where TId : struct
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletionTime { get; set; }
    public string? DeleterId { get; set; }
}