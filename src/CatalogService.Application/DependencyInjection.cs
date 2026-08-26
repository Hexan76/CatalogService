using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var configuration = services.GetConfiguration();

        Assembly assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(cfg => { }, assembly);
        services.AddValidatorsFromAssembly(assembly);

        services.AddStackExchangeRedisCache(cfg =>
        {
            cfg.Configuration = configuration.GetSection("Redis")["Configuration"];
        });

        services.AddHttpClientFramework(configuration);
        services.AddHttpClientService("ObjectStorageClient", configuration);

        // services.AddScoped<IVendorServiceClient, VendorServiceClient>();
        // services.AddScoped<ICatalogServiceClient, CatalogServiceClient>();
        // services.AddMemoryCache();

        return services;
    }
}
