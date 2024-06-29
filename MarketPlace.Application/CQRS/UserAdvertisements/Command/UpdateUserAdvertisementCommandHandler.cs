using MarketPlace.Application.Services;

namespace MarketPlace.Application.CQRS.UserAdvertisements.Command;

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
        
        // TODO: Refactor this part
        // TODO: Think about the case when the user wants to delete the image
        string imageUrl = advertisement.ImageUrl;
        
        if (request.Image is not null)
        {
            string directory = Path.Combine("UserAdvertisements", advertisement.CreatorId.ToString());
            string fileWithExtension = $"{advertisement.Id}{request.Extension}";
            
            imageUrl = Path.Combine(
                directory,
                fileWithExtension
            );
            
            await _imageService.UpdateImageAsync(request.Image, directory, fileWithExtension);
        }
        
        advertisement.Update(
            request.Title,
            request.Description,
            imageUrl
        );
            
        // ---------------------
        
        await tenant.UserAdvertisements.UpdateAsync(advertisement, cancellationToken);
        await tenant.CommitAsync(cancellationToken);
    }
}