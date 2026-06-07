using CatalogService.Domain;
using Framework.BuildingBlock;
using Framework.BuildingBlock.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Volo.Abp.Data;

namespace CatalogService.EntityFrameworkCore;

[ConnectionStringName(CatalogServiceDbProperties.ConnectionStringName)]
public class CatalogServiceDbContext : GenericDbContextWrapper<CatalogServiceDbContext>, ICatalogServiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public CatalogServiceDbContext(DbContextOptions<CatalogServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies()
        ;
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.RegisterEntityConfigurations(Assembly.GetExecutingAssembly());
    }
}
