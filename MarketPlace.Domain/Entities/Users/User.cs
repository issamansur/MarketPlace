namespace MarketPlace.Domain.Entities.Users;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    
    public User(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public static User Create(string name)
    {
        return new User(Guid.NewGuid(), name);
    }
}