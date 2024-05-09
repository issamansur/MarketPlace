namespace MarketPlace.Application.AdvertisementReviews.Commands;

public class CreateAdvertisementReviewCommand: IRequest<Guid>
{
    public Guid AdvertisementId { get; }
    public Guid UserId { get; }
    public int Rating { get; }
    public string Comment { get; }

    public CreateAdvertisementReviewCommand(Guid advertisementId, Guid userId, string comment, int rating)
    {
        AdvertisementId = advertisementId;
        UserId = userId;
        Comment = comment;
        Rating = rating;
        
        var validator = new CreateAdvertisementReviewCommandValidator();
        validator.ValidateAndThrow(this);
    }
}