using AnalyzerQC.ValueObject;

namespace AnalyzerQC.Application.Dtos;

public class ReagentCsvDto
{
    public string ReagentName { get; set; }
    public List<string> Levels { get; set; }

    public bool Status { get; set; }
    public string ModelGroupCode { get; set; }

    public ReagentCsvDto(string reagentName, List<string> levels, bool status, string modelGroupCode)
    {
        ReagentName = reagentName;
        Levels = levels;
        Status = status;
        ModelGroupCode = modelGroupCode;
    }
}