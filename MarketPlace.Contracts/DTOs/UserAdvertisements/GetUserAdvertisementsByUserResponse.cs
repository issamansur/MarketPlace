namespace MarketPlace.Contracts.DTOs.UserAdvertisements;

public record GetUserAdvertisementsByUserResponse(
    IEnumerable<GetUserAdvertisementResponse> UserAdvertisements
    );