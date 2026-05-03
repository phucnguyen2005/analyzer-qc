using AnalyzerQC.Application;
using AnalyzerQC.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

[ApiController]
[Route("api/assay-limits")]
public class AssayLimitController : ControllerBase
{
    private readonly IAssayLimitService _assayLimitService;

    public AssayLimitController(IAssayLimitService assayLimitService)
    {
        _assayLimitService = assayLimitService;
    }

    [HttpGet]
    [Route("list")]
    public async Task<List<AssayLimitDto>> GetListAssayLimits()
    {
        return await _assayLimitService.GetListAssayLimits();
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<AssayLimitDetailDto?> GetAssayLimitById([FromRoute] Guid id)
    {
        return await _assayLimitService.GetAssayLimitsById(id);
    }
    
    [Authorize]
    [HttpPost]
    [Consumes("multipart/form-data")]
    [Route("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadDto upload, CancellationToken cancellationToken)
    {
        var file = upload.File;
        await using var stream = file.OpenReadStream();
        var result = await _assayLimitService.ReadFileAsync(stream, file.FileName);
        if(result)
            return Ok();
        return BadRequest();
    }
    
    public class UploadDto
    {
        public IFormFile File { get; set; }
    }
}