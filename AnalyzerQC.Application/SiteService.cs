using AnalyzerQC.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AnalyzerQC.Application;

public interface ISiteService
{
    Task<List<SiteDto>> GetSitesBySiteId(Guid? siteId);
    Task<bool> DeleteSite(string sitecode);
    Task<bool> AddSite(CreateSiteDto site);
    Task<bool> UpdateSite(UpdateSiteDto site);
    Task<SiteSettingsDto?> GetSiteSettingsBySiteId(Guid siteId);
    Task<bool> UpdateSiteSettings(UpdateSiteSettingsDto site);
}

public class SiteService : ISiteService
{
    private readonly IAppDbContext _dbContext;

    public SiteService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    /*[Authorize]*/


    public async Task<List<SiteDto>> GetSitesBySiteId(Guid? siteId)
    {
        var data = _dbContext.Sites.AsQueryable()
            .Include(site => site.Analyzers)
            .ThenInclude(analyzer => analyzer.Model);


        return await data.Select(site => new SiteDto
        {
            SiteId = site.Id,
            SiteCode = site.SiteCode,
            SiteName = site.SiteName,
            TimeZone = site.TimeZone,
            Analyzers = site.Analyzers
                .Select(analyzer => new AnalyzerDto()
                {
                    SerialNumber = analyzer.SerialNumber,
                    ModelName = analyzer.Model.ModelName,
                }).ToList()
        }).ToListAsync();
    }


    public async Task<bool> DeleteSite(string sitecode)
    {
        var site = await _dbContext.Sites.SingleOrDefaultAsync(site => site.SiteCode == sitecode);
        if (site == null)
        {
            return false;
        }

        _dbContext.Sites.Remove(site);
        await _dbContext.SaveChangesAsync();
        return true;
    }


    public async Task<bool> AddSite(CreateSiteDto site)
    {
        await _dbContext.Sites.AddAsync(new Site(site.SiteName, site.SiteCode, site.Address, site.TimeZone,
            site.IsActive));
        await _dbContext.SaveChangesAsync();
        return true;
        // return 200 OK
        // return 400 Bad Request
        // return 404 Not Found
        // return 500 Internal Server Error
    }


    public async Task<bool> UpdateSite(UpdateSiteDto site)
    {
        var existingSite = await _dbContext.Sites.FirstOrDefaultAsync(s => s.Id == site.Id);
        if (existingSite == null) return false;


        existingSite.IsActive = site.IsActive;
        await _dbContext.SaveChangesAsync();
        return true;
    }


    public async Task<SiteSettingsDto?> GetSiteSettingsBySiteId(Guid siteId)
    {
        var data = await _dbContext.Sites
            .Include(site => site.Analyzers)
            .FirstOrDefaultAsync(s => s.Id == siteId);
        if (data == null) return null;
        var results = new SiteSettingsDto
        {
            SiteId = siteId,
            Frequency = data.Frequency,
            NotificationType = data.NotificationType,
            WorkingTime = data.WorkingTime,
            WorkingDays = data.WorkingDays
        };
        return results;
    }

    public async Task<bool> UpdateSiteSettings(UpdateSiteSettingsDto site)
    {
        var data = await _dbContext.Sites.FirstOrDefaultAsync(s => s.Id == site.Id);
        if (data == null) return false;
        data.Frequency = site.frequency;
        data.NotificationType = site.NotificationType;
        data.WorkingDays = site.WorkingDays;
        data.WorkingTime = site.WorkingTime;
        await _dbContext.SaveChangesAsync();
        return true;
    }
}