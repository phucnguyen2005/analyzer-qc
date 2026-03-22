using AnalyzerQC.Commons;

namespace AnalyzerQC;

public class AssayLimitParameter:FullAuditedEntity<int>
{
    public string Target { get; set; }
    public float LowerLimit { get; set; }
    public float UpperLimit { get; set; }
    public int ParameterId { get; set; }
    public Parameter Parameter { get; set; } = null!;
    public int AssayLimitId { get; set; }
    public AssayLimit AssayLimit { get; set; } = null!;
    //TODO: unit?
    public AssayLimitParameter(string target, float lowerLimit, float upperLimit, int parameterId)
    {
        Target = target;
        LowerLimit = lowerLimit;
        UpperLimit = upperLimit;
        ParameterId = parameterId;
    }
}