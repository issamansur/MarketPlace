using MarketPlace.Application.Services;

namespace MarketPlace.Application.CQRS.UserAdvertisements.Command;

public class DeleteUserAdvertisementCommandHandler: BaseHandler, IRequestHandler<DeleteUserAdvertisementCommand>
{
    private readonly IImageService _imageService;
    public DeleteUserAdvertisementCommandHandler(ITenantFactory tenantFactory, IImageService imageService)
        : base(tenantFactory)
    {
        ArgumentNullException.ThrowIfNull(imageService, nameof(imageService));
        _imageService = imageService;
    }

    public async Task Handle(DeleteUserAdvertisementCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        var userAdvertisement = await tenant.UserAdvertisements.GetByIdAsync(request.Id, cancellationToken);

        if (userAdvertisement.CreatorId != request.ChangerId)
        {
            throw new UnauthorizedAccessException(ApplicationErrors.UnauthorizedAccessError);
        }
        
        // At first, we need to delete the image from the storage... or not?
        await _imageService.DeleteImageAsync(userAdvertisement.ImageUrl);
        
        await tenant.UserAdvertisements.DeleteAsync(userAdvertisement, cancellationToken);
        await tenant.CommitAsync(cancellationToken);
    }
}