namespace MarketPlace.Application.AdvertisementReviews.Commands;

public class UpdateAdvertisementReviewHandler: BaseHandler, IRequestHandler<UpdateAdvertisementReviewCommand, Guid>
{
    public UpdateAdvertisementReviewHandler(ITenantFactory factory) : base(factory)
    {
    }

    public async Task<Guid> Handle(UpdateAdvertisementReviewCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        await tenant.Users.GetByIdAsync(request.UserId, cancellationToken);
        await tenant.UserAdvertisements.GetByIdAsync(request.AdvertisementId, cancellationToken);
     
        var advertisementReview = await tenant.AdvertisementReviews.GetByIdAsync(request.Id, cancellationToken);
        advertisementReview.UpdateReview(request.Rating, request.Comment);

        await tenant.CommitAsync(cancellationToken);

        return advertisementReview.Id;
    }
}