namespace AnalyzerQC.Commons;

public abstract class Entity<TId> where TId : struct
{
    public TId Id { get; protected set; }
}

public abstract class CreationAuditedEntity<TId> : Entity<TId> where TId : struct
{
    public DateTime CreationTime { get; protected set; }
    public string CreatorId { get; protected set; } = null!;
}

public abstract class AuditedEntity<TId> : CreationAuditedEntity<TId> where TId : struct
{
    public DateTime? LastModificationTime { get; protected set; }
    public string? LastModifierId { get; protected set; }
}

public abstract class FullAuditedEntity<TId> : AuditedEntity<TId> where TId : struct
{
    public bool IsDeleted { get; protected set; }
    public DateTime? DeletionTime { get; protected set; }
    public string? DeleterId { get; protected set; }
}