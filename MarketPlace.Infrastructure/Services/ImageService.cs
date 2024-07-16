using System.Runtime.Serialization.Formatters;
using MarketPlace.Application.Services;
using MarketPlace.Infrastructure.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace MarketPlace.Infrastructure.Services;

public class ImageService: IImageService
{
    private readonly StaticFilesOptions _optionsStaticFiles;
    private readonly ImageServiceOptions _optionsImageService;
    
    private int MaxImageSize => _optionsImageService.MaxSizeMb * 1024 * 1024;
    public IReadOnlyCollection<string> AllowedExtensions => _optionsImageService.AllowedExtensions.AsReadOnly();
    
    public ImageService(
        IOptions<StaticFilesOptions> optionsStaticFiles,
        IOptions<ImageServiceOptions> optionsImageService
    )
    {
        _optionsStaticFiles = optionsStaticFiles.Value;
        _optionsImageService = optionsImageService.Value;
    }
    
    private string GetPath(string imagePath) => 
        Path.Combine(
            Directory.GetCurrentDirectory(),
            _optionsStaticFiles.RealPath.TrimStart('/'),
            _optionsImageService.Directory.TrimStart('/'), 
            imagePath
        );
    
    public async Task SaveImageAsync(Stream image, string imagePath, CancellationToken cancellationToken)
    {
        var fullPath = GetPath(imagePath);
        
        if (image.Length > MaxImageSize)
        {
            throw new ArgumentException("Image is too large");
        }

        var extension = Path.GetExtension(imagePath);
        if (!AllowedExtensions.Contains(extension))
        {
            throw new ArgumentException("Invalid file extension");
        }

        string directoryPath = Path.GetDirectoryName(fullPath)!;
        Directory.CreateDirectory(directoryPath);
        
        await using var fileStream = new FileStream(fullPath, FileMode.Create);
        await image.CopyToAsync(fileStream, cancellationToken);
    }

    public void DeleteImage(string imagePath)
    {
        var fullPath = GetPath(imagePath);
        
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
    
    public async Task<Stream> GetResizedImageAsync(string imagePath, int width, int height, CancellationToken cancellationToken)
    {
        // Get the full path of the image
        var fullPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            imagePath
        );
        
        if (!File.Exists(fullPath))
        {
            throw new ArgumentException("Image does not exist");
        }
        
        using var image = await Image.LoadAsync(fullPath, cancellationToken);    
        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(width, height),
            Mode = ResizeMode.Max
        }));

        var memoryStream = new MemoryStream();
        await image.SaveAsJpegAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;

        return memoryStream;
    }
}