using MarketPlace.Application.CQRS.AdvertisementReviews.Filters;

namespace MarketPlace.Application.Repositories;

public interface IAdvertisementReviewRepository: ICrudRepository<AdvertisementReview>
{
     Task<IEnumerable<AdvertisementReview>> GetByAdvertisementIdAsync(AdvertisementReviewsFilter filter, CancellationToken cancellationToken);
}