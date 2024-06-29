namespace MarketPlace.Application.CQRS.Users.Commands;

public class CreateUserCommand: IRequest<Guid>
{
    public Guid RoleId { get; }
    public string Name { get; }

    public CreateUserCommand(string name, Guid roleId)
    {
        RoleId = roleId;
        Name = name;
        
        var validator = new CreateUserCommandValidator();
        validator.ValidateAndThrow(this);
    }
}