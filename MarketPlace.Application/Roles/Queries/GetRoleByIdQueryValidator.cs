namespace MarketPlace.Application.Roles.Queries;

public class GetRoleByIdQueryValidator: AbstractValidator<GetRoleByIdQuery>
{
    public GetRoleByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required.")
            .NotEmpty().WithMessage("Id cannot be empty.");
        // No need to check for empty guid:
        // .NotEqual(Guid.Empty).WithMessage("Id cannot be empty guid.");
        // .NotEmpty() already checks for empty guid.
    }
}