using AnalyzerQC.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AnalyzerQC.Application;

public interface IAssayLimitParameterService
{
    Task<List<AssayLimitParameterDto>> GetAssayLimitParameter(Guid assayLimitId, int parameterId);
    Task<bool> AddAssayLimitParameter(CreateAssayLimitParameterDto assayLimitParameter);
    Task<bool> UpdateAssayLimitParameter(UpdateAssayLimitParameterDto assayLimitParameter);
}
public class AssayLimitParameterService: IAssayLimitParameterService
{
    private readonly IAppDbContext _dbContext;
    public AssayLimitParameterService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<AssayLimitParameterDto>> GetAssayLimitParameter(Guid assayLimitId, int parameterId)
    {
        var data = _dbContext.AssayLimitParameters
            .Where(alp => alp.AssayLimitId == assayLimitId && alp.ParameterId == parameterId);
        return await data.Select(assayLimitParameter => new AssayLimitParameterDto
        {
            ParameterId = assayLimitParameter.ParameterId,
            AssayLimitId = assayLimitParameter.AssayLimitId,
            Target = assayLimitParameter.Target,
            LowerLimit = assayLimitParameter.LowerLimit,
            UpperLimit = assayLimitParameter.UpperLimit,
            ParameterUnit = assayLimitParameter.ParameterUnit
        }).ToListAsync();
    }

    public async Task<bool> AddAssayLimitParameter(CreateAssayLimitParameterDto assayLimitParameter)
    {
        var assayLimit =
            await _dbContext.AssayLimits.FirstOrDefaultAsync(al => al.Id == assayLimitParameter.AssayLimitId);
        if (assayLimit == null)
            return false;
        var parameter = await _dbContext.Parameters.FirstOrDefaultAsync(p => p.Id == assayLimitParameter.ParameterId);
        if (parameter == null)
            return false;
        var alp = new AssayLimitParameter(assayLimitParameter.Target,
            assayLimitParameter.LowerLimit,
            assayLimitParameter.UpperLimit,
            assayLimitParameter.ParameterId,
            assayLimitParameter.AssayLimitId,
            assayLimitParameter.ParameterUnit);
        await _dbContext.AssayLimitParameters.AddAsync(alp);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAssayLimitParameter(UpdateAssayLimitParameterDto assayLimitParameter)
    {
        var existingALP = await _dbContext.AssayLimitParameters.FirstOrDefaultAsync(alp =>
            alp.AssayLimitId == assayLimitParameter.AssayLimitId &&
            alp.ParameterId == assayLimitParameter.ParameterId);
        if (existingALP == null) return false;
        existingALP.Target = assayLimitParameter.Target;
        existingALP.LowerLimit = assayLimitParameter.LowerLimit;
        existingALP.UpperLimit = assayLimitParameter.UpperLimit;
        existingALP.ParameterUnit = assayLimitParameter.ParameterUnit;
        _dbContext.AssayLimitParameters.Update(existingALP);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}