using MarketPlace.Application.Services;
using MarketPlace.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace MarketPlace.WebAPI.Middlewares;

public class ImageResizingMiddleware
{
    private readonly RequestDelegate _next;
    
    private readonly IImageService _imageService;
    private readonly StaticFilesOptions _staticFileOptions;

    public ImageResizingMiddleware(
        RequestDelegate next, 
        IImageService imageService,
        IOptions<StaticFilesOptions> options
    )
    {
        _next = next;
        _imageService = imageService;
        _staticFileOptions = options.Value;
    }
    
    CancellationToken GetCancellationToken(HttpContext? context) => (context?.RequestAborted ?? CancellationToken.None);

    public async Task InvokeAsync(HttpContext? context)
    {
        var cancellationToken = GetCancellationToken(context);
        
        var requestPath = context!.Request.Path.Value!;

        // If the request is not an image, pass it to the next middleware
        if (!IsImageRequest(requestPath))
        {
            await _next(context);
            return;
        }
        
        
        var imagePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            requestPath.TrimStart('/')
        );
        
        // If the image does not exist, pass the request to the next middleware
        if (!File.Exists(imagePath))
        {
            await _next(context);
            return;
        }

        var width = context.Request.Query["width"];
        var height = context.Request.Query["height"];
        
        // If the width or height is not specified, pass the request to the next middleware
        if (string.IsNullOrEmpty(width) || string.IsNullOrEmpty(height))
        {
            await _next(context);
            return;
        }
        
        // If the width or height is not a number, pass the request to the next middleware
        if (!int.TryParse(width, out var widthValue) || !int.TryParse(height, out var heightValue))
        {
            await context.Response.WriteAsync("Width and height must be numbers", cancellationToken);
            return;
        }

        // If image exists and width and height were specified, resize the image
        var stream = await _imageService.GetResizedImageAsync(imagePath, widthValue, heightValue, cancellationToken);
        
        stream.Position = 0;
        context.Response.ContentType = "image/jpeg";

        SetCacheHeaders(context);
        
        await stream.CopyToAsync(context.Response.Body, cancellationToken);
    }

    private bool IsImageRequest(string path)
    {
        return _imageService.AllowedExtensions.Contains(Path.GetExtension(path));
    }
    
    private void SetCacheHeaders(HttpContext context)
    {
        context.Response.Headers.CacheControl = $"public, max-age={_staticFileOptions. CacheExpireInMinutes * 60}";
        context.Response.Headers.Expires = DateTime.UtcNow.AddMinutes(_staticFileOptions.CacheExpireInMinutes).ToString("R");
    }
}