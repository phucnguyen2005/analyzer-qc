using AnalyzerQC.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyzerQC.Infrastructure.Database;

public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
{
    public void Configure(EntityTypeBuilder<Parameter> builder)
    {
        builder
            .ToTable(nameof(Parameter).ToSnakeCase());
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasColumnName(nameof(Parameter.Id).ToSnakeCase());
        builder
            .Property(e => e.ParameterName)
            .HasColumnName(nameof(Parameter.ParameterName).ToSnakeCase())
            .IsRequired();
        builder
            .Property(e => e.ParameterUnits)
            .HasColumnName(nameof(Parameter.ParameterUnits).ToSnakeCase())
            .IsRequired()
            .HasConversion(units => string.Join(' ', units.Select(l => $"{l.UnitCode}:{l.ConversionFactor}")),
                str => str.Split(' ', StringSplitOptions.None)
                    .Select(x => new ParameterUnit(
                        x.Split(':', StringSplitOptions.None)[0],
                        float.Parse(x.Split(':', StringSplitOptions.None)[1]))).ToList());
        builder
            .HasMany(e => e.AssayLimitParameters)
            .WithOne(e => e.Parameter)
            .HasForeignKey(e => e.ParameterId);
        builder
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(Parameter.CreationTime).ToSnakeCase());
        builder
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(Parameter.CreatorId).ToSnakeCase());
    }
}