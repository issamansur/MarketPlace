namespace MarketPlace.Application.CQRS.UserAdvertisements.Queries;

public class GetUserAdvertisementQueryHandler : BaseHandler,
    IRequestHandler<GetUserAdvertisementQuery, UserAdvertisement>
{
    public GetUserAdvertisementQueryHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }
    
    public async Task<UserAdvertisement> Handle(GetUserAdvertisementQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        var advertisement = await tenant.UserAdvertisements.GetByIdAsync(request.UserAdvertisementId, cancellationToken);
        
        return advertisement;
    }
}