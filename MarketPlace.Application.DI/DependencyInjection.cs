using Microsoft.Extensions.DependencyInjection;

namespace MarketPlace.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Common.ITenantFactory).Assembly)
        );
            
        return services;
    }
}