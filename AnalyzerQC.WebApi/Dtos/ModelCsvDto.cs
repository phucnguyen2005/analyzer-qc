namespace AnalyzerQC.WebApi.Dtos;

public class ModelCsvDto
{
    public ModelCsvDto(int id, string modelCode, string modelName, string modelGroupCode, int modelGroupId)
    {
        Id = id;
        ModelName = modelName;
        ModelCode = modelCode;
        ModelGroupCode = modelGroupCode;
        ModelGroupId = modelGroupId;
    }

    public int Id { get; set; }
    public string ModelName { get; set; }
    public string ModelCode { get; set; }
    public string ModelGroupCode { get; set; }
    public int ModelGroupId { get; set; }
}