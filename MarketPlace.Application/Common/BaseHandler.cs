namespace MarketPlace.Application.Common;

public class BaseHandler
{
    private readonly ITenantFactory _tenantFactory;
    
    public BaseHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory;
    }
    
    public ITenantRepository GetTenant()
    {
        return _tenantFactory.GetRepository();
    }
}