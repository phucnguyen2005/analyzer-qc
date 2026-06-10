using AnalyzerQC.Commons;

namespace AnalyzerQC.Infrastructure.Database;

using AnalyzerQC.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class QcUploadConfiguration : IEntityTypeConfiguration<QcUpload>
{
    public void Configure(EntityTypeBuilder<QcUpload> builder)
    {
        builder
            .ToTable(nameof(QcUpload).ToSnakeCase());
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasColumnName(nameof(QcUpload.Id).ToSnakeCase());
        builder
            .Property(e => e.FileName)
            .HasColumnName(nameof(QcUpload.FileName).ToSnakeCase());
        builder
            .Property(e => e.AnalyzerId)
            .HasColumnName(nameof(QcUpload.AnalyzerId).ToSnakeCase());
        builder
            .HasOne(e => e.Analyzer)
            .WithMany(e => e.QcUpload)
            .HasForeignKey(e => e.AnalyzerId);

        builder
            .Property(e => e.Time)
            .HasColumnName(nameof(QcUpload.Time).ToSnakeCase());
        builder
            .Property(e => e.FileKey)
            .HasColumnName(nameof(QcUpload.FileKey).ToSnakeCase())
            .IsRequired();

        builder
            .Property(e => e.UploadType)
            .HasColumnName(nameof(QcUpload.UploadType).ToSnakeCase())
            .IsRequired()
            .HasConversion<string>();
        builder
            .Property(p => p.UploadStatus)
            .HasColumnName(nameof(QcUpload.UploadStatus).ToSnakeCase())
            .IsRequired()
            .HasConversion<string>();
        builder
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(QcUpload.CreationTime).ToSnakeCase());
        builder
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(QcUpload.CreatorId).ToSnakeCase());
        builder
            .Property(s => s.LastModificationTime)
            .HasColumnName(nameof(QcUpload.LastModificationTime).ToSnakeCase());
        builder
            .Property(s => s.LastModifierId)
            .HasColumnName(nameof(QcUpload.LastModifierId).ToSnakeCase());
        builder
            .Property(s => s.DeleterId)
            .HasColumnName(nameof(QcUpload.DeleterId).ToSnakeCase());
        builder
            .Property(s => s.DeletionTime)
            .HasColumnName(nameof(QcUpload.DeletionTime).ToSnakeCase());
        builder
            .Property(s => s.IsDeleted)
            .HasColumnName(nameof(QcUpload.IsDeleted).ToSnakeCase());
    }
}