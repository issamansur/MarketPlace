namespace MarketPlace.Application.CQRS.UserAdvertisements.Queries;

public class GetUserAdvertisementByNumberQuery: IRequest<UserAdvertisement>
{
    public int UserAdvertisementNumber { get; }
    
    public GetUserAdvertisementByNumberQuery(int userAdvertisementNumber)
    {
        UserAdvertisementNumber = userAdvertisementNumber;

        var validation = new GetUserAdvertisementByNumberQueryValidator();
        validation.ValidateAndThrow(this);
    }
}