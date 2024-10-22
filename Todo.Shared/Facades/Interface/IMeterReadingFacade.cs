using Todo.Shared.Models;

namespace Todo.Shared.Facades.Interface;

public interface IMeterReadingFacade
{
    StandardResponse<bool> StoreReadings(MeterReadings req);
    StandardResponse<ElectricityReading> GetReadings(string smartMeterId);
}