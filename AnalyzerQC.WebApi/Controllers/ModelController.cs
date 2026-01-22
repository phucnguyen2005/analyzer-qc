using AnalyzerQC.WebApi.Database;
using AnalyzerQC.WebApi.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ModelController : ControllerBase
{
    
    private readonly AppDbContext _dbContext;
    public ModelController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet] // http methods
    public List<Model> GetModel([FromQuery] string? modelCode) // method of ModelController
    {
        var data = _dbContext.Models;

        if (!string.IsNullOrEmpty(modelCode))
        {
            return data.Where(model => model.ModelCode==modelCode).ToList();
        }

        return data.ToList();
    }

    [HttpGet]
    [Route("{id}")]
    public Model? GetModelById(int id)
    {
        var model=_dbContext.Models.FirstOrDefault(model => model.Id==id);
        return model;
    }

    [HttpPost]
    public IActionResult AddModel([FromBody] CreateModelDto model)
    {
        var modelGroup = _dbContext.ModelGroups.FirstOrDefault(m => m.Id == model.ModelGroupId); // LINQ
        if (modelGroup == null) return NotFound("ModelGroupCode not found");
        _dbContext.Models.Add(new Model(model.ModelCode, model.ModelName, modelGroup.Id));
        _dbContext.SaveChanges();
        return Ok();
    }
}