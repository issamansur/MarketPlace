namespace MarketPlace.Infrastructure.Data.Repositories;

public class RoleRepository: BaseRepository, IRoleRepository
{
    public RoleRepository(MarketPlaceDbContext context) : base(context)
    {
        
    }

    public Task<Guid> CreateAsync(Role entity, CancellationToken cancellationToken)
    {
        Context.Roles.Add(entity);
        return Task.FromResult(entity.Id);
    }

    public async Task<Role> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Roles
            .AsNoTracking()
            .FirstAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Role?> TryGetByTitleAsync(string title, CancellationToken cancellationToken)
    {
        return await Context.Roles
            .AsNoTracking().
            FirstOrDefaultAsync(x => x.Title == title, cancellationToken);
    }

    public Task UpdateAsync(Role entity, CancellationToken cancellationToken)
    {
        Context.Roles.Update(entity);
        return Task.CompletedTask;
    }
}