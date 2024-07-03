using System.Runtime.CompilerServices;

namespace MarketPlace.Application.Common;

public static class ApplicationErrors
{
    public static readonly string UnauthorizedAccessError = "User does not have permission to access this resource";
    
    public static string AlreadyExistError<T>(T value, [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        return $"{valueExpression} with these unique parameters already exists.";
    }
    
    public static readonly string InvalidQueryError = "Query must not be null.";
    public static readonly string InvalidPageError = "Page must be greater than 0.";
    public static readonly string InvalidPageSizeError = "PageSize must be greater than 0.";
    public static readonly string InvalidSortTypeError = "UserAdvertisementSortType is not valid.";
    
    public static readonly string UserAdvertisementsCountLimitException = "User advertisements count limit reached.";
}