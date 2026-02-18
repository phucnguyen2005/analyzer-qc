using AnalyzerQC.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnalyzerQC.Application;

public interface IModelService
{
    Task<List<Model>> GetModel(string? modelCode);
    Task<Model?> GetModelById(int id);
    Task<bool> AddModel(CreateModelDto model);
}

public class ModelService : IModelService
{
    private readonly IAppDbContext _dbContext;

    public ModelService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Model>> GetModel([FromQuery] string? modelCode) // method of ModelController
    {
        var data = _dbContext.Models;

        if (!string.IsNullOrEmpty(modelCode))
        {
            return await data.Where(model => model.ModelCode == modelCode).ToListAsync();
        }

        return await data.ToListAsync();
    }


    public async Task<Model?> GetModelById(int id)
    {
        var model = await _dbContext.Models.FirstOrDefaultAsync(model => model.Id == id);
        return model;
    }


    public async Task<bool> AddModel([FromBody] CreateModelDto model)
    {
        var modelGroup = await _dbContext.ModelGroups.FirstOrDefaultAsync(m => m.Id == model.ModelGroupId); // LINQ
        if (modelGroup == null) return false;
        await _dbContext.Models.AddAsync(new Model(model.ModelCode, model.ModelName, modelGroup.Id));
        await _dbContext.SaveChangesAsync();
        return true;
    }
}