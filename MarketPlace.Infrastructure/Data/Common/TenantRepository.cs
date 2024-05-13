namespace MarketPlace.Infrastructure.Data.Common;

public class TenantRepository: ITenantRepository
{
    private readonly MarketPlaceDbContext _context;
    
    public IRoleRepository Roles { get; }
    public IUserRepository Users { get; }
    
    public IUserAdvertisementRepository UserAdvertisements { get; }
    public IAdvertisementReviewRepository AdvertisementReviews { get; }

    public TenantRepository(MarketPlaceDbContext context)
    {
        _context = context;
        
        Roles = new RoleRepository(context);
        Users = new UserRepository(context);
        
        UserAdvertisements = new UserAdvertisementRepository(context);
        AdvertisementReviews = new AdvertisementReviewRepository(context);
    }
    
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}