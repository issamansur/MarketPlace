namespace MarketPlace.Application.AdvertisementReviews.Commands;

public class CreateAdvertisementReviewCommandValidator: AbstractValidator<CreateAdvertisementReviewCommand>
{
    public CreateAdvertisementReviewCommandValidator()
    {
        RuleFor(x => x.AdvertisementId)
            .NotNull().WithMessage("AdvertisementId is required.")
            .NotEmpty().WithMessage("AdvertisementId cannot be empty.");
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId is required.")
            .NotEmpty().WithMessage("UserId cannot be empty.");
        RuleFor(x => x.Comment)
            .NotNull().WithMessage("Comment is required. If you don't want to leave a comment, please leave it empty.")
            .MaximumLength(500).WithMessage("Comment must not exceed 200 characters.");
        RuleFor(x => x.Rating)
            .NotNull().WithMessage("Rating is required.")
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
    }
}