using Todo.Shared.Models;

namespace Todo.Shared.Facades.Interface;

public interface IPricePlanComparatorFacade
{
    StandardResponse<PricePlanComparator> CalculateCostForEachPricePlan(string smartMeterId);
    StandardResponse<ObjectDictionary> RecommendCheapestPricePlans(string smartMeterId, int? limit);
}