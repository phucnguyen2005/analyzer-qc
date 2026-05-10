using AnalyzerQC.Commons;
using AnalyzerQC.ValueObject;

namespace AnalyzerQC;

public class AssayLimitParameter : FullAuditedEntity<Guid>
{
    public float Target { get; set; }
    public float LowerLimit { get; set; }
    public float UpperLimit { get; set; }
    public int ParameterId { get; set; }
    public Parameter Parameter { get; set; } = null!;
    public Guid AssayLimitId { get; set; }

    public AssayLimit AssayLimit { get; set; } = null!;
    public ParameterUnit ParameterUnit;

    private AssayLimitParameter()
    {
    }

    public AssayLimitParameter(float target, float lowerLimit, float upperLimit, int parameterId, Guid assayLimitId,
        ParameterUnit parameterUnit)
    {
        Target = target;
        LowerLimit = lowerLimit;
        UpperLimit = upperLimit;
        ParameterId = parameterId;
        AssayLimitId = assayLimitId;
        ParameterUnit = parameterUnit;
    }
}