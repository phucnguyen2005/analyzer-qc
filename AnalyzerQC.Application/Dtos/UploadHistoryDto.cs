using AnalyzerQC.Commons;

namespace AnalyzerQC.Application.Dtos;

public class UploadHistoryDto
{
    public DateTime UploadTimestamp { get; set; }
    public Guid AnalyzerId { get; set; }
    public Guid SiteId { get; set; }
    public string FileName { get; set; }
    public QcUploadType UploadType { get; set; }
    public Status Status { get; set; }
}