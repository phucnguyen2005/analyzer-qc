using AnalyzerQC.WebApi.Database;
using AnalyzerQC.WebApi.Dtos;
using AnalyzerQC.WebApi.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

using AnalyzerQC;

[ApiController]
[Route("[controller]")]
public class SiteController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public SiteController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [Authorize]
    [HttpGet]
    public List<Site> GetSiteBySiteCode([FromQuery] string? sitecode)
    {
        var data = _dbContext.Sites;

        if (!string.IsNullOrEmpty(sitecode))
        {
            return data.Where(site => site.SiteCode == sitecode).ToList();
        }

        return data.ToList();
    }


    [HttpDelete]
    [Route("{sitecode}")]
    public IActionResult DeleteSite(string sitecode)
    {
        var site = _dbContext.Sites.SingleOrDefault(site => site.SiteCode == sitecode);
        if (site == null)
        {
            return Ok("Site not found");
        }

        _dbContext.Sites.Remove(site);
        return Ok();
    }

    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    [HttpPost] // HTTP POST
    public IActionResult AddSite([FromBody] CreateSiteDto site)
    {
        _dbContext.Sites.Add(new Site(site.SiteName, site.SiteCode, site.Address, site.TimeZone, site.IsActive));

        return Ok();
        // return 200 OK
        // return 400 Bad Request
        // return 404 Not Found
        // return 500 Internal Server Error
    }

    [HttpPut]
    public IActionResult UpdateAnalyzer([FromBody] UpdateSiteDto site)
    {
        var existingSite = _dbContext.Sites.FirstOrDefault(s => s.Id == site.Id);
        if (existingSite == null) return NotFound("Id not found");


        existingSite.IsActive = site.IsActive;

        return Ok();
    }
}