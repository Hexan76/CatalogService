using Framework.BuildingBlock.Data;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using CatalogService.Infrastructure;

// Keep the same Npgsql timestamp behavior the DbContext factory relies on.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = Host.CreateApplicationBuilder(args);

// Reuse the exact same infrastructure registration as the API. This wires up the
// OrderDbContext and the IDatabaseMigrator (registered inside AddInfrastructure).
builder.Services.AddInfrastructure(builder.Configuration);

using var host = builder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

try
{
    var command = args.FirstOrDefault()?.ToLowerInvariant();

    using var scope = host.Services.CreateScope();
    var migrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();

    switch (command)
    {
        // "status" / "list": only report migration state, don't touch the database.
        case "status":
        case "list":
        {
            var applied = await migrator.GetAppliedMigrationsAsync();
            var pending = await migrator.GetPendingMigrationsAsync();

            logger.LogInformation(
                "Applied migrations ({Count}): {Migrations}",
                applied.Count, applied.Count == 0 ? "-" : string.Join(", ", applied));
            logger.LogInformation(
                "Pending migrations ({Count}): {Migrations}",
                pending.Count, pending.Count == 0 ? "-" : string.Join(", ", pending));
            break;
        }

        // default (no args) or "migrate": apply pending migrations + run seeders.
        case null:
        case "migrate":
            await migrator.MigrateAsync();
            break;

        default:
            logger.LogError(
                "Unknown command '{Command}'. Supported commands: migrate (default), status.",
                command);
            return 1;
    }

    logger.LogInformation("CatalogService.DbMigrator finished successfully.");
    return 0;
}
catch (Exception ex)
{
    logger.LogError(ex, "CatalogService.DbMigrator failed.");
    return 1;
}
