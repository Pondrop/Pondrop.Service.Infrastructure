using Pondrop.Service.Models;

namespace Pondrop.Service.Interfaces;

public interface ICheckpointRepository<T> : IContainerRepository<T> where T : EventEntity
{
    Task<int> RebuildAsync();
    Task<T?> UpsertAsync(long expectedVersion, T item);

    Task FastForwardAsync(T item);
}