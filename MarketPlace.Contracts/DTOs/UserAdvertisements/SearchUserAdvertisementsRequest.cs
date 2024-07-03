using MarketPlace.Application.Enums;

namespace MarketPlace.Contracts.DTOs.UserAdvertisements;

public record SearchUserAdvertisementsRequest(
    string Query = "",
    int Page = 1, 
    int PageSize = 10, 
    UserAdvertisementSortTypes UserAdvertisementSortType = UserAdvertisementSortTypes.None,
    bool IsDesc = false
    );