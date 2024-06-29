namespace MarketPlace.Application.CQRS.UserAdvertisements.Command;

public class CreateUserAdvertisementCommandValidator: AbstractValidator<CreateUserAdvertisementCommand>
{
    public CreateUserAdvertisementCommandValidator()
    {
        RuleFor(x => x.CreatorId)
            .NotEmpty().WithMessage("CreatorId is required.");
        
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Title is required.")
            .MinimumLength(Constraints.USER_AD_MIN_TITLE_LENGTH)
            .MaximumLength(Constraints.USER_AD_MAX_TITLE_LENGTH).WithMessage(DomainErrors.UserAdTitleLengthError);
        
        RuleFor(x => x.Description)
            .NotNull().WithMessage("Description is required.")
            .MinimumLength(Constraints.USER_AD_MIN_DESC_LENGTH)
            .MaximumLength(Constraints.USER_AD_MAX_DESC_LENGTH).WithMessage(DomainErrors.UserAdDescLengthError);
    }
}