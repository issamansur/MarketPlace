using MarketPlace.Application.AdvertisementReviews.Filters;

namespace MarketPlace.Application.AdvertisementReviews.Queries;

public class GetReviewsByAdvertisementQuery: IRequest<IEnumerable<AdvertisementReview>>
{
    public AdvertisementReviewsFilter Filter { get; }

    public GetReviewsByAdvertisementQuery(AdvertisementReviewsFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter, nameof(filter));
        
        Filter = filter;
        // Validation is done in the filter
    }
}