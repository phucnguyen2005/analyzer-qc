using AnalyzerQC.Commons;

namespace AnalyzerQC;

public class QcUpload: FullAuditedEntity<Guid>
{
    public string FileName { get; private set; }
    public Status UploadStatus { get; private set; }
    

    public QcUploadType UploadType { get; private set; }
    public Analyzer Analyzer { get; private set; }
    public Guid AnalyzerId { get; private set; }
    public DateTime Time { get; private set; }
    public string FileKey { get; private set; }

    private QcUpload()
    {
    }

    public QcUpload(string fileName, Status uploadStatus, QcUploadType uploadType, Guid analyzerId, DateTime time,
        string fileKey)
    {
        FileName = fileName;
        UploadStatus = uploadStatus;
        UploadType = uploadType;
        AnalyzerId = analyzerId;
        Time = time;
        FileKey = fileKey;
    }
}