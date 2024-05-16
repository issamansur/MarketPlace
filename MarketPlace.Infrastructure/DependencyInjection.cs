using Microsoft.Extensions.DependencyInjection;

namespace MarketPlace.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ITenantFactory, TenantFactory>();
        services.AddSingleton<IImageService, ImageService>();

        return services;
    }
}