namespace MarketPlace.Domain.Entities.Users;

public class User
{
    public Guid Id { get; private init; }
    public Guid RoleId { get; private init; }
    public string Name { get; private set; }
    
    public User(Guid id, Guid roleId, string name)
    {
        // Validation
        if (id == Guid.Empty)
            throw new ArgumentException(DomainErrors.NullError(id));
        if (roleId == Guid.Empty)
            throw new ArgumentException(DomainErrors.NullError(roleId));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(DomainErrors.NullError(name));
        if (name.Length < Constraints.USER_MIN_NAME_LENGTH || name.Length > Constraints.USER_MAX_NAME_LENGTH)
            throw new ArgumentException(DomainErrors.UserNameLengthError);
        
        // Set properties
        Id = id;
        RoleId = roleId;
        Name = name;
    }
    
    public static User Create(Guid roleId, string name)
    {
        return new User(Guid.NewGuid(), roleId, name);
    }
}