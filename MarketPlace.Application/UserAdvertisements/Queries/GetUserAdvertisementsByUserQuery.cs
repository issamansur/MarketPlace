using MarketPlace.Application.UserAdvertisements.Filters;

namespace MarketPlace.Application.UserAdvertisements.Queries;

public class GetUserAdvertisementsByUserQuery: IRequest<IEnumerable<UserAdvertisement>>
{
    public UserAdvertisementsByUserFilter Filter { get; }

    public GetUserAdvertisementsByUserQuery(UserAdvertisementsByUserFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter, nameof(filter));
        
        Filter = filter;
        // Validation is done in the filter
    }
}