using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using MarketPlace.Infrastructure.Options;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace MarketPlace.Infrastructure.Services;

public class AwsImageService: IImageService
{
    private readonly IAmazonS3 _client;
    
    private readonly ImageServiceOptions _optionsImageService;
    private readonly AWSOptions _optionsAws;
    public IReadOnlyCollection<string> AllowedExtensions => 
        _optionsImageService.AllowedExtensions.AsReadOnly();
    private int MaxImageSize => _optionsImageService.MaxSizeMb * 1024 * 1024;
    
    public AwsImageService(
        IAmazonS3 client,
        IOptions<ImageServiceOptions> optionsImageService,
        IOptions<AWSOptions> optionsAws
        )
    {
        _optionsImageService = optionsImageService.Value;
        _optionsAws = optionsAws.Value;
        
        // If the AWS Region defined for your default user is different
        // from the Region where your Amazon S3 bucket is located,
        // pass the Region name to the S3 client object's constructor.
        // For example: RegionEndpoint.USWest2.
        //_client = new AmazonS3Client();
        _client = client;
    }
    
    private string GetPath(string imagePath) => 
        Path.Combine(
            _optionsImageService.Directory.TrimStart('/'), 
            imagePath
        ).Replace('\\', '/');
    
    private string GetResizedPath(string imagePath, int width, int height) => 
        Path.Combine(
            Path.GetDirectoryName(imagePath) ?? "",
            $"{Path.GetFileNameWithoutExtension(imagePath)}_{width}x{height}.jpeg"
        ).Replace('\\', '/');
    
    public async Task SaveImageAsync(Stream image, string imagePath, CancellationToken cancellationToken)
    {
        if (image.Length > MaxImageSize)
        {
            throw new ArgumentException("Image is too large");
        }
        
        var bucketName = _optionsAws.ImageBucketName;
        
        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = GetPath(imagePath),
            InputStream = image,
        };
        
        var response = await _client.PutObjectAsync(request, cancellationToken);
        
        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new ApplicationException("Failed to save image");
        }
    }

    public async Task<Stream> GetResizedImageAsync(string imagePath, int width, int height, CancellationToken cancellationToken)
    {
        var bucketName = _optionsAws.ImageBucketName;

        var getRequest = new GetObjectRequest
        {
            BucketName = bucketName,
            Key = imagePath,
        };

        // Issue request and remember to dispose of the response
        var response = await _client.GetObjectAsync(getRequest, cancellationToken);
        
        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new ApplicationException("Failed to get image");
        }
        
        var fullPathResized = GetResizedPath(imagePath, width, height);
        
        getRequest = new GetObjectRequest
        {
            BucketName = bucketName,
            Key = fullPathResized,
        };

        try
        {
            response = await _client.GetObjectAsync(getRequest, cancellationToken);
        }
        catch (AmazonS3Exception e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
            {
                using var image = await Image.LoadAsync(response.ResponseStream, cancellationToken);
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(width, height),
                    Mode = ResizeMode.Max
                }));

                var memoryStream = new MemoryStream();
                
                await image.SaveAsJpegAsync(memoryStream, cancellationToken);
                memoryStream.Position = 0;

                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fullPathResized,
                    InputStream = memoryStream,
                };

                await _client.PutObjectAsync(putRequest, cancellationToken);
                
                response = await _client.GetObjectAsync(getRequest, cancellationToken);
            }
            else
            {
                throw;
            }
        }
        
        return response.ResponseStream;
    }

    public async Task DeleteImageAsync(string imagePath, CancellationToken cancellationToken)
    {
        var bucketName = _optionsAws.ImageBucketName;
        
        var request = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = GetPath(imagePath),
        };
        
        var response = await _client.DeleteObjectAsync(request, cancellationToken);
        
        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new ApplicationException("Failed to delete image");
        }
    }
}