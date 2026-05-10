using AnalyzerQC.Application;
using AnalyzerQC.Commons.Auditing;
using AnalyzerQC.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AnalyzerQC.Infrastructure;

public class AuditInterceptor(IUserContext userContext) : SaveChangesInterceptor
{
    private readonly string _currentUserId = userContext.UserId.ToString();
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        => !ApplyAuditInfo(eventData) ? result : base.SavingChanges(eventData, result);

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
        => !ApplyAuditInfo(eventData)
            ? ValueTask.FromResult(result)
            : base.SavingChangesAsync(eventData, result, cancellationToken);

    private bool ApplyAuditInfo(DbContextEventData eventData)
    {
        if (eventData.Context is not AppDbContext) return false;

        UpdateCreationTimeEntities(eventData.Context);
        UpdateModificationTimeEntities(eventData.Context);
        UpdateDeletionTimeEntities(eventData.Context);

        return true;
    }

    private void UpdateCreationTimeEntities(DbContext appDbContext)
    {
        var hasCreationTimeEntries = appDbContext.ChangeTracker.Entries<IHasCreationTime>();

        foreach (var entityEntry in hasCreationTimeEntries)
        {
            if (entityEntry.State != EntityState.Added) continue;
            entityEntry.Property(e => e.CreationTime).CurrentValue = DateTime.UtcNow;

            // If the entity is type of ICreationAuditedObject<T>, we should set CreatorId
            if (entityEntry.Entity is ICreationAuditedObject)
                entityEntry.Property(nameof(ICreationAuditedObject.CreatorId)).CurrentValue = _currentUserId;
        }
    }

    private void UpdateModificationTimeEntities(DbContext appDbContext)
    {
        var hasModificationTimeEntries = appDbContext.ChangeTracker.Entries<IHasModificationTime>();

        foreach (var entityEntry in hasModificationTimeEntries)
        {
            if (entityEntry.State is not (EntityState.Added or EntityState.Modified)) continue;

            entityEntry.Property(e => e.LastModificationTime).CurrentValue = DateTime.Now;

            if (entityEntry.Entity is IModificationAuditedObject)
                entityEntry.Property(nameof(IModificationAuditedObject.LastModifierId)).CurrentValue = _currentUserId;
        }
    }

    private void UpdateDeletionTimeEntities(DbContext appDbContext)
    {
        var hasDeletionTimeEntries = appDbContext.ChangeTracker.Entries<IHasDeletionTime>();
        
        foreach (var entityEntry in hasDeletionTimeEntries)
        {
            if (entityEntry.State != EntityState.Deleted) continue;

            entityEntry.Property(e => e.DeletionTime).CurrentValue = DateTime.Now;

            if (entityEntry.Entity is IDeletionAuditedObject)
                entityEntry.Property(nameof(IDeletionAuditedObject.DeleterId)).CurrentValue = _currentUserId;
        }
    }
}