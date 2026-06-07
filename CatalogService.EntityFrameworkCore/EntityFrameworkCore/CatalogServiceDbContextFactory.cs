using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CatalogService.EntityFrameworkCore;

public class CatalogServiceDbContextFactory : IDesignTimeDbContextFactory<CatalogServiceDbContext>
{
    public CatalogServiceDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var configuration = BuildConfiguration();

        CatalogServiceEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<CatalogServiceDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Write"));

        return new CatalogServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CatalogService.Host/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}