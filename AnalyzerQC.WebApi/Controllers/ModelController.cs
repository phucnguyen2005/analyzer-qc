using AnalyzerQC.WebApi.Database;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ModelController
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
    
}