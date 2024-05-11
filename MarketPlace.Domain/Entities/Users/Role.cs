namespace MarketPlace.Domain.Entities.Users;

// Add this class, because we can have multiple roles in the future (Not only Users and Admin )
public class Role
{
    public Guid Id { get; private init; }
    public string Title { get; private set; }

    public Role(Guid id, string title)
    {
        // Validation
        if (id == Guid.Empty)
            throw new ArgumentException(DomainErrors.NullError(id));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(DomainErrors.NullError(title));
        if (title.Length < Constraints.ROLE_MIN_TITLE_LENGTH || title.Length > Constraints.ROLE_MAX_TITLE_LENGTH)
            throw new ArgumentException(DomainErrors.RoleTitleLengthError);
        
        // Set properties
        Id = id;
        Title = title;
    }
    
    public static Role Create(string title)
    {
        return new Role(Guid.NewGuid(), title);
    }
}