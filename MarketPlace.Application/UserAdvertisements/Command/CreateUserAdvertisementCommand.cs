namespace MarketPlace.Application.UserAdvertisements.Command;

public class CreateUserAdvertisementCommand: IRequest<Guid>
{
    public Guid CreatorId { get; }
    public string Title { get; }
    public string Description { get; }
    public string? ImageUrl { get; }

    public CreateUserAdvertisementCommand(
        Guid creatorId,
        string title,
        string description,
        string? imageUrl
        )
    {
        CreatorId = creatorId;
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
    }
}