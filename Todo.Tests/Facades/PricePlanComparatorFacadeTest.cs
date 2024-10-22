using Todo.Shared.Constants;
using Todo.Shared.Facades.Interface;
using Todo.Shared.Mappers;
using Todo.Shared.Mappers.Interface;
using Todo.Shared.Models;
using Todo.Shared.Utils;
using Xunit;

namespace Todo.Tests.Facades;

public class PricePlanComparatorFacadeTest
{
    private readonly List<PricePlan> _pricePlans;
    private readonly IAccountFacade _accountFacade;
    private readonly IMeterReadingFacade _meterReadingFacade;
    private readonly IModelMapper _modelMapper;
    private readonly IPricePlanComparatorFacade _facade;

    public PricePlanComparatorFacadeTest()
    {
        _pricePlans = Data.PricePlans;
        _accountFacade = new AccountFacade(Data.SmartMeterToPricePlanAccounts);
        _meterReadingFacade = new MeterReadingFacade(new MeterAssociatedReadings(SharedUtils.GenerateMeterElectricityReadings()));
        _modelMapper = new ModelMapper();
        
        _facade = new PricePlanComparatorFacade(_pricePlans, _accountFacade, _meterReadingFacade, _modelMapper);
    }
    
    [Fact]
    public void CalculateCostForEachPricePlanShouldReturnNoData()
    {
        var resp = _facade.CalculateCostForEachPricePlan("non-existent-id");
        Assert.Equal(Constant.Message.NO_DATA, resp.Message);
    }
    
    [Fact]
    public void CalculateCostForEachPricePlanShouldReturnSuccess()
    {
        var resp = _facade.CalculateCostForEachPricePlan(Constant.SmartMeter.SMART_METER_ZERO);
        Assert.Equal(Constant.Message.SUCCESS, resp.Message);
        Assert.Equal(Constant.PricePlan.MOST_EVIL_PRICE_PLAN_ID, resp.Result.PricePlanId);
    }
    
    [Fact]
    public void RecommendCheapestPricePlansShouldReturnSmartMeterNotFound()
    {
        var resp = _facade.RecommendCheapestPricePlans("non-existent-id", 1);
        Assert.Equal("Smart Meter ID (non-existent-id) not found", resp.Message);
    }
    
    [Fact]
    public void RecommendCheapestPricePlansShouldReturnSuccess()
    {
        var resp = _facade.RecommendCheapestPricePlans(Constant.SmartMeter.SMART_METER_ZERO, 1);
        Assert.Equal(Constant.Message.SUCCESS, resp.Message);
        Assert.Single(resp.Results);
    }
    
    [Fact]
    public void RecommendCheapestPricePlansShouldReturnSuccessWithNoLimit()
    {
        var resp = _facade.RecommendCheapestPricePlans(Constant.SmartMeter.SMART_METER_ZERO, null);
        Assert.Equal(Constant.Message.SUCCESS, resp.Message);
        Assert.Equal(3, resp.Results.Count);
    }
}