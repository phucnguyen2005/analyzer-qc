using AnalyzerQC.Application;
using AnalyzerQC.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

[ApiController]
[Route("api/lots")]
public class LotController : ControllerBase
{
    private readonly ILotService _lotService;

    public LotController(ILotService lotService)
    {
        _lotService = lotService;
    }

    [HttpGet]
    [Route("{lotCode}")]
    public async Task<Lot?> GetLotByCode([FromRoute] string lotCode)
    {
        return await _lotService.GetLotByCode(lotCode);
    }

    [HttpGet]
    [Route("list")]
    public async Task<List<LotDto>> GetLotList()
    {
        return await _lotService.GetLotList();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateLot([FromBody] UpdateLotDto lot)
    {
        var result = await _lotService.UpdateLot(lot);
        if (result) return Ok();
        return BadRequest();
    }
}