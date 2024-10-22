namespace Todo.Shared.Facades.Interface;

public class AccountFacade : IAccountFacade
{
    private readonly Dictionary<string, string> _smartMeterToPricePlanAccounts;
    
    public AccountFacade(Dictionary<string, string> smartMeterToPricePlanAccounts) { // Constructor.
        _smartMeterToPricePlanAccounts = smartMeterToPricePlanAccounts; // Set the _smartMeterToPricePlanAccounts property to the smartMeterToPricePlanAccounts parameter.
    }
    
    public string? GetPricePlanIdForSmartMeterId(string smartMeterId)
    {
        if (!_smartMeterToPricePlanAccounts.ContainsKey(smartMeterId)) return null; // If the dictionary does not contain the key, return null.
        
        return _smartMeterToPricePlanAccounts[smartMeterId];
    }
}