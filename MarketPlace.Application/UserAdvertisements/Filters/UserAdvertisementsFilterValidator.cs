namespace MarketPlace.Application.UserAdvertisements.Filters;

public class UserAdvertisementsFilterValidator: AbstractValidator<UserAdvertisementsFilter>
{
    public UserAdvertisementsFilterValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage(ApplicationErrors.InvalidPageError);
        
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage(ApplicationErrors.InvalidPageSizeError);
        
        RuleFor(x => x.SortType)
            .IsInEnum().WithMessage(ApplicationErrors.InvalidSortTypeError);
    }
}