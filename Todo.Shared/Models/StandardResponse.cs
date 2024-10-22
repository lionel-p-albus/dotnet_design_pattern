namespace Todo.Shared.Models;

public class StandardResponse<T>
{
    public string Message { get; set; }
    public T Result { get; set; }
    public List<T> Results { get; set; }
}