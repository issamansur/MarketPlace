namespace MarketPlace.Contracts.DTOs.UserAdvertisements;

public record GetUserAdvertisementsResponse(
    IEnumerable<UserAdvertisementDTO> UserAdvertisements
    );