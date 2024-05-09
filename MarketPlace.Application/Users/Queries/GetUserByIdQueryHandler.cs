namespace MarketPlace.Application.Users.Queries;

public class GetUserByIdQueryHandler: BaseHandler, IRequestHandler<GetUserByIdQuery, User>
{
    public GetUserByIdQueryHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        var user = await tenant.Users.GetByIdAsync(request.Id, cancellationToken);
        
        return user;
    }
}