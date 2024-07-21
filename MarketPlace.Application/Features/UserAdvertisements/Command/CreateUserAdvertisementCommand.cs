namespace MarketPlace.Application.Features.UserAdvertisements.Command;

public class CreateUserAdvertisementCommand: IRequest<Guid>
{
    public Guid CreatorId { get; }
    public string Title { get; }
    public string Description { get; }
    public Stream? Image { get; }
    public string? Extension { get; }

    public CreateUserAdvertisementCommand(
        Guid creatorId,
        string title,
        string description,
        Stream? image,
        string? extension
        )
    {
        CreatorId = creatorId;
        Title = title;
        Description = description;
        Image = image;
        Extension = extension;
    }
}