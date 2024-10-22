namespace Todo.Shared.Models;

public class MeterAssociatedReadings
{
    public MeterAssociatedReadings(Dictionary<string, List<ElectricityReading>> readings)
    {
        Datas = readings;
    }
    
    public Dictionary<string, List<ElectricityReading>> Datas { get; set; }
}