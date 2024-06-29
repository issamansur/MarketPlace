using MarketPlace.Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace MarketPlace.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add Options<ProjectSettings>
        services.AddOptions<ProjectSettings>()
            .BindConfiguration(nameof(ProjectSettings));
        
        // Add MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ITenantFactory).Assembly)
        );
            
        return services;
    }
}