using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyzerQC.Infrastructure.Database;

public class AssayLimitParameterConfiguration : IEntityTypeConfiguration<AssayLimitParameter>
{
    public void Configure(EntityTypeBuilder<AssayLimitParameter> builder)
    {
        builder
            .ToTable(nameof(AssayLimitParameter).ToSnakeCase());
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasColumnName(nameof(AssayLimitParameter.Id).ToSnakeCase());
        builder
            .Property(e => e.Target)
            .HasColumnName(nameof(AssayLimitParameter.Target).ToSnakeCase());
        builder
            .Property(e => e.LowerLimit)
            .HasColumnName(nameof(AssayLimitParameter.LowerLimit).ToSnakeCase());
        builder
            .Property(e => e.UpperLimit)
            .HasColumnName(nameof(AssayLimitParameter.UpperLimit).ToSnakeCase());
        builder
            .Property(e => e.ParameterId)
            .HasColumnName(nameof(AssayLimitParameter.ParameterId).ToSnakeCase());
        builder
            .Property(e => e.AssayLimitId)
            .HasColumnName(nameof(AssayLimitParameter.AssayLimitId).ToSnakeCase());
        builder
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(AssayLimitParameter.CreationTime).ToSnakeCase());
        builder
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(AssayLimitParameter.CreatorId).ToSnakeCase());
        builder
            .Property(s => s.LastModificationTime)
            .HasColumnName(nameof(AssayLimitParameter.LastModificationTime).ToSnakeCase());
        builder
            .Property(s => s.LastModifierId)
            .HasColumnName(nameof(AssayLimitParameter.LastModifierId).ToSnakeCase());
        builder
            .Property(s => s.DeleterId)
            .HasColumnName(nameof(AssayLimitParameter.DeleterId).ToSnakeCase());
        builder
            .Property(s => s.DeletionTime)
            .HasColumnName(nameof(AssayLimitParameter.DeletionTime).ToSnakeCase());
        builder
            .Property(s => s.IsDeleted)
            .HasColumnName(nameof(AssayLimitParameter.IsDeleted).ToSnakeCase());
    }
}