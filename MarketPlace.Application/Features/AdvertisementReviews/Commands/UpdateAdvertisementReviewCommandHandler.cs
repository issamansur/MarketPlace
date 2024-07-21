namespace MarketPlace.Application.CQRS.AdvertisementReviews.Commands;

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

        var advertisementReview = await tenant.AdvertisementReviews.GetByIdAsync(request.Id, cancellationToken);
        
        if (advertisementReview.CreatorId != request.ChangerId)
        {
            throw new UnauthorizedAccessException(ApplicationErrors.UnauthorizedAccessError);
        }
        
        advertisementReview.Update(request.Rating, request.Comment);
        await tenant.AdvertisementReviews.UpdateAsync(advertisementReview, cancellationToken);
        // TODO: Check is this correct or not
        //await tenant.CommitAsync(cancellationToken);
    }
}