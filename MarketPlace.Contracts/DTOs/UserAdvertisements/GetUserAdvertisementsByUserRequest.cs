using MarketPlace.Application.Enums;

namespace MarketPlace.Contracts.DTOs.UserAdvertisements;

public record GetUserAdvertisementsByUserRequest(
    Guid UserId,
    int Page = 1, 
    int PageSize = 10, 
    SortTypes SortType = SortTypes.None,
    bool IsDesc = false
    );