using System.Reflection;
using Amazon.S3;
using MapsterMapper;
using MarketPlace.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;

namespace MarketPlace.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ITenantFactory, TenantFactory>();

        services.AddOptions<StaticFilesOptions>()
            .BindConfiguration(nameof(StaticFilesOptions));
        
        services.AddImageServiceAws();
        
        services.AddContracts();

        return services;
    }
    
    private static IServiceCollection AddImageServiceStatic(this IServiceCollection services)
    {
        services.AddOptions<ImageServiceOptions>()
            .BindConfiguration(nameof(ImageServiceOptions));
        
        services.AddSingleton<IImageService, PhysicalImageService>();
        
        return services;
    }
    
    private static IServiceCollection AddImageServiceAws(this IServiceCollection services)
    {
        services.AddAWSService<IAmazonS3>();
        
        services.AddOptions<AWSOptions>()
            .BindConfiguration(nameof(AWSOptions));
        services.AddOptions<ImageServiceOptions>()
            .BindConfiguration(nameof(ImageServiceOptions));
        
        services.AddSingleton<IImageService, AwsImageService>();
        
        return services;
    }
    
    private static IServiceCollection AddContracts(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        
        return services;
    }
}