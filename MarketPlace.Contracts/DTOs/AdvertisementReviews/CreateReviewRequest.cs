namespace MarketPlace.Contracts.DTOs.AdvertisementReviews;

// TODO: CreatorId should be generated from the token
public record CreateReviewRequest(
    Guid AdvertisementId, 
    Guid CreatorId, // !!!
    string Comment, 
    int Rating
    );