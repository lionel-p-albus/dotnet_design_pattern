using Todo.Shared.Constants;
using Todo.Shared.Mappers.Interface;
using Todo.Shared.Models;

namespace Todo.Shared.Facades.Interface;

public class PricePlanComparatorFacade : IPricePlanComparatorFacade
{
    private readonly List<PricePlan> _pricePlans;
    private readonly IAccountFacade _accountFacade;
    private readonly IMeterReadingFacade _meterReadingFacade;
    private readonly IModelMapper _modelMapper;
    
    public PricePlanComparatorFacade(List<PricePlan> pricePlans, IAccountFacade accountFacade, IMeterReadingFacade meterReadingFacade, IModelMapper modelMapper)
    {
        _pricePlans = pricePlans;
        _accountFacade = accountFacade;
        _meterReadingFacade = meterReadingFacade;
        _modelMapper = modelMapper;
    }
    
    public StandardResponse<PricePlanComparator> CalculateCostForEachPricePlan(string smartMeterId)
    {
        var resp = new StandardResponse<PricePlanComparator>();
        
        string? pricePlanId = _accountFacade.GetPricePlanIdForSmartMeterId(smartMeterId);
        Dictionary<string, decimal> costPerPricePlan = GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);
        
        if (!costPerPricePlan.Any()) // If there are no costs, return a 404.
        {
            resp.Message = Constant.Message.NO_DATA;
            resp.Result = new PricePlanComparator();
            
            return resp;
        }


        resp.Message = Constant.Message.SUCCESS;
        resp.Result = new PricePlanComparator()
        {
            PricePlanId = pricePlanId,
            PricePlanComparisons = _modelMapper.MapDictionaries(costPerPricePlan)
        };
        
        return resp;
    }

    public StandardResponse<ObjectDictionary> RecommendCheapestPricePlans(string smartMeterId, int? limit)
    {
        var resp = new StandardResponse<ObjectDictionary>();
        
        var consumptionForPricePlans = GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);

        if (!consumptionForPricePlans.Any())
        {
            resp.Message = string.Format("Smart Meter ID ({0}) not found", smartMeterId);
            resp.Results = new List<ObjectDictionary>();

            return resp;
        }
        
        var recommendations = ConvertToDictionary(consumptionForPricePlans.OrderBy(pricePlanComparison => pricePlanComparison.Value)); // Order the price plans by their costs.

        // If a limit is specified and the number of recommendations is greater than the limit, return the first n recommendations.
        if (limit.HasValue && limit.Value < recommendations.Count())
        {
            resp.Message = Constant.Message.SUCCESS;
            resp.Results = _modelMapper.MapDictionaries(recommendations.Take(limit.Value).ToDictionary(x => x.Key, x => x.Value));
            return resp;
        }
        
        resp.Message = Constant.Message.SUCCESS;
        resp.Results = _modelMapper.MapDictionaries(recommendations);
        return resp;
    }

    #region Private Function
    private decimal CalculateAverageReading(List<ElectricityReading> electricityReadings)
    {
        /***
         * In C#, the Aggregate method is used to apply an accumulator function over a sequence. It processes each element in the sequence and accumulates the result.
         * It is often used to perform operations like summing, concatenating, or combining elements in a collection.
         */
        var newSummedReadings = electricityReadings.Select(readings => readings.Reading).Aggregate((reading, accumulator) => reading + accumulator); // Sum the readings.

        return newSummedReadings / electricityReadings.Count(); // Return the average reading.
    }

    private decimal CalculateTimeElapsed(List<ElectricityReading> electricityReadings)
    {
        var first = electricityReadings.Min(reading => reading.Time); // Get the first reading.
        var last = electricityReadings.Max(reading => reading.Time); // Get the last reading.

        return (decimal)(last - first).TotalHours; // Return the time elapsed.
    }
    
    private decimal CalculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan)
    {
        var average = CalculateAverageReading(electricityReadings);
        var timeElapsed = CalculateTimeElapsed(electricityReadings);
        
        var averagedCost = average/timeElapsed; // Calculate the averaged cost.
        
        return Math.Round(averagedCost * pricePlan.UnitRate, 3); // Return the rounded cost. ปัดเศษทศนิยม 3 ตำแหน่ง
    }
    
    private Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId)
    {
        List<ElectricityReading> electricityReadings = _meterReadingFacade.GetReadings(smartMeterId).Results;

        // If there are no electricity readings, return an empty dictionary.
        if (!electricityReadings.Any()) return new Dictionary<string, decimal>();
            
        return _pricePlans.ToDictionary(plan => plan.PlanName, plan => CalculateCost(electricityReadings, plan)); // Return a dictionary of price plans and their costs.
    }
    
    private static Dictionary<string, decimal> ConvertToDictionary(IOrderedEnumerable<KeyValuePair<string, decimal>> datas)
    {
        return datas.ToDictionary(x => x.Key, x => x.Value);
    }
    #endregion
}