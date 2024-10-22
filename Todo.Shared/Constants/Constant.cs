namespace Todo.Shared.Constants;

public static class Constant
{
    public static class Message
    {
        public static readonly string SUCCESS = "SUCCESS";
        public static readonly string FAIL = "FAIL";
        public static readonly string NO_DATA = "NO_DATA";
        public static readonly string INVALID = "INPUT_IS_INVALID";
    }


    public static class PricePlan
    {
        public static readonly string MOST_EVIL_PRICE_PLAN_ID = "price-plan-0";
        public static readonly string RENEWABLES_PRICE_PLAN_ID = "price-plan-1";
        public static readonly string STANDARD_PRICE_PLAN_ID = "price-plan-2";
    }

    public static class PricePlanComparator
    {
        public static readonly string PRICE_PLAN_ID_KEY = "pricePlanId";
        public static readonly string PRICE_PLAN_COMPARISONS_KEY = "pricePlanComparisons";
    }
    
    public static class SmartMeter
    {
        public static readonly string SMART_METER_ZERO = "smart-meter-0";
        public static readonly string SMART_METER_ONE = "smart-meter-1";
        public static readonly string SMART_METER_TWO = "smart-meter-2";
        public static readonly string SMART_METER_THREE = "smart-meter-3";
        public static readonly string SMART_METER_FOUR = "smart-meter-4";
    }
}