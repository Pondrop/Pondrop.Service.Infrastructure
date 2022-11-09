using Newtonsoft.Json.Linq;

namespace Pondrop.Service.Events;

public interface IEventPayload
{
    DateTime CreatedUtc { get; }
    DateTime? DeletedUtc { get; }
}