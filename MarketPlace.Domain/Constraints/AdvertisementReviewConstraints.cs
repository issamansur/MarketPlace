namespace MarketPlace.Domain;

public static partial class Constraints
{
    public static readonly int REVIEW_MIN_RATING = 1;
    public static readonly int REVIEW_MAX_RATING = 5;
    public static readonly int REVIEW_MIN_COMMENT_LENGTH = 0;
    public static readonly int REVIEW_MAX_COMMENT_LENGTH = 200;
}