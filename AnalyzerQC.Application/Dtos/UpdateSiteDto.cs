using System.ComponentModel.DataAnnotations;

namespace AnalyzerQC.Application.Dtos;

public class UpdateSiteDto
{
    public Guid Id { get; set; }

    [Required]
    [Range(8, 100, ErrorMessage = "Site name length is between 8 and 100")]
    public string? SiteName { get; set; }

    [Required]
    [StringLength(8, ErrorMessage = "Site code must be 8 characters")]
    public string? SiteCode { get; set; }


    public string? Address { get; set; }
    public string? TimeZone { get; set; }
    public bool IsActive { get; set; }
}