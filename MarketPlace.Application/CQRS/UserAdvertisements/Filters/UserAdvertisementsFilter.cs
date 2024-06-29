using MarketPlace.Application.Enums;

namespace MarketPlace.Application.CQRS.UserAdvertisements.Filters;

public class UserAdvertisementsFilter
{
    public int Page { get; }
    public int PageSize { get; }
    public SortTypes SortType { get; }
    public bool IsDesc { get; }
    
    public UserAdvertisementsFilter(
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
        
        var validator = new UserAdvertisementsFilterValidator();
        validator.ValidateAndThrow(this);
    }
}