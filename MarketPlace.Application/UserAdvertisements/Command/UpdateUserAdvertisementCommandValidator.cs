namespace MarketPlace.Application.UserAdvertisements.Command;

public class UpdateUserAdvertisementCommandValidator: AbstractValidator<UpdateUserAdvertisementCommand>
{
    public UpdateUserAdvertisementCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        
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