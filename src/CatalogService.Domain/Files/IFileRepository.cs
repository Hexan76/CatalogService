using Framework.BuildingBlock.Data;

namespace CatalogService.Files;

public interface IFileRepository : IGenericRepository<FileEntity, long>
{
    Task<Dictionary<long, FileEntity>> GetPrimaryFilesAsync(
           IEnumerable<long> entityIds,
           CancellationToken cancellationToken = default);
}
