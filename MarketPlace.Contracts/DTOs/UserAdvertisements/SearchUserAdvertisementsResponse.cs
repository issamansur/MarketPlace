namespace MarketPlace.Contracts.DTOs.UserAdvertisements;

public record SearchUserAdvertisementsResponse(
    IEnumerable<GetUserAdvertisementResponse> UserAdvertisements
    );