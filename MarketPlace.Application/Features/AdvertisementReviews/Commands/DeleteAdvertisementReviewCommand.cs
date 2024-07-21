namespace MarketPlace.Application.Features.AdvertisementReviews.Commands;

public class DeleteAdvertisementReviewCommand: IRequest
{
    public Guid ReviewId { get; }

    public DeleteAdvertisementReviewCommand(Guid reviewId)
    {
        ReviewId = reviewId;

        var validator = new DeleteAdvertisementReviewCommandValidator();
        validator.ValidateAndThrow(this);
    }
}