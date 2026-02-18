using AnalyzerQC.Application;
using AnalyzerQC.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ModelController : ControllerBase
{
    
    private readonly IModelService _modelService;
    public ModelController(IModelService modelService)
    {
        _modelService = modelService;
    }
    [HttpGet] // http methods
    public async Task<List<Model>> GetModel([FromQuery] string? modelCode) // method of ModelController
    {
        return await _modelService.GetModel(modelCode);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<Model?> GetModelById(int id)
    {
        return await _modelService.GetModelById(id);
    }

    [HttpPost]
    public async Task<IActionResult> AddModel([FromBody] CreateModelDto model)
    {
        var result = await _modelService.AddModel(model);
        if (!result) return BadRequest("Failed to add model");
        return Ok();
    }
}