namespace AnalyzerQC.Application.Dtos;

public class DownloadFileDto
{
    public Stream Stream { get; set; } = default!;
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = "application/octet-stream";
}