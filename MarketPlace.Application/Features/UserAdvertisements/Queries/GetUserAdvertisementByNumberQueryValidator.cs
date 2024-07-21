namespace MarketPlace.Application.Features.UserAdvertisements.Queries;

public class GetUserAdvertisementByNumberQueryValidator: AbstractValidator<GetUserAdvertisementByNumberQuery>
{
    public GetUserAdvertisementByNumberQueryValidator()
    {
        RuleFor(x => x.UserAdvertisementNumber)
            .NotEmpty().WithMessage("UserAdvertisementNumber is required.");
    }
}