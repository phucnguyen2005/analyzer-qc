using AnalyzerQC.Application.Dtos;

namespace AnalyzerQC.Application;

public interface IQcUploadService
{
    public Task<bool> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    public Task<bool> DeleteFileAsync(string fileName);
    public Task<string?> GetFileAsync(string fileName);
    public Task<bool> RenameFileAsync(string fileName, string newFileName);
    public Task<DownloadFileDto> DownloadFileAsync(string fileName);

    Task<List<UploadHistoryDto>> FilterFiles(DateTime? fromDate, DateTime? toDate, Guid? analyzerId,
        Guid? siteId, string? type, string? status);
}