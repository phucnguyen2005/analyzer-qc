using AnalyzerQC.ValueObject;

namespace AnalyzerQC.Application.Dtos;

public class AssayLimitDetailDto
{
    public int ReagentId { get; set; }
    public Guid LotId { get; set; }
    public Level Level { get; set; }
    public List<AssayLimitParameterDto> AssayLimitParameters { get; set; } = [];
}