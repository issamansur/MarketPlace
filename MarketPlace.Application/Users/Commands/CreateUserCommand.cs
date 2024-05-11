namespace MarketPlace.Application.Users.Commands;

public class CreateUserCommand: IRequest<Guid>
{
    // TODO: Need to create entity for Account (1-1 with User) with Email and Password. Will change to CreateAccountCommand
    public string Name { get; }
    public Guid RoleId { get; }

    public CreateUserCommand(string name, Guid roleId)
    {
        Name = name;
        RoleId = roleId;
        
        var validator = new CreateUserCommandValidator();
        validator.ValidateAndThrow(this);
    }
}