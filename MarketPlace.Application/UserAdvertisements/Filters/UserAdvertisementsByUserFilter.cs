using MarketPlace.Application.Enums;

namespace MarketPlace.Application.UserAdvertisements.Filters;

public class UserAdvertisementsByUserFilter
{
    public Guid UserId { get; }
    public int Page { get; }
    public int PageSize { get; }
    public SortTypes SortType { get; }
    public bool IsDesc { get; }
    
    public UserAdvertisementsByUserFilter(
        Guid userId,
        int page = 1, 
        int pageSize = 10, 
        SortTypes sortType = SortTypes.None,
        bool isDesc = false
    )
    {
        Page = page;
        PageSize = pageSize;
        SortType = sortType;
        IsDesc = isDesc;
        
        var validator = new UserAdvertisementsByUserFilterValidator();
        validator.ValidateAndThrow(this);
    }
}