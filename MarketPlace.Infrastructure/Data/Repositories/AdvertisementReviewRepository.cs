using MarketPlace.Application.CQRS.AdvertisementReviews.Filters;
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
                
                await Context.UserAdvertisements
                    .Where(x => x.Id == advertisementId)
                    .ExecuteUpdateAsync(
                        setters => setters
                            .SetProperty(
                                x => x.RatingCount,
                                x => x.RatingCount + 1
                            )
                            .SetProperty(
                                x => x.RatingSum,
                                x => x.RatingSum + entity.Rating
                            )
                            .SetProperty(
                                x => x.Rating,
                                x => (double)(x.RatingSum + entity.Rating) / (x.RatingCount + 1)
                            ),
                        cancellationToken
                    );

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
                var advertisementReview = await Context.AdvertisementReviews.FirstAsync(x => x.Id == entity.Id, cancellationToken);
                var oldRating = advertisementReview.Rating;
                
                await Context.SaveChangesAsync(cancellationToken);

                var advertisementId = entity.AdvertisementId;
                var newRating = entity.Rating;
                var diff = newRating - oldRating;
                
                await Context.UserAdvertisements
                    .Where(x => x.Id == advertisementId)
                    .ExecuteUpdateAsync(
                        setters => setters
                            .SetProperty(
                                x => x.RatingSum,
                                x => x.RatingSum + diff
                            )
                            .SetProperty(
                                x => x.Rating,
                                x => (double)(x.RatingSum + diff) / x.RatingCount
                            ),
                        cancellationToken
                    );

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
        return await Context.AdvertisementReviews
            .AsNoTracking()
            .FirstAsync(x => x.Id == id, cancellationToken);
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
                
                await Context.UserAdvertisements
                    .Where(x => x.Id == advertisementId)
                    .ExecuteUpdateAsync(
                        setters => setters
                            .SetProperty(
                                x => x.RatingSum,
                                x => x.RatingSum - rating
                            )
                            .SetProperty(
                                x => x.RatingCount,
                                x => x.RatingCount - 1
                            )
                            .SetProperty(
                                x => x.Rating,
                                x => x.RatingCount == 1 ? 0 : (double)(x.RatingSum - rating) / (x.RatingCount - 1)
                            ),
                        cancellationToken
                    );

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