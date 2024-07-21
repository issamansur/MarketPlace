namespace MarketPlace.Application.Features.UserAdvertisements.Queries;

public class GetUserAdvertisementByNumberQueryHandler : BaseHandler,
    IRequestHandler<GetUserAdvertisementByNumberQuery, UserAdvertisement>
{
    public GetUserAdvertisementByNumberQueryHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }
    
    public async Task<UserAdvertisement> Handle(GetUserAdvertisementByNumberQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        var advertisements = await tenant.UserAdvertisements.GetByNumberAsync(request.UserAdvertisementNumber, cancellationToken);
        
        return advertisements;
    }
}