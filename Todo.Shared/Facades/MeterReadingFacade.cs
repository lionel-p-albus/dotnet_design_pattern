using Todo.Shared.Constants;
using Todo.Shared.Models;

namespace Todo.Shared.Facades.Interface;

public class MeterReadingFacade : IMeterReadingFacade
{
    private readonly MeterAssociatedReadings _meterAssociatedReadings;

    public MeterReadingFacade(MeterAssociatedReadings meterAssociatedReadings)
    {
        _meterAssociatedReadings = meterAssociatedReadings;
    }
    
    public StandardResponse<bool> StoreReadings(MeterReadings req)
    {
        var resp = new StandardResponse<bool>()
        {
            Message = Constant.Message.INVALID,
            Result = false
        };

        if (!IsMeterReadingsValid(req)) return resp;

        if (!_meterAssociatedReadings.Datas.ContainsKey(req.SmartMeterId))
        {
            // _meterAssociatedReadings.Datas.Add(req.SmartMeterId, new List<ElectricityReading>());
            resp.Message = Constant.Message.FAIL;
            return resp;
        }
        
        req.ElectricityReadings.ForEach(electricityReading => _meterAssociatedReadings.Datas[req.SmartMeterId].Add(electricityReading)); // add each electricity reading to the dictionary.
        
        resp.Message = Constant.Message.SUCCESS;
        resp.Result = true;
        
        return resp;
    }

    public StandardResponse<ElectricityReading> GetReadings(string smartMeterId)
    {
        var resp = new StandardResponse<ElectricityReading>();
        var results = new List<ElectricityReading>();
        
        if (_meterAssociatedReadings.Datas.ContainsKey(smartMeterId)) 
            results = _meterAssociatedReadings.Datas[smartMeterId];
        
        resp.Message = Constant.Message.SUCCESS;
        resp.Results = results;

        return resp;
    }
    
    #region Private Function
    private bool IsMeterReadingsValid(MeterReadings meterReadings)
    {
        string smartMeterId = meterReadings.SmartMeterId;
        List<ElectricityReading> electricityReadings = meterReadings.ElectricityReadings;
        
        return (smartMeterId != null && smartMeterId.Any()) && (electricityReadings != null && electricityReadings.Any());
    }
    #endregion
}