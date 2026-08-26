using Framework.BuildingBlock.EntityFrameworkCore;
using Framework.BuildingBlock.Settings;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.EntityFrameworkCore;

public class CatalogServiceDbContext : FrameworkDbContext
{
    public CatalogServiceDbContext(DbContextOptions<CatalogServiceDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();
#endif
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.SettingConfigure();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogServiceDbContext).Assembly);
    }
}
