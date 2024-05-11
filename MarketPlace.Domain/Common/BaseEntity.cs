namespace MarketPlace.Domain.Common;

public abstract class BaseEntity<TId>
{
    // DDD? Нет, не слышал
    public TId Id { get; private init; }
}