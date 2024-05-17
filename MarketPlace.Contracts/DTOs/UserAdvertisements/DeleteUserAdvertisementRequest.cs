namespace MarketPlace.Contracts.DTOs.UserAdvertisements;

// TODO: ChangerId should be generated from the token
public record DeleteUserAdvertisementRequest(Guid Id, Guid ChangerId);