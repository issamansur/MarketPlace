using MarketPlace.Application.Services;

namespace MarketPlace.Application.Features.UserAdvertisements.Command;

public class UpdateUserAdvertisementCommandHandler: BaseHandler, IRequestHandler<UpdateUserAdvertisementCommand>
{
    private readonly IImageService _imageService;
    
    public UpdateUserAdvertisementCommandHandler(ITenantFactory tenantFactory, IImageService imageService) : 
        base(tenantFactory)
    {
        ArgumentNullException.ThrowIfNull(imageService, nameof(imageService));
        _imageService = imageService;
    }
    
    public async Task Handle(UpdateUserAdvertisementCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        var advertisement = await tenant.UserAdvertisements.GetByIdAsync(request.Id, cancellationToken);
        if (advertisement.CreatorId != request.ChangerId)
        {
            throw new UnauthorizedAccessException(ApplicationErrors.UnauthorizedAccessError);
        }
        
        string imageUrl;
        
        if (request.Image is not null)
        {
            imageUrl = Path.Combine(
                Constants.UserAdvertisementsPath,
                request.ChangerId.ToString(),
                $"{advertisement.Id}{request.Extension}"
            );
            
            await _imageService.SaveImageAsync(request.Image, imageUrl, cancellationToken);
        }
        else
        {
            imageUrl = advertisement.ImageUrl;
        }
        
        advertisement.Update(
            request.Title,
            request.Description,
            imageUrl
        );
        
        await tenant.UserAdvertisements.UpdateAsync(advertisement, cancellationToken);
        await tenant.CommitAsync(cancellationToken);
    }
}