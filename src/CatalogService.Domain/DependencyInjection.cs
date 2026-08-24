using CatalogService.Categories;
using CatalogService.Customizations;
using CatalogService.Files;
using CatalogService.Items;
using CatalogService.Menus;

using Framework.RabbitMQ;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        var configuration= services.GetConfiguration();
        services.AddHttpClientFramework(configuration);
        
        services.AddFrameworkRabbitMQ(configuration);
        services.AddScoped<ItemManager>();
        services.AddScoped<FileManager>();
        services.AddScoped<MenuManager>();
        services.AddScoped<CategoryManager>();
        services.AddScoped<CustomizationManager>();
        return services;
    }
}
