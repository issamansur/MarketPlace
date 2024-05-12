namespace MarketPlace.Application.Common;

public interface ICrudRepository<T>
{
    Task<Guid> CreateAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid entityId, CancellationToken cancellationToken);
    
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}