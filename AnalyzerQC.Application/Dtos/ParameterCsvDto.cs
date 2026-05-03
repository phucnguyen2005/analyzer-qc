using AnalyzerQC.ValueObject;

namespace AnalyzerQC.Application.Dtos;

public class ParameterCsvDto
{
    public int Id { get; set; }
    public string ParameterCode { get; set; }
    public string ParameterName { get; set; }
    public List<ParameterUnit> ParameterUnits { get; set; } = [];
    public ParameterCsvDto(int id, string parameterCode, string parameterName, List<ParameterUnit> parameterUnits)
    {
        Id = id;
        ParameterCode = parameterCode;
        ParameterName = parameterName;
        ParameterUnits = parameterUnits;
    }
}