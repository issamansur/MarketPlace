namespace MarketPlace.Application.Repositories;

public interface IRoleRepository: ICrudRepository<Role>
{
    Task<Role?> TryGetByNameAsync(string name);
}