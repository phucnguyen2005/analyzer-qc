using AnalyzerQC.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyzerQC.Infrastructure.Database;

public class SiteConfiguration : IEntityTypeConfiguration<Site>
{
    public void Configure(EntityTypeBuilder<Site> builder)
    {
        builder
            .ToTable(nameof(Site).ToSnakeCase());
        builder
            .HasKey(e => e.Id);
        builder
            .Property(s => s.Id)
            .HasColumnName(nameof(Site.Id).ToSnakeCase())
            .IsRequired();
        builder
            .HasIndex(e => e.SiteCode)
            .IsUnique();
        builder
            .Property(s => s.SiteCode)
            .HasColumnName(nameof(Site.SiteCode).ToSnakeCase())
            .IsRequired();
        builder
            .Property(e => e.SiteName)
            .HasColumnName(nameof(Site.SiteName).ToSnakeCase())
            .HasMaxLength(Site.MaxSiteNameLength)
            .IsRequired();
        builder
            .Property(s => s.Address)
            .HasColumnName(nameof(Site.Address).ToSnakeCase())
            .IsRequired();
        builder
            .Property(s => s.TimeZone)
            .HasColumnName(nameof(Site.TimeZone).ToSnakeCase())
            .IsRequired();
        builder
            .Property(s => s.IsActive)
            .HasColumnName(nameof(Site.IsActive).ToSnakeCase())
            .IsRequired();
        builder
            .HasMany(e => e.Analyzers)
            .WithOne(e => e.AssignedSite)
            .HasForeignKey(e => e.SiteId);
        builder
            .Property(s => s.WorkingTime)
            .HasColumnName(nameof(Site.WorkingTime).ToSnakeCase())
            .IsRequired();
        builder
            .Property(s => s.Frequency)
            .HasColumnName(nameof(Site.Frequency).ToSnakeCase())
            .IsRequired();
        builder
            .Property(s => s.NotificationType)
            .HasColumnName(nameof(Site.NotificationType).ToSnakeCase())
            .IsRequired();
        builder
            .Property(p => p.WorkingDays)
            .HasColumnName(nameof(Site.WorkingDays).ToSnakeCase())
            .IsRequired()
            .HasConversion(
                workingDays => string.Join(',', workingDays),
                str => str.Split(',', StringSplitOptions.None)
                    .Select(Enum.Parse<WorkingDays>).ToList()
            );
        builder
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(Site.CreationTime).ToSnakeCase());
        builder
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(Site.CreatorId).ToSnakeCase());
        builder
            .Property(s => s.LastModificationTime)
            .HasColumnName(nameof(Site.LastModificationTime).ToSnakeCase());
        builder
            .Property(s => s.LastModifierId)
            .HasColumnName(nameof(Site.LastModifierId).ToSnakeCase());
        builder
            .Property(s => s.DeleterId)
            .HasColumnName(nameof(Site.DeleterId).ToSnakeCase());
        builder
            .Property(s => s.DeletionTime)
            .HasColumnName(nameof(Site.DeletionTime).ToSnakeCase());
        builder
            .Property(s => s.IsDeleted)
            .HasColumnName(nameof(Site.IsDeleted).ToSnakeCase());
    }
}