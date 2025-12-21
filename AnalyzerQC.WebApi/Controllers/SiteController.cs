using AnalyzerQC.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

using AnalyzerQC;

[ApiController]
[Route("[controller]")]
public class SiteController : ControllerBase
{
    [HttpGet]
    public List<Site> GetSiteBySiteCode([FromQuery] string? sitecode)
    {
        var data = Repositories.Sites;

        if (!string.IsNullOrEmpty(sitecode))
        {
            data = data.Where(site => site.SiteCode == sitecode).ToList();
        }

        return data;
    }


    [HttpDelete]
    [Route("{sitecode}")]
    public IActionResult DeleteSite(string sitecode)
    {
        var site = Repositories.Sites.SingleOrDefault(site => site.SiteCode == sitecode);
        if (site == null)
        {
            return Ok("Site not found");
        }

        Repositories.Sites.Remove(site);
        return Ok();
    }


    [HttpPost] // HTTP POST
    public IActionResult AddSite([FromBody] CreateSiteDto site)
    {
        Repositories.Sites.Add(new Site(site.SiteName, site.SiteCode, site.Address, site.TimeZone, site.IsActive));

        return Ok();
        // return 200 OK
        // return 400 Bad Request
        // return 404 Not Found
        // return 500 Internal Server Error
    }

    [HttpPut]
    public IActionResult UpdateAnalyzer([FromBody] UpdateSiteDto site)
    {
        var siteName = Repositories.Sites.FirstOrDefault(m => m.SiteName == site.SiteName); // LINQ
        if (siteName == null) return NotFound("SiteName not found");

        var siteCode = Repositories.Sites.FirstOrDefault(s => s.SiteCode == site.SiteCode);
        if (siteCode == null) return NotFound("SiteCode not found");


        var existingSite = Repositories.Sites.Where(s => s.Id == site.Id ).FirstOrDefault();

        existingSite.IsActive = site.IsActive;

        return Ok();
    }
}