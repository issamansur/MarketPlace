namespace MarketPlace.Application.Features.AdvertisementReviews.Commands;

public class CreateAdvertisementReviewCommandValidator: AbstractValidator<CreateAdvertisementReviewCommand>
{
    public CreateAdvertisementReviewCommandValidator()
    {
        RuleFor(x => x.AdvertisementId)
            .NotNull().WithMessage("AdvertisementReviewId is required.")
            .NotEmpty().WithMessage("AdvertisementReviewId cannot be empty.");
        RuleFor(x => x.CreatorId)
            .NotNull().WithMessage("CreatorId is required.")
            .NotEmpty().WithMessage("CreatorId cannot be empty.");
        RuleFor(x => x.Comment)
            .MinimumLength(Constraints.REVIEW_MIN_COMMENT_LENGTH)
            .MaximumLength(Constraints.REVIEW_MAX_COMMENT_LENGTH)
            .WithMessage(DomainErrors.ReviewCommentLengthError);
        RuleFor(x => x.Rating)
            .InclusiveBetween(Constraints.REVIEW_MIN_RATING, Constraints.REVIEW_MAX_RATING)
            .WithMessage(DomainErrors.ReviewRatingError);
    }
}