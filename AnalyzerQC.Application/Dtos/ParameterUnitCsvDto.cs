namespace AnalyzerQC.Application.Dtos;

public class ParameterUnitCsvDto
{
    public string UnitCode { get; set; }
    public float ConversionFactor { get; set; }
    public ParameterUnitCsvDto(string unitCode, float conversionFactor)
    {
        UnitCode = unitCode;
        ConversionFactor = conversionFactor;
    }
}