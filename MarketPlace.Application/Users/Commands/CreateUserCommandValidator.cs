namespace MarketPlace.Application.Users.Commands;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(5).MaximumLength(20).WithMessage("Name must be between 5 and 20 characters.")
            .Matches("^[a-zA-Zа-яА-Я0-9]*$").WithMessage("Name must contain only letters and numbers.");
    }
}