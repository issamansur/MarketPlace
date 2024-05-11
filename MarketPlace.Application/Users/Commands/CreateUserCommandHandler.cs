namespace MarketPlace.Application.Users.Commands;

public class CreateUserCommandHandler: BaseHandler, IRequestHandler<CreateUserCommand, Guid>
{
    public CreateUserCommandHandler(ITenantFactory tenantFactory): base(tenantFactory)
    {
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        var user = User.Create(request.RoleId, request.Name);
        
        await tenant.Users.CreateAsync(user, cancellationToken);
        await tenant.CommitAsync(cancellationToken);
        
        return user.Id;
    }
}