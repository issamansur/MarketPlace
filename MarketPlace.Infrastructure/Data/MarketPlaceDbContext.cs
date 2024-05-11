namespace MarketPlace.Infrastructure.Data;

public class MarketPlaceDbContext: DbContext
{
    public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> options): base(options)
    {
    }
    
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserAdvertisement> UserAdvertisements => Set<UserAdvertisement>();
    public DbSet<AdvertisementReview> AdvertisementReviews => Set<AdvertisementReview>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarketPlaceDbContext).Assembly);
    }
}