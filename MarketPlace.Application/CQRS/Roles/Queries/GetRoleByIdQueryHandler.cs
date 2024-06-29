namespace MarketPlace.Application.CQRS.Roles.Queries;

public class GetRoleByIdQueryHandler: BaseHandler, IRequestHandler<GetRoleByIdQuery, Role>
{
    public GetRoleByIdQueryHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }

    public async Task<Role> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        var role = await tenant.Roles.GetByIdAsync(request.Id, cancellationToken);
        
        return role;
    }
}