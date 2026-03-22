using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyzerQC.Infrastructure.Database;

public class ModelGroupConfiguration : IEntityTypeConfiguration<ModelGroup>
{
    public void Configure(EntityTypeBuilder<ModelGroup> builder)
    {
        builder
            .ToTable(nameof(ModelGroup).ToSnakeCase());
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasColumnName(nameof(ModelGroup.Id).ToSnakeCase());
        builder
            .Property(e => e.ModelGroupName)
            .HasColumnName(nameof(ModelGroup.ModelGroupName).ToSnakeCase())
            .IsRequired();
        builder
            .HasIndex(e => e.ModelGroupCode)
            .IsUnique();
        builder
            .Property(e => e.ModelGroupCode)
            .HasColumnName(nameof(ModelGroup.ModelGroupCode).ToSnakeCase());
        builder
            .HasMany(e => e.Models)
            .WithOne(e => e.ModelGroup)
            .HasForeignKey(e => e.ModelGroupId);
        builder
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(ModelGroup.CreationTime).ToSnakeCase());
        builder
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(ModelGroup.CreatorId).ToSnakeCase());
        builder
            .HasMany(e => e.Reagents)
            .WithMany(e => e.ModelGroups)
            .UsingEntity(j => j.ToTable("model_group_reagent"));
    }
}