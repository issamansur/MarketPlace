namespace MarketPlace.Application.Features.UserAdvertisements.Command;

public class UpdateUserAdvertisementCommand: IRequest
{
    public Guid Id { get; }
    public Guid ChangerId { get; }
    public string Title { get; }
    public string Description { get; }
    public Stream? Image { get; }
    public string? Extension { get; }

    public UpdateUserAdvertisementCommand(
        Guid id,
        Guid changerId,
        string title,
        string description,
        Stream? image,
        string? extension
        )
    {
        Id = id;
        ChangerId = changerId;
        Title = title;
        Description = description;
        Image = image;
        Extension = extension;
        
        var validator = new UpdateUserAdvertisementCommandValidator();
        validator.ValidateAndThrow(this);
    }
}