namespace MarketPlace.Contracts.DTOs.AdvertisementReviews;

public record GetReviewResponse(
    Guid Id,
    Guid AdvertisementId,
    Guid CreatorId,
    string Comment,
    int Rating,
    DateTimeOffset DateCreated,
    DateTimeOffset DateUpdated
    );