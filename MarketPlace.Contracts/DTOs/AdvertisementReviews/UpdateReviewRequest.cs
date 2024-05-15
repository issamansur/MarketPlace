namespace MarketPlace.Contracts.DTOs.AdvertisementReviews;

// TODO: ChangerId should be generated from the token
public record UpdateReviewRequest(
    Guid Id,
    Guid ChangerId, // !!!
    string Comment,
    int Rating
    );