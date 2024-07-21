using MarketPlace.Application.Features.AdvertisementReviews.Queries.Filters;

namespace MarketPlace.Application.Repositories;

public interface IAdvertisementReviewRepository: ICrudRepository<AdvertisementReview>
{
     Task<IReadOnlyCollection<AdvertisementReview>> GetByAdvertisementIdAsync(AdvertisementReviewsFilter filter, CancellationToken cancellationToken);
     Task DeleteAsync(AdvertisementReview entity, CancellationToken cancellationToken);
}