using System.Reflection;

using CatalogService.PricingService;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using Framework.RabbitMQ;

namespace CatalogService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var configuration = services.GetConfiguration();

        Assembly assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddRabbitMQConsumers(assembly);
        services.AddAutoMapper(cfg => { }, assembly);
        services.AddValidatorsFromAssembly(assembly);

        services.AddStackExchangeRedisCache(cfg =>
        {
            cfg.Configuration = configuration.GetSection("Redis")["Configuration"];
        });

        services.AddHttpClientFramework(configuration);
        services.AddHttpClientService("ObjectStorageClient", configuration);
        services.AddHttpClientService("PricingClient", configuration);
        services.AddScoped<CurrentPriceClient>();


        // services.AddScoped<IVendorServiceClient, VendorServiceClient>();
        // services.AddScoped<ICatalogServiceClient, CatalogServiceClient>();
        // services.AddMemoryCache();

        return services;
    }
}
