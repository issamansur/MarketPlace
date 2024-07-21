namespace MarketPlace.Application.CQRS.UserAdvertisements.Filters;

public class UserAdvertisementsFilterValidator: AbstractValidator<UserAdvertisementsFilter>
{
    public UserAdvertisementsFilterValidator()
    {
        RuleFor(x => x.Query)
            .NotNull().WithMessage(ApplicationErrors.InvalidQueryError);
        
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage(ApplicationErrors.InvalidPageError);
        
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage(ApplicationErrors.InvalidPageSizeError);
        
        RuleFor(x => x.UserAdvertisementSortType)
            .IsInEnum().WithMessage(ApplicationErrors.InvalidSortTypeError);
    }
}