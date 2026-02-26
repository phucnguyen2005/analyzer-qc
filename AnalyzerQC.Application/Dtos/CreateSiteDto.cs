using System.ComponentModel.DataAnnotations;

namespace AnalyzerQC.Application.Dtos;

public class CreateSiteDto
{
    [Required]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Site name length is between 8 and 100")]
    public string SiteName { get; set; } = null!;

    [Required]
    [StringLength(8, ErrorMessage = "Site code must be 8 characters")]
    public string SiteCode { get; set; } = null!;

    public string Address { get; set; } = null!;
    public string TimeZone { get; set; } = null!;
    public bool IsActive { get; set; }
}