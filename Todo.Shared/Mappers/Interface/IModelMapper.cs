using Todo.Shared.Models;

namespace Todo.Shared.Mappers.Interface;

public interface IModelMapper
{
    List<ObjectDictionary> MapDictionaries(Dictionary<string, decimal> dictionary);
}