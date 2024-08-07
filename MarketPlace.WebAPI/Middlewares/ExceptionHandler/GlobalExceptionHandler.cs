using System.Net;
using Amazon.S3;
using Microsoft.AspNetCore.Diagnostics;

namespace MarketPlace.WebAPI.Middlewares.ExceptionHandler;

public class GlobalExceptionHandler: IExceptionHandler
{ private readonly ILogger<GlobalExceptionHandler> _logger;
    
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        
        _logger = logger;
    }
    
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError($"An error occurred while processing your request: {exception.Message}");
        var errorResponse = new ErrorResponse
        {
            Message = exception.Message
        };
        switch (exception)
        {
            case FileNotFoundException:
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Title = exception.GetType().Name;
                break;
            case ArgumentNullException:
            case ApplicationException:
            case InvalidOperationException:
            case FluentValidation.ValidationException:
            case AmazonS3Exception:
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Title = exception.GetType().Name;
                break;
            default:
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Title = "Internal Server Error";
                break;
        }
        httpContext.Response.StatusCode = errorResponse.StatusCode;
        httpContext.Response.ContentType = "application/problem+json";
        await httpContext
            .Response
            .WriteAsJsonAsync(errorResponse, cancellationToken);
        return true;
    }
}