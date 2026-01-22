namespace AnalyzerQC.WebApi.Dtos;

public class SiteDto
{
    public string SiteCode { get; set; }
    public string SiteName { get; set; }
    public List<Analyzer> Analyzers{ get; set; }  //use analyzerDto
    public string TimeZone { get; set; }
    
}