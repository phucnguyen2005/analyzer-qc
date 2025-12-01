namespace AnalyzerQC;

public class Model
{
    public const int ModelCodeLength = 8;
    public const int MinModelNameLength = 8;
    public const int MaxModelNameLength = 100;
    public const string ModelNameLengthError = "Model name length is not valid";
    public const string ModelCodeLengthError = "Model code length is not valid";


    public static int Count = 1;
    public int Id { get; private set; }
    public string ModelCode { get; private set; }
    public string ModelName { get; private set; }

    public Model(int id, string modelCode, string modelName)
    {
        if (modelCode.Length != ModelCodeLength)
        {
            throw new ArgumentException(ModelCodeLengthError);
        }

        if (modelName.Length < MinModelNameLength || modelName.Length > MaxModelNameLength)
        {
            throw new ArgumentException(ModelNameLengthError);
        }

        Id = Count++;
        ModelCode = modelCode;
        ModelName = modelName;
    }
}