namespace MarketPlace.Application.AdvertisementReviews.Commands;

public class UpdateAdvertisementReviewCommandHandler: BaseHandler, IRequestHandler<UpdateAdvertisementReviewCommand>
{
    public UpdateAdvertisementReviewCommandHandler(ITenantFactory factory) : base(factory)
    {
    }

    public async Task Handle(UpdateAdvertisementReviewCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        await tenant.Users.GetByIdAsync(request.ChangerId, cancellationToken);
     
        // TODO: Create custom SQL query to update the advertisement rating
        var advertisementReview = await tenant.AdvertisementReviews.GetByIdAsync(request.Id, cancellationToken);
        
        if (advertisementReview.CreatorId != request.ChangerId)
        {
            throw new UnauthorizedAccessException(ApplicationErrors.UnauthorizedAccessError);
        }
        
        advertisementReview.Update(request.Rating, request.Comment);
        await tenant.AdvertisementReviews.UpdateAsync(advertisementReview, cancellationToken);
        // Not needed because the rating is updated in the same transaction
        //await tenant.CommitAsync(cancellationToken);
    }
}