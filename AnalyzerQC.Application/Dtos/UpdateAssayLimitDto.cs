using AnalyzerQC.ValueObject;

namespace AnalyzerQC.Application.Dtos;

public class UpdateAssayLimitDto
{
    public Guid Id { get; set; }
    public Level Level { get; set; }
}