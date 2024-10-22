using AutoMapper;
using Todo.Shared.Mappers.Interface;
using Todo.Shared.Models;

namespace Todo.Shared.Mappers;

public class ModelMapper : IModelMapper
{
    private readonly IMapper _mapper;

    public ModelMapper()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            // cfg.CreateMap<Dictionary<string, decimal>, PricePlanComparison>();
        }).CreateMapper();
    }

    public List<ObjectDictionary> MapDictionaries(Dictionary<string, decimal> dictionary)
    {
        var resp = new List<ObjectDictionary>();
        
        var keys = dictionary.Keys.ToList();
        foreach (var key in keys)
        {
            resp.Add(new ObjectDictionary(){Key = key, Value = dictionary[key]});
        }

        return resp;
    }
}