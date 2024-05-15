using Microsoft.AspNetCore.Http;

namespace MarketPlace.Contracts.DTOs.UserAdvertisements;

public record CreateUserAdvertisementRequest(
    Guid CreatorId,
    string Title,
    string Description,
    IFormFile? Image
    );