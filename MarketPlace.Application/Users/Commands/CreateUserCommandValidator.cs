namespace MarketPlace.Application.Users.Commands;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .MinimumLength(Constraints.USER_MIN_NAME_LENGTH)
            .MaximumLength(Constraints.USER_MAX_NAME_LENGTH).WithMessage(DomainErrors.UserNameLengthError)
            .Matches("^[a-zA-Zа-яА-Я0-9]*$").WithMessage("Name must contain only letters and numbers.");
    }
}