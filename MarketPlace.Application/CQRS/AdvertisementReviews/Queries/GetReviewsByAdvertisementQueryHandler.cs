namespace MarketPlace.Application.CQRS.AdvertisementReviews.Queries;

public class GetReviewsByAdvertisementQueryHandler : BaseHandler,
    IRequestHandler<GetReviewsByAdvertisementQuery, IEnumerable<AdvertisementReview>>
{
    public GetReviewsByAdvertisementQueryHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }
    
    public async Task<IEnumerable<AdvertisementReview>> Handle(GetReviewsByAdvertisementQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        await tenant.UserAdvertisements.GetByIdAsync(request.Filter.AdvertisementId, cancellationToken);
        
        var reviews = await tenant.AdvertisementReviews.GetByAdvertisementIdAsync(request.Filter, cancellationToken);
        
        return reviews;
    }
}