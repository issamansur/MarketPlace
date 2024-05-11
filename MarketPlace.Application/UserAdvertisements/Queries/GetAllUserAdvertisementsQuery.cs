using MarketPlace.Application.UserAdvertisements.Filters;

namespace MarketPlace.Application.UserAdvertisements.Queries;

public class GetAllUserAdvertisementsQuery: IRequest<IEnumerable<UserAdvertisement>>
{
    public UserAdvertisementsFilter Filter { get; }

    public GetAllUserAdvertisementsQuery(UserAdvertisementsFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter, nameof(filter));
        
        Filter = filter;
        // Validation is done in the filter
    }
}