namespace MarketPlace.Application.Common;

public interface ITenantFactory
{
    ITenantRepository GetRepository();
}