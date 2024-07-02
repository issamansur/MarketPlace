namespace MarketPlace.Domain.Entities.UserAdvertisements;

public class AdvertisementReview
{
    public Guid Id { get; private init; }
    public Guid AdvertisementId { get; private init; }
    public Guid CreatorId { get; private init; }
    
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    
    public DateTimeOffset DateCreated { get; private init; }
    public DateTimeOffset DateUpdated { get; private set; }

    private AdvertisementReview(
        Guid id,
        Guid advertisementId,
        Guid creatorId,
        int rating,
        string comment,
        DateTimeOffset dateCreated,
        DateTimeOffset dateUpdated
        )
    {
        // Validation
        if (id == Guid.Empty)
            throw new ArgumentException(DomainErrors.NullError(id));
        if (advertisementId == Guid.Empty)
            throw new ArgumentException(DomainErrors.NullError(advertisementId));
        if (creatorId == Guid.Empty)
            throw new ArgumentException(DomainErrors.NullError(creatorId));
        if (rating < Constraints.REVIEW_MIN_RATING || rating > Constraints.REVIEW_MAX_RATING)
            throw new ArgumentException(DomainErrors.ReviewRatingError);
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException(DomainErrors.NullError(comment));
        if (comment.Length < Constraints.REVIEW_MIN_COMMENT_LENGTH || comment.Length > Constraints.REVIEW_MAX_COMMENT_LENGTH)
            throw new ArgumentException(DomainErrors.ReviewCommentLengthError);
        if (dateCreated > dateUpdated)
            throw new ArgumentException(DomainErrors.InvalidError(dateCreated));
        
        // Set properties
        Id = id;
        AdvertisementId = advertisementId;
        CreatorId = creatorId;
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
    
    public void Update(int rating, string comment)
    {
        if (rating < Constraints.REVIEW_MIN_RATING || rating > Constraints.REVIEW_MAX_RATING)
            throw new ArgumentException(DomainErrors.ReviewRatingError);
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException(DomainErrors.NullError(comment));
        if (comment.Length < Constraints.REVIEW_MIN_COMMENT_LENGTH || comment.Length > Constraints.REVIEW_MAX_COMMENT_LENGTH)
            throw new ArgumentException(DomainErrors.ReviewCommentLengthError);
        
        Rating = rating;
        Comment = comment;
        DateUpdated = DateTimeOffset.UtcNow;
    }
}