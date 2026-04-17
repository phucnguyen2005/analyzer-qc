namespace AnalyzerQC.Application.Dtos;

public class UpdateLotDto
{
    public string LotCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }
}