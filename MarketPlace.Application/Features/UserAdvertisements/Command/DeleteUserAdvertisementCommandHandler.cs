using MarketPlace.Application.Services;

namespace MarketPlace.Application.Features.UserAdvertisements.Command;

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
        
        await tenant.UserAdvertisements.DeleteAsync(userAdvertisement, cancellationToken);
        
        await _imageService.DeleteImageAsync(userAdvertisement.ImageUrl, cancellationToken);
        
        await tenant.CommitAsync(cancellationToken);
    }
}