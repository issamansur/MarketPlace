namespace MarketPlace.Contracts.DTOs.UserAdvertisements;

public record GetUserAdvertisementResponse(
    Guid Id,
    Guid CreatorId,
        
    int Number,
    string Title, 
    string Description,
    string? ImageUrl,
    
    double Rating,
        
    DateTimeOffset DateCreated,
    DateTimeOffset DateUpdated,
    DateTimeOffset DateExpired
    );