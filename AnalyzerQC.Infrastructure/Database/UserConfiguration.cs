using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyzerQC.Infrastructure.Database;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(u => u.UserName).IsUnique();
        builder
            .Property(u => u.UserName).IsRequired().HasMaxLength(100);
        builder
            .ToTable(nameof(User).ToSnakeCase());
        builder
            .Property(u => u.UserName)
            .HasColumnName(nameof(User.UserName).ToSnakeCase())
            .HasMaxLength(User.MaxUserNameLength)
            .IsRequired();
        builder.HasKey(e => e.Id);
        builder
            .Property(s => s.Id)
            .HasColumnName(nameof(User.Id).ToSnakeCase())
            .IsRequired();
        builder
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(User.CreationTime).ToSnakeCase());
        builder
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(User.CreatorId).ToSnakeCase());
        builder
            .Property(s => s.LastModificationTime)
            .HasColumnName(nameof(User.LastModificationTime).ToSnakeCase());
        builder
            .Property(s => s.LastModifierId)
            .HasColumnName(nameof(User.LastModifierId).ToSnakeCase());
        builder
            .Property(s => s.DeleterId)
            .HasColumnName(nameof(User.DeleterId).ToSnakeCase());
        builder
            .Property(s => s.DeletionTime)
            .HasColumnName(nameof(User.DeletionTime).ToSnakeCase());
        builder
            .Property(s => s.IsDeleted)
            .HasColumnName(nameof(User.IsDeleted).ToSnakeCase());
    }
}