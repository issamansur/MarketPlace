namespace MarketPlace.Application.UserAdvertisements.Queries;

public class GetAllUserAdvertisementsQueryHandler : BaseHandler,
    IRequestHandler<GetAllUserAdvertisementsQuery, IEnumerable<UserAdvertisement>>
{
    public GetAllUserAdvertisementsQueryHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }
    
    public async Task<IEnumerable<UserAdvertisement>> Handle(GetAllUserAdvertisementsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        var advertisements = await tenant.UserAdvertisements.GetAllUserAdvertisementsAsync(request.Filter, cancellationToken);
        
        return advertisements;
    }
}