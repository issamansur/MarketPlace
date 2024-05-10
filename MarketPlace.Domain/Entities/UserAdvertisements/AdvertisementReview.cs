namespace MarketPlace.Domain.Entities.UserAdvertisements;

public class AdvertisementReview
{
    public Guid Id { get; private init; }
    public Guid AdvertisementId { get; private init; }
    public Guid ReviewerId { get; private init; }
    
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    
    public DateTimeOffset DateCreated { get; private init; }
    public DateTimeOffset DateUpdated { get; private set; }

    public AdvertisementReview(
        Guid id,
        Guid advertisementId,
        Guid reviewerId,
        int rating,
        string comment,
        DateTimeOffset dateCreated,
        DateTimeOffset dateUpdated
        )
    {
        // Validation
        if (id == Guid.Empty)
            throw new ArgumentException(Errors.NullError(id));
        if (advertisementId == Guid.Empty)
            throw new ArgumentException(Errors.NullError(advertisementId));
        if (reviewerId == Guid.Empty)
            throw new ArgumentException(Errors.NullError(reviewerId));
        if (rating < Constraints.REVIEW_MIN_RATE || rating > Constraints.REVIEW_MAX_RATE)
            throw new ArgumentException(Errors.ReviewRateError);
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException(Errors.NullError(comment));
        if (comment.Length < Constraints.REVIEW_MIN_COMMENT_LENGTH || comment.Length > Constraints.REVIEW_MAX_COMMENT_LENGTH)
            throw new ArgumentException(Errors.ReviewCommentLengthError);
        if (dateCreated > dateUpdated)
            throw new ArgumentException(Errors.InvalidError(dateCreated));
        
        // Set properties
        Id = id;
        AdvertisementId = advertisementId;
        ReviewerId = reviewerId;
        Rating = rating;
        Comment = comment;
        DateCreated = dateCreated;
        DateUpdated = dateUpdated;
    }
    
    public static AdvertisementReview Create(
        Guid advertisementId,
        Guid reviewerId,
        int rating,
        string comment
        )
    {
        var date = DateTimeOffset.UtcNow;
        
        return new AdvertisementReview(
            id: Guid.NewGuid(),
            advertisementId,
            reviewerId,
            rating,
            comment,
            dateCreated: date,
            dateUpdated: date
        );
    }
    
    public void UpdateReview(int rating, string comment)
    {
        if (rating < Constraints.REVIEW_MIN_RATE || rating > Constraints.REVIEW_MAX_RATE)
            throw new ArgumentException(Errors.ReviewRateError);
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException(Errors.NullError(comment));
        if (comment.Length < Constraints.REVIEW_MIN_COMMENT_LENGTH || comment.Length > Constraints.REVIEW_MAX_COMMENT_LENGTH)
            throw new ArgumentException(Errors.ReviewCommentLengthError);
        
        Rating = rating;
        Comment = comment;
        DateUpdated = DateTimeOffset.UtcNow;
    }
}