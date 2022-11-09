using Newtonsoft.Json;
using Pondrop.Service.Events;

namespace Pondrop.Service.Events;

public record EventPayload : IEventPayload
{
    public DateTime CreatedUtc { get; } = DateTime.UtcNow;

    public DateTime? DeletedUtc { get; }
}