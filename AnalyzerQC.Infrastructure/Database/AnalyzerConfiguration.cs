using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyzerQC.Infrastructure.Database;

public class AnalyzerConfiguration : IEntityTypeConfiguration<Analyzer>
{
    public void Configure(EntityTypeBuilder<Analyzer> builder)
    {
        builder
            .ToTable(nameof(Analyzer).ToSnakeCase());
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasColumnName(nameof(Analyzer.Id).ToSnakeCase());
        builder
            .Property(e => e.SerialNumber)
            .HasColumnName(nameof(Analyzer.SerialNumber).ToSnakeCase())
            .IsRequired();
        builder
            .Property(e => e.Status)
            .HasColumnName(nameof(Analyzer.Status).ToSnakeCase())
            .IsRequired();
        builder
            .Property(e => e.SiteId)
            .HasColumnName(nameof(Analyzer.SiteId).ToSnakeCase());
        builder
            .Property(e => e.ModelId)
            .HasColumnName(nameof(Analyzer.ModelId).ToSnakeCase());
        builder
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(Analyzer.CreationTime).ToSnakeCase());
        builder
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(Analyzer.CreatorId).ToSnakeCase());
        builder
            .Property(s => s.LastModificationTime)
            .HasColumnName(nameof(Analyzer.LastModificationTime).ToSnakeCase());
        builder
            .Property(s => s.LastModifierId)
            .HasColumnName(nameof(Analyzer.LastModifierId).ToSnakeCase());
        builder
            .Property(s => s.DeleterId)
            .HasColumnName(nameof(Analyzer.DeleterId).ToSnakeCase());
        builder
            .Property(s => s.DeletionTime)
            .HasColumnName(nameof(Analyzer.DeletionTime).ToSnakeCase());
        builder
            .Property(s => s.IsDeleted)
            .HasColumnName(nameof(Analyzer.IsDeleted).ToSnakeCase());
    }
}