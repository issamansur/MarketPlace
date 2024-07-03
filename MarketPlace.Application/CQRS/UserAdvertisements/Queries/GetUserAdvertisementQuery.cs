namespace MarketPlace.Application.CQRS.UserAdvertisements.Queries;

public class GetUserAdvertisementQuery: IRequest<UserAdvertisement>
{
    public Guid UserAdvertisementId { get; }
    
    public GetUserAdvertisementQuery(Guid userAdvertisementId)
    {
        UserAdvertisementId = userAdvertisementId;

        var validation = new GetUserAdvertisementQueryValidator();
        validation.ValidateAndThrow(this);
    }
}