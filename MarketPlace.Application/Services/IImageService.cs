namespace MarketPlace.Application.Services;

public interface IImageService
{
    Task SaveImageAsync(Stream image, string imagePath, CancellationToken cancellationToken);
    Task DeleteImageAsync(string imagePath, CancellationToken cancellationToken);
}