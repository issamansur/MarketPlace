namespace MarketPlace.Application.UserAdvertisements.Command;

public class CreateUserAdvertisementCommandHandler: BaseHandler, IRequestHandler<CreateUserAdvertisementCommand, Guid>
{
    public CreateUserAdvertisementCommandHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }
    
    public async Task<Guid> Handle(CreateUserAdvertisementCommand request, CancellationToken cancellationToken)
    {
        var tenant = GetTenant();
        
        var userAdvertisement = UserAdvertisement.Create(
            request.CreatorId,
            request.Title,
            request.Description,
            request.ImageUrl
        );
        
        await tenant.UserAdvertisements.CreateAsync(userAdvertisement, cancellationToken);
        await tenant.CommitAsync(cancellationToken);
        
        return userAdvertisement.Id;
    }
}