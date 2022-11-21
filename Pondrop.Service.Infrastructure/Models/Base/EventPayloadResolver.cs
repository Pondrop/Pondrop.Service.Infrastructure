using Pondrop.Service.Events;
using System.Collections.Concurrent;
using System.Reflection;

namespace Pondrop.Service.Events;

public class DefaultEventTypePayloadResolver : IEventTypePayloadResolver
{
    private static DefaultEventTypePayloadResolver? Default;
    public static IEventTypePayloadResolver Instance => Default ?? throw new NullReferenceException();

    private readonly ConcurrentDictionary<string, Type> _typeLookup = new ConcurrentDictionary<string, Type>();

    private readonly string _assemblyFullName;
    private readonly string _baseNamespace;

    public DefaultEventTypePayloadResolver(Assembly baseAssembly)
    {
        _assemblyFullName = baseAssembly.FullName;
        _baseNamespace = $"{baseAssembly.GetName().Name}.Events";
    }

    public static void Init(Assembly baseAssembly)
    {
        Default ??= new DefaultEventTypePayloadResolver(baseAssembly);
    }

    public Type? GetEventPayloadType(string streamType, string eventName)
    {
        if (!_typeLookup.TryGetValue(eventName, out var type))
        {
            type = Type.GetType($"{_baseNamespace}.{streamType}.{eventName}, {_assemblyFullName}");
            if (type is not null)
                _typeLookup.AddOrUpdate(eventName, type, (k, v) => type);
        }

        return type;
        }
    }