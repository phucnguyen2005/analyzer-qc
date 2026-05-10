using AnalyzerQC.Commons;
using AnalyzerQC.ValueObject;

namespace AnalyzerQC;

public class Parameter : CreationAuditedEntity<int>
{
    public string ParameterName { get; set; }
    public string ParameterCode { get; set; }
    public List<ParameterUnit> ParameterUnits { get; set; } = [];
    public List<AssayLimitParameter> AssayLimitParameters { get; set; } = [];

    private Parameter()
    {
    }

    public Parameter(string parameterName, string parameterCode, List<ParameterUnit> parameterUnits)
    {
        ParameterName = parameterName;
        ParameterCode = parameterCode;
        ParameterUnits = parameterUnits;
    }

    public void AddParameterUnit(ParameterUnit parameterUnit)
    {
        ParameterUnits.Add(parameterUnit);
    }
}