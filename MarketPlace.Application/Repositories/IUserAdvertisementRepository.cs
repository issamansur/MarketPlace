using MarketPlace.Application.CQRS.UserAdvertisements.Filters;

namespace MarketPlace.Application.Repositories;

public interface IUserAdvertisementRepository: ICrudRepository<UserAdvertisement>
{
    Task<IReadOnlyCollection<UserAdvertisement>> GetAllUserAdvertisementsAsync(UserAdvertisementsFilter filter, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<UserAdvertisement>> GetUserAdvertisementsByUserIdAsync(UserAdvertisementsByUserFilter filter, CancellationToken cancellationToken);
    
    Task DeleteAsync(UserAdvertisement entity, CancellationToken cancellationToken);
}