using Microsoft.EntityFrameworkCore;
using AnalyzerQC.ValueObject;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyzerQC.Infrastructure.Database;

public class AssayLimitConfiguration : IEntityTypeConfiguration<AssayLimit>
{
    public void Configure(EntityTypeBuilder<AssayLimit> builder)
    {
        builder
            .ToTable(nameof(AssayLimit).ToSnakeCase());
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasColumnName(nameof(AssayLimit.Id).ToSnakeCase());
        builder
            .Property(e => e.LotId)
            .HasColumnName(nameof(AssayLimit.LotId).ToSnakeCase());
        builder
            .Property(e => e.ReagentId)
            .HasColumnName(nameof(AssayLimit.ReagentId).ToSnakeCase());
        builder
            .HasMany(e => e.AssayLimitParameters)
            .WithOne(e => e.AssayLimit)
            .HasForeignKey(e => e.AssayLimitId);
        builder
            .Property(e => e.Level)
            .HasColumnName(nameof(AssayLimit.Level).ToSnakeCase())
            .IsRequired()
            .HasConversion(
                level => $"{level.LevelCode}:{level.LevelName}",
                str => new Level(
                    str.Split(':', StringSplitOptions.None)[0],
                    str.Split(':', StringSplitOptions.None)[1]));
        builder
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(AssayLimit.CreationTime).ToSnakeCase());
        builder
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(AssayLimit.CreatorId).ToSnakeCase());
        builder
            .Property(s => s.LastModificationTime)
            .HasColumnName(nameof(AssayLimit.LastModificationTime).ToSnakeCase());
        builder
            .Property(s => s.LastModifierId)
            .HasColumnName(nameof(AssayLimit.LastModifierId).ToSnakeCase());
        builder
            .Property(s => s.DeleterId)
            .HasColumnName(nameof(AssayLimit.DeleterId).ToSnakeCase());
        builder
            .Property(s => s.DeletionTime)
            .HasColumnName(nameof(AssayLimit.DeletionTime).ToSnakeCase());
        builder
            .Property(s => s.IsDeleted)
            .HasColumnName(nameof(AssayLimit.IsDeleted).ToSnakeCase());
    }
}