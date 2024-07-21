using MarketPlace.Application.Enums;

namespace MarketPlace.Application.Features.AdvertisementReviews.Queries.Filters;

public class AdvertisementReviewsFilter
{
    public Guid AdvertisementId { get; }
    public int Page { get; }
    public int PageSize { get; }
    public AdvertisementReviewSortTypes AdvertisementReviewSortType { get; }
    public bool IsDesc { get; }
    
    public AdvertisementReviewsFilter(
        Guid advertisementId, 
        int page = 1, 
        int pageSize = 10, 
        AdvertisementReviewSortTypes advertisementReviewSortType = AdvertisementReviewSortTypes.None,
        bool isDesc = false
    )
    {
        AdvertisementId = advertisementId;
        Page = page;
        PageSize = pageSize;
        AdvertisementReviewSortType = advertisementReviewSortType;
        IsDesc = isDesc;
        
        var validator = new AdvertisementReviewsFilterValidator();
        validator.ValidateAndThrow(this);
    }
}