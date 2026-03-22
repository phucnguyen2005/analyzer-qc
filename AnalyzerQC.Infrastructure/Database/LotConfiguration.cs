using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyzerQC.Infrastructure.Database;

public class LotConfiguration : IEntityTypeConfiguration<Lot>
{
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
        builder
            .ToTable(nameof(Lot).ToSnakeCase());
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasColumnName(nameof(Lot.Id).ToSnakeCase());
        builder
            .HasIndex(e => e.LotCode)
            .IsUnique();
        builder
            .Property(e => e.LotCode)
            .HasColumnName(nameof(Lot.LotCode).ToSnakeCase());
        builder
            .Property(e => e.StartDate)
            .HasColumnName(nameof(Lot.StartDate).ToSnakeCase());
        builder
            .Property(e => e.ExpiryDate)
            .HasColumnName(nameof(Lot.ExpiryDate).ToSnakeCase());
        builder
            .Property(e => e.IsActive)
            .HasColumnName(nameof(Lot.IsActive).ToSnakeCase());
        builder
            .HasMany(e => e.Reagents)
            .WithMany(e => e.Lots)
            .UsingEntity(j => j.ToTable("lot_reagent"));
        builder
            .HasMany(e => e.AssayLimits)
            .WithOne(e => e.Lot)
            .HasForeignKey(e => e.LotId);
        builder
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(Lot.CreationTime).ToSnakeCase());
        builder
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(Lot.CreatorId).ToSnakeCase());
        builder
            .Property(s => s.LastModificationTime)
            .HasColumnName(nameof(Lot.LastModificationTime).ToSnakeCase());
        builder
            .Property(s => s.LastModifierId)
            .HasColumnName(nameof(Lot.LastModifierId).ToSnakeCase());
        builder
            .Property(s => s.DeleterId)
            .HasColumnName(nameof(Lot.DeleterId).ToSnakeCase());
        builder
            .Property(s => s.DeletionTime)
            .HasColumnName(nameof(Lot.DeletionTime).ToSnakeCase());
        builder
            .Property(s => s.IsDeleted)
            .HasColumnName(nameof(Lot.IsDeleted).ToSnakeCase());
    }
}