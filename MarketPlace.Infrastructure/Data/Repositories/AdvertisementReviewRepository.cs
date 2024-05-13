using MarketPlace.Application.AdvertisementReviews.Filters;
using MarketPlace.Application.Enums;

namespace MarketPlace.Infrastructure.Data.Repositories;

public class AdvertisementReviewRepository: BaseRepository, IAdvertisementReviewRepository
{
    public AdvertisementReviewRepository(MarketPlaceDbContext context) : base(context)
    {
    }

    public async Task<Guid> CreateAsync(AdvertisementReview entity, CancellationToken cancellationToken)
    {
        using (var transaction = await Context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                Context.AdvertisementReviews.Add(entity);
                await Context.SaveChangesAsync(cancellationToken);
                
                var advertisementId = entity.AdvertisementId;
                var rating = entity.Rating;
                var sql = $"UPDATE User_Advertisements " +
                          $"SET Sum_Rating = Sum_Rating + {rating}, " +
                          $"Count_Rating = Count_Rating + 1, " +
                          $"Rating = (Sum_Rating + {rating}) / (Count_Rating + 1) " +
                          $"WHERE Id = '{advertisementId}'";
                
                await Context.Database.ExecuteSqlRawAsync(sql, cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return entity.Id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }

    public async Task UpdateAsync(AdvertisementReview entity, CancellationToken cancellationToken)
    {
        using (var transaction = await Context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                var advertisementReview = Context.AdvertisementReviews.Local.First(x => x.Id == entity.Id);
                var oldRating = advertisementReview.Rating;
                
                Context.AdvertisementReviews.Update(entity);
                await Context.SaveChangesAsync(cancellationToken);

                var advertisementId = entity.AdvertisementId;
                var newRating = entity.Rating;
                var diff = newRating - oldRating;
                var sql = $"UPDATE User_Advertisements " +
                          $"SET Sum_Rating = Sum_Rating + {diff}, " +
                          $"Rating = (Sum_Rating + {diff}) / Count_Rating " +
                          $"WHERE Id = '{advertisementId}'";
                
                await Context.Database.ExecuteSqlRawAsync(sql, cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }

    public async Task<AdvertisementReview> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.AdvertisementReviews.AsNoTracking().FirstAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<AdvertisementReview>> GetByAdvertisementIdAsync(AdvertisementReviewsFilter filter, CancellationToken cancellationToken)
    {
        var res = Context.AdvertisementReviews.AsNoTracking()
                .Where(x => x.AdvertisementId == filter.AdvertisementId);
        
        if (filter.SortType == SortTypes.ByRating)
        {
            res = filter.IsDesc? res.OrderByDescending(x => x.Rating) : res.OrderBy(x => x.Rating);
        }
        else
        {
            res = filter.IsDesc? res.OrderByDescending(x => x.DateCreated) : res.OrderBy(x => x.DateCreated);
        }

        res = res.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);

        return await res.ToListAsync(cancellationToken);
    }

    // For future implementation
    public async Task DeleteAsync(AdvertisementReview entity, CancellationToken cancellationToken)
    {
        using (var transaction = await Context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                Context.AdvertisementReviews.Remove(entity);
                await Context.SaveChangesAsync(cancellationToken);

                var advertisementId = entity.AdvertisementId;
                var rating = entity.Rating;
                var sql = $"UPDATE User_Advertisements " +
                          $"SET Rating_Sum = Rating_Sum - {rating}, " +
                          $"Rating_Count = Rating_Count - 1, " +
                          $"Rating = CASE WHEN Rating_Count = 1 THEN 0 ELSE (Rating_Sum - {rating} / (Rating_Count - 1) END " +
                          $"WHERE Id = '{advertisementId}'";
                
                await Context.Database.ExecuteSqlRawAsync(sql, cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}