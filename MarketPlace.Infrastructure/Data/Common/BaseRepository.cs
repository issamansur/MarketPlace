namespace MarketPlace.Infrastructure.Data.Common;

public abstract class BaseRepository
{
    private protected MarketPlaceDbContext Context { get; }
    
    public BaseRepository(MarketPlaceDbContext context)
    {
        Context = context;
    }
}