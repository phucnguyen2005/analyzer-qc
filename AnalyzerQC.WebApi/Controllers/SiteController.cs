using AnalyzerQC.WebApi.Database;
using AnalyzerQC.WebApi.Dtos;
using AnalyzerQC.WebApi.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    /*[Authorize]*/
    [HttpGet]
    public List<SiteDto> GetSitesBySiteCode([FromQuery] string? sitecode)
    {
        var data = _dbContext.Sites.AsQueryable()
            .Include(site => site.Analyzers)
            .ThenInclude(analyzer => analyzer.Model);
            
        
        return data.Select(site => new SiteDto
        {
            SiteCode = site.SiteCode,
            SiteName = site.SiteName,
            TimeZone = site.TimeZone,
            Analyzers = site.Analyzers
                .Select(analyzer=> new AnalyzerDto()
                {
                    SerialNumber = analyzer.SerialNumber,
                    ModelName = analyzer.Model.ModelName,
                }).ToList()
        }).ToList();
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
        _dbContext.SaveChanges();
        return Ok();
    }

    /*[Authorize]*/
    [HttpPost] // HTTP POST
    public IActionResult AddSite([FromBody] CreateSiteDto site)
    {
        _dbContext.Sites.Add(new Site(site.SiteName, site.SiteCode, site.Address, site.TimeZone, site.IsActive));
        _dbContext.SaveChanges();
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
        _dbContext.SaveChanges();
        return Ok();
    }
    
    [HttpGet]
    [Route("{siteId}/sitesettings")]
    public async Task<SiteSettingsDto?> GetSiteSettingsBySiteId([FromRoute] Guid siteId)
    {
        
        var data = await _dbContext.Sites
            .Include(site => site.Analyzers)
            .FirstOrDefaultAsync(s => s.Id == siteId);  
        if(data == null) return null;
        var results = new SiteSettingsDto
        {
            Frequency = data.Frequency,
            NotificationType = data.NotificationType,
            WorkingTime = data.WorkingTime, //TODO: should display as standard
            WorkingDays = data.WorkingDays
           
        };
        return results;
    }

    [HttpPut]
    [Route("{siteId}/sitesettings")]
    public IActionResult UpdateSiteSettings([FromBody] UpdateSiteSettingsDto site)//TODO: async
    {
        var data = _dbContext.Sites.FirstOrDefault(s => s.Id == site.Id);
        if (data == null) return NotFound("Id not found");
        data.Frequency = site.frequency;
        data.NotificationType = site.NotificationType;
        data.WorkingDays = site.WorkingDays; 
        data.WorkingTime = site.WorkingTime;
        _dbContext.SaveChanges();
        return Ok();
        
    }
}
