using MarketPlace.Application.CQRS.AdvertisementReviews.Filters;

namespace MarketPlace.Application.Repositories;

public interface IAdvertisementReviewRepository: ICrudRepository<AdvertisementReview>
{
     Task<IReadOnlyCollection<AdvertisementReview>> GetByAdvertisementIdAsync(AdvertisementReviewsFilter filter, CancellationToken cancellationToken);
}