using CatalogService.BusinessTypes;
using CatalogService.Categories;
using CatalogService.Customizations;
using CatalogService.EntityFrameworkCore;
using CatalogService.Files;
using CatalogService.Items;
using CatalogService.Menus;
using CatalogService.Settings;

using Framework.BuildingBlock.Abstracts;
using Framework.BuildingBlock.EntityFrameworkCore;
using Framework.BuildingBlock.Settings;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Scoped DbContext registration. This is what FrameworkDbContext, the repositories
        // and the database migrator all rely on:
        //  - it registers OrderDbContext as a scoped service (needed by OrderRepository and
        //    by EfCoreDatabaseMigrator which resolves the context from a scope), and
        //  - it stores the *scoped* application service provider on the options, which is how
        //    FrameworkDbContext obtains IClock / ILazyServiceProvider lazily.
        services.AddDbContext<CatalogServiceDbContext>(options =>
             options.UseNpgsql(connectionString));

        // Wires up the framework data stack for OrderDbContext:
        // IClock + ILazyServiceProvider (required by FrameworkDbContext on every SaveChanges),
        // the generic repository and the unit of work.
        services.AddFrameworkData<CatalogServiceDbContext>();

        services.AddScoped<IBusinessTypeRepository, BusinessTypeRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICustomizationRepository, CustomizationRepository>();
        services.AddScoped<ICustomizationGroupRepository, CustomizationGroupRepository>();

        services.AddFrameworkSettings(configuration, typeof(CatalogSettingDefinitionProvider).Assembly);

        services.Configure<SettingsCacheOptions>(options =>
        {
            options.AppKey = "CatalogService";
        });

        // Registers IDatabaseMigrator (from the building block) for OrderDbContext so it can be
        // used anywhere in the system (API startup, workers, or the OrderService.DbMigrator console).
        services.AddDatabaseMigrator<CatalogServiceDbContext>();

        // Seed contributors run automatically after migrations are applied.
        // services.AddDataSeedContributor<OrderStatusDefinitionSeeder>();
        // services.AddDataSeedContributor<OrderStatusTransitionSeeder>();

        services.Configure<ClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });


        return services;
    }
}
