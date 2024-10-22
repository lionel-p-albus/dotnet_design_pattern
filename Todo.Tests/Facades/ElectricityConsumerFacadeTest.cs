using Todo.Controllers;
using Todo.Shared.Constants;
using Todo.Shared.Facades.Interface;
using Todo.Shared.Models;
using Todo.Shared.Utils;
using Xunit;

namespace Todo.Tests.Facades;

public class ElectricityConsumerFacadeTest
{
    private readonly MeterAssociatedReadings _meterAssociatedReadings;
    private readonly IMeterReadingFacade _facade;
    private readonly MeterReadingController _controller;

    public ElectricityConsumerFacadeTest()
    {
        _meterAssociatedReadings = new MeterAssociatedReadings(SharedUtils.GenerateMeterElectricityReadings());
        _facade = new MeterReadingFacade(_meterAssociatedReadings);
        // _controller = new MeterReadingController(null, _facade);
    }
    
    [Fact]
    public void StoreReadingsShouldReturnFalse()
    {
        var req = new MeterReadings
        {
            SmartMeterId = "",
            ElectricityReadings = new List<ElectricityReading>
            {
                new ElectricityReading
                {
                    Reading = 100m,
                    Time = DateTime.Now
                }
            }
        };

        var resp = _facade.StoreReadings(req);
        Assert.False(resp.Result);
    }
    
    [Fact]
    public void StoreReadingsShouldReturnFalseWhenSmartMeterIdNotFund()
    {
        var req = new MeterReadings
        {
            SmartMeterId = "smart-meter-id",
            ElectricityReadings = new List<ElectricityReading>
            {
                new ElectricityReading
                {
                    Reading = 100m,
                    Time = DateTime.Now
                }
            }
        };

        var resp = _facade.StoreReadings(req);
        Assert.False(resp.Result);
        Assert.Equal(Constant.Message.FAIL, resp.Message);
    }
    
    [Fact]
    public void StoreReadingsShouldReturnSuccess()
    {
        var req = new MeterReadings
        {
            SmartMeterId = Constant.SmartMeter.SMART_METER_ZERO,
            ElectricityReadings = new List<ElectricityReading>
            {
                new ElectricityReading {Time = DateTime.Now.AddMinutes(-30), Reading = 35m},
                new ElectricityReading {Time = DateTime.Now.AddMinutes(-15), Reading = 45m},
            }
        };

        var resp = _facade.StoreReadings(req);
        Assert.True(resp.Result);
        Assert.Equal(Constant.Message.SUCCESS, resp.Message);

        var readingResp = _facade.GetReadings(Constant.SmartMeter.SMART_METER_ZERO);
        Assert.Equal(22, readingResp.Results.Count);
    }
}