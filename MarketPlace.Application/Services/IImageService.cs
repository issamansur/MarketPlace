namespace MarketPlace.Application.Services;

public interface IImageService
{
    Task UploadImageAsync(Stream image, string directory, string fileWithExtension);
    Task UpdateImageAsync(Stream image, string directory, string fileWithExtension);
    Task DeleteImageAsync(string imagePath);
}