namespace Todo.Shared.Models;

public class ElectricityConsumerReq
{
    public string SmartMeterId { get; set; }
    public List<ElectricityConsumer> ElectricityConsumer { get; set; }
}