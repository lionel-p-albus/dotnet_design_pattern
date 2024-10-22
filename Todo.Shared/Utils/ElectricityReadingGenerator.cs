using Todo.Shared.Models;

namespace Todo.Shared.Utils;

public class ElectricityReadingGenerator
{
    public ElectricityReadingGenerator()
    {
    }

    public List<ElectricityReading> Generate(int number)
    {
        var readings = new List<ElectricityReading>();
        var random = new Random();
        for (int i = 0; i < number; i++)
        {
            var reading = (decimal) random.NextDouble();
            var electricityReading = new ElectricityReading
            {
                Reading = reading,
                Time = DateTime.Now.AddSeconds(-i * 10) // AddSeconds is not available in .NET 5
            };
            
            readings.Add(electricityReading);
        }
        
        readings.Sort((reading1, reading2) => reading1.Time.CompareTo(reading2.Time)); // Sort the readings by time.
        return readings;
    }
}