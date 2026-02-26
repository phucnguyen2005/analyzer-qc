using AnalyzerQC.Application;
using AnalyzerQC.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

//TODO: update route where applicable
[ApiController]
[Route("api/analyzers")]
public class AnalyzerController : ControllerBase
{
    private readonly IAnalyzerService _analyzerService;

    public AnalyzerController(IAnalyzerService analyzerService)
    {
        _analyzerService = analyzerService;
    }

    [HttpGet]
    public async Task<List<Analyzer>> GetAnalyzers(string? sitecode)
    {
        return await _analyzerService.GetAnalyzers(sitecode);
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<Analyzer?> GetAnalyzerById([FromRoute] Guid id)
    {
        return await _analyzerService.GetAnalyzerById(id);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAnalyzer(Guid id)
    {
        var result = await _analyzerService.DeleteAnalyzer(id);
        if (!result) return NotFound("Id not found");
        return Ok();
    }


    [HttpPost] // HTTP POST
    public async Task<IActionResult> AddAnalyzer([FromBody] CreateAnalyzerDto analyzer)
    {
        var result = await _analyzerService.AddAnalyzer(analyzer);
        if (!result) return BadRequest("Failed to add analyzer");
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAnalyzer([FromBody] UpdateAnalyzerDto analyzer)
    {
        var result = await _analyzerService.UpdateAnalyzer(analyzer);
        if (!result) return BadRequest("Failed to update analyzer");
        return Ok();
    }
}