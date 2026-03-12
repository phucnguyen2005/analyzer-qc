using AnalyzerQC.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AnalyzerQC.Application;

public interface IAnalyzerService
{
    Task<List<Analyzer>> GetAnalyzers(string? sitecode);
    Task<Analyzer?> GetAnalyzerById(Guid id);
    Task<bool> DeleteAnalyzer(Guid id);
    Task<bool> AddAnalyzer(CreateAnalyzerDto analyzer);
    Task<bool> UpdateAnalyzer(UpdateAnalyzerDto analyzer);
}

public class AnalyzerService : IAnalyzerService
{
    private readonly IAppDbContext _dbContext;

    public AnalyzerService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Analyzer>> GetAnalyzers(string? sitecode)
    {
        var data = _dbContext.Analyzers;

        if (!string.IsNullOrEmpty(sitecode))
        {
            return await data.Where(analyzer => analyzer.AssignedSite.SiteCode == sitecode).ToListAsync();
        }

        return await data.ToListAsync();
    }


    public async Task<Analyzer?> GetAnalyzerById(Guid id)
    {
        var data = _dbContext.Analyzers;
        return await data.SingleOrDefaultAsync(analyzer => analyzer.Id == id);
    }

    public async Task<bool> DeleteAnalyzer(Guid id)
    {
        var analyzer = await _dbContext.Analyzers.SingleOrDefaultAsync(analyzer => analyzer.Id == id);

        if (analyzer == null)
        {
            return false;
        }

        _dbContext.Analyzers.Remove(analyzer);
        return true;
    }


    public async Task<bool> AddAnalyzer(CreateAnalyzerDto analyzer)
    {
        var model = await _dbContext.Models.FirstOrDefaultAsync(m => m.ModelCode == analyzer.ModelCode); // LINQ
        if (model == null) return false;

        // kiem tra site


        var site = _dbContext.Sites.FirstOrDefault(s => s.SiteCode == analyzer.SiteCode);
        if (site == null) return false;

        var newAnalyzer = new Analyzer(model, site, analyzer.SerialNumber, analyzer.Status);
        await _dbContext.Analyzers.AddAsync(newAnalyzer);
        await _dbContext.SaveChangesAsync();
        return true;
        // return 200 OK
        // return 400 Bad Request
        // return 404 Not Found
        // return 500 Internal Server Error
    }

    public async Task<bool> UpdateAnalyzer(UpdateAnalyzerDto analyzer)
    {
        var existingAnalyzer = await _dbContext.Analyzers
            .FirstOrDefaultAsync(a => a.Id == analyzer.Id);
        if (existingAnalyzer == null) return false;
        existingAnalyzer.Status = analyzer.Status;
        existingAnalyzer.SerialNumber = analyzer.SerialNumber;
        await _dbContext.SaveChangesAsync();
        return true;
    }
}