namespace MarketPlace.Application.UserAdvertisements.Command;

public class DeleteUserAdvertisementCommand: IRequest
{
    public Guid Id { get; }
    public Guid ChangerId { get; }

    public DeleteUserAdvertisementCommand(Guid id, Guid changerId)
    {
        Id = id;
        ChangerId = changerId;
        
        var validator = new DeleteUserAdvertisementCommandValidator();
        validator.ValidateAndThrow(this);
    }
}