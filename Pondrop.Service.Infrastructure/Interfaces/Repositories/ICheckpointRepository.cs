using Pondrop.Service.Models;

namespace Pondrop.Service.Interfaces;

public interface ICheckpointRepository<T> : IContainerRepository<T>
{
    Task<int> RebuildAsync();
    Task<T?> UpsertAsync(long expectedVersion, T item);

    Task FastForwardAsync(T item);
}