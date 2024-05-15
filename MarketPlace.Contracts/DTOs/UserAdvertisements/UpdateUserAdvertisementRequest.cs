using Microsoft.AspNetCore.Http;

namespace MarketPlace.Contracts.DTOs.UserAdvertisements;

// TODO: ChangerId should be generated from the token
public record UpdateUserAdvertisementRequest(
    Guid Id,
    Guid ChangerId, // !!!
    string Title,
    string Description,
    IFormFile? Image
    );