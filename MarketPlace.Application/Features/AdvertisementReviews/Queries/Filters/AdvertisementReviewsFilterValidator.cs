namespace MarketPlace.Application.Features.AdvertisementReviews.Queries.Filters;

public class AdvertisementReviewsFilterValidator: AbstractValidator<AdvertisementReviewsFilter>
{
    public AdvertisementReviewsFilterValidator()
    {
        // Bad solution, with hardcoded values
        RuleFor(x => x.AdvertisementId)
            .NotEmpty().WithMessage("AdvertisementId is required.");
        
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage(ApplicationErrors.InvalidPageError);
        
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage(ApplicationErrors.InvalidPageSizeError);
        
        RuleFor(x => x.AdvertisementReviewSortType)
            .IsInEnum().WithMessage(ApplicationErrors.InvalidSortTypeError);
    }
}