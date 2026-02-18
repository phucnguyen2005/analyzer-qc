using AnalyzerQC.Application;
using AnalyzerQC.Application.Dtos;

using AnalyzerQC.WebApi.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnalyzerQC.WebApi.Controllers;



[ApiController]
[Route("[controller]")]
public class SiteController : ControllerBase
{
    private readonly ISiteService _siteService;

    public SiteController(ISiteService siteService)
    {
        _siteService = siteService;
    }
    /*[Authorize]*/
    [HttpGet]
    public async Task<List<SiteDto>> GetSitesBySiteId([FromQuery] Guid? siteId)
    {
        return await _siteService.GetSitesBySiteId(siteId);
    }


    [HttpDelete]
    [Route("{sitecode}")]
    public async Task<IActionResult> DeleteSite(string sitecode)
    {
        var result = await _siteService.DeleteSite(sitecode);
        if (!result) return NotFound("Id not found");
        return Ok();
    }

    /*[Authorize]*/
    [HttpPost] // HTTP POST
    public async Task<IActionResult> AddSite([FromBody] CreateSiteDto site)
    {
        var result = await _siteService.AddSite(site);
        if (!result) return BadRequest("Failed to add site");
         return Ok();
        // return 200 OK
        // return 400 Bad Request
        // return 404 Not Found
        // return 500 Internal Server Error
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSite([FromBody] UpdateSiteDto site)
    {
        var result = await _siteService.UpdateSite(site);
        if (!result) return BadRequest("Failed to update site");
        return Ok();
    }
    
    [HttpGet]
    [Route("{siteId}/sitesettings")]
    public async Task<SiteSettingsDto?> GetSiteSettingsBySiteId([FromRoute] Guid siteId)
    {
        return await _siteService.GetSiteSettingsBySiteId(siteId);
    }

    [HttpPut]
    [Route("{siteId}/sitesettings")]
    public async Task<IActionResult> UpdateSiteSettings([FromBody] UpdateSiteSettingsDto site)
    {
        var result = await _siteService.UpdateSiteSettings(site);
        if (!result) return BadRequest("Failed to update site settings");
        return Ok();
    }
}
