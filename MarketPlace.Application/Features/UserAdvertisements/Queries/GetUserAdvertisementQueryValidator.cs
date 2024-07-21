namespace MarketPlace.Application.CQRS.UserAdvertisements.Queries;

public class GetUserAdvertisementQueryValidator: AbstractValidator<GetUserAdvertisementQuery>
{
    public GetUserAdvertisementQueryValidator()
    {
        RuleFor(x => x.UserAdvertisementId)
            .NotEmpty().WithMessage("UserAdvertisementId is required.");
    }
}