using MarketPlace.Application.Enums;

namespace MarketPlace.Application.CQRS.UserAdvertisements.Filters;

public class UserAdvertisementsFilter
{
    public int Page { get; }
    public int PageSize { get; }
    public UserAdvertisementSortTypes UserAdvertisementSortType { get; }
    public bool IsDesc { get; }
    
    public UserAdvertisementsFilter(
        int page = 1, 
        int pageSize = 10, 
        UserAdvertisementSortTypes userAdvertisementSortType = UserAdvertisementSortTypes.None,
        bool isDesc = false
    )
    {
        Page = page;
        PageSize = pageSize;
        UserAdvertisementSortType = userAdvertisementSortType;
        IsDesc = isDesc;
        
        var validator = new UserAdvertisementsFilterValidator();
        validator.ValidateAndThrow(this);
    }
}