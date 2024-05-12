namespace MarketPlace.Application.UserAdvertisements.Command;

public class DeleteUserAdvertisementCommandHandler: BaseHandler, IRequestHandler<DeleteUserAdvertisementCommand, Guid>
{
    public DeleteUserAdvertisementCommandHandler(ITenantFactory tenantFactory)
        : base(tenantFactory)
    {
    }

    public async Task<Guid> Handle(DeleteUserAdvertisementCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        var userAdvertisement = await tenant.UserAdvertisements.GetByIdAsync(request.Id, cancellationToken);

        if (userAdvertisement.CreatorId != request.ChangerId)
        {
            throw new UnauthorizedAccessException(ApplicationErrors.UnauthorizedAccessError);
        }
        
        await tenant.UserAdvertisements.DeleteAsync(userAdvertisement.Id, cancellationToken);
        await tenant.CommitAsync(cancellationToken);

        return userAdvertisement.Id;
    }
}