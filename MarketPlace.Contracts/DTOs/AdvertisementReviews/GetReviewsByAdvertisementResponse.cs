using MarketPlace.Domain.Entities.UserAdvertisements;

namespace MarketPlace.Contracts.DTOs.AdvertisementReviews;

public record GetReviewsByAdvertisementResponse(
    IEnumerable<ReviewDTO> Reviews
    );