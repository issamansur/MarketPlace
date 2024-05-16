namespace MarketPlace.Application.Services;

public interface IImageService
{
    Task UploadImageAsync(Stream image, string imagePath);
    Task UpdateImageAsync(Stream image, string imagePath);
    Task DeleteImageAsync(string imagePath);
}