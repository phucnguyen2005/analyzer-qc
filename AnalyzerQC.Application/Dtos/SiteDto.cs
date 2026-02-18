namespace AnalyzerQC.Application.Dtos;

public class SiteDto
{
    //TODO: include siteId
    public Guid SiteId { get; set; }
    public string SiteCode { get; set; }
    public string SiteName { get; set; }
    public List<AnalyzerDto> Analyzers{ get; set; }  //use analyzerDto
    public string TimeZone { get; set; }
    
}