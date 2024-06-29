using MarketPlace.Application.CQRS.UserAdvertisements.Filters;

namespace MarketPlace.Application.CQRS.UserAdvertisements.Queries;

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