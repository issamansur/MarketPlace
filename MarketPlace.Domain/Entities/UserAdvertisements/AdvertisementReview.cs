namespace MarketPlace.Domain.Entities.UserAdvertisements;

public class AdvertisementReview
{
    public Guid Id { get; private set; }
    public Guid AdvertisementId { get; private set; }
    public Guid ReviewerId { get; private set; }
    
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    
    public DateTimeOffset DateCreated { get; private set; }
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
        return new AdvertisementReview(
            Guid.NewGuid(),
            advertisementId,
            reviewerId,
            rating,
            comment,
            DateTimeOffset.Now,
            DateTimeOffset.Now
        );
    }
    
    public void UpdateReview(int rating, string comment)
    {
        Rating = rating;
        Comment = comment;
        DateUpdated = DateTimeOffset.Now;
    }
}