namespace MarketPlace.Infrastructure.Data.Repositories;

public class RoleRepository: BaseRepository, IRoleRepository
{
    public RoleRepository(MarketPlaceDbContext context) : base(context)
    {
        
    }

    public async Task<Guid> CreateAsync(Role entity, CancellationToken cancellationToken)
    {
        Context.Roles.Add(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<Role> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Roles.AsNoTracking().FirstAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Role?> TryGetByTitleAsync(string title, CancellationToken cancellationToken)
    {
        return await Context.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Title == title, cancellationToken);
    }

    // Other methods are not implemented (not needed for the test)
    public async Task UpdateAsync(Role entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Role entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}