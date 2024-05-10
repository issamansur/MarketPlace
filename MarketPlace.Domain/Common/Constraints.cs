namespace MarketPlace.Domain.Common;

public static class Constraints
{
    // Role
    public static readonly int ROLE_MIN_TITLE_LENGTH = 5;
    public static readonly int ROLE_MAX_TITLE_LENGTH = 20;
    
    // User
    public static readonly int USER_MIN_NAME_LENGTH = 5;
    public static readonly int USER_MAX_NAME_LENGTH = 20;
    
    // User Advertisement
    public static readonly int USER_AD_MIN_TITLE_LENGTH = 5;
    public static readonly int USER_AD_MAX_TITLE_LENGTH = 20;
    public static readonly int USER_AD_MIN_DESC_LENGTH = 20;
    public static readonly int USER_AD_MAX_DESC_LENGTH = 200;
    
    public static readonly int USER_AD_DAYS_TO_EXPIRE = 7;
    
    // Advertisement Review
    public static readonly int REVIEW_MIN_RATE = 1;
    public static readonly int REVIEW_MAX_RATE = 5;
    public static readonly int REVIEW_MIN_COMMENT_LENGTH = 0;
    public static readonly int REVIEW_MAX_COMMENT_LENGTH = 200;
}