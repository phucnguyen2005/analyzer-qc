using System.Globalization;
using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using AnalyzerQC.Application;
using AnalyzerQC.Application.Dtos;
using AnalyzerQC.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AnalyzerQC.Infrastructure;

public class QcUploadService : IQcUploadService
{
    private readonly IAmazonS3 _s3Client;
    private readonly AwsOptions _awsOptions;
    private readonly IAppDbContext _dbContext;

    public QcUploadService(IAmazonS3 s3Client, IOptions<AwsOptions> awsOptions, IAppDbContext dbContext)
    {
        _s3Client = s3Client;
        _awsOptions = awsOptions.Value;
        _dbContext = dbContext;
    }

    public async Task<bool> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        try
        {
            if (fileName.Split("_")[0] != "QcFile")
            {
                throw new ArgumentException("File name must start with 'QcFile'");
            }

            if (fileName[^3..] != "txt")
            {
                throw new ArgumentException("File must be a .txt file");
            }


            var reader = new StreamReader(fileStream);
            if(await reader.ReadLineAsync() is not { } firstLine ||
               await reader.ReadLineAsync() is not { } secondLine ||
               await reader.ReadLineAsync() is not { } thirdLine)
            {
                throw new ArgumentException("File must have at least 3 lines");
            }
            await ProcessFirstLine(firstLine);
            await ProcessSecondLine(secondLine);
            await ProcessThirdLine(thirdLine);

            var bucketExists =
                await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _awsOptions.BucketName);
            if (!bucketExists)
            {
                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = _awsOptions.BucketName,
                    UseClientRegion = true
                };
                await _s3Client.PutBucketAsync(putBucketRequest);
            }

            // Gửi request upload file
            var putRequest = new PutObjectRequest
            {
                BucketName = _awsOptions.BucketName,
                Key = fileName,
                InputStream = fileStream,
                ContentType = contentType
            };

            var response = await _s3Client.PutObjectAsync(putRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[S3 Upload Error]: {ex.Message}");
            throw;
        }
    }

    public async Task<DownloadFileDto> DownloadFileAsync(string fileName)
    {
        try
        {
            var getRequest = new GetObjectRequest()
            {
                BucketName = _awsOptions.BucketName,
                Key = fileName,
            };
            var response = await _s3Client.GetObjectAsync(getRequest);
            return new DownloadFileDto
            {
                FileName = fileName,
                ContentType = response.Headers.ContentType,
                Stream = response.ResponseStream
            };
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == HttpStatusCode.NotFound ||
                                           ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return null!;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[S3 Get Error]: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteFileAsync(string fileName)
    {
        var bucketExists =
            await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _awsOptions.BucketName);
        if (!bucketExists)
        {
            var putBucketRequest = new PutBucketRequest
            {
                BucketName = _awsOptions.BucketName,
                UseClientRegion = true
            };
            await _s3Client.PutBucketAsync(putBucketRequest);
        }

        var deleteObjectRequest = new DeleteObjectRequest()
        {
            BucketName = _awsOptions.BucketName,
            Key = fileName,
        };
        var response = await _s3Client.DeleteObjectAsync(deleteObjectRequest);
        return response.HttpStatusCode == HttpStatusCode.NoContent;
    }

    public async Task<string?> GetFileAsync(string fileName)
    {
        try
        {
            var getRequest = new GetObjectRequest()
            {
                BucketName = _awsOptions.BucketName,
                Key = fileName,
            };
            var response = await _s3Client.GetObjectAsync(getRequest);
            using var reader = new StreamReader(response.ResponseStream);
            return await reader.ReadToEndAsync();
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == HttpStatusCode.NotFound ||
                                           ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[S3 Get Error]: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> RenameFileAsync(string fileName, string newFileName)
    {
        try
        {
            var getRequest = new GetObjectRequest()
            {
                BucketName = _awsOptions.BucketName,
                Key = fileName,
            };
            var response = await _s3Client.GetObjectAsync(getRequest);
            var putRequest = new PutObjectRequest
            {
                BucketName = _awsOptions.BucketName,
                Key = newFileName,
                InputStream = response.ResponseStream,
                ContentType = response.Headers.ContentType,
                Headers =
                {
                    ContentLength = response.ContentLength
                }
            };

            var putResponse = await _s3Client.PutObjectAsync(putRequest);
            if (putResponse.HttpStatusCode != HttpStatusCode.OK) return false;

            var deleteRequest = new DeleteObjectRequest()
            {
                BucketName = _awsOptions.BucketName,
                Key = fileName,
            };
            var deleteResponse = await _s3Client.DeleteObjectAsync(deleteRequest);
            return deleteResponse.HttpStatusCode == HttpStatusCode.NoContent;
        }
        catch (AmazonS3Exception ex)
        {
            Console.WriteLine($"[S3 Rename Error]");
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine($"ErrorCode: {ex.ErrorCode}");
            Console.WriteLine($"StatusCode: {ex.StatusCode}");
            return false;
        }
    }

    public async Task<List<UploadHistoryDto>> FilterFiles(DateTime? fromDate, DateTime? toDate, Guid? analyzerId,
        Guid? siteId, string? type, string? status)
    {
        var query = _dbContext.QcUploads
            .AsNoTracking()
            .Include(x => x.Analyzer)
            .ThenInclude(a => a.AssignedSite)
            .AsQueryable();

        if (fromDate.HasValue)
            query = query.Where(x => x.Time >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.Time <= toDate.Value);

        if (analyzerId.HasValue)
            query = query.Where(x => x.AnalyzerId == analyzerId.Value);

        if (siteId.HasValue)
            query = query.Where(x => x.Analyzer.SiteId == siteId.Value);

        if (type != null && Enum.TryParse<QcUploadType>(type, true, out var parsedType))
            query = query.Where(x => x.UploadType == parsedType);

        if (status != null && Enum.TryParse<Status>(status, true, out var parsedStatus))
            query = query.Where(x => x.UploadStatus == parsedStatus);

        return await query
            .Select(x => new UploadHistoryDto
            {
                UploadTimestamp = x.Time,
                AnalyzerId = x.Analyzer.Id,
                SiteId = x.Analyzer.SiteId,
                FileName = x.FileName,
                UploadType = x.UploadType,
                Status = x.UploadStatus
            })
            .ToListAsync();
    }

    private async Task<Analyzer> ProcessFirstLine(string firstLine)
    {
        var parts = firstLine.Split('|');
        var siteCode = parts[1];
        var modelCode = parts[3];
        var serialNumber = parts[4];

        var analyzer = await _dbContext.Analyzers
            .Include(a => a.AssignedSite)
            .Include(a => a.Model)
            .FirstOrDefaultAsync(a =>
                a.AssignedSite.SiteCode == siteCode &&
                a.Model.ModelCode == modelCode &&
                a.SerialNumber == serialNumber);

        if (analyzer == null)
        {
            throw new ArgumentException("Analyzer not found");
        }

        return analyzer;
    }

    private async Task<Reagent> ProcessSecondLine(string secondLine)
    {
        var parts = secondLine.Split('|');
        var reagentName = parts[1];
        var lotCode = parts[3];

        var reagent = await _dbContext.Reagents
            .Include(r => r.Lots)
            .FirstOrDefaultAsync(r =>
                r.ReagentName == reagentName &&
                r.Lots.Any(l => l.LotCode == lotCode));

        if (reagent == null)
        {
            throw new ArgumentException("Reagent not found");
        }

        return reagent;
    }

    private async Task<Parameter> ProcessThirdLine(string thirdLine)
    {
        var parts = thirdLine.Split('|');
        var date = parts[1];
        var time = parts[2];
        if (!DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dateParsed))
            throw new ArgumentException("Invalid date format");
        if (!TimeSpan.TryParseExact(time, "hhmmss", null, out var timeParsed))
            throw new ArgumentException("Invalid time format");

        var parameters = await _dbContext.Parameters.ToListAsync();
        foreach (string part in parts[3].Split("\\"))
        {
            var param = part.Split("^")[0];
            var parameter = parameters.FirstOrDefault(p => p.ParameterCode == param);
            if (parameter == null)
            {
                throw new ArgumentException("Parameter not found");
            }
        }

        return null!;
    }
}