using AnalyzerQC.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AnalyzerQC.Application;

public interface ILotService
{
    Task<Lot?> GetLotByCode(string code);
    Task<List<LotDto>> GetLotList();

    Task<bool> UpdateLot(UpdateLotDto lot);
}

public class LotService : ILotService
{
    private readonly IAppDbContext _appDbContext;

    public LotService(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    //TODO: get by id or get by code, not both
    public async Task<Lot?> GetLotByCode(string code)
    {
        var data = _appDbContext.Lots;

        return await data.FirstOrDefaultAsync(l => l.LotCode == code);
    }

    public async Task<List<LotDto>> GetLotList()
    {
        var data = _appDbContext.Lots;
        return await data.Select(lot => new LotDto
        {
            Id = lot.Id,
            LotCode = lot.LotCode,
        }).ToListAsync();
    }

    public async Task<bool> UpdateLot(UpdateLotDto lot)
    {
        var existingLot = await _appDbContext.Lots.FirstOrDefaultAsync(l => l.LotCode == lot.LotCode);
        if (existingLot == null) return false;
        existingLot.StartDate = lot.StartDate;
        existingLot.ExpiryDate = lot.ExpiryDate;
        existingLot.IsActive = lot.IsActive;
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}