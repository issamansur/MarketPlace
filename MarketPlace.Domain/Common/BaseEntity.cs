namespace MarketPlace.Domain.Common;

public class BaseEntity<TId>
{
    // DDD? Нет, не слышал
    public TId Id { get; init; }
}