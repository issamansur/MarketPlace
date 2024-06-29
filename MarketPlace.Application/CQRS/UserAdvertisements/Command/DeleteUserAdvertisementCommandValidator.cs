namespace MarketPlace.Application.CQRS.UserAdvertisements.Command;

public class DeleteUserAdvertisementCommandValidator: AbstractValidator<DeleteUserAdvertisementCommand>
{
    public DeleteUserAdvertisementCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required.")
            .NotEmpty().WithMessage("Id cannot be empty.");
        
        RuleFor(x => x.ChangerId)
            .NotNull().WithMessage("ChangerId is required.")
            .NotEmpty().WithMessage("ChangerId cannot be empty.");
    }
}