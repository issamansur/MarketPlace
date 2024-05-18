using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MarketPlace.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        /*
        services.Configure<BulletinsConfigurationOptions>(
            services.Configuration.GetSection("BulletinsConfigurationOptions"));
        */
        
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Common.ITenantFactory).Assembly)
        );
            
        return services;
    }
}