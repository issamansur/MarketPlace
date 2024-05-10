namespace MarketPlace.Domain.Entities.Users;

public class User
{
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    
    public User(Guid id, string name)
    {
        // Validation
        if (id == Guid.Empty)
            throw new ArgumentException(Errors.NullError(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(Errors.NullError(name));
        if (name.Length < Constraints.USER_MIN_NAME_LENGTH || name.Length > Constraints.USER_MAX_NAME_LENGTH)
            throw new ArgumentException(Errors.UserNameLengthError);
        
        // Set properties
        Id = id;
        Name = name;
    }
    
    public static User Create(string name)
    {
        return new User(Guid.NewGuid(), name);
    }
}