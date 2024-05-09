namespace MarketPlace.Application.Common;

public interface ICrudRepository<T>
{
    Task<Guid> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<T> DeleteAsync(T entity, CancellationToken cancellationToken);
    
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}