namespace AnalyzerQC.WebApi.Dtos;

public class ModelGroupCsvDto
{
    public int Id { get; set; }
    public string ModelGroupName { get; set; }
    public string ModelGroupCode { get; set; }

    public ModelGroupCsvDto(int id, string modelGroupCode, string modelGroupName)
    {
        Id = id;
        ModelGroupName = modelGroupName;
        ModelGroupCode = modelGroupCode;
    }
}