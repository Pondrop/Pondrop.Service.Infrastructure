using Pondrop.Service.Events;

namespace Pondrop.Service.Interfaces;

public interface IServiceBusService
{
    Task SendMessageAsync(object payload);

    Task SendMessageAsync(string subject, object payload);
}