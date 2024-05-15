namespace MarketPlace.Contracts.DTOs.AdvertisementReviews;

public record GetReviewsByAdvertisementResponse(
    IEnumerable<GetReviewResponse> Reviews
    );