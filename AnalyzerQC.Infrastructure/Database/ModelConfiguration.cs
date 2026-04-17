using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyzerQC.Infrastructure.Database;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder
            .ToTable(nameof(Model).ToSnakeCase());
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasColumnName(nameof(Model.Id).ToSnakeCase());
        builder
            .HasIndex(e => e.ModelCode)
            .IsUnique();
        builder
            .Property(e => e.ModelCode)
            .HasColumnName(nameof(Model.ModelCode).ToSnakeCase());
        builder
            .Property(e => e.ModelName)
            .HasColumnName(nameof(Model.ModelName).ToSnakeCase())
            .IsRequired();
        builder
            .HasMany(e => e.Analyzers)
            .WithOne(e => e.Model)
            .HasForeignKey(e => e.ModelId);
        builder
            .Property(e => e.ModelGroupId)
            .HasColumnName(nameof(Model.ModelGroupId).ToSnakeCase());
        builder
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(Model.CreationTime).ToSnakeCase());
        builder
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(Model.CreatorId).ToSnakeCase());
    }
}