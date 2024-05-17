using MarketPlace.Application.Services;

namespace MarketPlace.Infrastructure.Services;

// TODO: Refactor this class
// TODO: Look at the possibility of using the IFileProvider interface
// TODO: Look at the possibility of added a methods to get the resized image (Use the ImageMagick library) (or ImageSharp)
public class ImageService: IImageService
{
    private const int MaxImageSize = 2 * 1024 * 1024;
    private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png"];
    
    public async Task UploadImageAsync(Stream image, string directory, string fileName)
    {
        if (image.Length > MaxImageSize)
        {
            throw new ArgumentException("Image is too large");
        }

        var extension = Path.GetExtension(fileName);
        if (!_allowedExtensions.Contains(extension))
        {
            throw new ArgumentException("Invalid file extension");
        }
        
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var filePath = Path.Combine(directory, fileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await image.CopyToAsync(fileStream);
    }

    public async Task DeleteImageAsync(string imagePath)
    {
        // TODO: Check on correctness
        if (File.Exists(imagePath))
        {
            await Task.Run(() => File.Delete(imagePath));
        }
    }
    
    public async Task UpdateImageAsync(Stream image, string directory, string fileName)
    {
        if (image.Length > MaxImageSize)
        {
            throw new ArgumentException("Image is too large");
        }
        
        if (!_allowedExtensions.Contains(Path.GetExtension(fileName)))
        {
            throw new ArgumentException("Invalid file extension");
        }
        
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            await UploadImageAsync(image, directory, fileName);
        }
        else
        {
            string tempFileName = "temp_" + fileName;
            await UploadImageAsync(image, directory, tempFileName);
            await DeleteImageAsync(Path.Combine(directory, fileName));
            File.Move(Path.Combine(directory,tempFileName), Path.Combine(directory, fileName));
        }
    }
}