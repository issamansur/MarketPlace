using MarketPlace.Application.Features.UserAdvertisements.Queries.Filters;
using MarketPlace.Application.Options;
using MarketPlace.Application.Services;
using Microsoft.Extensions.Options;

namespace MarketPlace.Application.Features.UserAdvertisements.Command;

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
        
        string imageUrl;
        
        if (request.Image is not null)
        {
            imageUrl = Path.Combine(
                Constants.UserAdvertisementsPath,
                request.CreatorId.ToString(),
                $"{userAdvertisement.Id}{request.Extension}"
            );
            
            await _imageService.SaveImageAsync(request.Image, imageUrl, cancellationToken);
        }
        else
        {
            imageUrl = Path.Combine(
                Constants.UserAdvertisementsPath,
                Constants.DefaultImage
            );
        }
        
        userAdvertisement.SetImageUrl(imageUrl);
        
        await tenant.UserAdvertisements.CreateAsync(userAdvertisement, cancellationToken);
        await tenant.CommitAsync(cancellationToken);
        
        return userAdvertisement.Id;
    }
}