namespace MarketPlace.Infrastructure.Data.Repositories;

public class UserRepository: BaseRepository, IUserRepository
{
    public UserRepository(MarketPlaceDbContext context) : base(context)
    {
    }

    public Task<Guid> CreateAsync(User entity, CancellationToken cancellationToken)
    {
        Context.Users.Add(entity);
        return Task.FromResult(entity.Id);
    }

    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Users
            .AsNoTracking()
            .FirstAsync(x => x.Id == id, cancellationToken);
    }

    public Task UpdateAsync(User entity, CancellationToken cancellationToken)
    {
        Context.Users.Update(entity);
        return Task.CompletedTask;
    }
}