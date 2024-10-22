using System.Net;
using System.Runtime.Serialization;

namespace Todo.Shared.Models;

public class DefaultException : Exception
{
    public DefaultException() => Status = HttpStatusCode.BadRequest;

    public DefaultException(string message) : base(message)
    {
        Status = HttpStatusCode.BadRequest;
    }

    public DefaultException(string message, Exception innerException) : base(message, innerException)
    {
        Status = HttpStatusCode.BadRequest;
    }

    protected DefaultException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Status = HttpStatusCode.BadRequest;
    }

    public HttpStatusCode Status { get; set; }
}