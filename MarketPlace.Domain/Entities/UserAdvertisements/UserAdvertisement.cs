namespace MarketPlace.Domain.Entities.UserAdvertisements;

// Choose this name to avoid confusion with new possible entity for Advertisement
public class UserAdvertisement
{
    public Guid Id { get; private init; }
    public Guid CreatorId { get; private init; }
    
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string? ImageUrl { get; private set; }
    
    public int SumRating { get; private set; }
    public int CountRating { get; private set; }
    public double Rating => CountRating == 0 ? 0 : (double)SumRating / CountRating;
    
    public DateTimeOffset DateCreated { get; private init; }
    public DateTimeOffset DateUpdated { get; private set; }
    public DateTimeOffset DateExpired { get; private set; }
    
    public bool IsActive { get; private set; }

    public UserAdvertisement(
        Guid id,
        Guid creatorId,
        
        string title, 
        string description,
        string? imageUrl,
        
        int sumRating,
        int countRating,
        
        DateTimeOffset dateCreated,
        DateTimeOffset dateUpdated,
        DateTimeOffset dateExpired,
        
        bool isActive
        )
    {
        // Validation
        if (id == Guid.Empty)
            throw new ArgumentException(Errors.NullError(id));
        if (creatorId == Guid.Empty)
            throw new ArgumentException(Errors.NullError(creatorId));
        
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(Errors.NullError(title));
        if (title.Length < Constraints.USER_AD_MIN_TITLE_LENGTH || title.Length > Constraints.USER_AD_MAX_TITLE_LENGTH)
            throw new ArgumentException(Errors.UserAdTitleLengthError);
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException(Errors.NullError(description));
        if (description.Length < Constraints.USER_AD_MIN_DESC_LENGTH || description.Length > Constraints.USER_AD_MAX_DESC_LENGTH)
            throw new ArgumentException(Errors.UserAdDescLengthError);
        
        if (sumRating < 0 || sumRating > Constraints.REVIEW_MAX_RATE * countRating)
            throw new ArgumentException(Errors.InvalidError(sumRating));
        if (countRating < 0)
            throw new ArgumentException(Errors.InvalidError(countRating));
        
        if (dateCreated > dateUpdated || dateCreated > dateExpired)
            throw new ArgumentException(Errors.InvalidError(dateCreated));
        
        
        // Set properties
        Id = id;
        CreatorId = creatorId;
        
        Title = title;
        Description = description;
        ImageUrl = imageUrl;

        SumRating = sumRating;
        CountRating = countRating;
        
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
            
            title,
            description,
            imageUrl,
            
            sumRating: 0,
            countRating: 0,
            
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
        SumRating += rating;
        CountRating++;
    }
    */
}