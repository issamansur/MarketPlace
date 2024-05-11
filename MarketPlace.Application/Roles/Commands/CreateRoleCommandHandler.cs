namespace MarketPlace.Application.Roles.Commands;

public class CreateRoleCommandHandler: BaseHandler, IRequestHandler<CreateRoleCommand, Guid> 
{
    public CreateRoleCommandHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }
    
    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var role = Role.Create(request.Title);
        
        var tenant = GetTenant();
        
        if (await tenant.Roles.TryGetByNameAsync(role.Title) != null)
        {
            throw new InvalidOperationException(ApplicationErrors.AlreadyExistError(role));
        }
        
        Guid roleGuid = await tenant.Roles.CreateAsync(role, cancellationToken);
        await tenant.CommitAsync(cancellationToken);

        return roleGuid;
    }
}