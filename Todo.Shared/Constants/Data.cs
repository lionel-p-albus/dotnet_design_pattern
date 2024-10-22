using Todo.Shared.Enums;
using Todo.Shared.Models;

namespace Todo.Shared.Constants;

public static class Data
{
    private static readonly Dictionary<string, string> _SmartMeterToPricePlanAccounts = new Dictionary<string, string>()
    {
        {Constant.SmartMeter.SMART_METER_ZERO, Constant.PricePlan.MOST_EVIL_PRICE_PLAN_ID},
        {Constant.SmartMeter.SMART_METER_ONE, Constant.PricePlan.RENEWABLES_PRICE_PLAN_ID},
        {Constant.SmartMeter.SMART_METER_TWO, Constant.PricePlan.MOST_EVIL_PRICE_PLAN_ID},
        {Constant.SmartMeter.SMART_METER_THREE, Constant.PricePlan.STANDARD_PRICE_PLAN_ID},
        {Constant.SmartMeter.SMART_METER_FOUR, Constant.PricePlan.RENEWABLES_PRICE_PLAN_ID},
    };
    
    private static readonly List<PricePlan> _PricePlans = new List<PricePlan>
    {
        new PricePlan
        {
            PlanName = Constant.PricePlan.MOST_EVIL_PRICE_PLAN_ID,
            EnergySupplier = Supplier.DrEvilsDarkEnergy,
            UnitRate = 10m,
            PeakTimeMultiplier = new List<PeakTimeMultiplier>()
        },
        new PricePlan
        {
            PlanName = Constant.PricePlan.RENEWABLES_PRICE_PLAN_ID,
            EnergySupplier = Supplier.TheGreenEco,
            UnitRate = 2m,
            PeakTimeMultiplier = new List<PeakTimeMultiplier>()
        },
        new PricePlan
        {
            PlanName = Constant.PricePlan.STANDARD_PRICE_PLAN_ID,
            EnergySupplier = Supplier.PowerForEveryone,
            UnitRate = 1m,
            PeakTimeMultiplier = new List<PeakTimeMultiplier>()
        }
    };
    
    public static Dictionary<string, string> SmartMeterToPricePlanAccounts => _SmartMeterToPricePlanAccounts; // Get the smart meter to price plan accounts.
    public static List<PricePlan> PricePlans => _PricePlans; // Get the price plans.
}