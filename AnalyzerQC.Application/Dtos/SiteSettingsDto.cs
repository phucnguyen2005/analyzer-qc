using AnalyzerQC.Commons;

namespace AnalyzerQC.Application.Dtos;

public class SiteSettingsDto
{
    //TODO: include siteId
    public float Frequency { get; set; }
    public NotificationTypes NotificationType { get; set; }
    public List<WorkingDays> WorkingDays { get; set; }
    public string WorkingTime { get; set; }
}