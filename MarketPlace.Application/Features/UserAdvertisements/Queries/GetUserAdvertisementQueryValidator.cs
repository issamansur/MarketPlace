namespace MarketPlace.Application.Features.UserAdvertisements.Queries;

public class GetUserAdvertisementQueryValidator: AbstractValidator<GetUserAdvertisementQuery>
{
    public GetUserAdvertisementQueryValidator()
    {
        RuleFor(x => x.UserAdvertisementId)
            .NotEmpty().WithMessage("UserAdvertisementId is required.");
    }
}