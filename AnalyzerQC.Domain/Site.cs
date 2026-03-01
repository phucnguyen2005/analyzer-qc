using AnalyzerQC.Commons;

namespace AnalyzerQC;

public class Site : FullAuditedEntity<Guid>
{
    public const string SiteNameLengthError = "Site name length is not valid";
    public const string SiteCodeLengthError = "Site code length is not valid";
    public const string SiteFullError = "Site full";
    public const int MinSiteNameLength = 8;
    public const int SiteCodeLength = 8;
    public const int MaxSiteNameLength = 100;
    public const int MaxAnalyzer = 10;


    public string SiteName { get; private set; }
    public string SiteCode { get; set; }
    public string Address { get; private set; }
    public string TimeZone { get; private set; } = null!;
    public bool IsActive { get; set; }
    public List<Analyzer> Analyzers { get; private set; }
    public string WorkingTime { get; set; }
    public float Frequency { get; set; }
    public NotificationTypes NotificationType { get; set; }


    public List<WorkingDays> WorkingDays { get; set; }

    private Site()
    {
    } //for efcore

    public Site(string siteName, string siteCode, string address, string timeZone, bool isActive)
    {
        if (siteCode.Length != SiteCodeLength)
        {
            throw new ArgumentException(SiteCodeLengthError);
        }

        Update(siteName, address, timeZone);
        SiteCode = siteCode;
        IsActive = isActive;
        Analyzers = [];
        //Default settings for sites
        Frequency = 8;
        WorkingDays =
        [
            Commons.WorkingDays.Monday, Commons.WorkingDays.Tuesday, Commons.WorkingDays.Wednesday,
            Commons.WorkingDays.Thursday, Commons.WorkingDays.Friday
        ];
        NotificationType = NotificationTypes.Email;
        WorkingTime = "08:00";
    }

    public void Update(string siteName, string address, string timeZone)
    {
        if (siteName.Length < MinSiteNameLength || siteName.Length > MaxSiteNameLength)
        {
            throw new ArgumentException(SiteNameLengthError);
        }

        SiteName = siteName;

        TimeZone = timeZone;
        Address = address;
    }

    public void AddAnalyzer(Analyzer analyzer)
    {
        if (Analyzers.Count >= MaxAnalyzer)
        {
            throw new InvalidOperationException(SiteFullError);
        }

        Analyzers.Add(analyzer);
    }
}