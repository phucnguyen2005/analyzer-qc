using AnalyzerQC.Commons;
using AnalyzerQC.ValueObject;

namespace AnalyzerQC;

public class Parameter:CreationAuditedEntity<int>
{
    public string ParameterName { get; set; }
    public string ParameterCode { get; set; }
    public List<ParameterUnit> ParameterUnits { get; set; } = [];
    public List<AssayLimitParameter> AssayLimitParameters { get; set; } = [];
    public Parameter()
    {
    }
    public Parameter(string parameterName, string parameterCode)
    {
        ParameterName = parameterName;
        ParameterCode = parameterCode;
    }

    public void AddParameterUnit(ParameterUnit parameterUnit)
    {
        ParameterUnits.Add(parameterUnit);
    }
}