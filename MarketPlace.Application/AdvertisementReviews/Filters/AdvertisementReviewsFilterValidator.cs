namespace MarketPlace.Application.AdvertisementReviews.Filters;

public class AdvertisementReviewsFilterValidator: AbstractValidator<AdvertisementReviewsFilter>
{
    public AdvertisementReviewsFilterValidator()
    {
        RuleFor(x => x.AdvertisementId)
            .NotEmpty().WithMessage("AdvertisementId is required.");
        
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page must be greater than 0.");
        
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0.");
        
        RuleFor(x => x.SortType)
            .IsInEnum().WithMessage("SortType is not valid.");
    }
}