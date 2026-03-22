namespace AnalyzerQC.ValueObject;

public class ParameterUnit: Commons.ValueObject
{
    public string UnitCode { get; private set; }
    public float ConversionFactor { get; private set; }

    public ParameterUnit(string unitCode, float conversionFactor)
    {
        UnitCode = unitCode;
        ConversionFactor = conversionFactor;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UnitCode;
        yield return ConversionFactor;
    }
}