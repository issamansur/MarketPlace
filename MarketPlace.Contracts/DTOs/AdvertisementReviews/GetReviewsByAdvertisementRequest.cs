using MarketPlace.Application.Enums;

namespace MarketPlace.Contracts.DTOs.AdvertisementReviews;

public record GetReviewsByAdvertisementRequest(
    Guid AdvertisementId, 
    int Page = 1, 
    int PageSize = 10, 
    AdvertisementReviewSortTypes AdvertisementReviewSortType = AdvertisementReviewSortTypes.None,
    bool IsDesc = false
    );