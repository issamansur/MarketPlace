using MarketPlace.Application.CQRS.UserAdvertisements.Filters;
using MarketPlace.Application.Enums;

namespace MarketPlace.Infrastructure.Data.Repositories;

public class UserAdvertisementRepository: BaseRepository, IUserAdvertisementRepository
{
    public UserAdvertisementRepository(MarketPlaceDbContext context) : base(context)
    {
        // TODO: Add Service for Image
    }

    public async Task<Guid> CreateAsync(UserAdvertisement entity, CancellationToken cancellationToken)
    {
        // TODO: Add check on max count of advertisements
        
        Context.UserAdvertisements.Add(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(UserAdvertisement entity, CancellationToken cancellationToken)
    {
        Context.UserAdvertisements.Update(entity);
    }

    public async Task DeleteAsync(UserAdvertisement entity, CancellationToken cancellationToken)
    {
        Context.UserAdvertisements.Remove(entity);
    }

    public async Task<UserAdvertisement> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.UserAdvertisements.AsNoTracking().FirstAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<UserAdvertisement>> GetAllUserAdvertisementsAsync(UserAdvertisementsFilter filter, CancellationToken cancellationToken)
    {
        var res = Context.UserAdvertisements.AsNoTracking();
        
        if (filter.SortType == SortTypes.ByRating)
        {
            res = filter.IsDesc? res.OrderByDescending(x => x.Rating) : res.OrderBy(x => x.Rating);
        }
        else
        {
            res = filter.IsDesc? res.OrderByDescending(x => x.DateCreated) : res.OrderBy(x => x.DateCreated);
        }

        res = res.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);

        return await res.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserAdvertisement>> GetUserAdvertisementsByUserIdAsync(UserAdvertisementsByUserFilter filter, CancellationToken cancellationToken)
    {
        var res = Context.UserAdvertisements.AsNoTracking()
            .Where(x => x.CreatorId == filter.UserId);
        
        if (filter.SortType == SortTypes.ByRating)
        {
            res = filter.IsDesc? res.OrderByDescending(x => x.Rating) : res.OrderBy(x => x.Rating);
        }
        else
        {
            res = filter.IsDesc? res.OrderByDescending(x => x.DateCreated) : res.OrderBy(x => x.DateCreated);
        }

        res = res.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);

        return await res.ToListAsync(cancellationToken);
    }
}