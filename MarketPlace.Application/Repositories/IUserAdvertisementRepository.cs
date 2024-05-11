using MarketPlace.Application.UserAdvertisements.Filters;

namespace MarketPlace.Application.Repositories;

public interface IUserAdvertisementRepository: ICrudRepository<UserAdvertisement>
{
    Task<IEnumerable<UserAdvertisement>> GetAllUserAdvertisementsAsync(UserAdvertisementsFilter filter, CancellationToken cancellationToken);
    Task<IEnumerable<UserAdvertisement>> GetUserAdvertisementsByUserIdAsync(UserAdvertisementsByUserFilter filter, CancellationToken cancellationToken);
}