namespace MarketPlace.Application.Features.AdvertisementReviews.Commands;

public class DeleteAdvertisementReviewCommandValidator: AbstractValidator<DeleteAdvertisementReviewCommand>
{
    public DeleteAdvertisementReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId)
            .NotNull().WithMessage("AdvertisementReviewId is required.")
            .NotEmpty().WithMessage("AdvertisementReviewId cannot be empty.");
    }
}