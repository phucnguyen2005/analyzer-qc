using System.ComponentModel.DataAnnotations;

namespace AnalyzerQC.Application.Dtos;

public class CreateAnalyzerDto
{
    [Required]
    [StringLength(8, ErrorMessage = "Model code must be 8 characters")]
    public string ModelCode { get; set; } = null!;

    [Required]
    [StringLength(8, ErrorMessage = "Site code must be 8 characters")]
    public string SiteCode { get; set; } = null!;

    public bool Status { get; set; }

    [Required]
    [StringLength(8, ErrorMessage = "Serial number must be 8 characters")]
    public string SerialNumber { get; set; } = null!;
}