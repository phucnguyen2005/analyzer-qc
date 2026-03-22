using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnalyzerQC.ValueObject;


namespace AnalyzerQC.Infrastructure.Database;

public class ReagentConfiguration : IEntityTypeConfiguration<Reagent>
{
    public void Configure(EntityTypeBuilder<Reagent> builder)
    {
        builder
            .ToTable(nameof(Reagent).ToSnakeCase());
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasColumnName(nameof(Reagent.Id).ToSnakeCase());
        builder
            .Property(e => e.ReagentName)
            .HasColumnName(nameof(Reagent.ReagentName).ToSnakeCase())
            .IsRequired();
        builder
            .Property(e => e.Description)
            .HasColumnName(nameof(Reagent.Description).ToSnakeCase());
        builder
            .Property(e => e.Status)
            .HasColumnName(nameof(Reagent.Status).ToSnakeCase())
            .IsRequired();
        builder
            .Property(e => e.Levels)
            .HasColumnName(nameof(Reagent.Levels).ToSnakeCase())
            .IsRequired()
            .HasConversion(
                levels => string.Join(' ', levels.Select(l => $"{l.LevelCode}:{l.LevelName}")),
                str => str.Split(' ', StringSplitOptions.None)
                    .Select(x => new Level(
                        x.Split(':', StringSplitOptions.None)[0],
                        x.Split(':', StringSplitOptions.None)[1]))
                    .ToList());
        builder
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(Reagent.CreationTime).ToSnakeCase());
        builder
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(Reagent.CreatorId).ToSnakeCase());
        builder
            .HasMany(e => e.AssayLimits)
            .WithOne(e => e.Reagent)
            .HasForeignKey(e => e.ReagentId);
    }
}