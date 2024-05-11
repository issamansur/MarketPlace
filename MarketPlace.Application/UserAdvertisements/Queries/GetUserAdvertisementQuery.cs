namespace MarketPlace.Application.UserAdvertisements.Queries;

public class GetUserAdvertisementQuery: IRequest<UserAdvertisement>
{
    public Guid UserAdvertisementId { get; }
    
    public GetUserAdvertisementQuery(Guid userAdvertisementId)
    {
        ArgumentNullException.ThrowIfNull(userAdvertisementId, nameof(userAdvertisementId));
        
        UserAdvertisementId = userAdvertisementId;

        var validation = new GetUserAdvertisementQueryValidator();
        validation.ValidateAndThrow(this);
    }
}