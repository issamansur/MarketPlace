namespace MarketPlace.Application.Features.AdvertisementReviews.Commands;

public class DeleteAdvertisementReviewCommandHandler: BaseHandler, IRequestHandler<DeleteAdvertisementReviewCommand>
{
    public DeleteAdvertisementReviewCommandHandler(ITenantFactory factory) : base(factory)
    {
    }
    
    public async Task Handle(DeleteAdvertisementReviewCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var tenant = GetTenant();
        
        var review = await tenant.AdvertisementReviews.GetByIdAsync(request.ReviewId, cancellationToken);
        
        await tenant.AdvertisementReviews.DeleteAsync(review, cancellationToken);
    }
}