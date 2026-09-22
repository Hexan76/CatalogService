using System.Globalization;

using CatalogService.Categories;
using CatalogService.Customizations;
using CatalogService.Files;
using CatalogService.Items;
using CatalogService.Localization;
using CatalogService.Menus;

using Framework.Localization;
using Framework.Localization.Extensions;
using Framework.RabbitMQ;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

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

        services.RegisterFrameworkLocalization();
        services.Configure<LocalizationResourceOptions>(options =>
        {
            options.AddResource<CatalogServiceResources>(
                Path.Combine("Localization", "Resources"),
                nameof(CatalogServiceResources));
        });

        var supportedCultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("fa-IR"),
            new CultureInfo("fa-AF"),
            new CultureInfo("ps-AF")
        };

        services.Configure<SupportedCulturesLocaliztionOptions>(options =>
        {
            options.Cultures.Add(new CultureInfo("en-US"));
            options.Cultures.Add(new CultureInfo("fa-IR"));
            options.Cultures.Add(new CultureInfo("fa-AF"));
            options.Cultures.Add(new CultureInfo("ps-AF"));
        });

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("en-US");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());
        });

        return services;
    }
}
