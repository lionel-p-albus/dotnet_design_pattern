using Todo.Shared.Enums;

namespace Todo.Shared.Models;

public class PricePlan
{
    public string PlanName { get; set; }
    public Supplier EnergySupplier { get; set; }
    public decimal UnitRate { get; set; }
    public IList<PeakTimeMultiplier> PeakTimeMultiplier { get; set; }
    
    public decimal GetPrice(DateTime datetime)
    {
        var multiplier = PeakTimeMultiplier.FirstOrDefault(m => m.DayOfWeek == datetime.DayOfWeek);

        if (multiplier?.Multiplier != null)
        {
            return multiplier.Multiplier * UnitRate;
        }
        else
        {
            return UnitRate;
        }
    }
}