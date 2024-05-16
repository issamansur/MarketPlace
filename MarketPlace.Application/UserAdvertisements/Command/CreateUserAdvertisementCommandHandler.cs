using MarketPlace.Application.Services;

namespace MarketPlace.Application.UserAdvertisements.Command;

public class CreateUserAdvertisementCommandHandler: BaseHandler, IRequestHandler<CreateUserAdvertisementCommand, Guid>
{
    private readonly IImageService _imageService;
    
    public CreateUserAdvertisementCommandHandler(ITenantFactory tenantFactory, IImageService imageService) : 
        base(tenantFactory)
    {
        ArgumentNullException.ThrowIfNull(imageService, nameof(imageService));
        _imageService = imageService;
    }
    
    public async Task<Guid> Handle(CreateUserAdvertisementCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        var userAdvertisement = UserAdvertisement.Create(
            request.CreatorId,
            request.Title,
            request.Description
        );

        // TODO: Refactor this part
        string imageUrl;
        
        if (request.Image is not null)
        {
            string directory = Path.Combine("UserAdvertisements", request.CreatorId.ToString());
            string fileWithExtension = $"{userAdvertisement.Id}{request.Extension}";
            
            imageUrl = Path.Combine(
                directory,
                fileWithExtension
                );
            
            await _imageService.UploadImageAsync(request.Image, directory, fileWithExtension);
        }
        else
        {
            // Is this the best way to set a default image?
            imageUrl = Path.Combine("UserAdvertisements", "default.png");
        }
        
        userAdvertisement.SetImageUrl(imageUrl);
            
        // ---------------------
        
        await tenant.UserAdvertisements.CreateAsync(userAdvertisement, cancellationToken);
        await tenant.CommitAsync(cancellationToken);
        
        return userAdvertisement.Id;
    }
}