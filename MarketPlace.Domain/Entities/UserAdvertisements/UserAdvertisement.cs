namespace MarketPlace.Domain.Entities.UserAdvertisements;

// Choose this name to avoid confusion with new possible entity for Advertisement
public class UserAdvertisement
{
    public Guid Id { get; private init; }
    public Guid CreatorId { get; private init; }
    
    public int Number { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string? ImageUrl { get; private set; }
    
    public int RatingSum { get; private set; }
    public int RatingCount { get; private set; }
    public double Rating { get; private set; }
    
    public DateTimeOffset DateCreated { get; private init; }
    public DateTimeOffset DateUpdated { get; private set; }
    public DateTimeOffset DateExpired { get; private set; }
    
    public bool IsActive { get; private set; }

    public UserAdvertisement(
        Guid id,
        Guid creatorId,
        
        int number,
        string title, 
        string description,
        string? imageUrl,
        
        int ratingSum,
        int ratingCount,
        double rating,
        
        DateTimeOffset dateCreated,
        DateTimeOffset dateUpdated,
        DateTimeOffset dateExpired,
        
        bool isActive
        )
    {
        // Validation
        if (id == Guid.Empty)
            throw new ArgumentException(DomainErrors.NullError(id));
        if (creatorId == Guid.Empty)
            throw new ArgumentException(DomainErrors.NullError(creatorId));
        
        if (number < 0)
            throw new ArgumentException(DomainErrors.InvalidError(number));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(DomainErrors.NullError(title));
        if (title.Length < Constraints.USER_AD_MIN_TITLE_LENGTH || title.Length > Constraints.USER_AD_MAX_TITLE_LENGTH)
            throw new ArgumentException(DomainErrors.UserAdTitleLengthError);
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException(DomainErrors.NullError(description));
        if (description.Length < Constraints.USER_AD_MIN_DESC_LENGTH || description.Length > Constraints.USER_AD_MAX_DESC_LENGTH)
            throw new ArgumentException(DomainErrors.UserAdDescLengthError);
        
        if (ratingSum < 0 || ratingSum > Constraints.REVIEW_MAX_RATING * ratingCount)
            throw new ArgumentException(DomainErrors.InvalidError(ratingSum));
        if (ratingCount < 0)
            throw new ArgumentException(DomainErrors.InvalidError(ratingCount));
        if (rating < 0 || rating > Constraints.REVIEW_MAX_RATING)
            throw new ArgumentException(DomainErrors.InvalidError(rating));
        
        if (dateCreated > dateUpdated || dateCreated > dateExpired)
            throw new ArgumentException(DomainErrors.InvalidError(dateCreated));
        
        // Set properties
        Id = id;
        CreatorId = creatorId;
        
        Title = title;
        Description = description;
        ImageUrl = imageUrl;

        RatingSum = ratingSum;
        RatingCount = ratingCount;
        Rating = rating;
        
        DateCreated = dateCreated;
        DateUpdated = dateUpdated;
        DateExpired = dateExpired;
        
        IsActive = isActive;
    }
    
    public static UserAdvertisement Create(
        Guid creatorId,
        string title, 
        string description,
        string? imageUrl
        )
    {
        var date = DateTimeOffset.UtcNow;
        
        return new UserAdvertisement(
            id: Guid.NewGuid(),
            creatorId,
            
            number: 0,
            title,
            description,
            imageUrl,
            
            ratingSum: 0,
            ratingCount: 0,
            rating: 0,
            
            dateCreated: date,
            dateUpdated: date,
            dateExpired: date.AddDays(Constraints.USER_AD_DAYS_TO_EXPIRE),
            
            isActive: true
            );
    }
    
    public void Update(
        string title, 
        string description,
        string? imageUrl
        )
    {
        
        // Validation
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(DomainErrors.NullError(title));
        if (title.Length < Constraints.USER_AD_MIN_TITLE_LENGTH || title.Length > Constraints.USER_AD_MAX_TITLE_LENGTH)
            throw new ArgumentException(DomainErrors.UserAdTitleLengthError);
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException(DomainErrors.NullError(description));
        if (description.Length < Constraints.USER_AD_MIN_DESC_LENGTH || description.Length > Constraints.USER_AD_MAX_DESC_LENGTH)
            throw new ArgumentException(DomainErrors.UserAdDescLengthError);
        
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        
        var date = DateTimeOffset.UtcNow;
        
        DateUpdated = date;
        DateExpired = date.AddDays(Constraints.USER_AD_DAYS_TO_EXPIRE);
    }
    
    // Use SQL query to add new review and update rating instead of this method
    /*
    public void AddRating(int rating)
    {
        RatingSum += rating;
        RatingCount++;
    }
    */
}