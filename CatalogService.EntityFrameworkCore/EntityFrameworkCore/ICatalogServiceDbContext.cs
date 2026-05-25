using CatalogService.Domain;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.EntityFrameworkCore;

[ConnectionStringName(CatalogServiceDbProperties.ConnectionStringName)]
public interface ICatalogServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
