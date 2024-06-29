namespace MarketPlace.Application.CQRS.Roles.Queries;

public class GetRoleByIdQueryValidator: AbstractValidator<GetRoleByIdQuery>
{
    public GetRoleByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required.")
            .NotEmpty().WithMessage("Id cannot be empty.");
    }
}