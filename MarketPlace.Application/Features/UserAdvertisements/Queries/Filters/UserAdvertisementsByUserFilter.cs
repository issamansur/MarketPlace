using MarketPlace.Application.Enums;

namespace MarketPlace.Application.Features.UserAdvertisements.Queries.Filters;

public class UserAdvertisementsByUserFilter
{
    public Guid UserId { get; }
    public int Page { get; }
    public int PageSize { get; }
    public UserAdvertisementSortTypes UserAdvertisementSortType { get; }
    public bool IsDesc { get; }
    
    public UserAdvertisementsByUserFilter(
        Guid userId,
        int page = 1, 
        int pageSize = 10, 
        UserAdvertisementSortTypes userAdvertisementSortType = UserAdvertisementSortTypes.None,
        bool isDesc = false
    )
    {
        UserId = userId;
        Page = page;
        PageSize = pageSize;
        UserAdvertisementSortType = userAdvertisementSortType;
        IsDesc = isDesc;
        
        var validator = new UserAdvertisementsByUserFilterValidator();
        validator.ValidateAndThrow(this);
    }
}