using Todo.Shared.Constants;
using Todo.Shared.Models;

namespace Todo.Shared.Utils;

public static class SharedUtils
{
    public static Dictionary<String, List<ElectricityReading>> GenerateMeterElectricityReadings()
    {
        var electricityReadings = new Dictionary<String, List<ElectricityReading>>();
        var generator = new ElectricityReadingGenerator();
        var smartMeterIds = Data.SmartMeterToPricePlanAccounts.Select(item => item.Key);
        
        foreach (var smartMeterId in smartMeterIds)
        {
            electricityReadings.Add(smartMeterId, generator.Generate(20));
        }
        
        return electricityReadings;
    }
}