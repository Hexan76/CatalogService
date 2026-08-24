using CatalogService.EntityFrameworkCore;

using Framework.BuildingBlock.EntityFrameworkCore;

namespace CatalogService.Files;

public class FileRepository : EfGenericRepository<CatalogServiceDbContext, FileEntity, long>, IFileRepository
{
    public FileRepository(CatalogServiceDbContext context) : base(context)
    {
    }

    public async Task<Dictionary<long, FileEntity>> GetPrimaryFilesAsync(
        IEnumerable<long> entityIds,
        CancellationToken cancellationToken = default)
    {
        var ids = entityIds.Distinct().ToList();

        var files = GetQueryable()
                .Where(f => ids.Contains(f.EntityId));

        return files
            .GroupBy(f => f.EntityId)
            .ToDictionary(
                g => g.Key,
                g => g.OrderBy(f => f.Priority).First());
    }
}
