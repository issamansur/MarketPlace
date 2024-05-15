namespace MarketPlace.Contracts.DTOs;

public record UserAdvertisementDTO(
    Guid Id,
        
    int Number,
    string Title, 
    string Description,
    string? ImageUrl,
    
    double Rating,
        
    string DateCreated,
    string DateUpdated,
    string DateExpired
    );