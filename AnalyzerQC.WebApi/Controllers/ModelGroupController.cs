using AnalyzerQC.Application;
using AnalyzerQC.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

[ApiController]
[Route("api/model-groups")]
public class ModelGroupController : ControllerBase
{
    private readonly IModelGroupService _modelGroupService;

    public ModelGroupController(IModelGroupService modelGroupService)
    {
        _modelGroupService = modelGroupService;
    }

    [HttpGet] // http methods
    public async Task<List<ModelGroup>> GetModelGroup([FromQuery] string? modelGroupCode) // method of ModelController
    {
        return await _modelGroupService.GetModelGroup(modelGroupCode);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ModelGroup?> GetModelGroupById(int id)
    {
        return await _modelGroupService.GetModelGroupById(id);
    }

    [HttpPost]
    public async Task<IActionResult> AddModelGroup([FromBody] CreateModelGroupDto modelGroup)
    {
        var result = await _modelGroupService.AddModelGroup(modelGroup);
        if (!result) return BadRequest("Failed to add model group");
        return Ok();
    }
}