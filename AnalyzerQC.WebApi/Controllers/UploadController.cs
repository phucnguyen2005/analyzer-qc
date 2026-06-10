using AnalyzerQC.Application;
using AnalyzerQC.Application.Dtos;
using AnalyzerQC.Commons;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

[ApiController]
[Route("api/qc-uploads")]
public class UploadController: ControllerBase{
    private readonly IQcUploadService _qcUploadService;

    public UploadController(IQcUploadService qcUploadService){
        _qcUploadService = qcUploadService;
    }
    
    
    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadFile(List<IFormFile> file){
        try
        {
            foreach (var formFile in file)
            {
                await _qcUploadService.UploadFileAsync(formFile.OpenReadStream(), formFile.FileName,
                    formFile.ContentType);
            }

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    

    [HttpGet]
    public async Task<IActionResult> FilterFiles([FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate, [FromQuery] Guid analyzerId, [FromQuery] Guid siteId, 
        [FromQuery] string type, [FromQuery] string status)
    {
        return await _qcUploadService.FilterFiles(fromDate, toDate, analyzerId, siteId, type, status) is { } result
            ? Ok(result)
            : NotFound();
    }
    
    [HttpPost]
    [Route("download")]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        var result = await _qcUploadService.DownloadFileAsync(fileName);
        return File(result.Stream, result.ContentType, result.FileName);
    }
}