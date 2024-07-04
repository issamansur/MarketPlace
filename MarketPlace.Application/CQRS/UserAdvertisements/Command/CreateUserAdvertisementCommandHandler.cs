using MarketPlace.Application.CQRS.UserAdvertisements.Filters;
using MarketPlace.Application.Options;
using MarketPlace.Application.Services;
using Microsoft.Extensions.Options;

namespace MarketPlace.Application.CQRS.UserAdvertisements.Command;

public class CreateUserAdvertisementCommandHandler: BaseHandler, IRequestHandler<CreateUserAdvertisementCommand, Guid>
{
    private readonly IImageService _imageService;
    private readonly ProjectSettings _projectSettings;
    
    public CreateUserAdvertisementCommandHandler(
        ITenantFactory tenantFactory,
        IOptions<ProjectSettings> projectSettings,
        IImageService imageService) : 
        base(tenantFactory)
    {
        ArgumentNullException.ThrowIfNull(imageService, nameof(imageService));
        _imageService = imageService;
        _projectSettings = projectSettings.Value;
    }
    
    public async Task<Guid> Handle(CreateUserAdvertisementCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        // TODO: Create a request in repository to get count of user advertisements by user id
        var userAdvertisements = 
            await tenant.UserAdvertisements.GetUserAdvertisementsByUserIdAsync(
                new UserAdvertisementsByUserFilter(request.CreatorId), cancellationToken);

        if (userAdvertisements.ToList().Count >= _projectSettings.MaxUserAdvertisementsCount)
        {
            throw new InvalidOperationException(ApplicationErrors.UserAdvertisementsCountLimitException);
        }
        
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