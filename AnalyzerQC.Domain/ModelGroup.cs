namespace AnalyzerQC;

public class ModelGroup
{
    public const int ModelGroupCodeLength = 8;
    public const int MinModelGroupNameLength = 8;
    public const int MaxModelGroupNameLength = 100;
    public const string ModelGroupNameLengthError = "Model group name length is not valid";
    public const string ModelGroupCodeLengthError = "Model group code length is not valid";

    public int Id { get; private set; }
    public string ModelGroupName { get; private set; }
    public string ModelGroupCode { get; private set; }
    public List<Model> Models { get; private set; } = []; //navigation property

    public ModelGroup(string modelGroupName, string modelGroupCode)
    {
        if (modelGroupCode.Length > ModelGroupCodeLength)
        {
            throw new ArgumentException(ModelGroupCodeLengthError);
        }

        if (modelGroupName.Length < MinModelGroupNameLength || modelGroupName.Length > MaxModelGroupNameLength)
        {
            throw new ArgumentException(ModelGroupNameLengthError);
        }

        ModelGroupName = modelGroupName;
        ModelGroupCode = modelGroupCode;
    }

    public void AddModel(Model model)
    {
        Models.Add(model);
    }
}