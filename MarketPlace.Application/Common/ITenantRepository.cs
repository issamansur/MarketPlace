using MarketPlace.Application.Repositories;

namespace MarketPlace.Application.Common;

public interface ITenantRepository
{
    IRoleRepository Roles { get; }
    IUserRepository Users { get; }
    
    IUserAdvertisementRepository UserAdvertisements { get; }
    IAdvertisementReviewRepository AdvertisementReviews { get; }
    
    Task CommitAsync(CancellationToken cancellationToken);
}