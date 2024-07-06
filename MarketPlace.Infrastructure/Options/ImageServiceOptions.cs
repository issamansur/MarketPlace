namespace MarketPlace.Infrastructure.Options;

public class ImageServiceOptions
{
    public int MaxImageSizeMb { get; init; }
    public string ImagesDirectory { get; init; }
}