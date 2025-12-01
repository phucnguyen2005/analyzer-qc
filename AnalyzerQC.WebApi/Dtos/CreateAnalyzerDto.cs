namespace AnalyzerQC.WebApi.Dtos;

public class CreateAnalyzerDto
{
    public string ModelCode { get; set; } = null!;
    public string SiteCode { get; set; } = null!;
    public bool Status { get; set; }
    public string SerialNumber { get; set; } = null!;
}