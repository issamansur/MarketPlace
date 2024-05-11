namespace MarketPlace.Application.UserAdvertisements.Command;

public class UpdateUserAdvertisementCommandHandler: BaseHandler, IRequestHandler<UpdateUserAdvertisementCommand, Guid>
{
    public UpdateUserAdvertisementCommandHandler(ITenantFactory tenantFactory) : base(tenantFactory)
    {
    }
    
    public async Task<Guid> Handle(UpdateUserAdvertisementCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        var advertisement = await tenant.UserAdvertisements.GetByIdAsync(request.Id, cancellationToken);
        if (advertisement.CreatorId != request.ChangerId)
        {
            throw new UnauthorizedAccessException(ApplicationErrors.UnauthorizedAccessError);
        }
        
        advertisement.Update(request.Title, request.Description, request.ImageUrl);
        await tenant.UserAdvertisements.UpdateAsync(advertisement, cancellationToken);
        await tenant.CommitAsync(cancellationToken);
        
        return advertisement.Id;
    }
}