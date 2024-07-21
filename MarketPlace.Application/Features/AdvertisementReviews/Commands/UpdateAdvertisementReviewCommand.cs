namespace MarketPlace.Application.Features.AdvertisementReviews.Commands;

public class UpdateAdvertisementReviewCommand: IRequest
{
    public Guid Id { get; }
    public Guid ChangerId { get; }
    public int Rating { get; }
    public string Comment { get; }

    public UpdateAdvertisementReviewCommand(
        Guid id,
        Guid changerId,
        string comment, 
        int rating
        )
    {
        Id = id;
        ChangerId = changerId;
        Comment = comment;
        Rating = rating;
        
        var validator = new UpdateAdvertisementReviewCommandValidator();
        validator.ValidateAndThrow(this);
    }
}