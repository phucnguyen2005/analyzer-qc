using AnalyzerQC.ValueObject;

namespace AnalyzerQC.Application.Dtos;

public class ReagentCsvDto
{
    public string ReagentName { get; set; }
    public List<Level> Levels { get; set; }
    public List<ModelGroup> ModelGroups { get; set; }
    public bool Status { get; set; }

    public ReagentCsvDto(string reagentName, List<Level> levels, bool status, List<ModelGroup> modelGroups)
    {
        ReagentName = reagentName;
        Levels = levels;
        Status = status;
        ModelGroups = modelGroups;
    }
}