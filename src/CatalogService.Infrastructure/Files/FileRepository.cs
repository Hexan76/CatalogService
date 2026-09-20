using CatalogService.EntityFrameworkCore;

using Framework.BuildingBlock.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Files;

public class FileRepository : EfGenericRepository<CatalogServiceDbContext, FileEntity, long>, IFileRepository
{
    public FileRepository(CatalogServiceDbContext context) : base(context)
    {
    }

    public async Task<FileEntity?> FindRelatedAsync(
        long entityId,
        string entityType,
        string role,
        CancellationToken cancellationToken = default)
    {
        var local = Set.Local.FirstOrDefault(x =>
            x.EntityId == entityId &&
            x.EntityType == entityType &&
            x.Role == role);

        if (local is not null)
            return local;

        return null;
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
