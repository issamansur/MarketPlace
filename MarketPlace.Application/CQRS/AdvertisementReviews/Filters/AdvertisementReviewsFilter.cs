using MarketPlace.Application.Enums;

namespace MarketPlace.Application.CQRS.AdvertisementReviews.Filters;

public class AdvertisementReviewsFilter
{
    public Guid AdvertisementId { get; }
    public int Page { get; }
    public int PageSize { get; }
    public SortTypes SortType { get; }
    public bool IsDesc { get; }
    
    public AdvertisementReviewsFilter(
        Guid advertisementId, 
        int page = 1, 
        int pageSize = 10, 
        SortTypes sortType = SortTypes.None,
        bool isDesc = false
    )
    {
        AdvertisementId = advertisementId;
        Page = page;
        PageSize = pageSize;
        SortType = sortType;
        IsDesc = isDesc;
        
        var validator = new AdvertisementReviewsFilterValidator();
        validator.ValidateAndThrow(this);
    }
}