namespace MarketPlace.Contracts.DTOs;

public record ReviewDTO(
    Guid ReviewId,
    string Comment,
    int Rating,
    string DateCreated,
    string DateUpdated
    );