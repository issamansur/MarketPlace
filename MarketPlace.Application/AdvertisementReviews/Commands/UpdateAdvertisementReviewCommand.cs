namespace MarketPlace.Application.AdvertisementReviews.Commands;

public class UpdateAdvertisementReviewCommand: IRequest<Guid>
{
    public Guid Id { get; }
    public Guid AdvertisementId { get; }
    public Guid ChangerId { get; }
    public int Rating { get; }
    public string Comment { get; }

    public UpdateAdvertisementReviewCommand(
        Guid id,
        Guid advertisementId,
        Guid changerId,
        string comment, 
        int rating
        )
    {
        Id = id;
        AdvertisementId = advertisementId;
        ChangerId = changerId;
        Comment = comment;
        Rating = rating;
        
        var validator = new UpdateAdvertisementReviewCommandValidator();
        validator.ValidateAndThrow(this);
    }
}