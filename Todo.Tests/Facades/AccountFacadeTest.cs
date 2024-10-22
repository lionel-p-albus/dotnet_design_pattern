using Todo.Shared.Constants;
using Todo.Shared.Facades.Interface;
using Xunit;

namespace Todo.Tests.Facades;

public class AccountFacadeTest
{
    private readonly Dictionary<string, string> _smartMeterToPricePlanAccounts;
    private readonly IAccountFacade _facade;

    public AccountFacadeTest()
    {
        _smartMeterToPricePlanAccounts = Data.SmartMeterToPricePlanAccounts;
        _facade = new AccountFacade(_smartMeterToPricePlanAccounts);
    }
    
    [Fact]
    public void GivenTheSmartMeterIdReturnsThePricePlanId()
    {
        var result = _facade.GetPricePlanIdForSmartMeterId(Constant.SmartMeter.SMART_METER_ZERO);
        Assert.Equal(Constant.PricePlan.MOST_EVIL_PRICE_PLAN_ID, result);
    }
        
    [Fact]
    public void GivenAnUnknownSmartMeterIdReturnsNull()
    {
        var result = _facade.GetPricePlanIdForSmartMeterId("non-existent");
        Assert.Null(result);
    }
}