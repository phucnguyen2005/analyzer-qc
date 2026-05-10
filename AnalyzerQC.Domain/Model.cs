using AnalyzerQC.Commons;

namespace AnalyzerQC;

public class Model : CreationAuditedEntity<int>
{
    public const int ModelCodeLength = 10;
    public const int MinModelNameLength = 8;
    public const int MaxModelNameLength = 100;
    public const string ModelNameLengthError = "Model name length is not valid";
    public const string ModelCodeLengthError = "Model code length is not valid";


    public int ModelGroupId { get; private set; }
    public ModelGroup ModelGroup { get; private set; }
    public string ModelCode { get; private set; }
    public string ModelName { get; private set; }
    public List<Analyzer> Analyzers { get; private set; }

    private Model()
    {
    }

    public Model(string modelCode, string modelName, int modelGroupId)
    {
        if (modelCode.Length > ModelCodeLength)
        {
            throw new ArgumentException(ModelCodeLengthError);
        }

        if (modelName.Length < MinModelNameLength || modelName.Length > MaxModelNameLength)
        {
            throw new ArgumentException(ModelNameLengthError);
        }

        ModelCode = modelCode;
        ModelName = modelName;
        ModelGroupId = modelGroupId;
    }
}