namespace MarketPlace.Application.CQRS.Roles.Commands;

public class CreateRoleCommandValidator: AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Title is required.")
            .MinimumLength(Constraints.ROLE_MIN_TITLE_LENGTH)
            .MaximumLength(Constraints.ROLE_MAX_TITLE_LENGTH).WithMessage(DomainErrors.RoleTitleLengthError);
    }
}