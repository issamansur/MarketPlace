namespace MarketPlace.Application.Roles.Commands;

public class CreateRoleCommandValidator: AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Title is required.")
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MaximumLength(20).WithMessage("Title must not exceed 20 characters.");
    }
}