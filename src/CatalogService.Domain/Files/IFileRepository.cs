using Framework.BuildingBlock.Data;

namespace CatalogService.Files;

public interface IFileRepository : IGenericRepository<FileEntity, long>
{
    Task<FileEntity?> FindRelatedAsync(
        long entityId,
        string entityType,
        string role,
        CancellationToken cancellationToken = default);

    Task<Dictionary<long, FileEntity>> GetPrimaryFilesAsync(
           IEnumerable<long> entityIds,
           CancellationToken cancellationToken = default);
}
