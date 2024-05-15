namespace MarketPlace.Application.Users.Commands;

public class CreateUserCommand: IRequest<Guid>
{
    // TODO: Need to create entity for Account (1-1 with User) with Email and Password. Will change to CreateAccountCommand
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