using MarketPlace.Application.CQRS.UserAdvertisements.Filters;
using MarketPlace.Application.Enums;

namespace MarketPlace.Infrastructure.Data.Repositories;

public class UserAdvertisementRepository: BaseRepository, IUserAdvertisementRepository
{
    public UserAdvertisementRepository(MarketPlaceDbContext context) : base(context)
    {
    }

    public Task<Guid> CreateAsync(UserAdvertisement entity, CancellationToken cancellationToken)
    {
        Context.UserAdvertisements.Add(entity);
        return Task.FromResult(entity.Id);
    }

    public Task UpdateAsync(UserAdvertisement entity, CancellationToken cancellationToken)
    {
        Context.UserAdvertisements.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(UserAdvertisement entity, CancellationToken cancellationToken)
    {
        Context.UserAdvertisements.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<UserAdvertisement> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.UserAdvertisements
            .AsNoTracking()
            .FirstAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<UserAdvertisement> GetByNumberAsync(int number, CancellationToken cancellationToken)
    {
        return await Context.UserAdvertisements
            .AsNoTracking()
            .FirstAsync(x => x.Number == number, cancellationToken);
    }

    public async Task<IReadOnlyCollection<UserAdvertisement>> GetAllUserAdvertisementsAsync(UserAdvertisementsFilter filter, CancellationToken cancellationToken)
    {
        var res = Context.UserAdvertisements.AsNoTracking();

        var query = filter.Query.Trim();
        
        if (!string.IsNullOrEmpty(filter.Query))
        {
            // Check if query is a number (possible advertisement number)
            if (int.TryParse(query, out var id))
            {
                // If query is a number, try to find advertisement by number
                var advertisement = res.FirstOrDefault(x => x.Number == id);
                if (advertisement != null)
                {
                    return new List<UserAdvertisement> {advertisement};
                }
            }
            
            // If query is not a number, search by title or description
            res = res.Where(x => 
                EF.Functions.ILike(x.Title, $"%{query}%") || 
                EF.Functions.ILike(x.Description, $"%{query}%")
            );
        }
        
        if (filter.UserAdvertisementSortType == UserAdvertisementSortTypes.ByRating)
        {
            res = filter.IsDesc? res.OrderByDescending(x => x.Rating) : res.OrderBy(x => x.Rating);
        }
        else
        {
            res = filter.IsDesc? res.OrderByDescending(x => x.DateUpdated) : res.OrderBy(x => x.DateCreated);
        }

        res = res.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);

        return await res.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<UserAdvertisement>> GetUserAdvertisementsByUserIdAsync(UserAdvertisementsByUserFilter filter, CancellationToken cancellationToken)
    {
        var res = Context.UserAdvertisements.AsNoTracking()
            .Where(x => x.CreatorId == filter.UserId);
        
        if (filter.UserAdvertisementSortType == UserAdvertisementSortTypes.ByRating)
        {
            res = filter.IsDesc? res.OrderByDescending(x => x.Rating) : res.OrderBy(x => x.Rating);
        }
        else
        {
            res = filter.IsDesc? res.OrderByDescending(x => x.DateUpdated) : res.OrderBy(x => x.DateCreated);
        }

        res = res.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);

        return await res.ToListAsync(cancellationToken);
    }
}