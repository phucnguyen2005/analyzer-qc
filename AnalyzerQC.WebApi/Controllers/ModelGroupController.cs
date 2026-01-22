using AnalyzerQC.WebApi.Database;
using AnalyzerQC.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class ModelGroupController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    public ModelGroupController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet] // http methods
    public List<ModelGroup> GetModelGroup([FromQuery] string? modelGroupCode) // method of ModelController
    {
        var data = _dbContext.ModelGroups;

        if (!string.IsNullOrEmpty(modelGroupCode))
        {
            return data.Where(modelGroup => modelGroup.ModelGroupCode==modelGroupCode).ToList();
        }

        return data.ToList();
    }

    [HttpGet]
    [Route("{id}")]
    public ModelGroup? GetModelGroupById(int id)
    {
        var modelGroup=_dbContext.ModelGroups.FirstOrDefault(model => model.Id==id);
        return modelGroup;
    }

    [HttpPost]
    public IActionResult AddModelGroup([FromBody] CreateModelGroupDto modelGroup)
    {
        _dbContext.ModelGroups.Add(new ModelGroup(modelGroup.ModelGroupCode, modelGroup.ModelGroupName));
        _dbContext.SaveChanges();
        return Ok();
    }
}