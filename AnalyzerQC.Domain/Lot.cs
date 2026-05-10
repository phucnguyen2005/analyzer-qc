using AnalyzerQC.Commons;

namespace AnalyzerQC;

public class Lot : FullAuditedEntity<Guid>
{
    public string LotCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }
    public List<Reagent> Reagents { get; set; } = [];
    public List<AssayLimit> AssayLimits { get; set; } = [];

    private Lot()
    {
    }

    public Lot(string lotCode, DateTime startDate, DateTime expiryDate, bool isActive)
    {
        if (lotCode.Length < 3 || lotCode.Length > 20)
        {
            throw new ArgumentException("Lot code must be between 3 and 20 characters long");
        }

        LotCode = lotCode;
        if (startDate > expiryDate)
        {
            throw new ArgumentException("Start date cannot be later than expiry date");
        }

        StartDate = startDate;
        ExpiryDate = expiryDate;
        IsActive = isActive;
    }
}