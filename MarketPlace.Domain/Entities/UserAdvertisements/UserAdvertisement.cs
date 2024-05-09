namespace MarketPlace.Domain.Entities.UserAdvertisements;

// Choose this name to avoid confusion with new possible entity for Advertisement
public class UserAdvertisement
{
    public Guid Id { get; private set; }
    public Guid CreatorId { get; private set; }
    
    public int Number { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string? ImageUrl { get; private set; }
    
    public int SumRating { get; private set; }
    public int CountRating { get; private set; }
    public double Rating => CountRating == 0 ? 0 : (double)SumRating / CountRating;
    
    public DateTimeOffset DateCreated { get; private set; }
    public DateTimeOffset DateUpdated { get; private set; }
    public DateTimeOffset DateExpired { get; private set; }
    public bool IsDeleted { get; private set; }

    public UserAdvertisement(
        Guid id,
        Guid creatorId,
        
        int number,
        string title, 
        string description,
        string? imageUrl,
        
        int sumRating,
        int countRating,
        
        DateTimeOffset dateCreated,
        DateTimeOffset dateUpdated,
        DateTimeOffset dateExpired,
        bool isDeleted
        )
    {
        Id = id;
        CreatorId = creatorId;
        
        Number = number;
        Title = title;
        Description = description;
        ImageUrl = imageUrl;

        SumRating = sumRating;
        CountRating = countRating;
        
        DateCreated = dateCreated;
        DateUpdated = dateUpdated;
        DateExpired = dateExpired;
        
        IsDeleted = isDeleted;
    }
    
    public static UserAdvertisement Create(
        Guid creatorId,
        int number,
        string title, 
        string description,
        string? imageUrl
        )
    {
        return new UserAdvertisement(
            Guid.NewGuid(),
            creatorId,
            number,
            title,
            description,
            imageUrl,
            0,
            0,
            DateTimeOffset.Now,
            DateTimeOffset.Now,
            DateTimeOffset.Now,
            false
            );
    }
    
    public void Update(
        int number,
        string title, 
        string description,
        string? imageUrl
        )
    {
        Number = number;
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        
        DateUpdated = DateTimeOffset.Now;
    }
    
    public void AddRating(int rating)
    {
        SumRating += rating;
        CountRating++;
    }
}