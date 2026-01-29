using AnalyzerQC.WebApi.Database;
using AnalyzerQC.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnalyzerQC.WebApi.Controllers;
//TODO: update route and apply async where applicable
[ApiController]
[Route("analyzers")]
public class AnalyzerController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public AnalyzerController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Analyzer>> GetAnalyzers([FromQuery] string? sitecode)
    {
        DbSet<Analyzer> data = _dbContext.Analyzers;

        if (!string.IsNullOrEmpty(sitecode))
        {
            return await data.Where(analyzer => analyzer.AssignedSite.SiteCode == sitecode).ToListAsync();
        }

        return await data.ToListAsync();
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<Analyzer?> GetAnalyzerById([FromRoute] Guid id)
    {
        var data = _dbContext.Analyzers;
        return await data.SingleOrDefaultAsync(analyzer => analyzer.Id == id);
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteAnalyzer(Guid id)
    {
        var analyzer = _dbContext.Analyzers.SingleOrDefault(analyzer => analyzer.Id == id);
        
        if (analyzer == null)
        {
            return Ok("Analyzer not found");
        }

        _dbContext.Analyzers.Remove(analyzer);
        return Ok();
    }


    [HttpPost] // HTTP POST
    public IActionResult AddAnalyzer([FromBody] CreateAnalyzerDto analyzer)
    {
        var model = _dbContext.Models.FirstOrDefault(m => m.ModelCode == analyzer.ModelCode); // LINQ
        if (model == null) return NotFound("ModelCode not found");

        // kiem tra site


        var site = _dbContext.Sites.FirstOrDefault(s => s.SiteCode == analyzer.SiteCode);
        if (site == null) return NotFound("SiteCode not found");

        var newAnalyzer = new Analyzer(model, site, analyzer.SerialNumber, analyzer.Status);
        _dbContext.Analyzers.Add(newAnalyzer);
        _dbContext.SaveChanges();
        return Ok();
        // return 200 OK
        // return 400 Bad Request
        // return 404 Not Found
        // return 500 Internal Server Error
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAnalyzer([FromBody] UpdateAnalyzerDto analyzer)
    {
        var existingAnalyzer = await _dbContext.Analyzers
            .FirstOrDefaultAsync(a => a.Id == analyzer.Id);
        if (existingAnalyzer == null) return NotFound("Id not found");
        existingAnalyzer.Status = analyzer.Status;
        existingAnalyzer.SerialNumber = analyzer.SerialNumber;
        _dbContext.SaveChanges ();
        return Ok();
    }
}