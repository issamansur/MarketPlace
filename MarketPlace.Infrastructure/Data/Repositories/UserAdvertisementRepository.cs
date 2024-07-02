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

    public async Task<IReadOnlyCollection<UserAdvertisement>> GetAllUserAdvertisementsAsync(UserAdvertisementsFilter filter, CancellationToken cancellationToken)
    {
        var res = Context.UserAdvertisements.AsNoTracking();
        
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