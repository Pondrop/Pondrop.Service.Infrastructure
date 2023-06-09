﻿using Newtonsoft.Json;
using Pondrop.Service.Events;

namespace Pondrop.Service.Models;

public abstract record EventEntity
{
    public static string GetStreamTypeName<T>() where T : EventEntity
        => GetStreamTypeName(typeof(T));
 
    public static string GetStreamId<T>(Guid id) where T : EventEntity
        => GetStreamId(typeof(T), id);
    
    private static string GetStreamTypeName(Type entityType)
        => entityType.Name.Replace("Entity", string.Empty, StringComparison.Ordinal);
    
    private static string GetStreamId(Type type, Guid id)
        => $"{GetStreamTypeName(type)}:{id}";

    [JsonProperty("streamId")]
    public string StreamId { get; private set; } = string.Empty;
    
    private Guid _id = Guid.Empty;
    [JsonProperty("id")]
    public Guid Id
    {
        get => _id;
        protected set
        {
            if (_id != value)
            {
                _id = value;
                StreamId = GetStreamId(GetType(), Id);
            }
        }
    }

    private string _streamType = string.Empty;
    [JsonProperty("streamType")]
    public string StreamType
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_streamType))
                StreamType = GetStreamTypeName(GetType());

            return _streamType;
        }
        protected set
        {
            if (!string.IsNullOrWhiteSpace(value) && _streamType != value)
            {
                _streamType = value;
                StreamId = GetStreamId(GetType(), Id);
            }
        }
    }
    
    [JsonProperty("atSequence")]
    public long AtSequence { get; protected set; }
    
    [JsonProperty("createdBy")]
    public string CreatedBy { get; protected set; } = string.Empty;
    
    [JsonProperty("updatedBy")]
    public string UpdatedBy { get; protected set; } = string.Empty;
    
    [JsonProperty("createdUtc")]
    public DateTime CreatedUtc { get; protected set; }
    
    [JsonProperty("updatedUtc")]
    public DateTime UpdatedUtc { get; protected set; }

    [JsonProperty("deletedUtc")]
    public DateTime? DeletedUtc { get; protected set; }


    public IEnumerable<IEvent> GetEvents() => Events;
    public IEnumerable<IEvent> GetEvents(long fromSequenceNumber) => Events.Where(i => i.SequenceNumber >= fromSequenceNumber);
    
    [JsonIgnore]
    public int EventsCount => Events.Count;
    
    public void Apply(IEnumerable<IEvent> events)
    {
        foreach (var e in events)
        {
            Apply(e);
        }
    }
    
    public abstract void Apply(IEventPayload eventPayloadToApply, string createdBy);
    
    protected readonly List<IEvent> Events = new List<IEvent>();
    protected abstract void Apply(IEvent eventToApply);
}