using AnalyzerQC.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

[ApiController]
[Route("analyzers")]
public class AnalyzerController : ControllerBase
{
    [HttpGet]
    public List<Analyzer> GetAnalyzers([FromQuery] string? sitecode)
    {
        var data = Repositories.Analyzers;

        if (!string.IsNullOrEmpty(sitecode))
        {
            data = data.Where(analyzer => analyzer.AssignedSite.SiteCode == sitecode).ToList();
        }

        return data;
    }

    /*
    [HttpGet]
    public List<Analyzer> GetAnalyzer()
    {
        return Repositories.Analyzers;
    }
*/
    [HttpGet]
    [Route("{id}")]
    public Analyzer? GetAnalyzerById(int id)
    {
        
        return Repositories.Analyzers.Where(analyzer => analyzer.Id == id).SingleOrDefault();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteAnalyzer(int id)
    {
        var analyzer = Repositories.Analyzers.Where(analyzer => analyzer.Id == id).SingleOrDefault();
        if (analyzer == null)
        {
            return Ok("Analyzer not found");
        }

        Repositories.Analyzers.Remove(analyzer);
        return Ok();
    }


    [HttpPost] // HTTP POST
    public IActionResult AddAnalyzer([FromBody] CreateAnalyzerDto analyzer)
    {
        var model = Repositories.Models.FirstOrDefault(m => m.ModelCode == analyzer.ModelCode); // LINQ
        if (model == null) return NotFound("ModelCode not found");

        // kiem tra site


        var site = Repositories.Sites.FirstOrDefault(s => s.SiteCode == analyzer.SiteCode);
        if (site == null) return NotFound("SiteCode not found");

        var newAnalyzer = new Analyzer(model, site, analyzer.SerialNumber, analyzer.Status);
        Repositories.Analyzers.Add(newAnalyzer);

        return Ok();
        // return 200 OK
        // return 400 Bad Request
        // return 404 Not Found
        // return 500 Internal Server Error
    }

    [HttpPut]
   
    public IActionResult UpdateAnalyzer([FromBody] UpdateAnalyzerDto analyzer)
    {
        var existingAnalyzer = Repositories.Analyzers
            .FirstOrDefault(a => a.Id == analyzer.Id);
        
        var model = Repositories.Models.FirstOrDefault(m => m.ModelCode == analyzer.ModelCode); // LINQ
        if (model == null) return NotFound("ModelCode not found");

        var site = Repositories.Sites.FirstOrDefault(s => s.SiteCode == analyzer.SiteCode);
        if (site == null) return NotFound("SiteCode not found");

        existingAnalyzer.Status = analyzer.Status;
        existingAnalyzer.SerialNumber = analyzer.SerialNumber;
        
        return Ok();
    }
}