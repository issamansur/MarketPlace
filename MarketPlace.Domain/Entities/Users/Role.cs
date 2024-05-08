namespace MarketPlace.Domain.Entities.Users;

// Add this class, because we can have multiple roles in the future (Not only Users and Admin )
public class Role
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }

    public Role(Guid id, string title)
    {
        Id = id;
        Title = title;
    }
    
    public static Role Create(string title)
    {
        return new Role(Guid.NewGuid(), title);
    }
}