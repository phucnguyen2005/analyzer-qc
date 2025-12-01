using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ModelController
{
    [HttpGet] // http methods
    public List<Model> GetModel([FromQuery] string? modelCode) // method of ModelController
    {
        var data = Repositories.Models;

        if (!string.IsNullOrEmpty(modelCode))
        {
            data = data.Where(model => model.ModelCode==modelCode).ToList();
        }

        return data;
    }

    [HttpGet]
    [Route("{id}")]
    public Model? GetModelById(int id)
    {
        var model=Repositories.Models.FirstOrDefault(model => model.Id==id);
        return model;
    }
    
}