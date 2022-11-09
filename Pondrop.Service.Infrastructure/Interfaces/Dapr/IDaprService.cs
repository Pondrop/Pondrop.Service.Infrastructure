using Pondrop.Service.Events;

namespace Pondrop.Service.Interfaces;

public interface IDaprService
{
    Task<bool> InvokeServiceAsync(string appId, string methodName, object? data = null);

    Task<bool> SendEventsAsync(string eventGridTopic, IEnumerable<IEvent> events);
}