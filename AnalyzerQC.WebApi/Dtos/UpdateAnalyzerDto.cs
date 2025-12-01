namespace AnalyzerQC.WebApi.Dtos;

public class UpdateAnalyzerDto
{
    public int Id { get; set; }
    public string ModelCode { get; set; } = null!;
    public string SiteCode { get; set; } = null!;
    public string SerialNumber { get; set; } = null!;
    public bool Status { get; set; }
}