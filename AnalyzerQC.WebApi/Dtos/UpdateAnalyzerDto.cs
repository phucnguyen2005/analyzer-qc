using System.ComponentModel.DataAnnotations;

namespace AnalyzerQC.WebApi.Dtos;

public class UpdateAnalyzerDto
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(8, ErrorMessage = "Model code must be 8 characters")]
    public string ModelCode { get; set; } = null!;
    
    [Required]
    [StringLength(8, ErrorMessage = "Site code must be 8 characters")]
    public string SiteCode { get; set; } = null!;
    
    [Required]
    [StringLength(8, ErrorMessage = "Serial number must be 8 characters")]
    public string SerialNumber { get; set; } = null!;
    public bool Status { get; set; }
}