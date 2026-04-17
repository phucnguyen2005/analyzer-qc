using AnalyzerQC.ValueObject;

namespace AnalyzerQC.Application.Dtos;

public class UpdateAssayLimitParameterDto
{
    public float Target { get; set; }
    public float LowerLimit { get; set; }
    public float UpperLimit { get; set; }
    public int ParameterId { get; set; }
    public Guid AssayLimitId { get; set; }
    public ParameterUnit ParameterUnit { get; set; }
}