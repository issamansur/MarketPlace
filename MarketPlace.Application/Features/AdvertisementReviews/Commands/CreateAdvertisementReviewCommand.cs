namespace MarketPlace.Application.Features.AdvertisementReviews.Commands;

public class CreateAdvertisementReviewCommand: IRequest<Guid>
{
    public Guid AdvertisementId { get; }
    public Guid CreatorId { get; }
    public int Rating { get; }
    public string Comment { get; }

    public CreateAdvertisementReviewCommand(Guid advertisementId, Guid creatorId, string comment, int rating)
    {
        AdvertisementId = advertisementId;
        CreatorId = creatorId;
        Comment = comment;
        Rating = rating;
        
        var validator = new CreateAdvertisementReviewCommandValidator();
        validator.ValidateAndThrow(this);
    }
}