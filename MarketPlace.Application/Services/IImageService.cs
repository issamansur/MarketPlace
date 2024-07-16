namespace MarketPlace.Application.Services;

public interface IImageService
{
    // Is better to use Options instead of properties
    IReadOnlyCollection<string> AllowedExtensions { get; }
    Task SaveImageAsync(Stream image, string imagePath, CancellationToken cancellationToken);
    void DeleteImage(string imagePath);
    Task<Stream> GetResizedImageAsync(string imagePath, int width, int height, CancellationToken cancellationToken);
}