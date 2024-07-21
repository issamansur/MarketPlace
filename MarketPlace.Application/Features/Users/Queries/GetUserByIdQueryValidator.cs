namespace MarketPlace.Application.CQRS.Users.Queries;

public class GetUserByIdQueryValidator: AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required.")
            .NotEmpty().WithMessage("Id cannot be empty.");
    }
}