using AnalyzerQC.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AnalyzerQC.Application;

public interface IModelGroupService
{
    Task<List<ModelGroup>> GetModelGroup(string? modelGroupCode);
    Task<ModelGroup?> GetModelGroupById(int id);
    Task<bool> AddModelGroup(CreateModelGroupDto modelGroup);
}

public class ModelGroupService : IModelGroupService
{
    private readonly IAppDbContext _dbContext;

    public ModelGroupService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ModelGroup>> GetModelGroup(string? modelGroupCode)
    {
        var data = _dbContext.ModelGroups;

        if (!string.IsNullOrEmpty(modelGroupCode))
        {
            return await data.Where(modelGroup => modelGroup.ModelGroupCode == modelGroupCode).ToListAsync();
        }

        return await data.ToListAsync();
    }

    public async Task<ModelGroup?> GetModelGroupById(int id)
    {
        var modelGroup = await _dbContext.ModelGroups.FirstOrDefaultAsync(model => model.Id == id);
        return modelGroup;
    }

    public async Task<bool> AddModelGroup(CreateModelGroupDto modelGroup)
    {
        await _dbContext.ModelGroups.AddAsync(new ModelGroup(modelGroup.ModelGroupCode, modelGroup.ModelGroupName));
        await _dbContext.SaveChangesAsync();
        return true;
    }
}