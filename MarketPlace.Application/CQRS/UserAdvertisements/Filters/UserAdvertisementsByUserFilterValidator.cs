namespace MarketPlace.Application.CQRS.UserAdvertisements.Filters;

public class UserAdvertisementsByUserFilterValidator: AbstractValidator<UserAdvertisementsByUserFilter>
{
    public UserAdvertisementsByUserFilterValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
        
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage(ApplicationErrors.InvalidPageError);
        
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage(ApplicationErrors.InvalidPageSizeError);
        
        RuleFor(x => x.UserAdvertisementSortType)
            .IsInEnum().WithMessage(ApplicationErrors.InvalidSortTypeError);
    }
}