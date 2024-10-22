using Microsoft.AspNetCore.Mvc;
using Todo.Shared.Constants;
using Todo.Shared.Facades.Interface;

namespace Todo.Controllers;

[ApiController]
[Route("price-plan-comparator")]
public class PricePlanComparatorController : ControllerBase
{
    private readonly ILogger<PricePlanComparatorController> _logger;
    private readonly IPricePlanComparatorFacade _facade;

    public PricePlanComparatorController(ILogger<PricePlanComparatorController> logger, IPricePlanComparatorFacade facade)
    {
        _logger = logger;
        _facade = facade;
    }
    
    [HttpGet("compare-all/{smartMeterId}")]
    public async Task<IActionResult> GetCalculatedCostForEachPricePlan(string smartMeterId)
    {
        _logger.LogInformation("Get priceplancomparator/compare-all/{smartMeterId}", smartMeterId);
        
        var resp = _facade.CalculateCostForEachPricePlan(smartMeterId);
        if (resp.Message == Constant.Message.NO_DATA) return StatusCode(404, resp);
        
        return Ok(resp);
    }
    
    [HttpGet("recommend/{smartMeterId}")]
    public async Task<IActionResult> GetRecommendCheapestPricePlans(string smartMeterId, int? limit)
    {
        _logger.LogInformation("Get priceplancomparator/recommend/{smartMeterId}", smartMeterId);
        
        var resp = _facade.RecommendCheapestPricePlans(smartMeterId, limit);
        if (resp.Message != Constant.Message.SUCCESS) return StatusCode(404, resp);
        
        return Ok(resp);
    }

}