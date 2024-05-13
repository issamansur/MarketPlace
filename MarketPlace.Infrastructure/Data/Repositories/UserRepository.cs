namespace MarketPlace.Infrastructure.Data.Repositories;

public class UserRepository: BaseRepository, IUserRepository
{
    public UserRepository(MarketPlaceDbContext context) : base(context)
    {
    }

    public async Task<Guid> CreateAsync(User entity, CancellationToken cancellationToken)
    {
        Context.Users.Add(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Users.AsNoTracking().FirstAsync(x => x.Id == id, cancellationToken);
    }

    // Other methods are not implemented (not needed for the test)
    public async Task UpdateAsync(User entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(User entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}