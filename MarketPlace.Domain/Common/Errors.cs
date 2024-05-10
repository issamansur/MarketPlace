using System.Runtime.CompilerServices;

namespace MarketPlace.Domain.Common;

public static class Errors
{
    // Constraints
    public static readonly string RoleTitleLengthError = 
        $"Role title length must be between {Constraints.ROLE_MIN_TITLE_LENGTH} and {Constraints.ROLE_MAX_TITLE_LENGTH} characters.";
    
    public static readonly string UserNameLengthError =
        $"User name length must be between {Constraints.USER_MIN_NAME_LENGTH} and {Constraints.USER_MAX_NAME_LENGTH} characters.";
    
    public static readonly string UserAdTitleLengthError =
        $"Review title length must be between {Constraints.USER_AD_MIN_TITLE_LENGTH} and {Constraints.USER_AD_MAX_DESC_LENGTH} characters.";
    
    public static readonly string UserAdDescLengthError =
        $"Review description length must be between {Constraints.USER_AD_MIN_DESC_LENGTH} and {Constraints.USER_AD_MAX_TITLE_LENGTH} characters.";

    public static readonly string ReviewRateError = 
        $"Review rate must be between {Constraints.REVIEW_MIN_RATE} and {Constraints.REVIEW_MAX_RATE}.";

    public static readonly string ReviewCommentLengthError =
        $"Review comment length must be between {Constraints.REVIEW_MIN_COMMENT_LENGTH} and {Constraints.REVIEW_MAX_COMMENT_LENGTH} characters.";

    public static string NullError<T>(T value, [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        return $"{valueExpression} cannot be empty or null.";
    }
    
    public static string InvalidError<T>(T value, [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        return $"{valueExpression} is invalid.";
    }
    
    public static string NotFoundError<T>(T value, [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        return $"{valueExpression} not found.";
    }
}