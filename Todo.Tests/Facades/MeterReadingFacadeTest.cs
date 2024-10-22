using Todo.Shared.Constants;
using Todo.Shared.Facades.Interface;
using Todo.Shared.Models;
using Todo.Shared.Utils;
using Xunit;

namespace Todo.Tests.Facades;

public class MeterReadingFacadeTest
{
    private readonly MeterAssociatedReadings _meterAssociatedReadings;
    private readonly IMeterReadingFacade _facade;

    public MeterReadingFacadeTest()
    {
        _meterAssociatedReadings = new MeterAssociatedReadings(SharedUtils.GenerateMeterElectricityReadings());
        _facade = new MeterReadingFacade(_meterAssociatedReadings);
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
    public void StoreReadingsShouldReturnSuccess()
    {
        var req = new MeterReadings
        {
            SmartMeterId = Constant.SmartMeter.SMART_METER_ZERO,
            ElectricityReadings = new List<ElectricityReading>
            {
                new ElectricityReading {Time = DateTime.Now.AddMinutes(-30), Reading = 35m},
                new ElectricityReading {Time = DateTime.Now.AddMinutes(-15), Reading = 35m},
            }
        };

        var resp = _facade.StoreReadings(req);
        Assert.True(resp.Result);
        Assert.Equal(Constant.Message.SUCCESS, resp.Message);
    }
    
    [Fact]
    public void GivenMeterIdThatDoesNotExistShouldReturnEmptyList()
    {
        var electricityReadings = _facade.GetReadings("unknown-id");
        Assert.Empty(electricityReadings.Results);
    }
    
    [Fact]
    public void GivenMeterReadingThatExistsShouldReturnMeterReadings()
    {
        var req = new MeterReadings
        {
            SmartMeterId = Constant.SmartMeter.SMART_METER_ZERO,
            ElectricityReadings = new List<ElectricityReading>
            {
                new ElectricityReading {Time = DateTime.Now, Reading = 25m}
            }
        };

        _facade.StoreReadings(req);
        var electricityReadings = _facade.GetReadings(Constant.SmartMeter.SMART_METER_ZERO);
        
        Assert.Equal(21, electricityReadings.Results.Count);
    }
}