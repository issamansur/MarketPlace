namespace MarketPlace.Application.CQRS.AdvertisementReviews.Commands;

public class UpdateAdvertisementReviewCommandValidator: AbstractValidator<UpdateAdvertisementReviewCommand>
{
    public UpdateAdvertisementReviewCommandValidator()
    {
        RuleFor(x => x.ChangerId)
            .NotNull().WithMessage("ChangerId is required.")
            .NotEmpty().WithMessage("ChangerId cannot be empty.");
        RuleFor(x => x.Comment)
            .MinimumLength(Constraints.REVIEW_MIN_COMMENT_LENGTH)
            .MaximumLength(Constraints.REVIEW_MAX_COMMENT_LENGTH)
            .WithMessage(DomainErrors.ReviewCommentLengthError);
        RuleFor(x => x.Rating)
            .InclusiveBetween(Constraints.REVIEW_MIN_RATING, Constraints.REVIEW_MAX_RATING)
            .WithMessage(DomainErrors.ReviewRatingError);
    }
}