using AnalyzerQC.ValueObject;

namespace AnalyzerQC.Application.Dtos;

public class ParameterCsvDto
{
    public int Id { get; set; }
    public string ParameterCode { get; set; }
    public string ParameterName { get; set; }
    public List<ParameterUnitCsvDto> ParameterUnits { get; set; }
    public ParameterCsvDto(string parameterCode, string parameterName)
    {
        ParameterCode = parameterCode;
        ParameterName = parameterName;
    }
}