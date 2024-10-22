using System.Net;
using Newtonsoft.Json;

namespace Todo.Shared.Models;

public class DefaultResponse
{
    [JsonProperty("error")]
    public string Error { get; set; } = HttpStatusCode.InternalServerError.ToString();

    [JsonProperty("exception")]
    public string Exception { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("stackTrace")]
    public string StackTrace { get; set; }

    [JsonProperty("path")]
    public string Path { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("timestamp")]
    public int Timestamp { get; set; } = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
}