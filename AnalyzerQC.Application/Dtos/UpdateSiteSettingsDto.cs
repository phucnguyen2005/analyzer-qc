using AnalyzerQC.Commons;

namespace AnalyzerQC.Application.Dtos;

public class UpdateSiteSettingsDto
{
    public Guid Id { get; set; }
    public float frequency { get; set; }
    public NotificationTypes NotificationType { get; set; }
    public List<WorkingDays> WorkingDays { get; set; }
    public string WorkingTime { get; set; }
}