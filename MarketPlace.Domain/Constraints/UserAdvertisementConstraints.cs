namespace MarketPlace.Domain;

public static partial class Constraints
{
    public static readonly int USER_AD_MIN_TITLE_LENGTH = 5;
    public static readonly int USER_AD_MAX_TITLE_LENGTH = 20;
    public static readonly int USER_AD_MIN_DESC_LENGTH = 20;
    public static readonly int USER_AD_MAX_DESC_LENGTH = 200;
    
    public static readonly int USER_AD_DAYS_TO_EXPIRE = 7;
}