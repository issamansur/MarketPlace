namespace MarketPlace.Application.CQRS.Roles.Commands;

public class CreateRoleCommand: IRequest<Guid>
{
    public string Title { get; }

    public CreateRoleCommand(string title)
    {
        Title = title;
        
        var validator = new CreateRoleCommandValidator();
        validator.ValidateAndThrow(this);
    }
}