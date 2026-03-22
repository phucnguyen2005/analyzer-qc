using AnalyzerQC.Commons;

namespace AnalyzerQC;

public class Lot:FullAuditedEntity<int>
{
    public string LotCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }
    public List<Reagent> Reagents { get; set; } = [];
    public List<AssayLimit> AssayLimits { get; set; } = [];

    public Lot(string lotCode, DateTime startDate, DateTime expiryDate, bool isActive)
    {
        LotCode = lotCode;
        StartDate = startDate;
        ExpiryDate = expiryDate;
        IsActive = isActive;
    }
}