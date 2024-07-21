using MarketPlace.Application.Features.UserAdvertisements.Queries.Filters;

namespace MarketPlace.Application.Repositories;

public interface IUserAdvertisementRepository: ICrudRepository<UserAdvertisement>
{
    Task<IReadOnlyCollection<UserAdvertisement>> GetAllUserAdvertisementsAsync(UserAdvertisementsFilter filter, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<UserAdvertisement>> GetUserAdvertisementsByUserIdAsync(
        UserAdvertisementsByUserFilter filter, CancellationToken cancellationToken);
    Task<UserAdvertisement> GetByNumberAsync(int requestUserAdvertisementNumber, CancellationToken cancellationToken);
    
    Task DeleteAsync(UserAdvertisement entity, CancellationToken cancellationToken);
}