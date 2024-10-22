using Microsoft.AspNetCore.Mvc;
using Todo.Shared.Constants;
using Todo.Shared.Facades.Interface;
using Todo.Shared.Models;

namespace Todo.Controllers;

[ApiController]
[Route("meter-reading")]
public class MeterReadingController : ControllerBase
{
    private readonly ILogger<MeterReadingController> _logger;
    private readonly IMeterReadingFacade _facade;
    
    public MeterReadingController(ILogger<MeterReadingController> logger, IMeterReadingFacade facade)
    {
        _logger = logger;
        _facade = facade;
    }
    
    [HttpPost ("store")]
    public async Task<IActionResult> Post([FromBody] StandardRequest<MeterReadings> req)
    {
        _logger.LogInformation("Post meterreading/store {Request}", req);
        
        var resp = _facade.StoreReadings(req.Request);
        
        if (resp.Message == Constant.Message.SUCCESS) return StatusCode(201, resp);
        return BadRequest(resp);
    }
    
    [HttpGet("read/{smartMeterId}")]
    public async Task<IActionResult> GetReading(string smartMeterId) {
        _logger.LogInformation("Post meterreading/read/{smartMeterId}", smartMeterId);
        
        var resp = _facade.GetReadings(smartMeterId);
        return Ok(resp);
    }
}