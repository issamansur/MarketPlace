using MarketPlace.Application.Services;
using MarketPlace.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace MarketPlace.Infrastructure.Services;

// TODO: Refactor this class
// TODO: Look at the possibility of using the IFileProvider interface
// TODO: Look at the possibility of added a methods to get the resized image (Use the ImageMagick library) (or ImageSharp)
public class ImageService: IImageService
{
    private readonly ImageServiceOptions _options;
    private int MaxImageSize => _options.MaxImageSizeMb * 1024 * 1024;
    private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png"];

    public ImageService(IOptions<ImageServiceOptions> options)
    {
        _options = options.Value;
    }
    
    private string GetPath(string fileName) => Path.Combine(_options.ImagesDirectory, fileName);
    
    public async Task SaveImageAsync(Stream image, string imagePath, CancellationToken cancellationToken)
    {
        var fullPath = GetPath(imagePath);
        
        if (image.Length > MaxImageSize)
        {
            throw new ArgumentException("Image is too large");
        }

        var extension = Path.GetExtension(imagePath);
        if (!_allowedExtensions.Contains(extension))
        {
            throw new ArgumentException("Invalid file extension");
        }

        string directoryPath = Path.GetDirectoryName(fullPath)!;
        Directory.CreateDirectory(directoryPath);
        
        await using var fileStream = new FileStream(fullPath, FileMode.Create);
        await image.CopyToAsync(fileStream, cancellationToken);
    }

    public async Task DeleteImageAsync(string imagePath, CancellationToken cancellationToken)
    {
        var fullPath = GetPath(imagePath);
        
        if (File.Exists(fullPath))
        {
            await Task.Run(() => File.Delete(fullPath), cancellationToken);
        }
    }
}