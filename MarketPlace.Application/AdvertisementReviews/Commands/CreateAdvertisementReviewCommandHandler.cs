namespace MarketPlace.Application.AdvertisementReviews.Commands;

public class CreateAdvertisementReviewCommandHandler: BaseHandler, IRequestHandler<CreateAdvertisementReviewCommand, Guid>
{
    public CreateAdvertisementReviewCommandHandler(ITenantFactory factory) : base(factory)
    {
    }

    public async Task<Guid> Handle(CreateAdvertisementReviewCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        await tenant.Users.GetByIdAsync(request.CreatorId, cancellationToken);
        await tenant.UserAdvertisements.GetByIdAsync(request.AdvertisementId, cancellationToken);
     
        var advertisementReview = AdvertisementReview.Create(
            request.AdvertisementId, request.CreatorId, request.Rating, request.Comment
            );

        // TODO: Create custom SQL query to update the advertisement rating
        await tenant.AdvertisementReviews.CreateAsync(advertisementReview, cancellationToken);
        // Not needed because the rating is updated in the same transaction
        //await tenant.CommitAsync(cancellationToken);

        return advertisementReview.Id;
    }
}