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
        
        // TODO: need or not? This return InvalidOperationException if user not found
        // but simple user cannot use this endpoint
        await tenant.Users.GetByIdAsync(request.Filter.UserId, cancellationToken);
        
        var advertisements = await tenant.UserAdvertisements.GetUserAdvertisementsByUserIdAsync(request.Filter, cancellationToken);
        
        return advertisements;
    }
}