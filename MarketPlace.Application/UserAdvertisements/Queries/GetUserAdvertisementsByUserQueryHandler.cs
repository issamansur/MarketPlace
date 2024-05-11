namespace MarketPlace.Application.UserAdvertisements.Queries;

public class GetUserAdvertisementsByUserQueryHandler: BaseHandler,
    IRequestHandler<GetUserAdvertisementsByUserQuery, IEnumerable<UserAdvertisement>>
{
    public GetUserAdvertisementsByUserQueryHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }
    
    public async Task<IEnumerable<UserAdvertisement>> Handle(GetUserAdvertisementsByUserQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        await tenant.Users.GetByIdAsync(request.Filter.UserId, cancellationToken);
        
        var advertisements = await tenant.UserAdvertisements.GetUserAdvertisementsByUserIdAsync(request.Filter, cancellationToken);
        
        return advertisements;
    }
}