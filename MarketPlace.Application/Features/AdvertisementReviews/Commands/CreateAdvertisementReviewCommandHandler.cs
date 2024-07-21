namespace MarketPlace.Application.CQRS.AdvertisementReviews.Commands;

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

        await tenant.AdvertisementReviews.CreateAsync(advertisementReview, cancellationToken);
        // TODO: Check is this correct or not
        //await tenant.CommitAsync(cancellationToken);

        return advertisementReview.Id;
    }
}