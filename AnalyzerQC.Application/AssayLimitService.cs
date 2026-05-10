using AnalyzerQC.Application.Dtos;
using AnalyzerQC.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnalyzerQC.Application;

public interface IAssayLimitService
{
    Task<List<AssayLimitDto>> GetListAssayLimits();
    Task<AssayLimitDetailDto?> GetAssayLimitsById(Guid id);
    Task<bool> ReadFileAsync(Stream stream, string fileName);
}

public class AssayLimitService : IAssayLimitService
{
    private readonly IAppDbContext _dbContext;
    private readonly ILogger<AssayLimitService> _logger;

    public AssayLimitService(IAppDbContext dbContext, ILogger<AssayLimitService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }


    public async Task<List<AssayLimitDto>> GetListAssayLimits()
    {
        var data = _dbContext.AssayLimits;

        return await data.Select(assayLimit => new AssayLimitDto
        {
            LotId = assayLimit.LotId,
            ReagentId = assayLimit.ReagentId,
        }).ToListAsync();
    }

    public async Task<AssayLimitDetailDto?> GetAssayLimitsById(Guid id)
    {
        var data = _dbContext.AssayLimits.AsQueryable()
            .Include(assayLimit => assayLimit.AssayLimitParameters)
            .ThenInclude(assayLimitParameter => assayLimitParameter.Parameter)
            .Where(assayLimit => assayLimit.Id == id);
        return await data.Select(assayLimit => new AssayLimitDetailDto
        {
            LotId = assayLimit.LotId,
            ReagentId = assayLimit.ReagentId,
            Level = assayLimit.Level,
            AssayLimitParameters = assayLimit.AssayLimitParameters.Select(assayLimitParameter =>
                new AssayLimitParameterDto
                {
                    ParameterId = assayLimitParameter.ParameterId,
                    AssayLimitId = assayLimitParameter.AssayLimitId,
                    Target = assayLimitParameter.Target,
                    LowerLimit = assayLimitParameter.LowerLimit,
                    UpperLimit = assayLimitParameter.UpperLimit,
                    ParameterUnit = assayLimitParameter.ParameterUnit
                }).ToList()
        }).FirstOrDefaultAsync();
    }


    public async Task<bool> ReadFileAsync(Stream stream, string fileName)
    {
        var assayLimit = await GetAssayLimitFromFile(fileName);
        if (assayLimit == null) return false;

        var assayLimitParameters = await BuildAssayLimitParameters(stream, assayLimit.Id);
        if (assayLimitParameters.Count == 0) return false;
        assayLimit.AssayLimitParameters = assayLimitParameters;

        await _dbContext.AssayLimits.AddAsync(assayLimit);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    private async Task<List<AssayLimitParameter>> BuildAssayLimitParameters(Stream stream, Guid assayLimitId)
    {
        var assayLimitParameters = new List<AssayLimitParameter>();
        var parameters = await _dbContext.Parameters.ToListAsync();
        using var reader = new StreamReader(stream);

        await reader.ReadLineAsync();
        while (await reader.ReadLineAsync() is { } line)
        {
            var lineParts = line.Split(',');
            var parameter = parameters.FirstOrDefault(p => p.ParameterCode == lineParts[0]);
            if (parameter == null)
            {
                _logger.LogWarning("Parameter code {ParameterCode} not found, skipping line", lineParts[0]);
                continue;
            }

            var unitCode = lineParts[1];
            var unit = parameter.ParameterUnits.FirstOrDefault(u => u.UnitCode == unitCode);
            if (unit == null)
            {
                _logger.LogWarning("Unit code {UnitCode} not found for parameter {ParameterCode}, skipping line",
                    unitCode, lineParts[0]);
                continue;
            }

            var lowerLimit = float.Parse(lineParts[2]);
            var target = float.Parse(lineParts[3]);
            var upperLimit = float.Parse(lineParts[4]);

            assayLimitParameters.Add(new AssayLimitParameter(
                target,
                lowerLimit,
                upperLimit,
                parameter.Id,
                assayLimitId,
                unit));
        }

        _logger.LogInformation($"Reading {assayLimitParameters.Count} assay limit parameters");
        return assayLimitParameters;
    }

    private async Task<AssayLimit?> GetAssayLimitFromFile(string fileName)
    {
        string[] parts = fileName.Replace(".csv", string.Empty).Split('_');
        var reagent = _dbContext.Reagents.FirstOrDefault(ra => ra.ReagentCode == parts[0]);
        if (reagent == null)
            return null;
        var startDateParseResult = DateTime.TryParse(parts[3], out var startDate);
        var expiryDateParseResult = DateTime.TryParse(parts[4], out var expiryDate);
        if (!startDateParseResult || !expiryDateParseResult)
            return null;
        var lot = _dbContext.Lots.FirstOrDefault(l => l.LotCode == parts[2]);

        if (lot == null)
        {
            lot = new Lot(parts[2], startDate, expiryDate, true);
            await _dbContext.Lots.AddAsync(lot);
        }

        if (!Level.TryParse(parts[1], out Level level))
            return null;

        var assayLimit = new AssayLimit(lot.Id, reagent.Id, level);
        return assayLimit;
    }
}