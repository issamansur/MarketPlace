namespace MarketPlace.Application.UserAdvertisements.Command;

public class UpdateUserAdvertisementCommand: IRequest<Guid>
{
    public Guid Id { get; }
    public Guid ChangerId { get; }
    public string Title { get; }
    public string Description { get; }
    public string? ImageUrl { get; }

    public UpdateUserAdvertisementCommand(
        Guid id,
        Guid changerId,
        string title,
        string description,
        string? imageUrl
        )
    {
        Id = id;
        ChangerId = changerId;
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        
        var validator = new UpdateUserAdvertisementCommandValidator();
        validator.ValidateAndThrow(this);
    }
}