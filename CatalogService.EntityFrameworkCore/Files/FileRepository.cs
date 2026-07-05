using CatalogService.EntityFrameworkCore;

using Framework.BuildingBlock.Repositories;

using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.Files;

public class FileRepository
    : EfCoreRepositoryFramework<CatalogServiceDbContext, FileEntity, Guid>,
      IFileRepository
{
    public FileRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Dictionary<Guid, FileEntity>> GetPrimaryFilesAsync(
        IEnumerable<Guid> entityIds,
        CancellationToken cancellationToken = default)
    {
        var ids = entityIds.Distinct().ToList();

        var files = await AsyncExecuter.ToListAsync(
            (await GetQueryableAsync())
                .Where(f => ids.Contains(f.EntityId)),
            cancellationToken);

        return files
            .GroupBy(f => f.EntityId)
            .ToDictionary(
                g => g.Key,
                g => g.OrderBy(f => f.Priority).First());
    }
}
